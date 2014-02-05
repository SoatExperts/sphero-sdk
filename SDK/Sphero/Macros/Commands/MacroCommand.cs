using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// Contains common Macro command informatinos
    /// </summary>
    public abstract class MacroCommand
    {
        protected byte CommandID;

        public abstract byte[] ToArray();
    }
}
