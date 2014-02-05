using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is the same as Rotate Over Time but instead of requiring an immediate value, command inherits this value from System Delay 2. 
    /// </summary>
    public class RotateOverSD2MacroCommand : MacroCommand
    {
        public int Angle { get; set; }

        public RotateOverSD2MacroCommand()
        {
            CommandID = (byte)MacroCommandID.RotateOverSD2;
        }

        public override byte[] ToArray()
        {
            byte[] angleBuffer = ByteHelper.IntegerToBytes(Angle);
            return new byte[] { CommandID, angleBuffer[0], angleBuffer[1] };
        }
    }
}
