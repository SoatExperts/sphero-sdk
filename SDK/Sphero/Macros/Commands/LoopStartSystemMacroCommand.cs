using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is a different way to begin a loop block, where the loop count is specified by API command instead of by an immediate value. 
    /// </summary>
    public class LoopStartSystemMacroCommand : MacroCommand
    {
        public LoopStartSystemMacroCommand()
        {
            CommandID = (byte)MacroCommandID.LoopStartSystem;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID };
        }
    }
}
