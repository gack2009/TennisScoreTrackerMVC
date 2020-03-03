using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TennisScoreTracker
{
    public class ScoreboardModel
    {
        [Required]
        public string player1Name { get; set; }

        [Required]
        public string player2Name { get; set; }

        private string m_player1Score;

        [Required]
        public string player1Score
        {
            get
            {
                return m_player1Score;
            }
            set
            {
                if (value == "Game")
                {
                    player1Score = $"Game {player1Name}";
                }
                else
                {
                    m_player1Score = value;
                }
            }
        }

        private string m_player2Score;

        [Required]
        public string player2Score
        {
            get
            {
                return m_player2Score;
            }
            set
            {
                if (value == "Game")
                {
                    m_player2Score = $"Game {player2Name}";
                }
                else
                {
                    m_player2Score = value;
                }
            }
        }

        [Required]
        public string scoreAction { get; set; }

        public ScoreboardModel()
        {
            player1Name = "Roger";
            player2Name = "Tim";
            player1Score = "0";
            player2Score = "0";
        }

        public ScoreboardModel(string player1Name, string player2Name, string player1Score, string player2Score)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player1Score = player1Score;
            this.player2Score = player2Score;
        }
    }
}
