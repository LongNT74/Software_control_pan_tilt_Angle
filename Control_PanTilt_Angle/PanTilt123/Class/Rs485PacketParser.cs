using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PanTilt123;

namespace PanTilt123.Class
{
    public static unsafe class Rs485PacketParser
    {
        private static readonly int payloadCRCIndex = 7;
        private static readonly int headerCRCIndex = 8;
        private static Byte seqNumberCounter = 0;

        private static bool flagFirstTimeGetControlInfo = true;

        public static Rs485PacketType TestBufferForPacket(byte[] inputBuffer, int offset, out Byte foundPacketLen)
        {
            foundPacketLen = new Byte();
            foundPacketLen = 0;

            int inputBufferLen = inputBuffer.Length - offset;
            // Check input data lenght is less than header lenght or not
            if (inputBufferLen < 9) return 0;

            if (inputBuffer[offset + 0] == '!' && inputBuffer[offset + 1] == 0x03 && inputBuffer[offset + 2] == 0x00 && inputBuffer[offset + 4] == 0x20)
            {
                // Check header crc
                byte calHeaderCrc = CRC8.Calculate(inputBuffer, (UInt32)offset + 1, 7); // Not including marker byte and crc byte
                byte rxHeaderCrc = inputBuffer[offset + 8];
                if (calHeaderCrc == rxHeaderCrc)
                {
                    // Check payload len 
                    UInt16 payloadLen = inputBuffer[offset + 5];
                    payloadLen += (UInt16)(inputBuffer[offset + 6] * 256);
                    if ((inputBufferLen >= (9 + payloadLen)) && payloadLen > 0) // 8 bytes header + payload
                    {
                        // check payload crc
                        byte calPayloadCrc = CRC8.Calculate(inputBuffer, (UInt32)offset + 9, payloadLen); // Not including marker byte and crc byte
                        byte rxPayloadCrc = inputBuffer[offset + 7];
                        if (calPayloadCrc == rxPayloadCrc)
                        {
                            foundPacketLen = (Byte)(9 + payloadLen);
                            // Check message control
                            Byte msgType = inputBuffer[offset + 13];
                            Byte msgCtrl = inputBuffer[offset + 14];

                            msgType &= 0x7F;    // clear MSB bit
                            switch (msgType)
                            {
                                case 0x7F:
                                    if (msgCtrl == (byte)MsgSystemInfo.DEVICE_ID)
                                    {
                                        return Rs485PacketType.Device_Id;
                                    }
                                    else
                                    {
                                        return Rs485PacketType.None;
                                    }
                                case (byte)MsgType.MOTOR_CONTROL_OFFSET:
                                    if (msgCtrl == (byte)MsgCtrlMotor.PAN_ANGLE)
                                    {
                                        return Rs485PacketType.Pan_Angle;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.TILT_ANGLE)
                                    {
                                        return Rs485PacketType.Tilt_Angle;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.PAN_STOP)
                                    {
                                        return Rs485PacketType.Pan_Stop;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.TILT_STOP)
                                    {
                                        return Rs485PacketType.Tilt_Stop;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.PAN_GOHOME)
                                    {
                                        return Rs485PacketType.Pan_GoHome;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.TILT_GOHOME)
                                    {
                                        return Rs485PacketType.Tilt_GoHome;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.PAN_CONTROL_FAULT)
                                    {
                                        return Rs485PacketType.Pan_Fault;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.TILT_CONTROL_FAULT)
                                    {
                                        return Rs485PacketType.Tilt_Fault;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.PAN_CONTROL_ACK)
                                    {
                                        return Rs485PacketType.Pan_Ack;
                                    }
                                    else if (msgCtrl == (byte)MsgCtrlMotor.TILT_CONTROL_ACK)
                                    {
                                        return Rs485PacketType.Tilt_Ack;
                                    }
                                    else
                                    {
                                        return Rs485PacketType.None;
                                    }
                                case (byte)MsgType.SYSTEM_INFO_OFFSET:
                                    if (msgCtrl == (byte)MsgSystemInfo.DEVICE_ID)
                                    {
                                        return Rs485PacketType.Device_Id;
                                    }
                                    else if(msgCtrl == (byte)MsgSystemInfo.DEVICE_INFO)
                                    {
                                        return Rs485PacketType.Device_Info;
                                    }
                                     
                                    else if(msgCtrl == (byte)MsgSystemInfo.SET_BAUDRATE)
                                    {
                                        return Rs485PacketType.Set_Baudrate;
                                    }
                                    else if(msgCtrl == (byte)MsgSystemInfo.GO_HOME)
                                    {
                                        return Rs485PacketType.Go_Home;
                                    }
                                    else
                                    {
                                        return Rs485PacketType.None;
                                    }

                                case (byte)MsgType.MOTOR_INFO_OFFSET:
                                    return Rs485PacketType.Param;
                                case (byte)MsgType.ENTER_DFU_MODE:
                                    return Rs485PacketType.DfuResponse;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return Rs485PacketType.None;
        }

        #region Parse Message Response
        public static void ParseDeviceInfo(byte[] deviceInfoBuffer)
        {
            int addressFieldOffset = 9;
            int serialFieldOffset = 16;
            int hwVersionFieldOffset = 32;
            int swVersionFieldOffset = 36;
            int uidFieldOffset = 10;

            int addrLen = MainWindowViewModel.GetInstance().RxDeviceInfo.Address.Length;
            int serialLen = MainWindowViewModel.GetInstance().RxDeviceInfo.SerialNumber.Length;
            int hwVerLen = MainWindowViewModel.GetInstance().RxDeviceInfo.HardwareVerison.Length;
            int fwVerLen = MainWindowViewModel.GetInstance().RxDeviceInfo.FirmwareVerison.Length;
            int uidLen = MainWindowViewModel.GetInstance().RxDeviceInfo.Uid.Length;

            byte[] addr = new byte[addrLen];
            byte[] serial = new byte[serialLen];
            byte[] hwVer = new byte[hwVerLen];
            byte[] fwVer = new byte[fwVerLen];
            byte[] uid = new byte[uidLen];

            // Get UID
            for (int i = 0; i < uidLen; i++)
            {
                uid[i] = deviceInfoBuffer[uidFieldOffset + i];
            }
            MainWindowViewModel.GetInstance().RxDeviceInfo.Uid = uid;

            // Get control address
            for (int i = 0; i < addrLen; i++)
            {
                addr[i] = deviceInfoBuffer[addressFieldOffset + i];
            }
            MainWindowViewModel.GetInstance().RxDeviceInfo.Address = addr;

            // Get s/n
            for (int i = 0; i < serialLen; i++)
            {
                serial[i] = deviceInfoBuffer[serialFieldOffset + i];
            }
            MainWindowViewModel.GetInstance().RxDeviceInfo.SerialNumber = serial;

            // Get h/w version
            for (int i = 0; i < hwVerLen; i++)
            {
                hwVer[i] = deviceInfoBuffer[hwVersionFieldOffset + i];
            }
            MainWindowViewModel.GetInstance().RxDeviceInfo.HardwareVerison = hwVer;

            // Get s/w version
            for (int i = 0; i < fwVerLen; i++)
            {
                fwVer[i] = deviceInfoBuffer[swVersionFieldOffset + i];
            }
            MainWindowViewModel.GetInstance().RxDeviceInfo.FirmwareVerison = fwVer;

            MainWindowViewModel.GetInstance().RxDeviceInfo.IsValid = true;
            //MainWindowViewModel.GetInstance().BtnSaveDeviceInfoEnable = true;


            LogViewer.Add(string.Format("Get response from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                deviceInfoBuffer[8], deviceInfoBuffer[9], deviceInfoBuffer[10],
                deviceInfoBuffer[11], deviceInfoBuffer[12], deviceInfoBuffer[13]), LogType.Info);
        }

        public static void ParseSetBaudrateResponse(byte[] setBaudrateBuffer)
        {
            FloatByteArray floatByteArray = new FloatByteArray();
            floatByteArray.Byte3 = setBaudrateBuffer[17];
            floatByteArray.Byte2 = setBaudrateBuffer[18];
            floatByteArray.Byte1 = setBaudrateBuffer[19];
            floatByteArray.Byte0 = setBaudrateBuffer[20];

            LogViewer.Add(string.Format("Get response set baudrate: {0} from: {1}.{2}.{3}.{4} - MsgType 0x{5:X2} - MsgCtrl 0x{6:X2}",
                floatByteArray.FValue, setBaudrateBuffer[9], setBaudrateBuffer[10], setBaudrateBuffer[11],
                setBaudrateBuffer[12], setBaudrateBuffer[13], setBaudrateBuffer[14]), LogType.Info);
        }
        public static void ParseBroadcastEnumrationResponse(byte[] broadcastInfoBuffer)
        {
            int addressFieldOffset = 9;
            int serialFieldOffset = 16;
            int hwVersionFieldOffset = 32;
            int swVersionFieldOffset = 36;
            int uidFieldOffset = 40;

            DeviceEnumrationInfo deviceEnumrationInfo = new DeviceEnumrationInfo();

            int addrLen = deviceEnumrationInfo.Address.Length;
            int serialLen = deviceEnumrationInfo.SerialNumber.Length;
            int hwVerLen = deviceEnumrationInfo.HwVersion.Length;
            int fwVerLen = deviceEnumrationInfo.FwVersion.Length;
            int uidLen = deviceEnumrationInfo.Uid.Length;

            // Set device no
            deviceEnumrationInfo.DeviceNo = MainWindowViewModel.GetInstance().DeviceEnumrationInfoList.Count + 1;


            // Get UID
            for (int i = 0; i < uidLen; i++)
            {
                deviceEnumrationInfo.Uid[i] = broadcastInfoBuffer[uidFieldOffset + i];
            }

            // Get control address
            for (int i = 0; i < addrLen; i++)
            {
                deviceEnumrationInfo.Address[i] = broadcastInfoBuffer[addressFieldOffset + i];
            }

            // Get s/n
            for (int i = 0; i < serialLen; i++)
            {
                deviceEnumrationInfo.SerialNumber[i] = broadcastInfoBuffer[serialFieldOffset + i];
            }

            // Get h/w version
            for (int i = 0; i < hwVerLen; i++)
            {
                deviceEnumrationInfo.HwVersion[i] = broadcastInfoBuffer[hwVersionFieldOffset + i];
            }

            // Get s/w version
            for (int i = 0; i < fwVerLen; i++)
            {
                deviceEnumrationInfo.FwVersion[i] = broadcastInfoBuffer[swVersionFieldOffset + i];
            }

            deviceEnumrationInfo.ConvertToString();

            if (MainWindowViewModel.GetInstance().IsDeviceEnumrationStarted)
            {
                MainWindowViewModel.GetInstance().DeviceEnumrationInfoList.Add(deviceEnumrationInfo);
            }


            LogViewer.Add(string.Format("Get response from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                broadcastInfoBuffer[9], broadcastInfoBuffer[10], broadcastInfoBuffer[11],
                broadcastInfoBuffer[12], broadcastInfoBuffer[13], broadcastInfoBuffer[14]), LogType.Info);
        }
        public static void ParseRxMotorInfoResponse(byte[] motorControlpacketInfoBuffer)
        {
            MotorInfoPacket motorControlInfoPacket = new MotorInfoPacket(0);

            int copyLenght = motorControlpacketInfoBuffer[15] + motorControlpacketInfoBuffer[16] * 256;  // data lenght of payload of packet

            if (copyLenght > sizeof(MotorInfoPacket))
            {
                copyLenght = sizeof(MotorInfoPacket);
            }
            for (int i = 0; i < copyLenght; i++)
            {
                motorControlInfoPacket.ByteArray[i] = motorControlpacketInfoBuffer[i + 17];
            }

            SettingWindowViewModel.GetInstance().MaxSpeed = (float)Math.Round(motorControlInfoPacket.MaxSpeed, 1);
            SettingWindowViewModel.GetInstance().Accelement = (float)Math.Round(motorControlInfoPacket.Accelement, 1);
            SettingWindowViewModel.GetInstance().SpeedKp = (float)Math.Round(motorControlInfoPacket.SpeedKp, 1);
            SettingWindowViewModel.GetInstance().SpeedKi = (float)Math.Round(motorControlInfoPacket.SpeedKi, 1);
            SettingWindowViewModel.GetInstance().PositionKp = (float)Math.Round(motorControlInfoPacket.PositionKp, 1);
            SettingWindowViewModel.GetInstance().DeadBand = (float)Math.Round(motorControlInfoPacket.DeadBand, 1);
            SettingWindowViewModel.GetInstance().Side = (float)Math.Round(motorControlInfoPacket.SideA, 1);
            SettingWindowViewModel.GetInstance().AngleOffset = (float)Math.Round(motorControlInfoPacket.AngleOffset, 1);

            LogViewer.Add(string.Format("Get response from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                motorControlpacketInfoBuffer[9], motorControlpacketInfoBuffer[10], motorControlpacketInfoBuffer[11], motorControlpacketInfoBuffer[12],
                motorControlpacketInfoBuffer[13], motorControlpacketInfoBuffer[14]), LogType.Info);
        }
        public static void ParseSetGetAngleResponse(byte[] responseBuffer)
        {
            FloatByteArray floatToByteArrayConverter = new FloatByteArray(0);
            floatToByteArrayConverter.Byte3 = responseBuffer[17];
            floatToByteArrayConverter.Byte2 = responseBuffer[18];
            floatToByteArrayConverter.Byte1 = responseBuffer[19];
            floatToByteArrayConverter.Byte0 = responseBuffer[20];

            if (responseBuffer[14] == 0x01)
            {
                MainWindowViewModel.GetInstance().RxPanAngle = Math.Round(floatToByteArrayConverter.FValue, 1);
            }
            else if (responseBuffer[14] == 0x02)
            {
                MainWindowViewModel.GetInstance().RxTiltAngle = Math.Round(floatToByteArrayConverter.FValue, 1);
            }

            LogViewer.Add(string.Format("Get response done from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                responseBuffer[9], responseBuffer[10], responseBuffer[11],
                responseBuffer[12], responseBuffer[13], responseBuffer[14]), LogType.Info);
        }
        public static void ParseStopAngleResponse(byte[] responseBuffer)
        {
            FloatByteArray floatToByteArrayConverter = new FloatByteArray(0);
            floatToByteArrayConverter.Byte3 = responseBuffer[17];
            floatToByteArrayConverter.Byte2 = responseBuffer[18];
            floatToByteArrayConverter.Byte1 = responseBuffer[19];
            floatToByteArrayConverter.Byte0 = responseBuffer[20];

            if (responseBuffer[14] == 0x01)
            {
                MainWindowViewModel.GetInstance().RxPanAngle = Math.Round(floatToByteArrayConverter.FValue, 1);
            }
            else if (responseBuffer[14] == 0x02)
            {
                MainWindowViewModel.GetInstance().RxTiltAngle = Math.Round(floatToByteArrayConverter.FValue, 1);
            }

            LogViewer.Add(string.Format("Get response stop from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                responseBuffer[9], responseBuffer[10], responseBuffer[11],
                responseBuffer[12], responseBuffer[13], responseBuffer[14]), LogType.Info);
        }
        public static void ParseFaultResponse(byte[] responseBuffer)
        {
            //if (responseBuffer[14] == 0x01)
            //{
            //    LogViewer.Add(string.Format("Get response fautlt pan"), LogType.Info);
            //}
            //else if (responseBuffer[14] == 0x02)
            //{
            //    LogViewer.Add(string.Format("Get response fautlt tilt"), LogType.Info);
            //}
        }
        public static void ParseAckCtrlResponse(byte[] responseBuffer)
        {
            // Check Message Type
            if(true)
            {
                if (responseBuffer[14] == 0x0E)
                {
                    LogViewer.Add(string.Format("Get response Ack pan from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                    responseBuffer[9], responseBuffer[10], responseBuffer[11],
                    responseBuffer[12], responseBuffer[13], responseBuffer[14]), LogType.Info);
                }
                else if (responseBuffer[14] == 0x0F)
                {
                    LogViewer.Add(string.Format("Get response Ack tilt from: {0}.{1}.{2}.{3} - MsgType 0x{4:X2} - MsgCtrl 0x{5:X2}",
                    responseBuffer[9], responseBuffer[10], responseBuffer[11],
                    responseBuffer[12], responseBuffer[13], responseBuffer[14]), LogType.Info);
                }
            }
        }
        #endregion

        #region Build Message To Send
        public static Byte[] BuildMsgSetAddress(byte[] newAddress)
        {
            UInt16 dataLenght = 4;
            if (newAddress != null) dataLenght = (UInt16)newAddress.Length;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)MsgType.BROADCAST;          // Message type: broadcast
            requestPacketBuffer[packetLen++] = (byte)MsgSystemInfo.DEVICE_INFO;    // Message control - Enter DFU mode
            requestPacketBuffer[packetLen++] = (byte)dataLenght;                 // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);          // Data lenght

            // Add data to payload
            for (int i = 0; i < newAddress.Length; i++){
                requestPacketBuffer[packetLen++] = newAddress[i];
            }
            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildDfuRequestPacket(MsgCtrlDfuRequest msgCtrl, byte[] data)
        {
            UInt16 dataLenght = 0;
            if(data != null) dataLenght = (UInt16)data.Length;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = 0x05;                        // Message type: DFU offset
            requestPacketBuffer[packetLen++] = (byte)msgCtrl;               // Message control - Enter DFU mode
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // Add data to payload
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    requestPacketBuffer[packetLen++] = data[i];
                }
            }
            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgBroadcast()
        {
            // Calculate data lenght and payload lenght
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            requestPacketBuffer[packetLen++] = 0xFF;            // Address[0]
            requestPacketBuffer[packetLen++] = 0xFF;            // Address[1]
            requestPacketBuffer[packetLen++] = 0xFF;            // Address[2]
            requestPacketBuffer[packetLen++] = 0xFF;            // Address[3]     
            requestPacketBuffer[packetLen++] = (byte)MsgType.BROADCAST;          // Message type: broadcast
            requestPacketBuffer[packetLen++] = (byte)MsgSystemInfo.DEVICE_ID;    // Message control - Enter DFU mode
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgDeviceInfo()
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.GET | MsgType.SYSTEM_INFO_OFFSET); // Msg Type
            requestPacketBuffer[packetLen++] = (byte)(MsgSystemInfo.DEVICE_INFO);
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgSystemGoHome()
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.SYSTEM_INFO_OFFSET); // Msg Type
            requestPacketBuffer[packetLen++] = (byte)(MsgSystemInfo.GO_HOME);
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgGoHomeMotor(MotorIndex motor)
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.MOTOR_CONTROL_OFFSET); // Msg Type
            if (motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)(MsgCtrlMotor.PAN_GOHOME);
            }
            else if (motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)(MsgCtrlMotor.TILT_GOHOME);
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgStopMotor(MotorIndex motor)
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.MOTOR_CONTROL_OFFSET); // Msg Type
            if (motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)(MsgCtrlMotor.PAN_STOP);
            }
            else if (motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)(MsgCtrlMotor.TILT_STOP);
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgSetAngleMotor(MotorIndex motor, float angle)
        {
            UInt16 dataLenght = 4;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.MOTOR_CONTROL_OFFSET);      // Msg type
            if (motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgCtrlMotor.PAN_ANGLE;                        // Msg Ctrl
            }
            else if (motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgCtrlMotor.TILT_ANGLE;                       // Msg Ctrl
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;             // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);      // Data lenght

            // Add data to payload
            FloatByteArray floatToByteArrayConverter = new FloatByteArray(0);
            floatToByteArrayConverter.FValue = angle;
            requestPacketBuffer[packetLen++] = floatToByteArrayConverter.Byte3;
            requestPacketBuffer[packetLen++] = floatToByteArrayConverter.Byte2;
            requestPacketBuffer[packetLen++] = floatToByteArrayConverter.Byte1;
            requestPacketBuffer[packetLen++] = floatToByteArrayConverter.Byte0;

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgGetAngleMotor(MotorIndex motor)
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.GET | MsgType.MOTOR_CONTROL_OFFSET);      // Message type
            if (motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgCtrlMotor.PAN_ANGLE;                        // Msg Ctrl
            }
            else if (motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgCtrlMotor.TILT_ANGLE;                       // Msg Ctrl
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgGetMotorInfo(MotorIndex motor)
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.GET | MsgType.MOTOR_INFO_OFFSET);     // Message type
            if(motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgMotorInfo.READ_PAN_PARAM;                       // Msg Ctrl
            }
            else if(motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgMotorInfo.READ_TILT_PARAM;                       // Msg Ctrl
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgSetMotorInfo(MotorIndex motor, MotorInfo motorInfo)
        {
            UInt16 dataLenght = 32;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.MOTOR_INFO_OFFSET);     // Message type
            if(motor == MotorIndex.PAN_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgMotorInfo.WRITE_PAN_PARAM;                      // Msg Ctrl
            }
            else if(motor == MotorIndex.TILT_MOTOR)
            {
                requestPacketBuffer[packetLen++] = (byte)MsgMotorInfo.WRITE_TILT_PARAM;                      // Msg Ctrl
            }
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // Build Data
            MotorInfoPacket motorInfoPacket = new MotorInfoPacket(0);
            motorInfoPacket.MaxSpeed = motorInfo.MaxSpeed;
            motorInfoPacket.Accelement = motorInfo.Accelement;
            motorInfoPacket.SpeedKp = motorInfo.SpeedKp;
            motorInfoPacket.SpeedKi = motorInfo.SpeedKi;
            motorInfoPacket.PositionKp = motorInfo.PositionKp;
            motorInfoPacket.DeadBand = motorInfo.DeadBand;
            motorInfoPacket.SideA = motorInfo.Side;
            motorInfoPacket.AngleOffset = motorInfo.AngleOffset;

            // Copy buffer
            for (int i = 0; i < dataLenght; i++)
            {
                requestPacketBuffer[packetLen++] = motorInfoPacket.ByteArray[i];
            }

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        public static Byte[] BuildMsgSetMotorDefault()
        {
            UInt16 dataLenght = 0;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);

            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.MOTOR_INFO_OFFSET);     // Message type
            requestPacketBuffer[packetLen++] = (byte)MsgMotorInfo.DEFAULT;                      // Msg Ctrl
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }

        public static Byte[] BuildMsgSetBaudrate(UInt32 baudrate)
        {
            UInt16 dataLenght = 4;
            UInt16 payloadLenght = (UInt16)(8 + dataLenght);
            FloatByteArray floatByteArray = new FloatByteArray();
            // Prepare packet buffer
            Byte[] requestPacketBuffer = new byte[9 + payloadLenght];

            // Build 8 byte Header
            int packetLen = 0;
            requestPacketBuffer[packetLen++] = 0x21;  // '!'
            requestPacketBuffer[packetLen++] = 0x00;  // PC
            requestPacketBuffer[packetLen++] = 0x03;  // Device
            requestPacketBuffer[packetLen++] = seqNumberCounter++;
            requestPacketBuffer[packetLen++] = 0x20;
            requestPacketBuffer[packetLen++] = (byte)payloadLenght;
            requestPacketBuffer[packetLen++] = (byte)(payloadLenght >> 8);
            requestPacketBuffer[packetLen++] = 0;
            requestPacketBuffer[packetLen++] = 0;

            // Build Payload
            byte[] ctrlAddr = MainWindowViewModel.GetInstance().TxDeviceInfo.Address;
            requestPacketBuffer[packetLen++] = ctrlAddr[0];
            requestPacketBuffer[packetLen++] = ctrlAddr[1];
            requestPacketBuffer[packetLen++] = ctrlAddr[2];
            requestPacketBuffer[packetLen++] = ctrlAddr[3];
            requestPacketBuffer[packetLen++] = (byte)(MsgType.SET | MsgType.SYSTEM_INFO_OFFSET);     // Message type
            requestPacketBuffer[packetLen++] = (byte)MsgSystemInfo.SET_BAUDRATE;                     // Msg Ctrl
            requestPacketBuffer[packetLen++] = (byte)dataLenght;            // Data lenght
            requestPacketBuffer[packetLen++] = (byte)(dataLenght >> 8);     // Data lenght
            // Add payload
            floatByteArray.FValue = (float)baudrate;
            requestPacketBuffer[packetLen++] = floatByteArray.Byte3;
            requestPacketBuffer[packetLen++] = floatByteArray.Byte2;
            requestPacketBuffer[packetLen++] = floatByteArray.Byte1;
            requestPacketBuffer[packetLen++] = floatByteArray.Byte0;

            // calculate payload crc
            requestPacketBuffer[payloadCRCIndex] = CRC8.Calculate(requestPacketBuffer, 9, payloadLenght);
            // calculate header crc
            requestPacketBuffer[headerCRCIndex] = CRC8.Calculate(requestPacketBuffer, 1, 7);     // Not including first and last byte of header

            return requestPacketBuffer;
        }
        #endregion
    }
}


    public enum Rs485PacketType
    {
        None,
        // Control Motor
        Pan_Angle,
        Tilt_Angle,
        Pan_Stop,
        Tilt_Stop,
        Pan_GoHome,
        Tilt_GoHome,
        Pan_Fault,
        Tilt_Fault,
        Pan_Ack,
        Tilt_Ack,
        // System info
        Device_Id,
        Device_Info,
        Set_Baudrate,
        Go_Home,
        // Param
        Param,
        // DFU Command
        DfuResponse
    }

    public enum MotorIndex
    {
        PAN_MOTOR = 0x01,
        TILT_MOTOR = 0x02,
    }

    public enum MsgType
    {
        BROADCAST = 0xFF,
        GET = 0x00,
        SET = 0x80,
        MOTOR_CONTROL_OFFSET = 0x01,
        SYSTEM_INFO_OFFSET = 0x02,
        MOTOR_INFO_OFFSET = 0x03,
        ENTER_DFU_MODE = 0x05
    }

    public enum MsgSystemInfo
    {
        DEVICE_ID = 0x01,
        DEVICE_INFO = 0x02,
        SET_BAUDRATE = 0x03,
        GO_HOME = 0x04,
    }

    public enum MsgCtrlMotor
    {
        PAN_ANGLE = 0x01,
        TILT_ANGLE = 0x02,
        PAN_STOP = 0x03,
        TILT_STOP = 0x04,
        PAN_GOHOME = 0x05,
        TILT_GOHOME = 0x06,
        PAN_CONTROL_FAULT = 0x0C,
        TILT_CONTROL_FAULT = 0x0D,
        PAN_CONTROL_ACK = 0x0E,
        TILT_CONTROL_ACK = 0x0F,
    }

    public enum MsgMotorInfo
    {
        READ_PAN_PARAM = 0x01,
        READ_TILT_PARAM = 0x02,
        WRITE_PAN_PARAM = 0x03,
        WRITE_TILT_PARAM = 0x04,
        DEFAULT = 0x05,
    }

    public enum MsgCtrlDfuRequest
    {
        EndDfu = 0x00,
        EntryDfu = 0x01,
        Erase = 0x02,
        SendNextPacket = 0x08,
    }


    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct MotorInfoPacket
    {
        [FieldOffset(0)]
        public float MaxSpeed;

        [FieldOffset(4)]
        public float Accelement;

        [FieldOffset(8)]
        public float SpeedKp;

        [FieldOffset(12)]
        public float SpeedKi;

        [FieldOffset(16)]
        public float PositionKp;

        [FieldOffset(20)]
        public float DeadBand;

        [FieldOffset(24)]
        public float SideA;

        [FieldOffset(28)]
        public float AngleOffset;

        // Byte array for easy data management
        [FieldOffset(0)]
        public fixed byte ByteArray[32];

        public MotorInfoPacket(int value = 0)
        {
            MaxSpeed = 0;
            Accelement = 0;
            SpeedKp = 0;
            SpeedKi = 0;
            PositionKp = 0;
            DeadBand = 0;
            SideA = 0;
            AngleOffset = 0;

            for (int i = 0; i < 32; i++)
            {
                ByteArray[i] = 0;
            }
        }
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct Uint32ByteArray
    {
        [FieldOffset(3)]
        public byte Byte0;
        [FieldOffset(2)]
        public byte Byte1;
        [FieldOffset(1)]
        public byte Byte2;
        [FieldOffset(0)]
        public byte Byte3;

        [FieldOffset(0)]
        public UInt32 Int;

        public Uint32ByteArray(int value = 0)
        {
            Byte0 = 0;
            Byte1 = 0;
            Byte2 = 0;
            Byte3 = 0;
            Int = 0;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FloatByteArray
    {
        [FieldOffset(3)]
        public byte Byte0;
        [FieldOffset(2)]
        public byte Byte1;
        [FieldOffset(1)]
        public byte Byte2;
        [FieldOffset(0)]
        public byte Byte3;

        [FieldOffset(0)]
        public float FValue;

        public FloatByteArray(float value = 0)
        {
            Byte0 = 0;
            Byte1 = 0;
            Byte2 = 0;
            Byte3 = 0;
            FValue = 0;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct ByteArray
    {
        [FieldOffset(0)]
        public byte Byte1;
        [FieldOffset(1)]
        public byte Byte2;
        [FieldOffset(2)]
        public byte Byte3;
        [FieldOffset(3)]
        public byte Byte4;
        [FieldOffset(4)]
        public byte Byte5;
        [FieldOffset(5)]
        public byte Byte6;
        [FieldOffset(6)]
        public byte Byte7;
        [FieldOffset(7)]
        public byte Byte8;
        [FieldOffset(0)]
        public int Int1;
        [FieldOffset(4)]
        public int Int2;
    }
