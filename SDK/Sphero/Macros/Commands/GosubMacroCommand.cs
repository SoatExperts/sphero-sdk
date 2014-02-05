using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// You can factor out common command sets and then call them using Gosub. 
    /// An illegal target aborts the macro and it is ignored in stream macro mode. 
    /// The call stack is currently one level deep and once it's full this command is just ignored. 
    /// There is no explicit return needed, just a macro end command. 
    /// *Like Goto, you cannot specify the Stream Macro ID as a destination. 
    /// </summary>
    public class GosubMacroCommand : MacroCommand
    {
        public byte TargetCommandID { get; set; }

        public GosubMacroCommand()
        {
            CommandID = (byte)MacroCommandID.Gosub;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, TargetCommandID };
        }
    }
}
