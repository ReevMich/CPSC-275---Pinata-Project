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
        private const double NORMAL_INCREMENT_ANGLE = 1.2f;
        private const double FAST_INCREMENT_ANGLE = 3f;

        private Line myLine;
        private Ellipse myEllipse;

        private bool goingRight;
        private bool recentlyHit;
        private double currentAngle;
        private double incrementAngle;

        private int numberOfHits = 0;

        public int NumberOfHits
        {
            get { return numberOfHits; }
        }

        public Pinata(MainWindow window)
        {
            InitializeComponents(window);

            goingRight = false;
            recentlyHit = false;
            currentAngle = 0;
            incrementAngle = NORMAL_INCREMENT_ANGLE;
        }

        private void InitializeComponents(MainWindow parentWindow)
        {
            // Create a Line Element
            //myLine = new Line();
            myLine = parentWindow.myLine;
            myLine.Stroke = Brushes.Black;
            myLine.X1 = parentWindow.Width / 2;
            myLine.X2 = parentWindow.Width / 2;
            myLine.Y1 = 0;
            myLine.Y2 = myLine.Y1 + 100;
            myLine.StrokeThickness = 2;

            // Create a Ellipse.
            myEllipse = parentWindow.myEllipse;
            // Create a SolidColorBrush with a red color to fill the
            // Ellipse with.

            myEllipse.Fill = Brushes.CadetBlue;

            myEllipse.Margin = new Thickness(myLine.X2 - (myEllipse.Width / 2), myLine.Y2, 0, 0);

            // Adds the two elements to the canvas.
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
            Point p1 = rotation.Transform(new Point(myLine.X1, myLine.Y1));
            Point p2 = rotation.Transform(new Point(myLine.X2, myLine.Y2));
            Console.WriteLine(p1);

            // Sets the center point of the ellipse to the p2 (end point of line).
            myEllipse.Margin = new Thickness(p2.X - (myEllipse.Width / 2), p2.Y - (myEllipse.Height / 2), 0, 0);

            // If the currentAngle is less than or equal to the negative MAX_ANGLE
            if (currentAngle <= -MAX_ANGLE && !recentlyHit)
            {
                goingRight = false;
            }
            // If the currentAngle is greater than or equal to the MAX_ANGLE
            else if (currentAngle >= MAX_ANGLE && !recentlyHit)
            {
                goingRight = true;
            }
            else if (currentAngle >= (MAX_ANGLE + 10) && recentlyHit)
            {
                goingRight = true;
            }
            else if (currentAngle <= -(MAX_ANGLE - 10) && recentlyHit)
            {
                Reset();
            }
        }

        public void Hit()
        {
            recentlyHit = true;
            goingRight = false;
            // if the ball was hit then increase the angle amount per tick.
            incrementAngle = FAST_INCREMENT_ANGLE;
            numberOfHits++;
            //myEllipse.Effect = new
        }

        public void Reset()
        {
            recentlyHit = false;
            incrementAngle = NORMAL_INCREMENT_ANGLE;
        }

        public void Hide()
        {
            myLine.Visibility = Visibility.Hidden;
            myEllipse.Visibility = Visibility.Hidden;
        }
    }
}