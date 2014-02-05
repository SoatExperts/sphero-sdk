using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// Begins a looping block, repeating the commands between this one and Loop End the specified number of times.
    /// A count of 0 is treated as 1, neither of which do anything additional. 
    /// A second Loop Start before a Loop End replaces the previous Loop Start. 
    /// You can use Goto and Gosub from within loop blocks.
    /// </summary>
    public class LoopStartMacroCommand : MacroCommand
    {
        public byte Count { get; set; }

        public LoopStartMacroCommand()
        {
            CommandID = (byte)MacroCommandID.LoopStart;
        }
        public override byte[] ToArray()
        {
            return new byte[] { CommandID, Count };
        }
    }
}
