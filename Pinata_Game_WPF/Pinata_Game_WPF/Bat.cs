using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Pinata_Game_WPF
{
    internal class Bat
    {
        private const double startAngle = 62;
        private const double maxAngle = - 90;
        private const double incAngle = 3;
        private BatState batState;
        private Line eLine;
        private MainWindow parWindow;
        private double i = 0;

        public Bat(MainWindow window)
        {
            this.parWindow = window;
            InitializeComponents(window);
        }

        private void InitializeComponents(MainWindow window)
        {
            eLine = window.myBat;
            eLine.Stroke = System.Windows.Media.Brushes.Red;
            eLine.X1 = window.Width / 1.5 ;
            eLine.X2 = window.Width / 1.5 ;
            eLine.Y1 = window.Height / 2;
            eLine.Y2 = eLine.Y1 - 100;
            eLine.StrokeThickness = 6;
            eLine.RenderTransform = new RotateTransform(startAngle, eLine.X1, eLine.Y1);
            batState = BatState.Ready;

        }

        // MIKE: I changed the name of this to Draw from drawBat. Its better to be consistent.
        public void Draw()
        {
            if (batState == BatState.Forwards)
            {
                eLine.RenderTransform = new RotateTransform(i -= incAngle, eLine.X1, eLine.Y1);
            }
            else if ( batState == BatState.Backwards)
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
            if(batState == BatState.Ready)
            {
                batState = BatState.Forwards;
            }
        }
    }
 }

public enum BatState
{
    Forwards, 
    Backwards,
    Ready
}