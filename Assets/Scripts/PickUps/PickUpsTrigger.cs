using UnityEngine;

namespace PickUps
{
    public delegate void PickUpsEventHandler();
    public class PickUpsTrigger : MonoBehaviour
    {
        public static event PickUpsEventHandler OnPickUpEntered;
        private const string PlayerTag = "Player";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(PlayerTag))
                OnPickUpEntered?.Invoke();
        }
    }
}