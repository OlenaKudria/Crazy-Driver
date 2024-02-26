using Config.Obstacles;
using Player;
using UnityEngine;

namespace Obstacles
{
    public class ObstaclesDamage : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;

        private void Start()
        {
            ObstaclesTrigger.OnObstaclesEntered += HandleObstaclesEntered;
        }

        private void HandleObstaclesEntered(ObstaclesConfig config)
        {
            float damage = config.Damage;
            if(damage > 0)
                healthSystem.Damage(damage);
        }
        
        private void OnDestroy()
        {
            ObstaclesTrigger.OnObstaclesEntered -= HandleObstaclesEntered;
        }
    }
}