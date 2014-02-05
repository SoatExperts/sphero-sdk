using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    public enum MotorMode
    {
        Off = 0x00,
        Forward = 0x01,
        Reverse = 0x02,
        Brake = 0x03,
        Ignore = 0x04
    }
}
