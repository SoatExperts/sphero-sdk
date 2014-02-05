using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    public enum MessageResponseCode
    {
        ORBOTIX_RSP_CODE_OK = 0x0,
        ORBOTIX_RSP_CODE_EGEN = 0x1,
        ORBOTIX_RSP_CODE_ECHKSUM = 0x2,
        ORBOTIX_RSP_CODE_EFRAG = 0x3,
        ORBOTIX_RSP_CODE_EBAD_CMD = 0x4,
        ORBOTIX_RSP_CODE_EUNSUPP = 0x5,
        ORBOTIX_RSP_CODE_EBAD_MSG = 0x6,
        ORBOTIX_RSP_CODE_EPARAM = 0x7,
        ORBOTIX_RSP_CODE_EEXEC = 0x8,
        ORBOTIX_RSP_CODE_EBAD_DID = 0x9,
        ORBOTIX_RSP_CODE_POWER_NOGOOD = 0x31,
        ORBOTIX_RSP_CODE_PAGE_ILLEGAL = 0x32,
        ORBOTIX_RSP_CODE_FLASH_FAIL = 0x33,
        ORBOTIX_RSP_CODE_MA_CORRUPT = 0x34,
        ORBOTIX_RSP_CODE_MSG_TIMEOUT = 035
    }
}
