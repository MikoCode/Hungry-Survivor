using System;
using UnityEngine;

namespace ScoreBoard
{
    public class ScoreManager : Scores
    {
        /*private ScoreManager()
        {
            highScores = PlayerPrefs.GetInt("HighScore", 0);
        }*/

        private void Start()
        {
            GetHighScore();
        }

        public override void SaveHighScore()
        {
            if (CurrentScores > highScores)
            {
                highScores = CurrentScores;
                PlayerPrefs.SetInt("HighScore", highScores);
                PlayerPrefs.Save();
            }
        }

        public override void ResetHighScore()
        {
            highScores = 0;
            PlayerPrefs.SetInt("HighScore", highScores);
            PlayerPrefs.Save();
        }

        public override int GetHighScore()
        {
            highScores = PlayerPrefs.GetInt("HighScore", 0);
            return highScores;
        }
    }
}