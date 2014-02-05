using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public class SensorData
    {
        private ThreeAxis _rawAccelerometer;
        //private ThreeAxis _rawGyroscope;

        private ThreeAxis _filteredAccelerometer;
        //private ThreeAxis _filteredGyroscope;

        private IMU _filteredIMU;

        // Mask 2
        private Quaternion _quaternion;
        private TwoAxis _odometer;
        private short _accelOne;
        private TwoAxis _velocity;

        public ThreeAxis RawAccelerometer { get { return this._rawAccelerometer; } }
        public ThreeAxis FilteredAccelerometer { get { return this._filteredAccelerometer; } }

        public IMU FilteredIMU { get { return this._filteredIMU; } }
        
        // TODO implements other data


        // Mask 2
        public Quaternion Quaterion { get { return _quaternion; } }
        public TwoAxis Odometer { get { return _odometer; } }
        public short AccelOne { get { return _accelOne; } }
        public TwoAxis Velocity { get { return _velocity; } }

        internal SensorData(uint mask, uint mask2, byte[] data)
        {
            int dataIndex = 0;

            #region Mask

            //  Mask
            for (uint i = 0x80000000; i > 0; i = i >> 1)
            {
                if ((i & Masks.RawAccelerometerX) > 0 && (mask & Masks.RawAccelerometerX) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateRawAccelerometerX);
                    dataIndex += 2;
                }
                else if ((i & Masks.RawAccelerometerY) > 0 && (mask & Masks.RawAccelerometerY) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateRawAccelerometerY);
                    dataIndex += 2;
                }

                else if ((i & Masks.RawAccelerometerZ) > 0 && (mask & Masks.RawAccelerometerZ) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateRawAccelerometerZ);
                    dataIndex += 2;
                }
                else if ((i & Masks.FilteredAccelerometerX) > 0 && (mask & Masks.FilteredAccelerometerX) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredAccelerometerX);
                    dataIndex += 2;
                }
                else if ((i & Masks.FilteredAccelerometerY) > 0 && (mask & Masks.FilteredAccelerometerY) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredAccelerometerY);
                    dataIndex += 2;
                }

                else if ((i & Masks.FilteredAccelerometerZ) > 0 && (mask & Masks.FilteredAccelerometerZ) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredAccelerometerZ);
                    dataIndex += 2;
                }
                //
                else if ((i & Masks.FilteredIMUPitch) > 0 && (mask & Masks.FilteredIMUPitch) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredIMUPitch);
                    dataIndex += 2;
                }
                else if ((i & Masks.FilteredIMURoll) > 0 && (mask & Masks.FilteredIMURoll) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredIMURoll);
                    dataIndex += 2;
                }

                else if ((i & Masks.FilteredIMUYaw) > 0 && (mask & Masks.FilteredIMUYaw) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateFilteredIMUYaw);
                    dataIndex += 2;
                }
                else if((i & mask & Masks.Reserved) > 0)
                {
                    dataIndex += 2;
                }

            }

            #endregion

            #region Mask2
            for (uint i = 0x80000000; i > 0; i = i >> 1)
            {

                // Quaternion Q0
                if ((i & Masks2.QuaternionQ0) > 0 && (mask2 & Masks2.QuaternionQ0) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateQuaternionQ0);
                    dataIndex += 2;
                }
                // Quaternion Q1
                else if ((i & Masks2.QuaternionQ1) > 0 && (mask2 & Masks2.QuaternionQ1) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateQuaternionQ1);
                    dataIndex += 2;
                }
                // Quaternion Q2
                else if ((i & Masks2.QuaternionQ2) > 0 && (mask2 & Masks2.QuaternionQ2) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateQuaternionQ2);
                    dataIndex += 2;
                }
                // Quaternion Q3
                else if ((i & Masks2.QuaternionQ3) > 0 && (mask2 & Masks2.QuaternionQ3) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateQuaternionQ3);
                    dataIndex += 2;
                }
                // Odometer X
                else if ((i & Masks2.OdometerX) > 0 && (mask2 & Masks2.OdometerX) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateOdometerX);
                    dataIndex += 2;
                }
                // Odometer Y
                else if ((i & Masks2.OdometerY) > 0 && (mask2 & Masks2.OdometerY) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateOdometerY);
                    dataIndex += 2;
                }
                // AccelOne
                else if ((i & Masks2.AccelOne) > 0 && (mask2 & Masks2.AccelOne) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateAccelOne);
                    dataIndex += 2;
                }
                // Velocity X
                else if ((i & Masks2.VelocityX) > 0 && (mask2 & Masks2.VelocityX) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateVelocityX);
                    dataIndex += 2;
                }
                // Velocity Y
                else if ((i & Masks2.VelocityY) > 0 && (mask2 & Masks2.VelocityY) > 0)
                {
                    ExtractAndUpdate(data, dataIndex, UpdateVelocityY);
                    dataIndex += 2;
                }

            }
            #endregion
        }

        private void ExtractAndUpdate(byte[] data, int dataIndex, Action<short> updateMethod)
        {
            if (updateMethod != null)
            {
                short value = data[dataIndex];
                value = (short)(value << 8);
                value += data[dataIndex + 1];
                updateMethod(value);
            }
        }


        #region Raw Accelerometer

        private void CheckRawAccelerometer()
        {
            if (_rawAccelerometer == null)
                _rawAccelerometer = new ThreeAxis();
        }

        private void UpdateRawAccelerometerX(short x)
        {
            CheckRawAccelerometer();

            _rawAccelerometer = new ThreeAxis(x, _rawAccelerometer.Y, _rawAccelerometer.Z);
        }

        private void UpdateRawAccelerometerY(short y)
        {
            CheckRawAccelerometer();

            _rawAccelerometer = new ThreeAxis(_rawAccelerometer.X, y , _rawAccelerometer.Z);
        }

        private void UpdateRawAccelerometerZ(short z)
        {
            CheckRawAccelerometer();

            _rawAccelerometer = new ThreeAxis(_rawAccelerometer.X, _rawAccelerometer.Y, z);
        }

        #endregion

        #region Filtered Accelerometer

        private void CheckFilteredAccelerometer()
        {
            if (_filteredAccelerometer == null)
                _filteredAccelerometer = new ThreeAxis();
        }

        private void UpdateFilteredAccelerometerX(short x)
        {
            CheckFilteredAccelerometer();

            _filteredAccelerometer = new ThreeAxis(x, _filteredAccelerometer.Y, _filteredAccelerometer.Z);
        }

        private void UpdateFilteredAccelerometerY(short y)
        {
            CheckFilteredAccelerometer();

            _filteredAccelerometer = new ThreeAxis(_filteredAccelerometer.X, y, _filteredAccelerometer.Z);
        }

        private void UpdateFilteredAccelerometerZ(short z)
        {
            CheckFilteredAccelerometer();

            _filteredAccelerometer = new ThreeAxis(_filteredAccelerometer.X, _filteredAccelerometer.Y, z);
        }

        #endregion

        #region IMU

        private void CheckFilteredIMU()
        {
            if (_filteredIMU == null)
                _filteredIMU = new IMU();
        }

        private void UpdateFilteredIMUPitch(short pitch)
        {
            CheckFilteredIMU();

            _filteredIMU = new IMU(pitch, _filteredIMU.Roll, _filteredIMU.Yaw);
        }

        private void UpdateFilteredIMURoll(short roll)
        {
            CheckFilteredIMU();

            _filteredIMU = new IMU(_filteredIMU.Pitch, roll, _filteredIMU.Yaw);
        }

        private void UpdateFilteredIMUYaw(short yaw)
        {
            CheckFilteredIMU();

            _filteredIMU = new IMU(_filteredIMU.Pitch, _filteredIMU.Roll, yaw);
        }

        #endregion

        #region Quaternion

        private void CheckQuaternion()
        {
            if (_quaternion == null)
                _quaternion = new Quaternion();
        }

        private void UpdateQuaternionQ0(short q0)
        {
            CheckQuaternion();

            _quaternion = new Quaternion(q0, _quaternion.Q1, _quaternion.Q2, _quaternion.Q3);
        }

        private void UpdateQuaternionQ1(short q1)
        {
            CheckQuaternion();

            _quaternion = new Quaternion(_quaternion.Q0, q1, _quaternion.Q2, _quaternion.Q3);
        }

        private void UpdateQuaternionQ2(short q2)
        {
            CheckQuaternion();

            _quaternion = new Quaternion(_quaternion.Q0, _quaternion.Q1, q2, _quaternion.Q3);
        }

        private void UpdateQuaternionQ3(short q3)
        {
            CheckQuaternion();

            _quaternion = new Quaternion(_quaternion.Q0, _quaternion.Q1, _quaternion.Q2, q3);
        }


        #endregion

        #region Odometer

        private void CheckOdometer()
        {
            if (_odometer == null)
                _odometer = new TwoAxis();
        }

        private void UpdateOdometerX(short x)
        {
            CheckOdometer();

            _odometer = new TwoAxis(x, _odometer.Y);
        }

        private void UpdateOdometerY(short y)
        {
            CheckOdometer();

            _odometer = new TwoAxis(_odometer.X, y);
        }

        #endregion

        #region AccelOne

        private void UpdateAccelOne(short accelOne)
        {
            _accelOne = accelOne;
        }

        #endregion

        #region Velocity

        private void CheckVelocity()
        {
            if (_velocity == null)
                _velocity = new TwoAxis();
        }

        private void UpdateVelocityX(short x)
        {
            CheckVelocity();

            _velocity = new TwoAxis(x, _velocity.Y);
        }

        private void UpdateVelocityY(short y)
        {
            CheckVelocity();

            _velocity = new TwoAxis(_velocity.X, y);
        }

        #endregion
    }
}
