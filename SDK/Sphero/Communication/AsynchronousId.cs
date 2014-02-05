using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    internal enum AsynchronousId
    {
        POWER_NOTIFICATION =0x01,
        LEVEL_1_DIAGNOSTIC = 0x02,
        SENSOR_DATA_STREAMING = 0x03,
        CONFIG_BLOCK_CONTENTS = 0x04,
        PRE_SLEEP_WARNING = 0x05,
        MACRO_MARKERS = 0x06,
        COLLISION_DETECTED = 0x07,
        ORBBASIC_PRINT_MSG = 0x08,
        ORBBASIC_ERROR_MSG_ASCII = 0x09,
        ORBBASIC_ERROR_MSG_BINARY = 0x0A,
        SELF_LEVEL_RESULT = 0x0B,
        GYRO_AXIS_LIMIT_EXCEEDED = 0x0C,
        SSB_DATA = 0x0D,
        LVL_UP = 0x0E,
        SHIELD_DAMAGE = 0x0F,
        XP_UPDATE = 0x10,
        BOOST_UPDATE = 0x11

    }
}
