using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// Terminates a looping block. If no actual loop is in process, or if the ID of the current macro doesn't match that of the Loop Start, this command is ignored. 
    /// </summary>
    public class LoopEndMacroCommand : MacroCommand
    {
        public LoopEndMacroCommand()
        {
            CommandID = (byte)MacroCommandID.LoopEnd;
        }
        public override byte[] ToArray()
        {
            return new byte[] { CommandID };
        }
    }
}
