using PanTilt123.Class;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanTilt123
{
    public partial class frmWindowSetting : Form
    {
        public SettingWindowViewModel SettingWindowViewModel = null;

        private ConcurrentQueue<Byte[]> queueTxPacket = null;
        private ConcurrentQueue<Byte[]> queueRxPacket = null;
        private MotorIndex motor = MotorIndex.PAN_MOTOR;
        private UInt32 baudrate = 9600;
        private Timer timeCheckQueueRx = new Timer();
        public frmWindowSetting(ConcurrentQueue<Byte[]> txQueue, ConcurrentQueue<Byte[]> rxQueue)
        {
            InitializeComponent();
            SettingWindowViewModel = SettingWindowViewModel.GetInstance();
            //this.DataContext = SettingWindowViewModel;
            queueTxPacket = txQueue;
            queueRxPacket = rxQueue;

            timeCheckQueueRx.Interval = 10;
            timeCheckQueueRx.Tick += timeCheckQueueRxHandler_Tick;
            //timeCheckQueueRx.Start();
        }

        private void timeCheckQueueRxHandler_Tick(object sender, EventArgs e)
        {
            timeCheckQueueRx.Stop();

            if (!queueRxPacket.IsEmpty)
            {
                byte[] packetBuffer;
                queueRxPacket.TryDequeue(out packetBuffer);
                Rs485PacketParser.ParseRxMotorInfoResponse(packetBuffer);
            }

            timeCheckQueueRx.Start();
        }
    }
}
