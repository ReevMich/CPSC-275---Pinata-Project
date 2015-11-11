using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinata_Game
{
    public partial class Form1 : Form
    {
        private Pinata pinata;
        private Bat bat;

        public Form1()
        {
            InitializeComponent();

            // Use double buffering to reduce flicker.
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            UpdateStyles();
            pinata = new Pinata(this);
            bat = new batdrawn(new PointF(new PointF(225, 300), 170 * Math.PI / 1.65);
        }

        private void tick(object sender, EventArgs e)
        {
            Refresh();

            Graphics graphics = CreateGraphics();

            pinata.Draw(graphics);

            Console.WriteLine("tick");
        }
    }
}