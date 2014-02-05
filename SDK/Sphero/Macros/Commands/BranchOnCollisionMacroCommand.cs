using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This command ties the macro system to collision detection system as of FW ver 1.10 and Macro Ver 4. 
    /// When enabled, the collision detection system sets a flag which the macro executive acknowledges and then executes a Goto command to the specified Macro ID. 
    /// *Like Goto, you cannot specify the Stream Macro ID as a destination. 
    /// But you can set the target as ID 00h which makes sure this feature is turned off – required if you're chaining between macros that alternately enable and disable this feature. 
    /// You must program and arm the collision detector separately (through an API, macro or orbBasic command) before this macro command will have any effect. 
    /// </summary>
    public class BranchOnCollisionMacroCommand : MacroCommand
    {
        public byte TargetMacroID { get; set; }

        public BranchOnCollisionMacroCommand()
        {
            CommandID = (byte)MacroCommandID.BranchOnCollision;
        }
        public override byte[] ToArray()
        {
            return new byte[] { CommandID, TargetMacroID };
        }
    }
}
