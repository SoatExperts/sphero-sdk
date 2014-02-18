using Sphero.Collisions;
using Sphero.Communication;
using Sphero.Core;
using Sphero.Internal;
using Sphero.Locator;
using Sphero.Macros;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Devices
{
    /// <summary>
    /// Sphero virtual device
    /// </summary>
    public class SpheroDevice
    {
        #region Fields

        // Handle connection with the physical device
        private SpheroConnection _connection;
        
        private byte[] _password;

        private uint _mask;

        private uint _mask2;

        #endregion

        public uint Mask { get { return this._mask; } }

        public uint Mask2 { get { return this._mask2; } }

        #region Constructor

        /// <summary>
        /// Create an new sphero with a connection
        /// </summary>
        /// <param name="connection">Current connection to the sphero</param>
        public SpheroDevice(SpheroConnection connection)
        {
            this._connection = connection;
            this._connection.SetSphero(this);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvlpercent"></param>
        public delegate void XpNotificationHandler(int lvlpercent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boostPercent"></param>
        public delegate void BoostNotificationHandler(int boostPercent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public delegate void SensorDataNotificationHandler(SensorData data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public delegate void CollisionDetectedHandler(CollisionData data);

        /// <summary>
        /// 
        /// </summary>
        public event XpNotificationHandler XpNotification;

        public event BoostNotificationHandler BoostNotification;

        public event SensorDataNotificationHandler SensorDataNotification;

        public event CollisionDetectedHandler CollisionDetected;

        #region Public Method

        /// <summary>
        /// This allows the smartphone client to adjust the orientation of Sphero by commanding a new reference heading in degrees, which ranges from 0 to 359. You will see the ball respond immediately to this command if stabilization is enabled.  
        /// </summary>
        /// <param name="headingAngle">Heading in degrees (from 0 to 359)</param>
        public void SetHeading(int headingAngle)
        {
            headingAngle = MathHelper.Clamp(headingAngle, 0, 359);
            byte[] headingAngleArray = ByteHelper.IntegerToBytes(MathHelper.Clamp(headingAngle, 0, 359));

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_HEADING, headingAngleArray);
            _connection.SendCommand(command);
        }

        public void StabilizationON()
        {

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_STABILIZATION, new byte[] { 0x01 });
            _connection.SendCommand(command);
        }

        public void StabilizationOFF()
        {

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_STABILIZATION, new byte[] { 0x00 });
            _connection.SendCommand(command);
        }


        /// <summary>
        /// Set the back LED intensity
        /// </summary>
        /// <param name="intensity">Intensity of the back LED (from 0 to 255)</param>
        public void SetBackLED(int intensity)
        {
            intensity = MathHelper.Clamp(intensity, 0 , 255);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_BACK_LED, new byte[] { (byte)intensity });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Set the back LED intensity
        /// </summary>
        /// <param name="intensity">Intensity of the back LED (from 0 to 1.0f)</param>
        public void SetBackLED(float intensity)
        {
            intensity = MathHelper.Clamp(intensity, 0, 1.0f);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_BACK_LED, new byte[] { (byte)MathHelper.Lerp(0, 255, intensity) });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Set the RGB color of the Front LED
        /// </summary>
        /// <param name="red">Red part of the color (from 0 to 255)</param>
        /// <param name="green">Green part of the color (from 0 to 255)</param>
        /// <param name="blue">Blue part of the color (from 0 to 255)</param>
        public void SetRGBLED(int red, int green, int blue)
        {
            red = MathHelper.Clamp(red, 0, 255);
            green = MathHelper.Clamp(green, 0, 255);
            blue = MathHelper.Clamp(blue, 0, 255);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_RBG_LED, new byte[] { (byte)red, (byte)green, (byte)blue });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Set the RGB color of the Front LED
        /// </summary>
        /// <param name="red">Red part of the color (from 0 to 1.0f)</param>
        /// <param name="green">Green part of the color (from 0 to 1.0f)</param>
        /// <param name="blue">Blue part of the color (from 0 to 1.0f)</param>
        public void SetRGBLED(float red, float green, float blue)
        {
            red = MathHelper.Clamp(red, 0, 1.0f);
            green = MathHelper.Clamp(green, 0, 1.0f);
            blue = MathHelper.Clamp(blue, 0, 1.0f);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_RBG_LED, new byte[] { (byte)MathHelper.Lerp(0, 255, red), (byte)MathHelper.Lerp(0, 255, green), (byte)MathHelper.Lerp(0, 255, blue) });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Roll along sphero in angle direction with speed velocity
        /// </summary>
        /// <param name="angle">Direction the sphero will roll (from 0 to 359)</param>
        /// <param name="speed">Sphero speed (from 0 to 255)</param>
        public void Roll(int angle, int speed)
        {
            byte[] byteAngle = ByteHelper.IntegerToBytes((int)MathHelper.Clamp(angle, 0, 359));
            speed = (int)MathHelper.Clamp(speed, 0, 255);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.ROLL, new byte[] { (byte)speed, byteAngle[0], byteAngle[1], 1 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Roll along sphero in angle direction with speed velocity
        /// </summary>
        /// <param name="angle">Direction the sphero will roll (from 0 to 359)</param>
        /// <param name="speed">Sphero speed (from 0 to 1.0f)</param>
        public void Roll(int angle, float speed)
        {
            byte[] byteAngle = ByteHelper.IntegerToBytes((int)MathHelper.Clamp(angle, 0, 359));
            speed = MathHelper.Clamp(speed, 0, 1.0f);

            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.ROLL, new byte[] { (byte)MathHelper.Lerp(0, 255, speed), byteAngle[0], byteAngle[1], 1 });
            _connection.SendCommand(command);
        }

        /// <summary>
        ///  This executes the boost macro from within the SSB
        /// </summary>
        public void BoostON()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.BOOST, new byte[] { 0x01 });
            _connection.SendCommand(command);
        }

        /// <summary>
        ///  This stop the boost macro from within the SSB
        /// </summary>
        public void BoostOFF()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.BOOST, new byte[] { 0x00 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Turn on soul block related asynchronous messages
        /// </summary>
        public void EnableSSB()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.ENABLE_SSB, new byte[] { 0x01 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Turn off soul block related asynchronous messages
        /// </summary>
        public void DisableSSB()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.ENABLE_SSB, new byte[] { 0x00 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Protected Sphero commands require a password and this returns the seed to you
        /// </summary>
        /// <param name="passwordCallback">Callback used to handle the response of the SpheroDevice</param>
        public void GetPassword(Action<byte[]> passwordCallback)
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.GET_PASSWORD_SEED, new byte[] { 0x00 });
            _connection.SendCommand(command, response =>
            {
                if(passwordCallback != null)
                {
                    _password = response.Data;
                    passwordCallback(response.Data);
                }
            });
        }

        /// <summary>
        /// This command retrieves Sphero's Soul Block
        /// </summary>
        public void GetSSB()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.GET_SSB, null);
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Assigns the normal mode of Sphero
        /// </summary>
        public void NormalMode()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_DEVICE_MODE, new byte[] { (byte)DeviceMode.NormalMode });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Assigns the user hack mode of Sphero
        /// </summary>
        public void UserHackMode()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_DEVICE_MODE, new byte[] { (byte)DeviceMode.UserHackMode });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Add some minutes experience
        /// </summary>
        /// <param name="minutes">minutes to add</param>
        /// <param name="toNextLevelCallback">Callback used to handle the response of the SpheroDevice</param>
        public void AddXP(int minutes, Action<byte[]> toNextLevelCallback)
        {
            if (_password == null)
                return;

            if (_password.Length != 4)
                return;

            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.ADD_XP, new byte[] { _password[0], _password[1], _password[2], _password[3], (byte)minutes });

            _connection.SendCommand(command, response =>
            {
                // TODO : handle response
                Debug.WriteLine("XP : ", BitConverter.ToString(response.ToArray()));

            });
        }

        /// <summary>
        /// Define data streaming notifications
        /// </summary>
        public void SetDataStreaming(int N, int M, uint mask, uint mask2)
        {
            if (M > 1)
                throw new NotImplementedException("Not implemented for M > 1");

            byte[] nArray = ByteHelper.IntegerToBytes(N);
            byte[] mArray = ByteHelper.IntegerToBytes(M);

            _mask = mask;
            _mask2 = mask2;

            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_DATA_STREAMING, new byte[] { nArray[0], nArray[1], // N
                                                                                                                                  mArray[0], mArray[1], // M
                                                                                                                                  (byte)(_mask >> 24), (byte)(_mask >> 16), (byte)(_mask >> 8), (byte)_mask, // MASK
                                                                                                                                  0x00,
                                                                                                                                  (byte)(_mask2 >> 24), (byte)(_mask2 >> 16), (byte)(_mask2 >> 8), (byte)_mask2});
             _connection.SendCommand(command);
        }


        /// <summary>
        /// Stop data streaming notifications
        /// </summary>
        public void StopDataStreaming()
        {
            byte[] N = ByteHelper.IntegerToBytes(20);
            byte[] M = ByteHelper.IntegerToBytes(1);


            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.SET_DATA_STREAMING, new byte[] { 0x00, 0x00, // N
                                                                                                                                  0x00, 0x00, // M
                                                                                                                                  0x00, 0x00, 0x00, 0x00, // MASK
                                                                                                                                  0x00,
                                                                                                                                  0x00, 0x00, 0x00, 0x00});
            _connection.SendCommand(command);
        }

        public void RequestFactoryConfigurationBlock()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.GET_CONFIGURATION_BLOCK, new byte[] { 0x00 });
            _connection.SendCommand(command);
        }

        public void RequestUserConfigurationBlock()
        {
            // Sending command
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.GET_CONFIGURATION_BLOCK, new byte[] { 0x01 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// This attempts to execute the specified macro.
        /// </summary>
        /// <param name="macroId">Id of the macro you want to execute</param>
        public void RunMacro(int macroId, Action<MessageResponseCode> callback)
        {
            macroId = (int)MathHelper.Clamp(macroId, 1, 255);

            // Sending command
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.RUN_MACRO, new byte[] { (byte)macroId });
            _connection.SendCommand(command, response =>
            {
                if (callback != null)
                {
                    callback(response.ResponseCode);
                }
            });
        }

        /// <summary>
        /// This attempts to execute the temporary macro.
        /// </summary>
        public void RunTemporaryMacro(Action<MessageResponseCode> callback)
        {
            RunMacro((int)MacroType.Temporary, callback);
        }

        /// <summary>
        /// This stores the attached macro definition into the persistent store for later execution. 
        /// This command can be sent even if other macros are executing. 
        /// You will receive a failure response if you attempt to send an ID number in the System Macro range, 255 for the Temp Macro and ID of an existing user macro in the storage block. 
        /// As with all macros, the longest definition that can be sent is 254 bytes (thus requiring DLEN to be FFh). 
        /// A special case of this command is to start and continue execution of the Stream Macro, ID 254.
        /// If a Temporary Macro is running it will be terminated and the Stream Macro will begin. If a Stream Macro is already running, this chunk will be appended (if there is room). 
        /// Stream Macros terminate via Abort or with a special END code. Refer to the Sphero Macro documentation for more detail.
        /// </summary>
        /// <param name="macro">Macro to save permanently</param>
        public void SaveMacro(Macro macro, Action<MessageResponseCode> callback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.SAVE_MACRO, macro.ToArray());
            _connection.SendCommand(command, response =>
            {
                if (callback != null)
                {
                    callback(response.ResponseCode);
                }
            });
        }

        /// <summary>
        /// This stores the attached macro definition into the temporary RAM buffer for later execution.
        /// </summary>
        /// <param name="macro">Macro to save temporary</param>
        public void SaveTemporaryMacro(Macro macro, Action<MessageResponseCode> callback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.SAVE_TEMPORARY_MACRO, macro.ToArray());
            _connection.SendCommand(command, response =>
            {
                if(callback != null)
                {
                    callback(response.ResponseCode);
                }
            });
        }

        /// <summary>
        /// This terminates any running macro and reinitializes the macro system. The table of any persistent user macros is cleared.
        /// </summary>
        public void ReinitMacroExecutive(Action<MessageResponseCode> callback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.REINIT_MACRO, null);
            _connection.SendCommand(command, response =>
            {
                if(callback != null)
                {
                    callback(response.ResponseCode);
                }
            });
        }

        /// <summary>
        /// This command aborts any executing macro and returns both its ID code and the command number currently in process. 
        /// An exception is a System Macro that is executing with the UNKILLABLE flag set. 
        /// A normal return code indicates the ID Code of the aborted macro as well as the command number at which execution was stopped. 
        /// A return ID code of 00h indicates that no macro was running and an ID code with FFFFh as the CmdNum that the macro was unkillable.
        /// </summary>
        /// <param name="statusCallback">Callback used to handle the response of the SpheroDevice</param>
        public void AbortMacro(Action<MacroStatus> statusCallback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.SPHERO, CommandIDs.Sphero.ABORT_MACRO, null);
            _connection.SendCommand(command, response =>
            {
                if(statusCallback != null)
                {
                    MacroStatus ms = new MacroStatus(response.Data);
                    statusCallback(ms);
                }
            });
        }

        /// <summary>
        /// Sphero contains a powerful analysis function to filter accelerometer data in order to detect collisions. 
        /// Because this is a great example of a high-level concept that humans excel and – but robots do not – a number of parameters control the behavior.
        /// When a collision is detected an asynchronous message is generated to the client .
        /// </summary>
        /// <param name="method">Detection method type to use</param>
        /// <param name="xThreshold">Threshold for the X (left/right) axe of Sphero. A value of 00h disables the contribution of that axis</param>
        /// <param name="xSpeed">Speed value for the X axe. This setting is ranged by the speed, then added to Xt to generate the final threshold value.</param>
        /// <param name="yThreshold">Threshold for the Y (front/back) axe of Sphero. A value of 00h disables the contribution of that axis</param>
        /// <param name="ySpeed">Speed value for the YX axe. This setting is ranged by the speed, then added to Yt to generate the final threshold value.</param>
        /// <param name="dead">Post-collision dead time to prevent retriggering; specified in 10ms increments.</param>
        public void ConfigureCollisionDetection(CollisionMethod method, byte xThreshold, byte xSpeed, byte yThreshold, byte ySpeed, byte dead)
        {
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.SPHERO, CommandIDs.Sphero.CONFIGURE_COLLISION_DETECTION, new byte[] { (byte)method, xThreshold, xSpeed, yThreshold, ySpeed, dead });
            _connection.SendCommand(command);
        }

        #endregion

        #region Internal Method
        internal void RaiseXpNotification(int xpValue)
        {
            if (XpNotification != null)
            {
                XpNotification((int)(((float)xpValue / 255.0f) * 100.0f));
            }
        }

        internal void RaiseBoostNotification(int boostValue)
        {
            if (BoostNotification != null)
            {
                BoostNotification((int)(((float)boostValue / 255.0f) * 100.0f));
            }
        }

        internal void RaiseSensorDataNotification(SensorData data)
        {


            if(SensorDataNotification != null)
            {
                SensorDataNotification(data);
            }

       }

        internal void RaiseCollisionDetected(CollisionData data)
        {
            if (CollisionDetected != null)
            {
                CollisionDetected(data);
            }

        }
        #endregion

        
    }
}
