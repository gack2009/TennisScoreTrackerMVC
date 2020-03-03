using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisScoreTracker
{
    public class ScoreService
    {
        /// <summary>
        /// Main method to get the scores
        /// </summary>
        /// <param name="currentScore"></param>
        /// <param name="action"></param>
        public void GetScoreByAction(ScoreboardModel currentScore, ActionType action)
        {
            var errors = validateInput(currentScore);
            if(errors.Count > 0)
            {
                throw new Exception(string.Join(", ", errors));
            }
            if (action == ActionType.Player1WinsAPoint)
            {
                if (currentScore.player1Score == "40" && currentScore.player2Score == "A")
                {
                    currentScore.player2Score = "40";
                }
                else
                {
                    currentScore.player1Score = GetScore(currentScore.player1Score, currentScore.player2Score);
                }

            }
            else if (action == ActionType.Player2WinsAPoint)
            {
                if (currentScore.player2Score == "40" && currentScore.player1Score == "A")
                {
                    currentScore.player1Score = "40";
                }
                else
                {
                    currentScore.player2Score = GetScore(currentScore.player2Score, currentScore.player1Score);
                }
            }
            else
            {
                throw new Exception($"Selected action ({action.ToString()}) is not recognized!");
            }
        }

        /// <summary>
        /// Check if the point receiver won the game
        /// </summary>
        /// <param name="pointReceiverScore"></param>
        /// <param name="opponentScore"></param>
        /// <returns></returns>
        private bool IsWinGame(string pointReceiverScore, string opponentScore)
        {
            if (pointReceiverScore == "40" && opponentScore != "40" && opponentScore != "A")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the player won the game or just need to add one point
        /// </summary>
        /// <param name="pointReceiverScore"></param>
        /// <param name="opponentScore"></param>
        /// <returns></returns>
        private string GetScore(string pointReceiverScore, string opponentScore)
        {
            if (IsWinGame(pointReceiverScore, opponentScore))
            {
                return "Game";
            }
            else
            {
                return AddAPointToPlayer(pointReceiverScore);
            }

        }

        /// <summary>
        /// Adds points based on tennis system
        /// </summary>
        /// <param name="playerCurrentScore"></param>
        /// <returns></returns>
        private string AddAPointToPlayer(string playerCurrentScore)
        {

            switch (playerCurrentScore)
            {
                case "0":
                    playerCurrentScore = "15";
                    break;
                case "15":
                    playerCurrentScore = "30";
                    break;
                case "30":
                    playerCurrentScore = "40";
                    break;
                case "40":
                    playerCurrentScore = "A";
                    break;
                case "A":
                    playerCurrentScore = "Game";
                    break;
                default:
                    throw new Exception($"The score ({playerCurrentScore}) is not a valid score to increment!");
            }

            return playerCurrentScore;
        }

        /// <summary>
        /// Validation for the input from the user
        /// </summary>
        /// <param name="inputScore"></param>
        /// <returns></returns>
        public bool IsValidScore(string inputScore)
        {
            string[] allowedScores = { "0", "15", "30", "40", "A" };
            if (!string.IsNullOrEmpty(inputScore) && allowedScores.Contains(inputScore))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Overload for the validation method to consider previous player score input
        /// </summary>
        /// <param name="inputScore"></param>
        /// <param name="previousPlayerInputScore"></param>
        /// <returns></returns>
        public bool IsValidScore(string inputScore, string previousPlayerInputScore)
        {
            if (IsValidScore(inputScore))
            {
                if (inputScore == "A" && inputScore == previousPlayerInputScore)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<string> validateInput(ScoreboardModel currentScore)
        {
            var errorsMessage = new List<string>();
            if(string.IsNullOrEmpty(currentScore.player1Name) || string.IsNullOrEmpty(currentScore.player2Name))
            {
                errorsMessage.Add("You must enter name for both players!");
            }
            if (!IsValidScore(currentScore.player1Score))
            {
                errorsMessage.Add($"You entered invalid score for {currentScore.player1Name}. Allowed input (0, 15, 30, 40, A).");
            }
            if(!IsValidScore(currentScore.player2Score))
            {
                errorsMessage.Add($"You entered invalid score for {currentScore.player2Name}. Allowed input (0, 15, 30, 40, A).");
            }
            if (!IsValidScore(currentScore.player2Score, currentScore.player1Score))
            {
                errorsMessage.Add($"Both players cannot have a score of (A) at the same time.");
            }
            return errorsMessage;
        }

        /// <summary>
        /// Gets the score in the same format presented in the example in the PDF
        /// </summary>
        /// <param name="currentScore"></param>
        /// <returns></returns>
        public string GetScoreString(ScoreboardModel currentScore)
        {
            if (currentScore.player1Score.Contains("Game"))
            {
                return currentScore.player1Score;
            }
            else if (currentScore.player2Score.Contains("Game"))
            {
                return currentScore.player2Score;
            }
            return $"{currentScore.player1Score.ToString()}-{currentScore.player2Score.ToString()}";
        }
    }
}