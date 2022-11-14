using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanTilt123.Class
{    public class MotorInfo : ViewModelBase
    {
        //private RequestType requestType = 0;
        //private ControlMode controlMode = 0;
        //private float position;
        private float maxSpeed;
        private float motorAcce;
        //private float weakMagnetic;
        private float speedKp;
        private float speedKi;
        private float positionKp;
        private float deadBand;
        private float side;
        private float angleOffset;

        //public RequestType RequestType { get => requestType; set { SetProperty(ref requestType, value); } }
        //public ControlMode ControlMode { get => controlMode; set { SetProperty(ref controlMode, value); } }
        //public float Position { get => position; set { SetProperty(ref position, value); } }
        public float MaxSpeed { get => maxSpeed; set { SetProperty(ref maxSpeed, value); } }
        public float Accelement { get => motorAcce; set { SetProperty(ref motorAcce, value); } }
        //public float WeakMagnetic { get => weakMagnetic; set { SetProperty(ref weakMagnetic, value); } }
        public float SpeedKp { get => speedKp; set { SetProperty(ref speedKp, value); } }
        public float SpeedKi { get => speedKi; set { SetProperty(ref speedKi, value); } }
        public float PositionKp { get => positionKp; set { SetProperty(ref positionKp, value); } }
        public float DeadBand { get => deadBand; set { SetProperty(ref deadBand, value); } }
        public float Side { get => side; set { SetProperty(ref side, value); } }
        public float AngleOffset { get => angleOffset; set { SetProperty(ref angleOffset, value); } }
    }

}
