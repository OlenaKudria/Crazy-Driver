using Config.Obstacles;
using UnityEngine;

namespace Obstacles
{
    public delegate void ObstaclesEventHandler(ObstaclesConfig config);
    public class ObstaclesTrigger : MonoBehaviour
    {
        public static event ObstaclesEventHandler OnObstaclesEntered;
        [SerializeField] private ObstaclesConfig config;
        private const string PlayerTag = "Player";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PlayerTag))
            {
                OnObstaclesEntered?.Invoke(config);
            }
        }
    }
}
