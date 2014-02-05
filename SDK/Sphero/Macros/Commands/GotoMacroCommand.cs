using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// You can chain macros with this and the Gosub command. 
    /// The sole parameter is the Macro ID of where you want to go to. 
    /// If the target ID doesn't exist, the macro aborts. 
    /// If it does then control is transferred with the current system state intact. 
    /// The macro flags of the target macro replace those currently in use. 
    /// *You cannot specify the Stream Macro ID as a destination.  
    /// </summary>
    public class GotoMacroCommand : MacroCommand
    {
        public byte TargetMacroID { get; set; }

        public GotoMacroCommand()
        {
            CommandID = (byte)MacroCommandID.Goto;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, TargetMacroID };
        }
    }
}
