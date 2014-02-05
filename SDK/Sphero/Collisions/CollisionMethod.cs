using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Collisions
{
    public enum CollisionMethod
    {
        Disable = 0x00,
        Enable = 0x01,
        EnableWithPostTriggerFiltering = 0x02
    }
}
