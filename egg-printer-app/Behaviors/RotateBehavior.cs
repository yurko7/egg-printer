using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using DevExpress.Mvvm.UI.Interactivity;

namespace YuKu.EggPrinter.Behaviors
{
    public sealed class RotateBehavior : Behavior<UIElement3D>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += AssociatedObjectOnMouseLeftButtonDown;
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
            AssociatedObject.MouseLeftButtonDown -= AssociatedObjectOnMouseLeftButtonDown;
            base.OnDetaching();
        }

        private void AssociatedObjectOnMouseLeftButtonDown(Object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _previousPosition = mouseButtonEventArgs.GetPosition(AssociatedObject);
        }

        private void AssociatedObjectOnMouseMove(Object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            Point position = mouseEventArgs.GetPosition(AssociatedObject);
            _angleX += (position.Y - _previousPosition.Y) % 360;
            _angleY += (position.X - _previousPosition.X) % 360;
            AssociatedObject.Transform = new Transform3DGroup
            {
                Children =
                {
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), _angleX)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _angleY))
                }
            };
            _previousPosition = position;
        }

        private Point _previousPosition;
        private Double _angleX;
        private Double _angleY;
    }
}
