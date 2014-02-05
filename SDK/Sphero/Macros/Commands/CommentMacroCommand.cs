using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is out of band data and no processing is performed upon it. 
    /// The macro is aborted if the Length points to a place outside of the current macro or outside of the valid data area on the Temp or Stream macro buffer.
    /// </summary>
    public class CommentMacroCommand : MacroCommand
    {
        public string Data { get; set; }

        public CommentMacroCommand()
        {
            CommandID = (byte)MacroCommandID.Comment;
        }
        public override byte[] ToArray()
        {

            byte[] lengthBuffer =ByteHelper.IntegerToBytes(Data.Length);


            if (String.IsNullOrEmpty(Data))
            {
                return new byte[] { CommandID, 0x00, 0x00 };
            }
            else
            {
                byte[] returnBuffer = new byte[Data.Length + 3];
                returnBuffer[0] = CommandID;
                returnBuffer[1] = lengthBuffer[0];
                returnBuffer[2] = lengthBuffer[1];
                Array.Copy(StringHelper.StringToAscii(Data), 0, returnBuffer, 3, Data.Length);

                return new byte[] { CommandID, lengthBuffer[0], lengthBuffer[1],  };
            }
        }
    }
}
