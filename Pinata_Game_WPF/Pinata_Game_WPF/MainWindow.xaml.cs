using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pinata_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pinata pinata;
        private Bat bat;
        private DispatcherTimer timer;
        private bool paused;

        private Storyboard storyBoard;

        public MainWindow()
        {
            StackPanel myPanel = new StackPanel();
            myPanel.Margin = new Thickness(10);

            InitializeComponent();
            storyBoard = new Storyboard();
            pinata = new Pinata(this);
            bat = new Bat(this);
            timer = new DispatcherTimer();

            paused = false;

            //  DispatcherTimer setup
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!paused)
            {
                bat.drawBat();
                pinata.Draw();
            }
        }

        /*
        private void dispatcherTime_Tick(object sender, EventArgs e)
        {
            bat.drawBat();
        }
        */

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Pause Logic will go in here.
            if (e.Key == Key.P)
            {
                paused = !paused;
            }
        }
    }
}