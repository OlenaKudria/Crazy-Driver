using Config.Enemies;
using UnityEngine;

namespace Enemies
{
    public delegate void EnemiesEventHandler(EnemiesConfig config);
    public class EnemiesTrigger : MonoBehaviour
    {
        public static event EnemiesEventHandler OnEnemiesEntered;
        [SerializeField] private EnemiesConfig config;
        private const string PlayerTag = "Player";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PlayerTag))
            {
                OnEnemiesEntered?.Invoke(config);
            }
        }
    }
}
