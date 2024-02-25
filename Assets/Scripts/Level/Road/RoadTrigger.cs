using UnityEngine;

namespace Level.Road
{
    public delegate void RoadEventHandler();
    
    public class RoadTrigger : MonoBehaviour
    {
        public static event RoadEventHandler OnRoadEntered;
        private const string PlayerTag = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(PlayerTag))
              OnRoadEntered?.Invoke();
        }
    }
}
