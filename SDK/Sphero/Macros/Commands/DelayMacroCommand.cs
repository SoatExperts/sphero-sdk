using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This causes an immediate delay in the execution of additional macro commands, while allowing the background ones to keep running.
    /// </summary>
    public class DelayMacroCommand : MacroCommand
    {
        public int Time { get; set; }

        public DelayMacroCommand()
        {
            CommandID = (byte)MacroCommandID.Delay;
        }


        public override byte[] ToArray()
        {
            byte[] timeBuffer = ByteHelper.IntegerToBytes(Time);

            return new byte[] { CommandID, timeBuffer[0], timeBuffer[1] };
        }
    }
}
