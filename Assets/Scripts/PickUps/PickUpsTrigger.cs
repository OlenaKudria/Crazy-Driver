using Config.PickUps;
using UnityEngine;

namespace PickUps
{
    public delegate void PickUpsEventHandler(PickUpsConfig config, GameObject prefab);
    public class PickUpsTrigger : MonoBehaviour
    {
        public static event PickUpsEventHandler OnPickUpEntered;
        [SerializeField] private PickUpsConfig config;
        private const string PlayerTag = "Player";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(PlayerTag))
                OnPickUpEntered?.Invoke(config, gameObject);
        }
    }
}