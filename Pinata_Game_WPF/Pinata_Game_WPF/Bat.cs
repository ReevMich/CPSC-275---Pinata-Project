using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pinata_Game_WPF
{
    internal class Bat
    {
        private Line eLine;
        private Bat batStick;
        private Ellipse createdEllipse;
        private MainWindow parWindow;
        private double angle;
        private double length;
        private Pen pen;
        private Pen eraser;
        private double ShiftAmountBat;
        private Boolean backwardBat = false;
        private bool shiftleft = true;
        private float incAngle = 1.5f;
        private float i = 0;

        public Bat(MainWindow window)
        {
            this.parWindow = window;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            eLine = new Line();
            eLine.Stroke = System.Windows.Media.Brushes.Red;
            eLine.X1 = parWindow.Width + length * Math.Cos(angle / 2);
            eLine.X2 = parWindow.Width + length * Math.Cos(angle / 2);
            eLine.Y1 = 0;
            eLine.Y2 = eLine.Y1 + 100;
            eLine.HorizontalAlignment = HorizontalAlignment.Left;
            eLine.VerticalAlignment = VerticalAlignment.Center;
            eLine.StrokeThickness = 2;
        }

        // MIKE: I changed the name of this to Draw from drawBat. Its better to be consistent.
        public void Draw()
        {
            if (shiftleft)
            {
                eLine.RenderTransform = new RotateTransform(i += incAngle, eLine.X1, eLine.Y1);
            }
            else
            {
                eLine.RenderTransform = new RotateTransform(i -= incAngle, eLine.X1, eLine.Y1);
            }

            RotateTransform rot = eLine.RenderTransform as RotateTransform;
            Point p1 = rot.Transform(new Point(eLine.X1, eLine.Y1));
            Point p2 = rot.Transform(new Point(eLine.X1, eLine.Y1));

            createdEllipse.Margin = new Thickness(p2.X - (createdEllipse.Width / 2), p2.Y - (createdEllipse.Height / 2), 0, 0);

            if (i == -57)
            {
                shiftleft = false;
                Console.WriteLine("Line Angle : left");
            }
            else if (i == 57)
            {
                shiftleft = true;
                Console.WriteLine("Line Angle : Right");
            }
        }
    }
}