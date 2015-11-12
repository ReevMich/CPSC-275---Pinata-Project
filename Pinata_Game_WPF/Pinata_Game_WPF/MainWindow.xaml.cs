using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pinata_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MAX_MISSED = 2;

        private Pinata pinata; // The Pinata object.
        private Bat bat; // The Bat object
        private DispatcherTimer timer; // the game timer.
        private bool isPaused; // is the game paused
        private bool isGameOver; // is the game over
        private int numMissed; // The number of misses the user has missed

        public MainWindow()
        {
            InitializeComponent();
            pinata = new Pinata(this);
            bat = new Bat(this);
            timer = new DispatcherTimer();
            isGameOver = false;
            isPaused = false;
            numMissed = 0;

            //  DispatcherTimer setup
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // if the game is not paused then, call the draw methods for our pinata
            // and our bat objects.
            if (!isPaused)
            {
                 bat.Draw();
                pinata.Draw();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Pause Logic will go in here.
            if (e.Key == Key.P)
            {
                PauseGame();
            }
        }

        private void GameOver()
        {
        }

        private void PauseGame()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                lbl_pause.Visibility = Visibility.Visible;
                bg_background.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_pause.Visibility = Visibility.Hidden;
                bg_background.Visibility = Visibility.Hidden;
            }
        }
    }
}