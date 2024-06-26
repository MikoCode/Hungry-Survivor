using UnityEngine;

namespace HungrySurvivor.Experience
{
    public class DestroyExpBalls : MonoBehaviour
    {
        public void DestroyAllExperienceInScene()
        {
            // Find all GameObjects with the Enemy script attached
            EXPBall[] exp = FindObjectsOfType<EXPBall>();

            // Loop through each enemy and destroy its GameObject
            foreach (EXPBall expBall in exp)
            {
                Destroy(expBall.gameObject);
            }

            Debug.Log("All experience have been destroyed!");
        }
    }
}