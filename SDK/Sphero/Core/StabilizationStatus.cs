using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    public enum StabilizationStatus
    {
        OFF = 0x00,
        OnWithReset = 0x01,
        OnWithoutReset = 0x02
    }
}
