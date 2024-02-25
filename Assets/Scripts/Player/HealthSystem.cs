using Config.Player;
using UI.Bars;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Damage(25);
                Debug.Log(_currentHealth);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                Heal(25);
                Debug.Log(_currentHealth);
            }
        }
        
        private void Damage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth < 0) _currentHealth = 0;
            bar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        
        private void Heal(float amount)
        {
            _currentHealth += amount;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
            bar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }
}
