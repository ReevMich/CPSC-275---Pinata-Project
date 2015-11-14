﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        private const string PATH = "../../images/";
        private const string EXTENSION = ".png";

        private BatState batState;
        private Line eLine;
        private MainWindow parWindow;
        private double i;
        int intValue = 100;
        private double smallMin = 2;
        private double smallMax = 4;
        private double smallRMin = 0;
        private double smallRMax = 1;

        public Bat(MainWindow window)
        {
            this.parWindow = window;
            InitializeComponents(window);
        }

        private void InitializeComponents(MainWindow window)
        {
            ImageBrush imgBrush = new ImageBrush(new BitmapImage(new Uri(PATH + "lightsaber1" + EXTENSION, UriKind.Relative)));
            eLine = window.myBat;

            eLine.Stroke = Brushes.Black;
            eLine.X1 = window.Width / 1.40;
            eLine.X2 = window.Width / 1.40;
            eLine.Y1 = window.Height / 1.35;
            eLine.Y2 = eLine.Y1 - 155;
            eLine.StrokeThickness = 75;
            eLine.Stroke = imgBrush;
            eLine.RenderTransform = new RotateTransform(startAngle, eLine.X1, eLine.Y1);
            batState = BatState.Ready;
            i = startAngle;
        }

        // MIKE: I changed the name of this to Draw from drawBat. Its better to be consistent.
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
                batState = BatState.Backwards;
            }
            else if (i >= startAngle)
            {
                batState = BatState.Ready;
            }
        }

        public void SwingBat()
        {
            if (batState == BatState.Ready)
            {
                batState = BatState.Forwards;
            }
        }

       public Point EndPoint
           {
              get { return new Point(eLine.Y2, eLine.X2); }
           }
        public Boolean IsCollision(Pinata pinata)
        {
            double diameter = pinata.MyEllipse.Width;
            double radius = pinata.MyEllipse.Width / 2;
            double length = Math.Sqrt((Math.Pow(radius - EndPoint.X, 2) + (Math.Pow(radius - EndPoint.X, 2))));

            if(length <= radius)
                {
                intValue -= 5;
                if (intValue <= 10)
                {
                    intValue = 10;
                }
                if( smallMin <= 4 && smallMax <= 6 || smallRMin >= -2 && smallRMax >= -1 )
                {
                    smallMin += .1;
                    smallMax += .1;
                    smallRMin += -.1;
                    smallRMax += -.1;
                  Window.timer.interval = intValue;
                  Window.UpdateLabels();
                }
                }

            return true;
        }
    }
}

public enum BatState
{
    Forwards,
    Backwards,
    Ready
}