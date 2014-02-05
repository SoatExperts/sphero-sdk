using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{

    /// <summary>
    /// Sphero's control system implements an intermediate rate limiter for the yaw axis, feeding smoothed transitions to that servo loop. 
    /// This sets the maximum increment. As of firmware version 0.92 the formula for converting the rate parameter R to degrees/second is: 40 + R/2
    /// which yields a smoothed range from 40 to 167 deg/s. 
    /// This only applies to Roll commands; if you use the macro command Rotate Over Time this setting is bypassed.  
    /// </summary>
    public class SetRotationRateMacroCommand : MacroCommand
    {
        public byte Rate { get; set; }

        public SetRotationRateMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetRotationRate;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, Rate };
        }
    }
}
