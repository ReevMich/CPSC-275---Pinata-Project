using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pinata_Game_WPF
{
    internal class Bat
    {
        private const double startAngle = 62;
        private const double maxAngle = -90;
        private const double incAngle = 3;
        private const string IMAGE_PATH = "../../images/";
        private const string SOUND_PATH = "../../sounds/";

        private MediaPlayer lightsaberLoop;
        private MediaPlayer lightsaberSwing;
        private MediaPlayer lightsaberStart;

        private bool collided; // flag if a collision has happened.
        private bool swung; // flag is the user has swung the bat.
        private BatState batState;
        private Line eLine;
        private double i;

        private int numMissed; // The number of misses the user has made

        public int NumMissed
        {
            get { return numMissed; }
        }

        public Point EndPoint
        {
            get { return new Point(eLine.X2, eLine.Y2); }
        }

        public void Initialize(MainWindow window)
        {
            ImageBrush imgBrush = new ImageBrush(new BitmapImage(new Uri(IMAGE_PATH + "lightsaber1" + ".png", UriKind.Relative)));

            // added 3 Media Player objects, they handle the sound for the bat class such as,
            // lightsaber loop, the turning of the lightsaber, and the lighsaber swing.
            lightsaberStart = new MediaPlayer();
            lightsaberLoop = new MediaPlayer();
            lightsaberSwing = new MediaPlayer();

            // Setting the volume of each sound.
            lightsaberStart.Volume = .45;
            lightsaberLoop.Volume = .15;
            lightsaberSwing.Volume = .40;

            // Opening each mp3 file that goes with each media player.
            lightsaberStart.Open(new Uri(SOUND_PATH + "lightsaber_start.mp3", UriKind.Relative));
            lightsaberLoop.Open(new Uri(SOUND_PATH + "lightsaber_loop.mp3", UriKind.Relative));
            lightsaberSwing.Open(new Uri(SOUND_PATH + "lightsaber_swing.mp3", UriKind.Relative));

            // have to create a event handler in order to loop the lightsaber_loop sound.
            lightsaberLoop.MediaEnded += LightsaberLoop_MediaEnded;

            // Plays the startup sound for the lightsaber.
            lightsaberStart.Play();
            // You have to reset the position of the media sound after you play it.
            lightsaberStart.Position = new TimeSpan();

            eLine = window.myBat;

            eLine.Stroke = Brushes.Black;
            eLine.X1 = window.TheCanvas.Width / 1.40;
            eLine.X2 = window.TheCanvas.Width / 1.40;
            eLine.Y1 = window.TheCanvas.Height / 1.25;
            eLine.Y2 = eLine.Y1 - 225;
            eLine.StrokeThickness = 100;
            eLine.Stroke = imgBrush;
            eLine.RenderTransform = new RotateTransform(startAngle, eLine.X1, eLine.Y1);
            batState = BatState.Ready;
            i = startAngle;

            lightsaberLoop.Play();

            swung = false;
        }

        private void LightsaberLoop_MediaEnded(object sender, EventArgs e)
        {
            lightsaberLoop.Position = new TimeSpan();
            lightsaberLoop.Play();
        }

        public void Draw()
        {
            if (batState == BatState.Forwards)
            {
                eLine.RenderTransform = new RotateTransform(i -= incAngle, eLine.X1, eLine.Y1);
            }
            else if (batState == BatState.Backwards)
            {
                eLine.RenderTransform = new RotateTransform(i += incAngle, eLine.X1, eLine.Y1);
            }

            if (i <= maxAngle)
            {
                if (swung == true)
                {
                    numMissed++;
                }
                batState = BatState.Backwards;
            }
            else if (i >= startAngle)
            {
                batState = BatState.Ready;
                collided = false;
            }
        }

        public void SwingBat()
        {
            if (batState == BatState.Ready)
            {
                batState = BatState.Forwards;
                lightsaberSwing.Play();
                lightsaberSwing.Position = new TimeSpan();
                swung = true;
            }
        }

        public bool IsCollision(Pinata pinata)
        {
            if (batState == BatState.Forwards && collided == false)
            {
                /*
                This is the correct solution for collision using the algorithm Hodges gave us. I forget what you had but you were close.
                double collisionPoint = Math.Sqrt(Math.Pow(pinata.MyEllipseCenterPoint.X - eLine.X2, 2) + Math.Pow(pinata.MyEllipseCenterPoint.Y - eLine.Y2, 2));

                if (collisionPoint <= pinata.MyEllipse.Width)
                {
                    Console.WriteLine("Collision");
                    return true;
                }
                */
                //This is the way i came up with.
                RotateTransform rotation = eLine.RenderTransform as RotateTransform;

                double radius = pinata.MyEllipse.Width / 2;
                Point p2 = rotation.Transform(new Point(eLine.X2, eLine.Y2));

                if (p2.X <= pinata.MyEllipseCenterPoint.X + (radius)
                    && p2.Y <= pinata.MyEllipseCenterPoint.Y + (radius))
                {
                    collided = true;
                    swung = false;
                    return true;
                }
            }

            return false;
        }

        public void StopAllSounds()
        {
            lightsaberLoop.Stop();
            lightsaberStart.Stop();
            lightsaberSwing.Stop();
        }
    }
}

public enum BatState
{
    Forwards,
    Backwards,
    Ready
}