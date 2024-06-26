using HungrySurvivor.Singleton;

namespace ScoreBoard
{
    public abstract class Scores : Singleton<Scores>
    {
        protected int highScores = 0;
        
        public int CurrentScores
        {
            get
            {
                return (int)GameManager.Instance.expSystem.currentEXP;
            }
        }

        public abstract void SaveHighScore();
        public abstract void ResetHighScore();
        public abstract int GetHighScore();
    }
}