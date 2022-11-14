
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PanTilt123.Class
{
    public class BootloaderProcessor
    {
        private readonly int numberOfPayloadBytePerBootLoaderPacket = 512;//2048; // Need to be multiple of 4 (for 32-bit align system)

        private readonly string firmwareVersionMarkerString = "FIRMWARE VERSION MARKER: LIFE IS THE MOST BEAUTIFUL THING IN THE WORLD@";

        public string BinaryFWFileName { set; get; }

        public UInt16 TotalPacket { set; get; }

        public UInt16 NextTxPacketNo { set; get; }

        public UInt32 TotalByteOfFirmware { set; get; }

        public int WaitingForResponseTimeoutCounter { set; get; }

        public Byte[] NewFirmwareVersion { set; get; }

        public Byte[] OldFirmwareVersion { set; get; }

        public List<Byte[]> PacketList { set; get; }

        public bool IsValid { set; get; }

        public BootloaderProcessingState State { set; get; }

        public Byte[] CommandQueryDeviceState { set; get; }

        private ConcurrentQueue<Byte[]> queueTxPacket = null;
        private ConcurrentQueue<Byte[]> queueRxPacket = null;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public BootloaderProcessor(ConcurrentQueue<Byte[]> txQueue, ConcurrentQueue<Byte[]> rxQueue)
        {
            // Assign tx/rx queue instance
            queueTxPacket = txQueue;
            queueRxPacket = rxQueue;

            NewFirmwareVersion = new Byte[3];
            PacketList = new List<Byte[]>();
            PacketList.Clear();
            TotalPacket = 0;
            NextTxPacketNo = 0;
            TotalByteOfFirmware = 0;
            IsValid = false;
            State = BootloaderProcessingState.Finish;
            WaitingForResponseTimeoutCounter = 0;
        }

        public bool ReadBinaryFile(String binaryFileName)
        {
            this.IsValid = false;

            // Get firmware version and get hardware version support
            try
            {
                using (TextReader textReader = new StreamReader(binaryFileName))
                {
                    string line;
                    bool firmwareVersionFound = false;
                    do
                    {
                        line = textReader.ReadLine();
                        if (line != null)
                        {
                            Console.WriteLine(line);
                            int indexOfFirmwareVersionMarker = line.IndexOf(firmwareVersionMarkerString);

                            if (indexOfFirmwareVersionMarker > 0)
                            {
                                string fiwmwareVersionString = line.Substring(indexOfFirmwareVersionMarker + firmwareVersionMarkerString.Length);
                                string[] fiwmwareVersionNumbers = fiwmwareVersionString.Split('.');
                                this.NewFirmwareVersion[0] = Convert.ToByte(fiwmwareVersionNumbers[0]);
                                this.NewFirmwareVersion[1] = Convert.ToByte(fiwmwareVersionNumbers[1]);
                                this.NewFirmwareVersion[2] = Convert.ToByte(fiwmwareVersionNumbers[2]);
                                firmwareVersionFound = true;
                            }
                        }
                    } while (line != null);

                    if (firmwareVersionFound == false)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                LogViewer.Add(e.ToString(), LogType.Error);
                return false;
            }

            UInt16 totalPayloadPacket = 0;
            UInt32 totalByteRead = 0;

            // Reading data from file
            try
            {
                // Get the list of payload data
                BinaryReader binaryReader = new BinaryReader(new FileStream(binaryFileName, FileMode.Open));
                PacketList.Clear();

                int readLen = 0;
                totalPayloadPacket = 0;
                totalByteRead = 0;
                do
                {
                    readLen = 0;
                    byte[] payLoadData = binaryReader.ReadBytes(numberOfPayloadBytePerBootLoaderPacket);
                    if (payLoadData != null)
                    {
                        readLen = payLoadData.Length;
                        if (readLen > 0)
                        {
                            totalByteRead = (UInt32)(totalByteRead + readLen);
                            PacketList.Add(payLoadData);
                            totalPayloadPacket++;
                            if (totalPayloadPacket > 65000)
                            {
                                totalPayloadPacket = 0;
                                return false;
                            }
                        }
                    }
                } while (readLen > 0);
                binaryReader.Close();
            }
            catch (IOException e)
            {
                LogViewer.Add(e.ToString(), LogType.Error);
                return false;
            }
            this.IsValid = true;
            TotalPacket = (ushort)PacketList.Count;
            TotalByteOfFirmware = totalByteRead;
            return true;
        }


        private BootloaderResultCode SendEntryDFUCommand()
        {
            BootloaderResultCode result = BootloaderResultCode.Ok;
            // Build message
            byte[] dfuInfo = new byte[9];
            // Add firmware
            dfuInfo[0] = NewFirmwareVersion[0];
            dfuInfo[1] = NewFirmwareVersion[1];
            dfuInfo[2] = NewFirmwareVersion[2];
            // Add total packet
            dfuInfo[3] = (byte)TotalPacket;
            dfuInfo[4] = (byte)(TotalPacket >> 8);
            // Length of firmware
            Uint32ByteArray uint32ByteArray = new Uint32ByteArray();
            uint32ByteArray.Int = TotalByteOfFirmware;
            dfuInfo[5] = uint32ByteArray.Byte3;
            dfuInfo[6] = uint32ByteArray.Byte2;
            dfuInfo[7] = uint32ByteArray.Byte1;
            dfuInfo[8] = uint32ByteArray.Byte0;
            byte[] txBuffer = Rs485PacketParser.BuildDfuRequestPacket(MsgCtrlDfuRequest.EntryDfu, dfuInfo);
            // Send message
            queueTxPacket.Enqueue(txBuffer);
            return result;
        }

        private BootloaderResultCode SendEraseDFUCommand()
        {
            BootloaderResultCode result = BootloaderResultCode.Ok;
            // Build message
            byte[] txBuffer = Rs485PacketParser.BuildDfuRequestPacket(MsgCtrlDfuRequest.Erase, null);
            // Send message
            queueTxPacket.Enqueue(txBuffer);
            return result;
        }

        private BootloaderResultCode SendNextBinaryPacket()
        {
            BootloaderResultCode result = BootloaderResultCode.Ok;
            // Build message
            byte[] dfuData = new byte[3 + 2 + 2 + 2 + 512];
            // Add firmware
            dfuData[0] = NewFirmwareVersion[0];
            dfuData[1] = NewFirmwareVersion[1];
            dfuData[2] = NewFirmwareVersion[2];
            // Add total packet
            dfuData[3] = (byte)TotalPacket;
            dfuData[4] = (byte)(TotalPacket >> 8);
            // Add packet no
            dfuData[5] = (byte)NextTxPacketNo;
            dfuData[6] = (byte)(NextTxPacketNo >> 8);
            // Add Packet length
            UInt16 lenPacket = (UInt16)PacketList[NextTxPacketNo].Length;
            dfuData[7] = (byte)lenPacket;
            dfuData[8] = (byte)(lenPacket >> 8);
            // Add Packet
            for (UInt16 i = 0; i < lenPacket; i++)
            {
                dfuData[9 + i] = PacketList[NextTxPacketNo][i];
            }
            byte[] txBuffer = Rs485PacketParser.BuildDfuRequestPacket(MsgCtrlDfuRequest.SendNextPacket, dfuData);
            // Send message
            queueTxPacket.Enqueue(txBuffer);
            return result;
        }

        private BootloaderResultCode SendExitDFUCommand()
        {
            BootloaderResultCode result = BootloaderResultCode.Ok;
            // Build message
            byte[] txBuffer = Rs485PacketParser.BuildDfuRequestPacket(MsgCtrlDfuRequest.EndDfu, null);
            // Send message
            queueTxPacket.Enqueue(txBuffer);
            return result;
        }

        private BootloaderResultCode DeviceResponseParse(Byte[] responseBuffer)
        {
            Byte msgControl = responseBuffer[17];
            switch (msgControl)
            {
                case 0x10:
                    return BootloaderResultCode.Done;
                case 0x20:
                    return BootloaderResultCode.Fault;
                default:
                    break;
            }
            return BootloaderResultCode.Nothing;
        }

        private async Task<BootloaderResultCode> BooloaderProcessing_Handler(CancellationToken cancellationToken)
        {
            BootloaderResultCode deviceResponseCode = BootloaderResultCode.Ok;
            int timeoutCounter = 0;
            int sendRetry = 0;
            do
            {
                switch (State)
                {
                    // Send command entry dfu mode
                    case BootloaderProcessingState.SendDfuCommand:
                        LogViewer.Add("Send DFU command to device", LogType.Info);
                        SendEntryDFUCommand();
                        State = BootloaderProcessingState.WaitingDeviceBootup;
                        timeoutCounter = 0;
                        break;
                    // Wait entry boot 
                    case BootloaderProcessingState.WaitingDeviceBootup:
                        if (deviceResponseCode == BootloaderResultCode.Done)
                        {
                            State = BootloaderProcessingState.EraseFlash;
                            timeoutCounter = 0;
                        }
                        else
                        {
                            if (timeoutCounter++ >= 60) // 3s
                            {
                                timeoutCounter = 0;
                                State = BootloaderProcessingState.SendDfuCommand;
                            }
                        }
                        break;
                    // Send command erase flash
                    case BootloaderProcessingState.EraseFlash:
                        LogViewer.Add("Device entered DFU mode, start erasing flash memory", LogType.Info);
                        SendEraseDFUCommand();
                        State = BootloaderProcessingState.WaitForErasing;
                        timeoutCounter = 0;
                        break;
                    // Wait erase done
                    case BootloaderProcessingState.WaitForErasing:
                        if (deviceResponseCode == BootloaderResultCode.Done)
                        {
                            LogViewer.Add("Device finish erasing flash memory, start send binary packet", LogType.Info);
                            State = BootloaderProcessingState.SendNextDatapacket;
                            NextTxPacketNo = 0;
                            sendRetry = 0;
                            timeoutCounter = 0;
                        }
                        else if(deviceResponseCode == BootloaderResultCode.Fault) 
                        {
                            State = BootloaderProcessingState.Error;
                            timeoutCounter = 0;
                        }
                        else
                        {
                            if (timeoutCounter++ >= 160) // 3s
                            {
                                timeoutCounter = 0;
                                State = BootloaderProcessingState.EraseFlash;
                            }
                        }
                        break;
                    // Send nextpacket
                    case BootloaderProcessingState.SendNextDatapacket:
                        SendNextBinaryPacket();
                        State = BootloaderProcessingState.WaitForWritingPacket;
                        timeoutCounter = 0;
                        break;
                    // Wait response packet
                    case BootloaderProcessingState.WaitForWritingPacket:
                        if(deviceResponseCode == BootloaderResultCode.Done)
                        {
                            sendRetry = 0;
                            NextTxPacketNo++;
                            LogViewer.Add(string.Format("Flash packet no {0} successfully", NextTxPacketNo), LogType.Info);
                            if (NextTxPacketNo < TotalPacket)
                            {
                                State = BootloaderProcessingState.SendNextDatapacket;
                                MainWindowViewModel.GetInstance().ProgressBarUpdateFwValue = NextTxPacketNo * 100 / TotalPacket;
                            }
                            else
                            {
                                LogViewer.Add(string.Format("Total {0} binary packet flashed successfully, stop DFU", NextTxPacketNo), LogType.Info);
                                State = BootloaderProcessingState.SendRebootCommand;
                            }
                        }
                        else if(deviceResponseCode == BootloaderResultCode.Fault)
                        {
                            if (timeoutCounter++ > 5)   // 3s
                            {
                                timeoutCounter = 0;
                                if (sendRetry++ > 3)
                                {
                                    sendRetry = 0;
                                    State = BootloaderProcessingState.Error;
                                }
                                else
                                {
                                    State = BootloaderProcessingState.SendNextDatapacket;
                                }
                            }
                        }
                        else
                        {
                            if (timeoutCounter++ >= 160) // 3s
                            {
                                timeoutCounter = 0;
                                State = BootloaderProcessingState.Error;
                            }
                        }
                        break;
                    // Send to exit dfu
                    case BootloaderProcessingState.SendRebootCommand:
                        SendExitDFUCommand();
                        State = BootloaderProcessingState.Finish;
                        break;

                    case BootloaderProcessingState.Error:
                        LogViewer.Add(string.Format("Bootloader update fault at {0} binary packet, stop DFU", NextTxPacketNo), LogType.Error);
                        return BootloaderResultCode.Error;

                    case BootloaderProcessingState.Finish:
                    default:
                        State = BootloaderProcessingState.Finish;
                        break;
                }

                // Delay because device and rs485 comunication are not that fast
                await Task.Delay(60);
                // Parse the response from device
                deviceResponseCode = BootloaderResultCode.Nothing;
                if (queueRxPacket.Count > 0)
                {
                    Byte[] responseBuffer;
                    if (queueRxPacket.TryDequeue(out responseBuffer) == true)
                    {
                        deviceResponseCode = DeviceResponseParse(responseBuffer);
                    }
                }
                // Check if user cancel the update process
                if (cancellationToken.IsCancellationRequested == true)
                {
                    return BootloaderResultCode.UserCancel;
                }
            }
            while (State != BootloaderProcessingState.Finish);

            return deviceResponseCode;
        }

        public async Task<BootloaderResultCode> StartDFU()
        {
            BootloaderResultCode result = BootloaderResultCode.Error;

            // Check binay file is valid or not
            if (IsValid == true)
            {
                // Clear rx queue
                byte[] response;
                while (queueRxPacket.TryDequeue(out response) == true) ;
                // Reset variable 
                NextTxPacketNo = 0;
                State = BootloaderProcessingState.SendDfuCommand;
                WaitingForResponseTimeoutCounter = 0;
                MainWindowViewModel.GetInstance().ProgressBarUpdateFwValue = 0;
                await Task.Run(() => BooloaderProcessing_Handler(cancellationTokenSource.Token)).ConfigureAwait(true);
                MainWindowViewModel.GetInstance().BtnFirmwareUpdateContent = "Update FW";
                cancellationTokenSource = new CancellationTokenSource();
                MainWindowViewModel.GetInstance().ProgressBarUpdateFwValue = 0;
                result = BootloaderResultCode.Ok;
            }
            return result;
        }

        public void StopDFU()
        {
            cancellationTokenSource.Cancel();
            NextTxPacketNo = 0;
        }
    }
}
