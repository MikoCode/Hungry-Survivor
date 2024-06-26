using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace ScoreBoard
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] 
        private Scores _scores;

        [SerializeField] 
        private TextMeshProUGUI _highScore;
        [SerializeField] 
        private TextMeshProUGUI _currentScores;

        [SerializeField] 
        private TextMeshProUGUI _highScoresInMenu;

        [Button]
        public void RefreshHighScores()
        {
            _scores.SaveHighScore();
            _highScore.text = _scores.GetHighScore().ToString();
            _highScoresInMenu.text = _scores.GetHighScore().ToString();
            _currentScores.text = _scores.CurrentScores.ToString();
        }
    }
}