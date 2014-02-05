using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is like the Roll command but it does not effect the heading. 
    /// It is especially useful when you don't know the current heading but want to stop without experiencing a turning glitch (Speed = 0). 
    /// </summary>
    public class SetSpeedMacroCommand : MacroCommand
    {
        public int Speed { get; set; }

        public byte PCD { get; set; }


        public SetSpeedMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetSpeed;
        }

        public override byte[] ToArray()
        {
            byte[] speedBuffer = ByteHelper.IntegerToBytes(Speed);
            return new byte[] { CommandID, speedBuffer[0], speedBuffer[1], PCD };
        }
    }
}
