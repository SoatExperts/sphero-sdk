using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This reassigns Sphero's current heading to the supplied value. 
    /// The units are degrees so the valid range is 0 to 359. This forms the basis for future roll commands. 
    /// For example if you assign the current heading to zero and issue a roll command along heading 90, Sphero will make a right turn
    /// </summary>
    public class SetHeadingMacroCommand : MacroCommand
    {
        public int Heading { get; set; }

        public byte PCD { get; set; }

        public SetHeadingMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetHeading;
        }

        public override byte[] ToArray()
        {
            byte[] integerBytes = ByteHelper.IntegerToBytes(Heading);
            return new byte[] { CommandID, integerBytes[0], integerBytes[1], PCD };
        }
    }
}
