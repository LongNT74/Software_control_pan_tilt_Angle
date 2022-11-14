using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace PanTilt123.Class
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MainWindowViewModel()
        {
            
        }

        private static MainWindowViewModel instance = null;
        public static MainWindowViewModel GetInstance()
        {
            
            if(instance == null)
            {
                instance = new MainWindowViewModel();
            }
            return instance;
        }

        // Comport
        private List<string> availbleComportList = new List<string>();
        private int comportName = 0;
        private int comportBaudrate = 0;
        //private BitmapSource comportConnectionStatusIcon = portDisConnectedImage;

        //private static BitmapImage portConnectedImage = new BitmapImage(new Uri("pack://application:,,/Image/port-connected.png"));
        //private static BitmapImage portDisConnectedImage = new BitmapImage(new Uri("pack://application:,,/Image/port-disconnected.png"));

        // Motor control
        private int motorType = 0;

        // Device info
        private DeviceInfo rxDeviceInfo = new DeviceInfo();
        private DeviceInfo txDeviceInfo = new DeviceInfo();
        private string newCtrlAddress = "192.168.1.1";

        // Tx motor control info
        private MotorInfo txMotorControlInfo = new MotorInfo();

        // Rx motor control info
        private MotorInfo rxMotorControlInfo = new MotorInfo();

        // PanTilt angle control
        private double txPanAngle = 0;
        private double rxPanAngle = 0;
        private double txTiltAngle = 0;
        private double rxTiltAngle = 0;
        private double txAngleStepSize = 5;

        // Progress bar
        private int progressBarDeviceEnumrationValue = 0;
        private int progressBarUpdateFwValue = 0;

        // Firmware update button
        private bool isDeviceEnumrationStarted = false;

        // Flags for read adc then add to data grid cell
        private int dataGridCurrentSelectedRowIndex = 0;

        private string btnFirmwareUpdateContent = "Update FW";

        private ObservableCollection<DeviceEnumrationInfo> deviceEnumrationInfoList = new ObservableCollection<DeviceEnumrationInfo>();
        //private bool isDeviceEnumrationStarted = false;
        public MotorInfo TxMotorControlInfo { get => txMotorControlInfo; set { SetProperty(ref txMotorControlInfo, value); } }
        public MotorInfo RxMotorControlInfo { get => rxMotorControlInfo; set { SetProperty(ref rxMotorControlInfo, value); } }
        public int SelectedComPortIndex { get => comportName; set { SetProperty(ref comportName, value); } }
        public int SelectedComPortBaudrate { get => comportBaudrate; set { SetProperty(ref comportBaudrate, value); } }
        public DeviceInfo RxDeviceInfo { get => rxDeviceInfo; set { SetProperty(ref rxDeviceInfo, value); } }
        public DeviceInfo TxDeviceInfo { get => txDeviceInfo; set { SetProperty(ref txDeviceInfo, value); } }
        public int MotorType { get => motorType; set { SetProperty(ref motorType, value); } }
        public int ProgressBarUpdateFwValue { get => progressBarUpdateFwValue; set { SetProperty(ref progressBarUpdateFwValue, value); } }
        public double TxPanAngle { get => txPanAngle; set { SetProperty(ref txPanAngle, value); } }
        public double RxPanAngle { get => rxPanAngle; set { SetProperty(ref rxPanAngle, value); } }
        public double TxTiltAngle { get => txTiltAngle; set { SetProperty(ref txTiltAngle, value); } }
        public double RxTiltAngle { get => rxTiltAngle; set { SetProperty(ref rxTiltAngle, value); } }
        public double TxAngleStepSize { get => txAngleStepSize; set { SetProperty(ref txAngleStepSize, value); } }
        public int DataGridCurrentSelectedRowIndex { get => dataGridCurrentSelectedRowIndex; set { SetProperty(ref dataGridCurrentSelectedRowIndex, value); } }
        public List<string> AvailbleComportList { get => availbleComportList; set { SetProperty(ref availbleComportList, value); } }
        //public BitmapSource ComportConnectionStatusIcon { get => comportConnectionStatusIcon; set { SetProperty(ref comportConnectionStatusIcon, value); } }
        public int ProgressBarDeviceEnumrationValue { get => progressBarDeviceEnumrationValue; set { SetProperty(ref progressBarDeviceEnumrationValue, value); } }
        public string NewCtrlAddress { get => newCtrlAddress; set { SetProperty(ref newCtrlAddress, value); } }
        public ObservableCollection<DeviceEnumrationInfo> DeviceEnumrationInfoList { get => deviceEnumrationInfoList; set { SetProperty(ref deviceEnumrationInfoList, value); } }
        public bool IsDeviceEnumrationStarted { get => isDeviceEnumrationStarted; set => isDeviceEnumrationStarted = value; }
        public string BtnFirmwareUpdateContent { get => btnFirmwareUpdateContent; set { SetProperty(ref btnFirmwareUpdateContent, value); } }

       

        //public void SetComportConnectionStatusIcon(bool isConnected)
        //{
        //    if (isConnected)
        //    {
        //        ComportConnectionStatusIcon = portConnectedImage;
        //    }
        //    else
        //    {
        //        ComportConnectionStatusIcon = portDisConnectedImage;
        //    }
        //}
    }
}
