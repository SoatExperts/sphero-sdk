using Sphero.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This turns on and off the control system which actively stabilizes Sphero. 
    /// If you intend to drive around, you should make sure the system that allows you to do it is enabled. 
    /// Note that sending raw motor commands implicitly disables the stabilization system
    /// </summary>
    public class SetStabilizationMacroCommand : MacroCommand
    {
        public StabilizationStatus Flag { get; set; }

        public byte PCD { get; set; }

        public SetStabilizationMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetStabilization;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, (byte)Flag, PCD };
        }
    }
}
