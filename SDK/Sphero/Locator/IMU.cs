using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public class IMU
    {
        private short _pitch;
        private short _roll;
        private short _yaw;

        public short Pitch { get { return _pitch; } }

        public short Roll { get { return _roll; } }
        
        public short Yaw { get { return _yaw; } }

        internal IMU() { }

        internal IMU(short pitch, short roll, short yaw)
        {
            _pitch = pitch;
            _roll = roll;
            _yaw = yaw;
        }
    }
}
