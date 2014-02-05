using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This signals the end of a stream macro. Most of the same rules as above apply, but if you use this command out of a stream macro then processing will abort. 
    /// </summary>
    public class StreamEndMacroCommand : MacroCommand
    {

        public StreamEndMacroCommand()
        {
            CommandID = (byte)MacroCommandID.StreamEnd;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID };
        }
    }
}
