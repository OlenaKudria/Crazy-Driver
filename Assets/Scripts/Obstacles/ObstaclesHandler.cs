using Config.Obstacles;
using Player;
using UnityEngine;

namespace Obstacles
{
    public class ObstaclesHandler : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private SpeedManager speedManager;

        private void Start()
        {
            ObstaclesTrigger.OnObstaclesEntered += HandleObstaclesEntered;
        }

        private void HandleObstaclesEntered(ObstaclesConfig config)
        {
            if(config.Damage > 0)
                healthSystem.Damage(config.Damage);
        }
        
        private void OnDestroy()
        {
            ObstaclesTrigger.OnObstaclesEntered -= HandleObstaclesEntered;
        }
    }
}