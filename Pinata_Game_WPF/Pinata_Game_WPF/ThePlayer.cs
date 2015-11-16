using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinata_Game_WPF
{
    public class ThePlayer : IComparable<ThePlayer>
    {
        private string name;
        private int score;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Score
        {
            get { return score; }
        }

        public ThePlayer(int score)
        {
            this.score = score;
        }

        public int CompareTo(ThePlayer other)
        {
            if (other != null)
                return score.CompareTo(other.score);
            else
                return -1;
        }
    }
}