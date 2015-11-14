// Main Class
// Michael Reeves && Daniel Ariello

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        private bool startedGame;
        private MediaPlayer mediaPlayer;

        public MainWindow()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("../../sounds/Epic Star Wars Music Compilation - Star Wars.mp3", UriKind.Relative));
            mediaPlayer.Play();
            startedGame = false;

            lbl_currentScore.Visibility = Visibility.Hidden;
            lbl_highscore.Visibility = Visibility.Hidden;
            myBat.Visibility = Visibility.Hidden;
            myLine.Visibility = Visibility.Hidden;
            myEllipse.Visibility = Visibility.Hidden;
            scoreBar.Visibility = Visibility.Hidden;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (startedGame)
            {
                // if the game is not paused then, call the draw methods for our pinata
                // and our bat objects.
                if (!isPaused)
                {
                    bat.Draw();
                    pinata.Draw();

                    if (bat.IsCollision(pinata))
                    {
                        pinata.Hit(); // Uncomment once collision method is working.
                    }

                    if (pinata.CurrentScore == 5)
                    {
                        GameOver();
                        UpdateLabels();
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (startedGame)
            {
                // Pause Logic will go in here.
                if (e.Key == Key.P)
                {
                    PauseGame();
                }

                if (e.Key == Key.Space)
                {
                    bat.SwingBat();
                }

                if (e.Key == Key.H)
                {
                    pinata.Hit();
                    UpdateLabels();
                }
            }
        }

        private void GameOver()
        {
            MessageBoxResult mbr = MessageBox.Show("Game Over, Would you like to play again?", "Game Over", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                pinata.Reset();
                mediaPlayer.Position = new TimeSpan(0, 21, 56);
            }
            else
            {
                Close();
            }
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

        private void UpdateLabels()
        {
            lbl_currentScore.Content = "Score: " + pinata.CurrentScore;
            lbl_highscore.Content = "Highscore: " + pinata.HighScore;
        }

        private void StartGame()
        {
            pinata = new Pinata(this);
            bat = new Bat(this);

            mediaPlayer.Position = new TimeSpan(0, 21, 56);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            isGameOver = false;
            isPaused = false;
            startedGame = true;

            numMissed = 0;

            // Shows all the game components
            lbl_currentScore.Visibility = Visibility.Visible;
            lbl_highscore.Visibility = Visibility.Visible;
            myBat.Visibility = Visibility.Visible;
            myLine.Visibility = Visibility.Visible;
            myEllipse.Visibility = Visibility.Visible;
            scoreBar.Visibility = Visibility.Visible;

            // Hide Title menu stuff.
            btn_startgame.Visibility = Visibility.Hidden;
            title_image.Visibility = Visibility.Hidden;

            timer.Start();

            UpdateLabels();
        }

        private void btn_startgame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}