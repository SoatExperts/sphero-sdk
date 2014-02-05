using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This signals the end of a normal macro. 
    /// If there is an address in the gosub stack then execution will resume after the Gosub that called it. 
    /// If a stream macro is running this command is ignored. 
    /// The macro flags contain some options that can be executed at the end of a macro
    /// </summary>
    public class EndMacroCommand : MacroCommand
    {
        public EndMacroCommand()
        {
            CommandID = (byte)MacroCommandID.MacroEnd;
        }
        public override byte[] ToArray()
        {
            return new byte[] { CommandID };
        }
    }
}
