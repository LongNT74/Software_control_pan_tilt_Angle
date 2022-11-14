using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanTilt123.Class
{
    public class SettingWindowViewModel : ViewModelBase
    {

        private SettingWindowViewModel() { }

        private static SettingWindowViewModel instance = null;
        public static SettingWindowViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingWindowViewModel();
            }
            return instance;
        }

        private float maxspeed = 0;
        private float accelement = 0;
        private float speedKp = 0;
        private float speedKi = 0;
        private float positionKp = 0;
        private float deadband = 0;
        private float side = 0;
        private float angleOffset = 0;

        public float MaxSpeed { get => maxspeed; set { SetProperty(ref maxspeed, value); } }
        public float Accelement { get => accelement; set { SetProperty(ref accelement, value); } }
        public float SpeedKp { get => speedKp; set { SetProperty(ref speedKp, value); } }
        public float SpeedKi { get => speedKi; set { SetProperty(ref speedKi, value); } }
        public float PositionKp { get => positionKp; set { SetProperty(ref positionKp, value); } }
        public float DeadBand { get => deadband; set { SetProperty(ref deadband, value); } }
        public float Side { get => side; set { SetProperty(ref side, value); } }
        public float AngleOffset { get => angleOffset; set { SetProperty(ref angleOffset, value); } }


    }
}
