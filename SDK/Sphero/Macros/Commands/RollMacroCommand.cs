using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This command gets Sphero to start rolling along the commanded speed and  heading. 
    /// If the stabilization system is off, this command will do nothing. 
    /// A speed of 00h also engages ramped down braking of roll speed.
    /// </summary>
    public class RollMacroCommand :MacroCommand
    {
        public byte Speed { get; set; }

        public short Heading { get; set; }

        public byte PCD { get; set; }

        public RollMacroCommand()
        {
            CommandID = (byte)MacroCommandID.Roll;
        }


        public override byte[] ToArray()
        {
            byte[] headingBuffer = ByteHelper.IntegerToBytes(Heading);

            return new byte[] { CommandID, Speed, headingBuffer[0], headingBuffer[1], PCD };
        }
    }
}
