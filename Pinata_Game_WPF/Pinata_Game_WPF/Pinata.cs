// Class: Pinata
// Michael Reeves

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pinata_Game_WPF
{
    internal class Pinata
    {
        private const int MAX_ANGLE = 75;
        private const double NORMAL_INCREMENT_ANGLE = Math.PI / 3.2;
        private const double FAST_INCREMENT_ANGLE = NORMAL_INCREMENT_ANGLE * Math.PI;
        private const string IMAGE_PATH = "../../images/";
        private const string SOUND_PATH = "../../sounds/";

        private MediaPlayer hitSound;

        private Line myLine;
        private Ellipse myEllipse;

        private bool goingRight;
        private bool recentlyHit;
        private double currentAngle;
        private double incrementAngle;

        private int currentScore;
        private int highScore;

        public int CurrentScore
        {
            get { return currentScore; }
        }

        public int HighScore
        {
            get { return highScore; }
        }

        public Ellipse MyEllipse
        {
            get { return myEllipse; }
        }

        /// <summary>
        /// The center point of the Ellipse
        /// </summary>
        public Point EllipseCenterPoint
        {
            get { return new Point(myEllipse.Width / 2, myEllipse.Height / 2); }
        }

        public Line MyLine
        {
            get { return myLine; }
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
            ImageBrush imgBrush = new ImageBrush(new BitmapImage(new Uri(IMAGE_PATH + "deathstar.png", UriKind.Relative)));
            hitSound = new MediaPlayer();
            hitSound.Open(new Uri(SOUND_PATH + "lightsaber_hit.mp3", UriKind.Relative));

            // Create a Line Element
            myLine = parentWindow.myLine;
            myLine.Stroke = Brushes.Black;
            myLine.X1 = parentWindow.Width / 2;
            myLine.X2 = parentWindow.Width / 2;
            myLine.Y1 = 0;
            myLine.Y2 = parentWindow.Width / 4;
            myLine.StrokeThickness = 4;
            myLine.Stroke = imgBrush;

            // Create a Ellipse.
            myEllipse = parentWindow.myEllipse;

            myEllipse.Fill = imgBrush;

            // Adds the two elements to the canvas.
        }

        public void Draw()
        {
            // If the direction is going right then subtract the incrementAngle from currentAngle.
            if (!goingRight)
            {
                myLine.RenderTransform = new RotateTransform(currentAngle += incrementAngle, myLine.X1, myLine.Y1);
            }
            // If the direction is going left then subtract the incrementAngle from currentAngle.
            else
            {
                myLine.RenderTransform = new RotateTransform(currentAngle -= incrementAngle, myLine.X1, myLine.Y1);
            }

            // Creates a RotateTransform object based of the myLine.Render transform object.
            RotateTransform rotation = myLine.RenderTransform as RotateTransform;

            // Create two point objects p1 = beginning point of a line, while, p2 = end point of a line.

            Point p1 = rotation.Transform(new Point(myLine.X1, myLine.Y1));
            Point p2 = rotation.Transform(new Point(myLine.X2, myLine.Y2));

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
            else if (currentAngle >= (MAX_ANGLE + Math.PI * 3) && recentlyHit)
            {
                goingRight = true;
            }
            else if (currentAngle <= -(MAX_ANGLE + Math.PI * 3) && recentlyHit)
            {
                ResetSpeed();
            }
        }

        public void Hit()
        {
            recentlyHit = true;
            goingRight = false;

            // if the ball was hit then increase the angle amount per tick.
            incrementAngle = FAST_INCREMENT_ANGLE;
            currentScore++;
            hitSound.Play();
            hitSound.Position = new TimeSpan();
        }

        public void Reset()
        {
            myLine.Y1 = 0;
            //myLine.Y2 = parentWindow.Width / 3;
            myEllipse.Margin = new Thickness(myLine.X2 - (myEllipse.Width / 2), myLine.Y2, 0, 0);
            goingRight = false;
            highScore = currentScore;
            currentScore = 0;
            currentAngle = 0;
            myLine.RenderTransform = new RotateTransform(currentAngle, myLine.X1, myLine.Y1);
            ResetSpeed();
        }

        private void ResetSpeed()
        {
            recentlyHit = false;
            incrementAngle = NORMAL_INCREMENT_ANGLE;
        }
    }
}