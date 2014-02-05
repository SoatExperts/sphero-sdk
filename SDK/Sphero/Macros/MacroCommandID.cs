using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros
{
    /// <summary>
    /// Enumeration of the different Macro commands ID
    /// </summary>
    public enum MacroCommandID
    {
        MacroEnd = 0x00,
        SetSD1 = 0x01,
        SetSD2 = 0x02,
        SetStabilization = 0x03,
        SetHeading = 0x04,
        Roll = 0x05,
        RollWithSD1 = 0x06,
        SetRGBLED = 0x07,
        SetRGBLEDWithSD2 = 0x08,
        SetBackLED = 0x09,
        SendRawMotorValues = 0x0A,
        Delay = 0x0B,
        Goto = 0x0C,
        Gosub = 0x0D,
        GoToSleep = 0x0E,
        SetSPD1 = 0x0F,
        SetSPD2 = 0x10,
        RollAtSPD1WithSD1 = 0x11,
        RollAtSPD2WithSD1 = 0x12,
        SetRotationRate = 0x13,
        FadeToRGB = 0x14,
        EmitMarker = 0x15,
        //0x16
        //0x17
        //0x18
        WaitUntilStopped=0x19,
        RotateOverTime=0x1A,
        StreamEnd=0x1B,
        Reserved=0x1C,
        Roll2=0x1D,
        LoopStart=0x1E,
        LoopEnd=0x1F,
        Comment=0x20,
        RotateOverSD1=0x21,
        RotateOverSD2=0x22,
        BranchOnCollision=0x23,
        LoopStartSystem=0x24,
        SetSpeed=0x25,
        //0x26
        ConfigureCollisionDetection=0x27
    }
}
