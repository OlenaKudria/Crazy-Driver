using UnityEngine;

namespace Obstacles
{
    public delegate void ObstaclesEventHandler();
    public class ObstaclesTrigger : MonoBehaviour
    {
        public static event ObstaclesEventHandler OnObstaclesEntered;
        private const string PlayerTag = "Player";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PlayerTag))
            {
                OnObstaclesEntered?.Invoke();
            }
        }
    }
}
