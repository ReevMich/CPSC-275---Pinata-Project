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
    internal class Pinata
    {
        private Line myLine;
        private Ellipse myEllipse;
        private MainWindow parentWindow;
        private bool goingRight = true;
        private float i = 0;
        private float incrementAngle = 1.5f;

        public Pinata(MainWindow window)
        {
            this.parentWindow = window;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Add a Line Element
            myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = parentWindow.Width / 2;
            myLine.X2 = parentWindow.Width / 2;
            myLine.Y1 = 0;
            myLine.Y2 = myLine.Y1 + 100;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;

            // Create a red Ellipse.
            myEllipse = CreateEllipse(25, 25, myLine.X2, myLine.Y2);

            // Create a SolidColorBrush with a red color to fill the
            // Ellipse with.
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values.
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = Color.FromArgb(255, 0, 0, 0);

            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 25;
            myEllipse.Height = 25;

            double left = myLine.X2 - (parentWindow.Width / 2);
            double top = myLine.Y2 - (parentWindow.Height / 2);

            parentWindow.TheCanvas.Children.Add(myLine);
            parentWindow.TheCanvas.Children.Add(myEllipse);
        }

        private Ellipse CreateEllipse(double width, double height, double desiredCenterX, double desiredCenterY)
        {
            Ellipse ellipse = new Ellipse { Width = width, Height = height };

            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);

            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
        }

        public void Draw()
        {
            if (goingRight)
            {
                myLine.RenderTransform = new RotateTransform(i -= incrementAngle, myLine.X1, myLine.Y1);
            }
            else
            {
                myLine.RenderTransform = new RotateTransform(i += incrementAngle, myLine.X1, myLine.Y1);
            }

            RotateTransform rotation = myLine.RenderTransform as RotateTransform;
            Point p1 = rotation.Transform(new Point(myLine.X1, myLine.Y1));
            Point p2 = rotation.Transform(new Point(myLine.X2, myLine.Y2));

            myEllipse.Margin = new Thickness(p2.X - (myEllipse.Width / 2), p2.Y - (myEllipse.Height / 2), 0, 0);

            if (i == -57)
            {
                goingRight = false;
                Console.WriteLine("Line Angle : right");
            }
            else if (i == 57)
            {
                goingRight = true;
                Console.WriteLine("Line Angle : left");
            }
        }
    }
}