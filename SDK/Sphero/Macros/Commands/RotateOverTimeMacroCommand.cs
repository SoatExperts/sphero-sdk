using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This command drives the yaw control system directly to effect an angular change over time. 
    /// The angle parameter is a signed number of degrees and time is of course in milliseconds. 
    /// For example, Sphero will spin around clockwise twice in four seconds if your parameters are 720 and 4000 (the byte sequence would be 02h, D0h, 0Fh, A0h).  
    /// Counterclockwise in five seconds would be -720, 5000 (bytes FDh, 30h, 13h, 88h). 
    /// 
    /// NOTE: This command runs in the background. Any roll commands executed before it is finished will be ignored. 
    /// In the above examples you need to be doing something for 4 and 5 seconds to give it time to finish (either other commands or simply the delay command). 
    /// </summary>
    public class RotateOverTimeMacroCommand : MacroCommand
    {
        public int Angle { get; set; }

        public int Time { get; set; }

        public RotateOverTimeMacroCommand()
        {
            CommandID = (byte)MacroCommandID.RotateOverTime;
        }

        public override byte[] ToArray()
        {
            byte[] angleBuffer = ByteHelper.IntegerToBytes(Angle);
            byte[] timeBuffer = ByteHelper.IntegerToBytes(Time);

            return new byte[] { CommandID, angleBuffer[0], angleBuffer[1], timeBuffer[0], timeBuffer[1] };
        }
    }
}
