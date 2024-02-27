using Config.PickUps;
using Player;
using UnityEngine;

namespace PickUps
{
    public class PickUpsHandler : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private ScoreSystem scoreSystem;

        private void Start()
        {
            PickUpsTrigger.OnPickUpEntered += HandlePickUpEntered;
        }

        private void HandlePickUpEntered(PickUpsConfig config, GameObject prefab)
        {
            if(config.Health > 0 )
                healthSystem.Heal(config.Health);

            if (config.Value > 0)
                scoreSystem.UpdateScore(config.Value);
            
            Destroy(prefab);
        }

        private void OnDestroy()
        {
            PickUpsTrigger.OnPickUpEntered -= HandlePickUpEntered;
        }
    }
}