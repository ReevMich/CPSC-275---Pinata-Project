using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace Pinata_Game
{
        internal class Bat
    {

        Bat batStick;
        private PointF center;
        private PointF endpoint;
        private double angle;
        private double length;
        private Pen pen;
        private Pen eraser;
        double ShiftAmountBat;
        Boolean backwardBat = false;
        private Form1 form1;

        public Bat( PointF center, double angle, double length)
        {
            this.center = center;
            this.angle = angle;
            this.length = length;

            pen = new Pen(Color.Cyan, 4);
            eraser = new Pen(Color.White, 4);
        }

        public void moveBat(double angle_change)
        {
            angle = angle + angle_change;
        }

        private void drawBat(Graphics grp, Pen batpen)
        {
            endpoint = new PointF((float)(center.X + length * Math.Cos(angle)), (float)(center.Y + length * Math.Sin(angle)));

            grp.DrawLine(batpen, center, endpoint);

        }
        public void drawBat(Graphics grp)
        {
            drawBat(grp, pen);
        } 

        public void undrawBat(Graphics grp)
        {
            drawBat(grp, eraser);
        }

        public PointF EndPoint
        {
            get { return endpoint; }
            set { endpoint = value; }
        }

        public Double valueOfBatStickAngle()
        {
            return this.angle;
        }

    }
}
