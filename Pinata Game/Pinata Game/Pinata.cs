using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

//0 45 90 135 180 225 270 315 360

namespace Pinata_Game
{
    internal class Pinata
    {
        private PointF BallCenterPoint = new PointF(50, 50);

        private Form1 form;

        private int angle;
        private int angleChange;

        private int minAngle, maxAngle;
        private SolidBrush ballBrush = new SolidBrush(Color.Red);

        public Pinata(Form1 form)
        {
            this.form = form;
            this.angle = 0;
            this.angleChange = 1;
            this.minAngle = 225;
            this.maxAngle = 135;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillEllipse(ballBrush, new Rectangle((form.ClientSize.Width / 2) - (int)(BallCenterPoint.X / 2), (form.ClientSize.Height / 2) - (int)(BallCenterPoint.Y / 2), (int)BallCenterPoint.X, (int)BallCenterPoint.Y));
        }

        public void Tick(Timer timer)
        {
        }

        public void Hit()
        {
        }

        public void Reset(Graphics graphics)
        {
        }
    }
}