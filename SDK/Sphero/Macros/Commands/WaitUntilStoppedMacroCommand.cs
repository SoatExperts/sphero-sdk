
using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This clever command will pause execution of macros until Sphero is determined "stopped" by the stabilization system or until the provided timeout expires. 
    /// You can use this, for example, at corners where you want roll commands to make sharp turns. 
    /// </summary>
    public class WaitUntilStoppedMacroCommand : MacroCommand
    {
        public int Time { get; set; }

        public WaitUntilStoppedMacroCommand()
        {
            CommandID = (byte)MacroCommandID.WaitUntilStopped;
        }

        public override byte[] ToArray()
        {
            byte[] timeBuffer = ByteHelper.IntegerToBytes(Time);
            return new byte[] { CommandID, timeBuffer[0], timeBuffer[1] };
        }
    }
}
