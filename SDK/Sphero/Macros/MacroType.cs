using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros
{
    /// <summary>
    /// Enumeration of the different Macro types
    /// </summary>
    public enum MacroType
    {
        Permanent = 0x01,
        Stream = 0xFE,
        Temporary = 0xFF
    }
}
