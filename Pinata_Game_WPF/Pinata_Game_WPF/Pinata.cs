// Class: Pinata
// Michael Reeves.
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pinata_Game_WPF
{
    internal class Pinata
    {
        private const int MAX_ANGLE = 75;
        private const double NORMAL_INCREMENT_ANGLE = 1f;
        private const double FAST_INCREMENT_ANGLE = 2f;

        private Line myLine;
        private Ellipse myEllipse;

        private bool goingRight;
        private double currentAngle;
        private double incrementAngle;

        public Pinata(MainWindow window)
        {
            InitializeComponents(window);

            goingRight = false;
            currentAngle = 0;
            incrementAngle = NORMAL_INCREMENT_ANGLE;
        }

        private void InitializeComponents(MainWindow parentWindow)
        {
            // Create a Line Element
            myLine = parentWindow.myLine;
            myLine.Stroke = Brushes.Black;
            myLine.X1 = parentWindow.Width / 2;
            myLine.X2 = parentWindow.Width / 2;
            myLine.Y1 = 0;
            myLine.Y2 = myLine.Y1 + 100;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;

            // Create a Ellipse.
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

            // Adds the two elements to the canvas.
            //parentWindow.TheCanvas.Children.Add(myLine);
            parentWindow.TheCanvas.Children.Add(myEllipse);
        }

        /// <summary>
        /// Creates a ellipse object
        /// </summary>
        /// <param name="width">The width of the Ellipse.</param>
        /// <param name="height">The height of the Ellipse.</param>
        /// <param name="desiredCenterX">Desired center x point.</param>
        /// <param name="desiredCenterY">Desired center y point.</param>
        /// <returns></returns>
        private Ellipse CreateEllipse(double width, double height, double desiredCenterX, double desiredCenterY)
        {
            // Creates
            Ellipse ellipse = new Ellipse { Width = width, Height = height };

            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height + 100);

            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
        }

        public void Draw()
        {
            // If the direction is going right then subtract the incrementAngle from currentAngle.
            if (goingRight)
            {
                myLine.RenderTransform = new RotateTransform(currentAngle -= incrementAngle, myLine.X1, myLine.Y1);
            }
            // If the direction is going left then subtract the incrementAngle from currentAngle.
            else
            {
                myLine.RenderTransform = new RotateTransform(currentAngle += incrementAngle, myLine.X1, myLine.Y1);
            }

            // Creates a RotateTransform object based of the myLine.Render transform object.
            RotateTransform rotation = myLine.RenderTransform as RotateTransform;

            // Create two point objects p1 = beginning point of a line, while, p2 = end point of a line.
            Point p2 = rotation.Transform(new Point(myLine.X2, myLine.Y2));

            // Sets the center point of the ellipse to the p2 (end point of line).
            myEllipse.Margin = new Thickness(p2.X - (myEllipse.Width / 2), p2.Y - (myEllipse.Height / 2), 0, 0);

            // If the currentAngle is less than or equal to the negative MAX_ANGLE
            if (currentAngle <= -MAX_ANGLE)
            {
                goingRight = false;
                Console.WriteLine(currentAngle);
                Console.WriteLine("Line Angle : right");
            }
            // If the currentAngle is greater than or equal to the MAX_ANGLE
            else if (currentAngle >= MAX_ANGLE)
            {
                goingRight = true;
                Console.WriteLine(currentAngle);
                Console.WriteLine("Line Angle : left");
            }
        }
    }
}