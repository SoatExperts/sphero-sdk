using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    internal static class CommandIDs
    {
        internal static class Core
        {
            internal const byte PING = 0x01;
            internal const byte GET_VERSIONING = 0x02;
            internal const byte SET_DEVICE_NAME = 0x10;
            internal const byte GET_BLUETOOTH_INFO = 0x11;
            internal const byte SET_POWER_NOTIFICATION = 0x21;
        }

        internal static class Sphero
        {
            internal const byte SET_HEADING = 0x01;
            internal const byte SET_STABILIZATION = 0x02;
            internal const byte SET_DATA_STREAMING = 0x11;
            internal const byte CONFIGURE_COLLISION_DETECTION = 0x12;
            internal const byte SET_RBG_LED = 0x20;
            internal const byte SET_BACK_LED = 0x21;
            internal const byte GET_RGB_LED = 0x22;
            internal const byte ROLL = 0x30;
            internal const byte BOOST = 0x31;
            internal const byte GET_CONFIGURATION_BLOCK = 0x40;
            internal const byte SET_DEVICE_MODE = 0x42;
            internal const byte GET_SSB = 0x46;
            internal const byte ADD_XP = 0x4C;
            internal const byte GET_PASSWORD_SEED = 0x4E;
            internal const byte ENABLE_SSB = 0x4F;
            internal const byte RUN_MACRO = 0x50;
            internal const byte SAVE_TEMPORARY_MACRO = 0x51;
            internal const byte SAVE_MACRO = 0x52;
            internal const byte REINIT_MACRO = 0x54;
            internal const byte ABORT_MACRO = 0x55;
            internal const byte GET_MACRO_STATUS= 0x56;
            internal const byte SET_MACRO_PARAMETER = 0x57;
            internal const byte APPEND_MACRO_CHUNK = 0x58;

            
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;

            return value;
        }

        public static byte[] FromRGBWToBytes(int r, int g, int b, int w)
        {
            r = Clamp(r, 0, 63);
            g = Clamp(g, 0, 63);
            b = Clamp(b, 0, 63);
            w = Clamp(w, 0, 63);

            int color = 0;

            color += w & 0x3F;
            color += (b & 0x3F) << 6;
            color += (g & 0x3F) << 12;
            color += (r & 0x3F) << 18;

            var returnedBytes = new[] { (byte)(color >> 16), (byte)(color >> 8), (byte)color };
            return returnedBytes;
        }

        public struct RGBW
        {
            public int r;
            public int g;
            public int b;
            public int w;
        }

        public static RGBW FromBytesToRGBW(byte[] color)
        {
            // TODO test array size


            RGBW rgbw;
            rgbw.r = (color[0] >> 2) & 0x3F;
            rgbw.g = (((color[0] & 0x03) << 6) & 0xC0) | ((((color[1] >> 2) & 0xF0) >> 4) & 0x0F);
            rgbw.b = (((color[1] & 0x03) << 6) & 0xC0) | ((((color[2] >> 2) & 0xF0) >> 4) & 0x0F);
            rgbw.w = color[2] & 0x3F;

            return rgbw;
        }
    }
}
