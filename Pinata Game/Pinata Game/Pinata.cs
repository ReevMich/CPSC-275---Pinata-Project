using System;
using System.Collections.Generic;
using System.Drawing;

//0 45 90 135 180 225 270 315 360

namespace Pinata_Game
{
    internal class Pinata
    {
        private PointF BallCenterPoint = new PointF(200, 200);

        private int angle;
        private int angleChange;

        private int minAngle, maxAngle;

        public Pinata()
        {
            this.angle = 0;
            this.angleChange = 1;
            this.minAngle = 225;
            this.maxAngle = 135;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void Rotate()
        {
        }
    }
}