using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public static class Masks
    {
        public const uint RawAccelerometerX = 0x80000000;
        public const uint RawAccelerometerY = 0x40000000;
        public const uint RawAccelerometerZ = 0x20000000;
        public const uint RawAccelerometer = RawAccelerometerX | RawAccelerometerY | RawAccelerometerZ;

        public const uint RawGyroscopeX = 0x10000000;
        public const uint RawGyroscopeY = 0x08000000;
        public const uint RawGyroscopeZ = 0x04000000;
        public const uint RawGyroscope = RawGyroscopeX | RawGyroscopeY | RawGyroscopeZ;


        public const uint FilteredIMUPitch = 0x00040000;
        public const uint FilteredIMURoll = 0x00020000;
        public const uint FilteredIMUYaw = 0x00010000;
        public const uint FilteredIMU = FilteredIMUPitch | FilteredIMURoll | FilteredIMUYaw;


        public const uint FilteredAccelerometerX = 0x00008000;
        public const uint FilteredAccelerometerY = 0x00004000;
        public const uint FilteredAccelerometerZ = 0x00002000;
        public const uint FilteredAccelerometer = FilteredAccelerometerX | FilteredAccelerometerY | FilteredAccelerometerZ;

        public const uint FilteredGyroscopeX = 0x00001000;
        public const uint FilteredGyroscopeY = 0x00000800;
        public const uint FilteredGyroscopeZ = 0x00000400;
        public const uint FilteredGyroscope = FilteredGyroscopeX | FilteredGyroscopeY | FilteredGyroscopeZ;

        public const uint Reserved = 0x3800381F;

        // TODO : Implements other message


    }

    public static class Masks2
    {
        public const uint QuaternionQ0 = 0x80000000;
        public const uint QuaternionQ1 = 0x40000000;
        public const uint QuaternionQ2 = 0x20000000;
        public const uint QuaternionQ3 = 0x10000000;
        public const uint Quaternion = QuaternionQ0 | QuaternionQ1 | QuaternionQ2 | QuaternionQ3;

        public const uint OdometerX = 0x08000000;
        public const uint OdometerY = 0x04000000;
        public const uint Odometer = OdometerX | OdometerY;

        public const uint AccelOne = 0x02000000;

        public const uint VelocityX = 0x01000000;
        public const uint VelocityY = 0x00800000;
        public const uint Velocity = VelocityX | VelocityY;
        
    }
}
