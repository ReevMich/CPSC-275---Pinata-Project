// Main Class
// Michael Reeves && Daniel Arellano

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
        private bool startedGame;
        private MediaPlayer mediaPlayer;

        public MainWindow()
        {
            InitializeComponent();

            pinata = new Pinata();
            bat = new Bat();

            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("../../sounds/Epic Star Wars Music Compilation - Star Wars.mp3", UriKind.Relative));
            mediaPlayer.Play();
            startedGame = false;

            ToggleMainMenu(true);

            lbl_highscore_mainmenu.Content = "High Score: " + 0;
        }

        /// <summary>
        /// StartGame sets up all the objects and variables that is going to be used. When the game starts
        /// </summary>
        private void StartGame()
        {
            pinata.Initialize(this);
            bat.Initialize(this);

            mediaPlayer.Position = new TimeSpan(0, 21, 56);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            isPaused = false;
            startedGame = true;

            ToggleMainMenu(false);

            timer.Start();

            UpdateLabels();
        }

        /// <summary>
        /// Tick Method check if the game has been started and if it is paused or not.
        /// If it is not paused it checks for collision between the Bat and the Pinata.
        /// and check the user has the amount of misses that would have them lose the game.
        /// If they lose the game they have an option of restarting the game; going back to
        /// the main menu to start a new game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        UpdateLabels();
                    }

                    // If the user has a missed two swings game over.
                    if (bat.NumMissed == 2)
                    {
                        MessageBoxResult mbr;
                        if (pinata.CurrentScore > pinata.HighScore)

                            mbr = MessageBox.Show("GameOver, \nCongratulations you have set a new high score of " + pinata.CurrentScore + "!! \nWould you like to play again?", "Game Over", MessageBoxButton.YesNo);
                        else
                            mbr = MessageBox.Show("GameOver, \nWould you like to play again?", "Game Over", MessageBoxButton.YesNo);

                        if (mbr == MessageBoxResult.Yes)
                        {
                            GameOver();
                            UpdateLabels();
                        }
                        else
                        {
                            Close();
                        }
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

                if (e.Key == Key.Space && !isPaused)
                {
                    bat.SwingBat();
                }
            }
        }

        /// <summary>
        /// Resets the objects and toggles the menu and restarts the music.
        /// </summary>
        private void GameOver()
        {
            pinata.Reset();
            pinata.StopAllSounds();
            bat.Reset(this);
            bat.StopAllSounds();
            mediaPlayer.Position = new TimeSpan(0, 0, 0);
            timer.Stop();
            startedGame = false;
            ToggleMainMenu(true);
        }

        /// <summary>
        /// Pausesd the game
        /// </summary>
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

        /// <summary>
        /// Updates the score and high score labels.
        /// </summary>
        private void UpdateLabels()
        {
            lbl_currentScore.Content = "Score: " + pinata.CurrentScore;
            lbl_highscore.Content = "High Score: " + pinata.HighScore;
            lbl_highscore_mainmenu.Content = lbl_highscore.Content;
        }

        /// <summary>
        /// Toggles between the game screen and the title screen.
        /// </summary>
        /// <param name="showMenu">Determines if you want to show the main menu</param>
        private void ToggleMainMenu(bool showMenu)
        {
            if (showMenu)
            {
                lbl_currentScore.Visibility = Visibility.Hidden;
                lbl_highscore.Visibility = Visibility.Hidden;
                myBat.Visibility = Visibility.Hidden;
                myLine.Visibility = Visibility.Hidden;
                myEllipse.Visibility = Visibility.Hidden;

                btn_startgame.Visibility = Visibility.Visible;
                title_image.Visibility = Visibility.Visible;
                lbl_highscore_mainmenu.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_currentScore.Visibility = Visibility.Visible;
                lbl_highscore.Visibility = Visibility.Visible;
                myBat.Visibility = Visibility.Visible;
                myLine.Visibility = Visibility.Visible;
                myEllipse.Visibility = Visibility.Visible;

                btn_startgame.Visibility = Visibility.Hidden;
                title_image.Visibility = Visibility.Hidden;
                lbl_highscore_mainmenu.Visibility = Visibility.Hidden;
            }
        }

        private void btn_startgame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}