using PanTilt123.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Data;

namespace PanTilt123
{
    public partial class frmMainWindow : Form
    {
        #region Public
        public MainWindowViewModel mainWindowViewModel = null;
        #endregion

        #region Private
        frmLogView windowLogViewer = null;
        frmWindowSetting windowSetting = null;

        private int devieEnumrationTimeCounter = 0;
        private SerialPort serialPort = new SerialPort();
        private Boolean isDeviceConnected = false;
        private Timer timerSerialPortRxDataParsing = new Timer();
        private BootloaderProcessor bootloaderProcessor = null;
        private int byteRemainStoreNormalModeCount = 0;
        

        #endregion

        #region Concurrentqueue
        private Byte[] bufferRemainStoreNormalMode = new Byte[4096];

        private ConcurrentQueue<Byte[]> serialPortRcvBufferQueue = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueTxPacket = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketSettingResponse = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketBootloaderResponse = new ConcurrentQueue<Byte[]>();

        private ConcurrentQueue<Byte[]> queueRxPacketSetGetAngleResponse = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketMotorStopResponse = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketMotorFaultResponse = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketMotorAckResponse = new ConcurrentQueue<Byte[]>();

        private ConcurrentQueue<Byte[]> queueRxPacketDeviceInfo = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketDeviceEnum = new ConcurrentQueue<Byte[]>();
        private ConcurrentQueue<Byte[]> queueRxPacketSetBaudrate = new ConcurrentQueue<Byte[]>();
        #endregion

        public frmMainWindow()
        {
            InitializeComponent();
            BindingData();
            windowLogViewer = new frmLogView();
            windowSetting = new frmWindowSetting(queueTxPacket, queueRxPacketSettingResponse);
        }

        public void BindingData()
        {
            mainWindowViewModel = MainWindowViewModel.GetInstance();
            DataBinding.Add(mainWindowViewModel);

            //binding data cbComport
            cbComport.DataSource = mainWindowViewModel.AvailbleComportList;
            
            if(mainWindowViewModel.AvailbleComportList.Count != 0)
            {
                cbComport.SelectedIndex = mainWindowViewModel.SelectedComPortIndex;
            }

            //binding data cbBaudrate
            cbBaudrate.SelectedIndex = 3;

            //binding data status
            DeviceInfo rxDeviceInfo = mainWindowViewModel.RxDeviceInfo;
            if(rxDeviceInfo.Address != null)
            {
                lblAddress.Text = Encoding.UTF8.GetString(rxDeviceInfo.Address);
            }
            if(rxDeviceInfo.SerialNumber != null)
            {
                lblSerial.Text = mainWindowViewModel.RxDeviceInfo.SerialNumber.ToString();
            }
            if(rxDeviceInfo.FirmwareVerison != null)
            {
                lblFw.Text = mainWindowViewModel.RxDeviceInfo.FirmwareVerison.ToString();
            }
            if(rxDeviceInfo.HardwareVerison != null)
            {
                lblHw.Text = mainWindowViewModel.RxDeviceInfo.HardwareVerison.ToString();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cbBaudrate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (cbBaudrate.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.ComportBaud = 9600;
                    Properties.Settings.Default.Save();
                    break;
                case 1:
                    Properties.Settings.Default.ComportBaud = 19200;
                    Properties.Settings.Default.Save();
                    break;
                case 2:
                    Properties.Settings.Default.ComportBaud = 57600;
                    Properties.Settings.Default.Save();
                    break;
                case 3:
                    Properties.Settings.Default.ComportBaud = 115200;
                    Properties.Settings.Default.Save();

                    break;
                default:
                    break;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            windowSetting.Show();
            //windowSetting.Owner = System.Windows.Application.Current.MainWindow; // We must also set the owner for this to work.
            windowSetting.StartPosition = FormStartPosition.CenterParent;
            windowSetting.Hide();

            // Init window log
            
            //windowLogViewer.Owner = System.Windows.Application.Current.MainWindow; // We must also set the owner for this to work.
            windowLogViewer.StartPosition = FormStartPosition.CenterParent;
            LogViewer.Add("Start initializing system...", LogType.Info);
            // Init bootloadaer process
            bootloaderProcessor = new BootloaderProcessor(queueTxPacket, queueRxPacketBootloaderResponse);
            // Init Comport list
            mainWindowViewModel.AvailbleComportList = GetAllAvailblePorts();
            //mainWindowViewModel.SetComportConnectionStatusIcon(false);

            mainWindowViewModel.SelectedComPortBaudrate = Properties.Settings.Default.ComportBaud;
            if (mainWindowViewModel.AvailbleComportList.Count >= 1)
            {
                mainWindowViewModel.SelectedComPortIndex = 0;
            }

            int portIdx = 0;
           
            foreach (string port in mainWindowViewModel.AvailbleComportList)
            {
                if (port == Properties.Settings.Default.ComportName)
                {
                    if (OpenComport(port, Properties.Settings.Default.ComportBaud) == true)
                    {
                        btnOpenPort.Text = "Close";
                        mainWindowViewModel.SelectedComPortIndex = portIdx;
                        break;
                    }
                }
                portIdx++;
            }
            timerSerialPortRxDataParsing.Interval = 10;
            timerSerialPortRxDataParsing.Tick += timerSerialPortDataHandler_Tick;
        }

        #region Comport managerment
        private List<string> GetAllAvailblePorts()
        {
            List<string> comPortList = new List<string>();
            foreach (String portName in SerialPort.GetPortNames())
            {
                comPortList.Add(portName);
            }
            return comPortList;
        }

        private bool OpenComport(string portName, int baudrate)
        {
            bool isTryOpenPort = false;
            bool isOpenSuccessful = false;
            try
            {
                serialPort.PortName = portName;
                serialPort.BaudRate = baudrate;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.Parity = Parity.None;
                serialPort.DataReceived += serialPort_DataReceived;

                if (serialPort.IsOpen == false)
                {
                    isTryOpenPort = true;
                    serialPort.Open();
                    timerSerialPortRxDataParsing.Start();
                    isDeviceConnected = true;
                    Properties.Settings.Default.ComportName = portName;
                    Properties.Settings.Default.ComportBaud = baudrate;
                    Properties.Settings.Default.Save();
                    //mainWindowViewModel.SetComportConnectionStatusIcon(true);
                    cbBaudrate.Enabled = false;
                    cbComport.Enabled = false;
                    isOpenSuccessful = true;
                }
                else
                {
                    serialPort.Close();
                    timerSerialPortRxDataParsing.Stop();
                    isDeviceConnected = false;
                    //mainWindowViewModel.SetComportConnectionStatusIcon(false);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                if (isTryOpenPort == true)
                {
                    System.Windows.MessageBox.Show("Can not open " + serialPort.PortName);
                    //mainWindowViewModel.SetComportConnectionStatusIcon(false);
                    isDeviceConnected = false;
                }
            }

            return isOpenSuccessful;
        }

        private bool CloseComPort()
        {
            bool isCloseSuccessful = false;

            try
            {
                if ((serialPort != null) && (serialPort.IsOpen == true))
                {
                    serialPort.Close();
                    //mainWindowViewModel.SetComportConnectionStatusIcon(false);
                    cbBaudrate.Enabled = true;
                    cbComport.Enabled = true;
                    isDeviceConnected = false;
                    isCloseSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                LogViewer.Add(ex.ToString(), LogType.Error);
            }
            return isCloseSuccessful;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int numByteToRead = serialPort.BytesToRead;
            byte[] buffer = new byte[numByteToRead];
            serialPort.Read(buffer, 0, numByteToRead);
            serialPortRcvBufferQueue.Enqueue(buffer);
        }

        private void timerSerialPortDataHandler_Tick(object sender, EventArgs e)
        {
            timerSerialPortRxDataParsing.Stop();

            // Read and parse log message, store result into queue
            ReadPacketFromSerialPort();

            // Display set/get angle response packet
            if (!queueRxPacketSetGetAngleResponse.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketSetGetAngleResponse.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseSetGetAngleResponse(packetBuffer);
            }

            // Display stop/angle response packet
            if (!queueRxPacketMotorStopResponse.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketMotorStopResponse.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseStopAngleResponse(packetBuffer);
            }

            // Display fault response packet
            if (!queueRxPacketMotorFaultResponse.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketMotorFaultResponse.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseFaultResponse(packetBuffer);
            }

            // Ack response
            if (!queueRxPacketMotorAckResponse.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketMotorAckResponse.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseAckCtrlResponse(packetBuffer);
            }

            // Display device enum
            if (!queueRxPacketDeviceEnum.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketDeviceEnum.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseBroadcastEnumrationResponse(packetBuffer);
            }

            // Display Rx device info packet
            if (!queueRxPacketDeviceInfo.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketDeviceInfo.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseDeviceInfo(packetBuffer);
            }

            // Set baudrate response
            if (!queueRxPacketSetBaudrate.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacketSetBaudrate.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseSetBaudrateResponse(packetBuffer);
            }

            // Check and send request to device
            if (!queueTxPacket.IsEmpty)
            {
                Byte[] txPacketBuffer;
                if (queueTxPacket.TryDequeue(out txPacketBuffer) == true)
                {
                    try
                    {
                        serialPort.Write(txPacketBuffer, 0, txPacketBuffer.Length);
                        //LogViewer.Add("Send request to device: " + serialPort.Encoding.GetString(txPacketBuffer), LogType.Info);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Can not send data to serial port: " + ex.ToString());
                    }
                }
            }

            // Update progressbar for device enumration
            if (mainWindowViewModel.IsDeviceEnumrationStarted == true)
            {
                devieEnumrationTimeCounter += timerSerialPortRxDataParsing.Interval; // miliseconds
                mainWindowViewModel.ProgressBarDeviceEnumrationValue = (devieEnumrationTimeCounter * 100) / 5000;
                if (devieEnumrationTimeCounter > 8000)
                {
                    mainWindowViewModel.IsDeviceEnumrationStarted = false;
                    devieEnumrationTimeCounter = 0;
                }
            }

            timerSerialPortRxDataParsing.Start();
        }

        void ReadPacketFromSerialPort()
        {
            int readByte = 0;
            // Get all buffer in queue
            List<Byte[]> bufferList = new List<byte[]>();
            while (!serialPortRcvBufferQueue.IsEmpty)
            {
                Byte[] buffer;
                serialPortRcvBufferQueue.TryDequeue(out buffer);
                bufferList.Add(buffer);
                readByte += buffer.Length;
            }

            Byte[] totalBuffer = new Byte[readByte + byteRemainStoreNormalModeCount];
            int totalBufferTail = 0;

            // Add remain byte from last time
            if (byteRemainStoreNormalModeCount > 0)
            {
                Buffer.BlockCopy(bufferRemainStoreNormalMode, 0, totalBuffer, totalBufferTail, byteRemainStoreNormalModeCount);
                totalBufferTail += byteRemainStoreNormalModeCount;
                byteRemainStoreNormalModeCount = 0;
            }

            // Copy all read byte into totalBuffer
            for (int i = 0; i < bufferList.Count; i++)
            {
                if (bufferList[i].Length > 0)
                {
                    Buffer.BlockCopy(bufferList[i], 0, totalBuffer, totalBufferTail, bufferList[i].Length);
                    totalBufferTail += bufferList[i].Length;
                }
            }

            // Find packet from totalBuffer
            int totalBufferHead = 0;
            for (int i = 0; i < totalBuffer.Length; i++)
            {
                // Check buffer for packet
                Byte packetLen = 0;
                Rs485PacketType packetType = Rs485PacketParser.TestBufferForPacket(totalBuffer, i, out packetLen);
                if ((packetLen > 9) && (packetType != Rs485PacketType.None))
                {
                    // Copy all data of packet , including header, payload, data of payload
                    // And then send it to expecting queues
                    Byte[] packetBuffer = new Byte[packetLen];
                    Buffer.BlockCopy(totalBuffer, i, packetBuffer, 0, packetBuffer.Length);
                    i += (int)packetLen;
                    totalBufferHead = i;

                    if (packetType == Rs485PacketType.Pan_Angle || packetType == Rs485PacketType.Tilt_Angle)
                    {
                        queueRxPacketSetGetAngleResponse.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Pan_Stop || packetType == Rs485PacketType.Tilt_Stop)
                    {
                        queueRxPacketMotorStopResponse.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Pan_GoHome || packetType == Rs485PacketType.Tilt_GoHome)
                    {

                    }
                    else if (packetType == Rs485PacketType.Pan_Fault || packetType == Rs485PacketType.Tilt_Fault)
                    {
                        queueRxPacketMotorFaultResponse.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Pan_Ack || packetType == Rs485PacketType.Tilt_Ack)
                    {
                        queueRxPacketMotorAckResponse.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Device_Id)
                    {
                        queueRxPacketDeviceEnum.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Device_Info)
                    {
                        queueRxPacketDeviceInfo.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Set_Baudrate)
                    {
                        queueRxPacketSetBaudrate.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.Go_Home)
                    {

                    }
                    else if (packetType == Rs485PacketType.Param)
                    {
                        queueRxPacketSettingResponse.Enqueue(packetBuffer);
                    }
                    else if (packetType == Rs485PacketType.DfuResponse)
                    {
                        queueRxPacketBootloaderResponse.Enqueue(packetBuffer);
                    }
                }
            }

            // Save the remain of totalBuffer to bufferRemainStore
            if (totalBufferHead < totalBuffer.Length)
            {
                byteRemainStoreNormalModeCount = totalBuffer.Length - totalBufferHead;
                if (byteRemainStoreNormalModeCount > bufferRemainStoreNormalMode.Length)
                {
                    byteRemainStoreNormalModeCount = bufferRemainStoreNormalMode.Length;
                }
                Buffer.BlockCopy(totalBuffer, totalBufferHead, bufferRemainStoreNormalMode, 0, byteRemainStoreNormalModeCount);
            }
        }
        #endregion

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            if (btnOpenPort.Text.Contains("Open"))
            {
                if (mainWindowViewModel.AvailbleComportList.Count > 0)
                {
                    if (mainWindowViewModel.SelectedComPortIndex < mainWindowViewModel.AvailbleComportList.Count)
                    {
                        string portName = mainWindowViewModel.AvailbleComportList[mainWindowViewModel.SelectedComPortIndex];
                        if (OpenComport(portName, Properties.Settings.Default.ComportBaud) == true)
                        {
                            btnOpenPort.Text = "Close";
                        }
                    }
                }
            }
            else
            {
                if (CloseComPort() == true)
                {
                    btnOpenPort.Text = "Open";
                    mainWindowViewModel.AvailbleComportList = GetAllAvailblePorts();
                }
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            windowSetting.Top = this.Top + 220;
            windowSetting.Left = this.Left + (this.Width - windowSetting.Width) / 2;
            windowSetting.Show();
        }

        private void btnLogView_Click(object sender, EventArgs e)
        {
            windowLogViewer.Top = this.Top + 220;
            windowLogViewer.Left = this.Left + 10;
            windowLogViewer.Width = this.Width - 20;
            windowLogViewer.Height = this.Height - 220 - 50;
            windowLogViewer.Show();
        }

        private void tbSetPan_ValueChanged(object sender, EventArgs e)
        {
            txtSetPan.Text = tbSetPan.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            txtSetTilt.Text = tbSetTilt.Value.ToString();
        }

        private void btnSetAddr_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] newAddr = new byte[4];
                string[] tokens = txtAddress.Text.ToString().Split('.');
                newAddr[0] = Convert.ToByte(tokens[0]);
                newAddr[1] = Convert.ToByte(tokens[1]);
                newAddr[2] = Convert.ToByte(tokens[2]);
                newAddr[3] = Convert.ToByte(tokens[3]);
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAddress(newAddr);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnSetPanAngle_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                if (mainWindowViewModel.TxPanAngle > 200) mainWindowViewModel.TxPanAngle = 200;
                if (mainWindowViewModel.TxPanAngle < -200) mainWindowViewModel.TxPanAngle = -200;
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.PAN_MOTOR, (float)mainWindowViewModel.TxPanAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnGetPanAngle_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgGetAngleMotor(MotorIndex.PAN_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnSetTitl_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                if (mainWindowViewModel.TxTiltAngle > 80) mainWindowViewModel.TxTiltAngle = 80;
                if (mainWindowViewModel.TxTiltAngle < -80) mainWindowViewModel.TxTiltAngle = -80;
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.TILT_MOTOR, (float)mainWindowViewModel.TxTiltAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnGetTilt_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgGetAngleMotor(MotorIndex.TILT_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnMovePanToLeft_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                mainWindowViewModel.TxPanAngle -= mainWindowViewModel.TxAngleStepSize;
                if (mainWindowViewModel.TxPanAngle < -200) mainWindowViewModel.TxPanAngle = -200;
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.PAN_MOTOR, (float)mainWindowViewModel.TxPanAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnMovePanToUp_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                mainWindowViewModel.TxTiltAngle += mainWindowViewModel.TxAngleStepSize;
                if (mainWindowViewModel.TxTiltAngle > 80) mainWindowViewModel.TxTiltAngle = 80;
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.TILT_MOTOR, (float)mainWindowViewModel.TxTiltAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnMovePanToDown_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                mainWindowViewModel.TxTiltAngle -= mainWindowViewModel.TxAngleStepSize;
                if (mainWindowViewModel.TxTiltAngle < -80) mainWindowViewModel.TxTiltAngle = -80;
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.TILT_MOTOR, (float)mainWindowViewModel.TxTiltAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnMovePanToRight_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                mainWindowViewModel.TxPanAngle += mainWindowViewModel.TxAngleStepSize;
                if (mainWindowViewModel.TxPanAngle > 200) mainWindowViewModel.TxPanAngle = 200;

                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSetAngleMotor(MotorIndex.PAN_MOTOR, (float)mainWindowViewModel.TxPanAngle);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
            }
        }

        private void btnEnumerateDevices_Click(object sender, EventArgs e)
        {
            // Step1: clear table
            MainWindowViewModel.GetInstance().DeviceEnumrationInfoList.Clear();
            // Step2: send broadcast command to scan device on rs485 bus
            byte[] deviceInfoBroadcastPacketBuffer = Rs485PacketParser.BuildMsgBroadcast();
            queueTxPacket.Enqueue(deviceInfoBroadcastPacketBuffer);
            devieEnumrationTimeCounter = 0;
            mainWindowViewModel.IsDeviceEnumrationStarted = true;
        }

        private void btnReadSelectedDeviceInfo_Click(object sender, EventArgs e)
        {
            byte[] deviceInfoBroadcastPacketBuffer = Rs485PacketParser.BuildMsgDeviceInfo();
            queueTxPacket.Enqueue(deviceInfoBroadcastPacketBuffer);
        }

        private void btnGoHome_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgSystemGoHome();
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
                LogViewer.Add(string.Format("Send message set go home"), LogType.Info);
            }
        }

        private void btnPanGoHome_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgGoHomeMotor(MotorIndex.PAN_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
                LogViewer.Add(string.Format("Send message set pan go home"), LogType.Info);
            }
        }

        private void btnTiltGoHome_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgGoHomeMotor(MotorIndex.TILT_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
                LogViewer.Add(string.Format("Send message set tilt go home"), LogType.Info);
            }
        }

        private void btnPanStop_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgStopMotor(MotorIndex.PAN_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
                LogViewer.Add(string.Format("Send message set pan stop"), LogType.Info);
            }
        }

        private void btnTiltStop_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected)
            {
                // Step1: build message
                byte[] requestPacketBuffer = Rs485PacketParser.BuildMsgStopMotor(MotorIndex.TILT_MOTOR);
                // Step2: put message to request queue
                queueTxPacket.Enqueue(requestPacketBuffer);
                LogViewer.Add(string.Format("Send message set tilt stop"), LogType.Info);
            }
        }

        private void btnUpdateFirmwareToSelectedDevice_Click(object sender, EventArgs e)
        {
            if (isDeviceConnected == false) return;
            if (MainWindowViewModel.GetInstance().BtnFirmwareUpdateContent == "Update FW")
            {
                // Check firmware was opened successfully or not
                if (bootloaderProcessor.IsValid == false)
                {
                    System.Windows.Forms.MessageBox.Show("File firmware không hợp lệ. Hãy mở lại.", "Invalid Firmware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var msgBoxRet = System.Windows.Forms.MessageBox.Show(string.Format("Cập nhật firmware lên phiên bản {0}.{1}.{2}",
                        bootloaderProcessor.NewFirmwareVersion[0], bootloaderProcessor.NewFirmwareVersion[1],
                        bootloaderProcessor.NewFirmwareVersion[2]), "Cornfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgBoxRet == DialogResult.Yes)
                    {
                        var ret = bootloaderProcessor.StartDFU();
                        MainWindowViewModel.GetInstance().BtnFirmwareUpdateContent = "Stop";
                    }
                }
            }
            else
            {
                var msgBoxRet = System.Windows.Forms.MessageBox.Show("Hủy bỏ quá trình cập nhật firmware có thể làm lỗi thiết bị?", "Cancel DFU Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (msgBoxRet == DialogResult.Yes)
                {
                    bootloaderProcessor.StopDFU();
                    //btnUpdateFirmware.Content = "Update FW";
                    MainWindowViewModel.GetInstance().BtnFirmwareUpdateContent = "Update FW";
                }
            }
        }

        private void btnOpenFirmwareFile_Click(object sender, EventArgs e)
        {
            MainWindowViewModel.GetInstance().BtnFirmwareUpdateContent = "Update FW";
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "Binary firmware files (*.bin) |*.bin";
            string fileDir = string.Empty;
            try
            {
                fileDir = System.IO.Path.GetDirectoryName(Properties.Settings.Default.LastOpenFirmwareFilePath);
            }
            catch (Exception ex)
            {
                LogViewer.Add(ex.ToString(), LogType.Error);
            }
            if (Directory.Exists(fileDir))
            {
                openFile.InitialDirectory = fileDir;
            }

            System.Windows.Forms.DialogResult result = openFile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (bootloaderProcessor.ReadBinaryFile(openFile.FileName) == true)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("Mở file firmware thành công, version: {0}.{1}.{2}",
                            bootloaderProcessor.NewFirmwareVersion[0],
                            bootloaderProcessor.NewFirmwareVersion[1],
                            bootloaderProcessor.NewFirmwareVersion[2]), "Open File Successfully", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        LogViewer.Add(String.Format("Read file successfully, fimware version {0}.{1}.{2}",
                            bootloaderProcessor.NewFirmwareVersion[0],
                            bootloaderProcessor.NewFirmwareVersion[1],
                            bootloaderProcessor.NewFirmwareVersion[2]), LogType.Info);
                        LogViewer.Add("Total packet: " + bootloaderProcessor.TotalPacket.ToString(), LogType.Info);

                        // Save file name
                        Properties.Settings.Default.LastOpenFirmwareFilePath = openFile.FileName;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Không thể đọc file firmware", "Open File Error");
                    }
                }
                catch (Exception ex)
                {
                    LogViewer.Add(ex.ToString(), LogType.Error);
                }
            }
        }
    }
}
