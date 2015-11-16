// Main Class
// Michael Reeves && Daniel Arellano

using System;
using System.Text;
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
        private ThePlayer[] highestPlayers;
        private int currentIndex;

        public MainWindow()
        {
            InitializeComponent();

            pinata = new Pinata();
            bat = new Bat();

            highestPlayers = new ThePlayer[10];
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("../../sounds/Epic Star Wars Music Compilation - Star Wars.mp3", UriKind.Relative));
            mediaPlayer.Play();
            startedGame = false;

            ToggleMainMenu(true);
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
                    if (bat.NumMissed == 1)
                    {
                        MessageBoxResult mbr;

                        if (isHighScore(pinata.CurrentScore))
                        {
                            HighScoreWindow window = new HighScoreWindow(pinata.CurrentScore);
                            window.ShowDialog();
                            // If the user clicked the submit score button
                            // Add the player to the list of players
                            if (window.DialogResult == true)
                            {
                                AddPlayerToList(window.Player);
                            }
                            UpdateList();
                        }
                        mbr = MessageBox.Show("GameOver, Would you like to play again?", "Game Over", MessageBoxButton.YesNo);

                        if (mbr == MessageBoxResult.Yes)
                        {
                            GameOver();
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
                myBat.Visibility = Visibility.Hidden;
                myLine.Visibility = Visibility.Hidden;
                myEllipse.Visibility = Visibility.Hidden;

                btn_startgame.Visibility = Visibility.Visible;
                title_image.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_currentScore.Visibility = Visibility.Visible;
                myBat.Visibility = Visibility.Visible;
                myLine.Visibility = Visibility.Visible;
                myEllipse.Visibility = Visibility.Visible;

                btn_startgame.Visibility = Visibility.Hidden;
                title_image.Visibility = Visibility.Hidden;
            }
        }

        private void btn_startgame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        /// <summary>
        /// Checks if the recent score from the user is even a high score.
        /// </summary>
        private bool isHighScore(int score)
        {
            if (currentIndex != highestPlayers.Length - 1)
                return true;
            for (int i = 0; i < highestPlayers.Length; i++)
            {
                if (highestPlayers[i] != null && score > highestPlayers[i].Score)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Add the player to the high scorers list.
        /// Then uses the selection sort to sort the list.
        /// </summary>
        /// <param name="player"></param>
        private void AddPlayerToList(ThePlayer player)
        {
            // Keep adding players until the list is full
            if (currentIndex < highestPlayers.Length)
            {
                highestPlayers[currentIndex] = player;
                currentIndex++;
            }
            // Once the list is full then lets find the player with the lowest score
            // and replace them the with newest score.
            else if (currentIndex == highestPlayers.Length)
            {
                int lowestIndex = 0;
                for (int i = 0; i < highestPlayers.Length; i++)
                {
                    if (highestPlayers[i].Score < highestPlayers[lowestIndex].Score)
                    {
                        lowestIndex = i;
                    }
                }

                if (highestPlayers[lowestIndex].Score < player.Score)
                {
                    highestPlayers[lowestIndex] = player;
                }
            }

            // SELECTION SORT //
            ThePlayer tmp;
            int min = 0;

            for (int j = 0; j < highestPlayers.Length - 1; j++)
            {
                min = j;
                if (highestPlayers[j] != null)
                {
                    for (int k = j + 1; k < highestPlayers.Length; k++)
                    {
                        if (highestPlayers[k] != null && highestPlayers[k].Score > highestPlayers[min].Score)
                        {
                            min = k;
                        }
                    }

                    tmp = highestPlayers[min];
                    highestPlayers[min] = highestPlayers[j];
                    highestPlayers[j] = tmp;
                }
            }
        }

        // Updates the list and displays the names in a well formatted manner.
        private void UpdateList()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < highestPlayers.Length; i++)
            {
                if (highestPlayers[i] != null)
                {
                    sb.AppendLine(String.Format("{0,-10}{1,10}", highestPlayers[i].Name, highestPlayers[i].Score));
                }
            }

            textBlock.Text = sb.ToString();
        }
    }
}