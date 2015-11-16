using System.Windows;

namespace Pinata_Game_WPF
{
    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    ///

    public partial class HighScoreWindow : Window
    {
        private ThePlayer player;

        public ThePlayer Player
        {
            get { return player; }
        }

        public HighScoreWindow(int score)
        {
            InitializeComponent();

            player = new ThePlayer(score);
            textBlock.Text = textBlock.Text + score + " points!";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 2 || textBox.Text.Length == 3)
            {
                player.Name = textBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter your initials to save your score.");
            }
        }
    }
}