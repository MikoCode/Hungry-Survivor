using UnityEngine;

namespace HungrySurvivor.Enemies
{
    public class DestroyEnemies : MonoBehaviour
    {
        public void DestroyAllEnemiesInScene()
        {
            // Find all GameObjects with the Enemy script attached
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            // Loop through each enemy and destroy its GameObject
            foreach (Enemy enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }

            Debug.Log("All enemies have been destroyed!");
        }
    }
}