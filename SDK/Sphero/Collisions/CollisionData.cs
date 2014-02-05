using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Collisions
{
    /// <summary>
    /// Represente a collision of sphero with obstacle
    /// </summary>
    public class CollisionData
    {
        private short _x;
        private short _y;
        private short _z;
        private byte _axis;
        private short _xMagnitude;
        private short _yMagnitude;
        private byte _speed;
        private int _timestamp;

        /// <summary>
        /// Init properties with raw data
        /// </summary>
        /// <param name="data"></param>
        internal CollisionData(byte[] data)
        {
            if (data == null) return;
            if (data.Length != 16)
                return;

            _x = ByteHelper.BytesToShort(new byte[] { data[0], data[1] });
            _y = ByteHelper.BytesToShort(new byte[] { data[2], data[3] });
            _z = ByteHelper.BytesToShort(new byte[] { data[4], data[5] });
            _axis = data[6];
            _xMagnitude = ByteHelper.BytesToShort(new byte[] { data[7], data[8] });
            _yMagnitude = ByteHelper.BytesToShort(new byte[] { data[9], data[10] });
            _speed = data[11];
            _timestamp = ByteHelper.BytesToInteger(new byte[] { data[12], data[13], data[14], data[15] });
        }
    }
}
