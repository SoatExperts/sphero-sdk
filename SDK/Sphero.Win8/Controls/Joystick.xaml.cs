using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using Sphero.Internal;
using System.Diagnostics;
using Windows.UI.Input;
namespace Sphero.Controls
{
    public partial class Joystick : UserControl
    {
        private bool isStarted = false;
        private uint idPointer = 0;

        double lastX = 0;
        double lastY = 0;
        double newX = 0;
        double newY = 0;

        double angle = 0;
        double basicAngle = 0;
        double normalizedDistance = 0;

        public delegate void MovingHandler(JoystickMoveEventArgs e);
        public delegate void CalibratingHandler(JoystickCalibrationEventArgs e);

        public event MovingHandler Moving;
        public event MovingHandler Released;

        public event CalibratingHandler Calibrating;
        public event CalibratingHandler CalibrationReleased;

        private Point center;
        private bool mainStickkMoving = false;
        private bool calibrationStickkMoving = false;

        public Joystick()
        {
            InitializeComponent();
        }

        public void Start()
        {
            if (!isStarted)
            {
                isStarted = true;
                Window.Current.Content.PointerPressed += Window_PointerPressed;
                Window.Current.Content.PointerReleased += Window_PointerReleased;
                Window.Current.Content.PointerMoved += Window_PointerMoved;
                Window.Current.Content.PointerExited += Window_PointerReleased;
            }
        }

        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;
                Window.Current.Content.PointerPressed -= Window_PointerPressed;
                Window.Current.Content.PointerReleased -= Window_PointerReleased;
                Window.Current.Content.PointerMoved -= Window_PointerMoved;
                Window.Current.Content.PointerExited -= Window_PointerReleased;
            }
        }

        private void Refresh(Point pointerPosition, Point centerPosition)
        {
            newX = pointerPosition.X - (sensor.ActualWidth / 2);
            newY = pointerPosition.Y - (sensor.ActualHeight / 2);

            double distance = Math.Sqrt(Math.Pow((pointerPosition.X - centerPosition.X), 2) + Math.Pow((pointerPosition.Y - centerPosition.Y), 2));
            normalizedDistance = distance / (sensor.ActualWidth / 2);
            angle = Math.Atan2(pointerPosition.Y - centerPosition.Y, pointerPosition.X - centerPosition.X) * 180 / Math.PI;

            basicAngle = angle;
            if (basicAngle > -90)
            {
                basicAngle -= 90;
                if (basicAngle < 0)
                    basicAngle = 180 + 180 + basicAngle;
            }
            else
                basicAngle = 270 + basicAngle;

            if (angle > 0)
                angle += 90;
            else
            {
                angle = 270 + (180 + angle);
                if (angle >= 360)
                    angle -= 360;
            }

            double radAngle = angle * Math.PI / 180.0f;
            newX = MathHelper.Clamp((float)newX, (float)-(sensor.ActualWidth * Math.Abs(Math.Sin(radAngle)) / 2), (float)(sensor.ActualWidth * Math.Abs(Math.Sin(radAngle)) / 2));
            newY = MathHelper.Clamp((float)newY, (float)-(sensor.ActualHeight * Math.Abs(Math.Cos(radAngle)) / 2), (float)(sensor.ActualHeight * Math.Abs(Math.Cos(radAngle)) / 2));

            double normalizedX = newX / (sensor.ActualWidth / 2.0f);
            double normalizedY = newY / (sensor.ActualHeight / 2.0f);

           // basicAngle = Math.Atan((double)((double)normalizedY / (double)normalizedX)) * 180.0f / Math.PI;

            
        }

        private void MoveJoystick(double moveX, double moveY)
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames animationFirstX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animationFirstY = new DoubleAnimationUsingKeyFrames();

            stickContainer.RenderTransform = new CompositeTransform();

            Storyboard.SetTarget(animationFirstX, stickContainer.RenderTransform);
            Storyboard.SetTargetProperty(animationFirstX, "(CompositeTransform.TranslateX)");
            
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastX });
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveX });

            Storyboard.SetTarget(animationFirstY, stickContainer.RenderTransform);
            Storyboard.SetTargetProperty(animationFirstY, "(CompositeTransform.TranslateY)");
            
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastY });
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveY });

            sb.Children.Add(animationFirstX);
            sb.Children.Add(animationFirstY);
            sb.Begin();

            lastX = moveX;
            lastY = moveY;

            //Debug.WriteLine("Move Joystick to: " + moveX + ", " + moveY);
        }

        private void ReplaceCalibrationStick()
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames rotationAnimation = new DoubleAnimationUsingKeyFrames();

            Storyboard.SetTarget(rotationAnimation, rotationTransform);
            Storyboard.SetTargetProperty(rotationAnimation, "(CompositeTransform.Rotation)");

            rotationAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = rotationTransform.Rotation });
            rotationAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = 135 });

            sb.Children.Add(rotationAnimation);
            sb.Begin();
        }

        private void Window_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (idPointer == 0)
            {
                PointerPoint point = e.GetCurrentPoint(LayoutRoot);
                PointerPoint point_Calibration = e.GetCurrentPoint(CalibrationPath);
                center = new Point(sensor.ActualWidth / 2, sensor.ActualHeight / 2);
                
                if (point_Calibration.Position.X > 0 &&
                    point_Calibration.Position.X < CalibrationPath.ActualWidth &&
                    point_Calibration.Position.Y > 0 &&
                    point_Calibration.Position.Y < CalibrationPath.ActualHeight)
                {
                    // Calibrage
                    calibrationStickkMoving = true;
                    mainStickkMoving = false;

                    idPointer = e.Pointer.PointerId;
                    Refresh(point.Position, center);
                }
                else if (point.Position.X > 0 && point.Position.X < LayoutRoot.ActualWidth && point.Position.Y > 0 && point.Position.Y < LayoutRoot.ActualHeight)
                {
                    calibrationStickkMoving = false;
                    mainStickkMoving = true;

                    // Stick principal
                    idPointer = e.Pointer.PointerId;

                    Refresh(point.Position, center);

                    MoveJoystick(newX, newY);

                    JoystickMoveEventArgs args = new JoystickMoveEventArgs
                    {
                        Angle = (int)angle,
                        Speed = (float)normalizedDistance
                    };

                    if(Moving != null)
                        Moving(args);

                    //Debug.WriteLine("Started: " + point.Position.X + ", " + point.Position.Y);
                }
            }
        }

        private void Window_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (idPointer == e.Pointer.PointerId)
            {

                //PointerPoint point = e.GetCurrentPoint(LayoutRoot);
                //Debug.WriteLine("Released: " + point.Position.X + ", " + point.Position.Y);

                if (calibrationStickkMoving)
                {
                    JoystickCalibrationEventArgs args = new JoystickCalibrationEventArgs
                    {
                        Angle = (int)basicAngle
                    };

                    if (CalibrationReleased != null)
                        CalibrationReleased(args);
                }


                if (mainStickkMoving)
                {
                    JoystickMoveEventArgs args = new JoystickMoveEventArgs
                        {
                            Angle = (int)angle,
                            Speed = (float)normalizedDistance
                        };

                    if (Released != null)
                        Released(args);
                }

                newX = 0;
                newY = 0;
                angle = 0;
                normalizedDistance = 0;

                MoveJoystick(newX, newY);

                

                if(calibrationStickkMoving)
                {
                    ReplaceCalibrationStick();
                }

                // Reinit calibration rotation
                rotationTransform.Rotation = 135;



                calibrationStickkMoving = false;
                mainStickkMoving = false;
                idPointer = 0;
            }
        }

        private void Window_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (idPointer == e.Pointer.PointerId)
            {
                PointerPoint point = e.GetCurrentPoint(mainStickContainer);
                Refresh(point.Position, center);

                Debug.WriteLine("X : {0}, Y : {1}", point.Position.X, point.Position.Y);

                if (mainStickkMoving)
                {
                    

                    MoveJoystick(newX, newY);

                    JoystickMoveEventArgs args = new JoystickMoveEventArgs
                    {
                        Angle = (int)angle,
                        Speed = (float)normalizedDistance
                    };

                    if (Moving != null)
                        Moving(args);
                }
                else if(calibrationStickkMoving)
                {
                    rotationTransform.Rotation = basicAngle;

                    JoystickCalibrationEventArgs args = new JoystickCalibrationEventArgs
                    {
                        Angle = (int)basicAngle
                    };

                    if (Calibrating != null)
                        Calibrating(args);

                    //Debug.WriteLine("Angle : {0}", (int)basicAngle);
                }

            }
        }
   }
}
