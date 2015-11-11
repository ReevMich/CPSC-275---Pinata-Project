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
        Bat batStick;
        private Ellipse createdEllipse;
        private MainWindow parWindow;
        private double angle;
        private double length;
        private Pen pen;
        private Pen eraser;
        double ShiftAmountBat;
        Boolean backwardBat = false;
        private bool  shiftleft = true;
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

            createdEllipse = EllipseCreation(6, 6, eLine.X2, eLine.Y2);

            SolidColorBrush createdSolidColorBrush = new SolidColorBrush();

            createdSolidColorBrush.Color = Color.FromArgb(255, 0, 0, 0);
            createdEllipse.Width = 8;
            createdEllipse.Height = 25;

            double left = eLine.X2 - (parWindow.Width + length * Math.Cos(angle / 2));
            double top = eLine.Y2 - (parWindow.Height + length * Math.Cos(angle / 2));

            parWindow.TheCanvas.Children.Add(eLine);
            parWindow.TheCanvas.Children.Add(createdEllipse);
        }
           
        private Ellipse EllipseCreation(double width, double height, double centerX, double centerY)
        {
            Ellipse ellipse = new Ellipse { Width = width, Height = height };

            double left = centerX + length * Math.Cos(angle / 2);
            double top = centerY + length * Math.Cos(angle / 2);

            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
        }

        public void drawBat()
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
