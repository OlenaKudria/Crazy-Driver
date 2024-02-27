using Config.Player;
using UI.Bars;
using UI.Panels;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private HealthBar bar;
        private float _currentHealth;
        private float _maxHealth;

        private void Awake()
        {
            _maxHealth = playerConfig.MaxHealth;
            _currentHealth = _maxHealth;
        }
        
        private void Start()
        {
            bar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        
        public void Damage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                uiManager.ShowFinishMenu(true);
            }
            bar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        
        public void Heal(float amount)
        {
            _currentHealth += amount;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
            bar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }
}