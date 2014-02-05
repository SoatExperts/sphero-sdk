using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Sphero.Internal;

namespace Sphero.Controls
{
    public partial class Joystick : UserControl
    {

        private bool isStarted = false;
        private int idPointer = -1;

        double lastX = 0;
        double lastY = 0;
        double newX = 0;
        double newY = 0;

        double angle = 0;
        double basicAngle = 0;
        double normalizedDistance = 0;

        public delegate void MovingHandler(object sender, JoystickMoveEventArgs e);
        public delegate void CalibratingHandler(object sender, JoystickCalibrationEventArgs e);

        public event MovingHandler Moving;
        public event MovingHandler Released;

        public event CalibratingHandler Calibrating;
        public event CalibratingHandler CalibrationReleased;

        private bool mainStickMoving = false;
        private bool calibrationStickMoving = false;

        public Joystick()
        {
            InitializeComponent();
        }

        public void Start()
        {
            if (!isStarted)
            {
                isStarted = true;
                Touch.FrameReported += Touch_FrameReported;
            }
        }

        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;
                Touch.FrameReported -= Touch_FrameReported;
            }
        }

       

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            if (Visibility == System.Windows.Visibility.Collapsed)
                return;

            TouchPointCollection points = e.GetTouchPoints(this);
            
            System.Windows.Point center = new System.Windows.Point(joystick.ActualWidth / 2, joystick.ActualHeight / 2);
            for (int i = 0; i < points.Count; i++)
            {
                TouchPoint point = points[i];

                // If Pointer enter on the main stick
                if (IsEnabled && point.Action == TouchAction.Down && XamlHelper.IsControlChildOf(point.TouchDevice.DirectlyOver, mainStickContainer) && idPointer == -1)
                {
                    #region Pointer enter on MainStick
 
                    // Lock stick type
                    calibrationStickMoving = false;
                    mainStickMoving = true;

                    // Store TouchDeviceId
                    idPointer = point.TouchDevice.Id;

                    // Updating sticks data
                    Refresh(point.Position, center);

                    // Update mainStick position
                    MoveJoystick(newX, newY);

                    // Prepare event args
                    JoystickMoveEventArgs args = new JoystickMoveEventArgs
                    {
                        Angle = (int)angle,
                        Speed = (float)normalizedDistance
                    };

                    // Fire event
                    if (Moving != null)
                        Moving(this, args);

                    #endregion
                }
                // Else if pointer enter on calibrationstick
                else if (IsEnabled && point.Action == TouchAction.Down && (XamlHelper.IsControlChildOf(point.TouchDevice.DirectlyOver, CalibrationPath) || XamlHelper.IsControlChildOf(point.TouchDevice.DirectlyOver, CalibrationTxt)) && idPointer == -1)
                {
                    // Lock stick type
                    calibrationStickMoving = true;
                    mainStickMoving = false;

                    // Store TouchDeviceId
                    idPointer = point.TouchDevice.Id;

                    // Updating sticks data
                    Refresh(point.Position, center);
                }
                // Else if the pointer moving
                else if (point.TouchDevice.Id == idPointer && point.Action == TouchAction.Move)
                {
                    // Updating sticks data
                    Refresh(point.Position, center);

                    // If calibrating
                    if (calibrationStickMoving)
                    {
                        // Update angle
                        rotationTransform.Rotation = basicAngle;

                        // Fire event
                        if (Calibrating != null)
                        {
                            JoystickCalibrationEventArgs args = new JoystickCalibrationEventArgs { Angle = (int)basicAngle };
                            Calibrating(this, args);
                        }

                    }
                    else if (mainStickMoving)
                    {
                        // Update main stick position
                        MoveJoystick(newX, newY);

                        // Fire event
                        if (Moving != null)
                        {
                            JoystickMoveEventArgs args = new JoystickMoveEventArgs
                            {
                                Angle = (int)angle,
                                Speed = (float)normalizedDistance
                            };

                            Moving(this, args);
                        }
                    }
                }
                // On pointer released
                else if (point.TouchDevice.Id == idPointer && point.Action == TouchAction.Up)
                {
                    // release the pointer Id
                    idPointer = -1;

                    
                    if (mainStickMoving)
                    {
                        // Fire event
                        if (Released != null)
                        {
                            JoystickMoveEventArgs args = new JoystickMoveEventArgs
                            {
                                Angle = (int)angle,
                                Speed = (float)normalizedDistance
                            };

                            Released(this, args);
                        }
                    }

                    if(calibrationStickMoving)
                    {
                        // Fire event
                        if (CalibrationReleased != null)
                        {
                            JoystickCalibrationEventArgs args = new JoystickCalibrationEventArgs { Angle = (int)basicAngle };
                            CalibrationReleased(this, args);
                        }
                    }

                    // Reinit data
                    newX = 0;
                    newY = 0;
                    angle = 0;
                    normalizedDistance = 0;

                    // Update main stick position
                    MoveJoystick(newX, newY);
                    
                    // Reinit calibration rotation
                    rotationTransform.Rotation = 135;

                    // Reinit flags
                    calibrationStickMoving = false;
                    mainStickMoving = false;
                }
            }
        }


        /// <summary>
        /// Compute data with pointer position
        /// </summary>
        /// <param name="pointerPosition"></param>
        /// <param name="centerPosition"></param>
        private void Refresh(System.Windows.Point pointerPosition, System.Windows.Point centerPosition)
        {
            // Position
            newX = pointerPosition.X - (joystick.ActualWidth / 2);
            newY = pointerPosition.Y - (joystick.ActualHeight / 2);

            // Compute data
            double distance = Math.Sqrt(Math.Pow((pointerPosition.X - centerPosition.X), 2) + Math.Pow((pointerPosition.Y - centerPosition.Y), 2));
            normalizedDistance = distance / (sensor.ActualWidth / 2);
            angle = Math.Atan2(pointerPosition.Y - centerPosition.Y, pointerPosition.X - centerPosition.X) * 180 / Math.PI;

            // Update basic angle (used for calibration)
            basicAngle = angle;
            if (basicAngle > -90)
            {
                basicAngle -= 90;
                if (basicAngle < 0)
                    basicAngle = 180 + 180 + basicAngle;
            }
            else
                basicAngle = 270 + basicAngle;

            // Update angle
            if (angle > 0)
                angle += 90;
            else
            {
                angle = 270 + (180 + angle);
                if (angle >= 360)
                    angle -= 360;
            }

            // Update position
            double radAngle = angle * Math.PI / 180.0f;
            newX = MathHelper.Clamp((float)newX, (float)-(sensor.ActualWidth * Math.Abs(Math.Sin(radAngle)) / 2), (float)(sensor.ActualWidth * Math.Abs(Math.Sin(radAngle)) / 2));
            newY = MathHelper.Clamp((float)newY, (float)-(sensor.ActualHeight * Math.Abs(Math.Cos(radAngle)) / 2), (float)(sensor.ActualHeight * Math.Abs(Math.Cos(radAngle)) / 2));
        }

        /// <summary>
        /// Move de main stick to the position
        /// </summary>
        /// <param name="moveX"></param>
        /// <param name="moveY"></param>
        private void MoveJoystick(double moveX, double moveY)
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames animationFirstX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animationFirstY = new DoubleAnimationUsingKeyFrames();

            stickContainer.RenderTransform = new CompositeTransform();

            Storyboard.SetTargetProperty(animationFirstX, new PropertyPath(CompositeTransform.TranslateXProperty));
            Storyboard.SetTarget(animationFirstX, stickContainer.RenderTransform);
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastX });
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveX });


            Storyboard.SetTargetProperty(animationFirstY, new PropertyPath(CompositeTransform.TranslateYProperty));
            Storyboard.SetTarget(animationFirstY, stickContainer.RenderTransform);
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastY });
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveY });

            sb.Children.Add(animationFirstX);
            sb.Children.Add(animationFirstY);
            sb.Begin();

            lastX = moveX;
            lastY = moveY;
        }

        private void ReplaceCalibrationStick()
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames rotationAnimation = new DoubleAnimationUsingKeyFrames();

            Storyboard.SetTarget(rotationAnimation, rotationTransform);
            Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath(CompositeTransform.RotationProperty));

            rotationAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = rotationTransform.Rotation });
            rotationAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = 135 });

            sb.Children.Add(rotationAnimation);
            sb.Begin();
        }
   }
}
