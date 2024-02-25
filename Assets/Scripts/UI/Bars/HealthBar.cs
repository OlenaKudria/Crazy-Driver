using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image barSprite;

        public void UpdateHealthBar(float maxHealth, float currentHealth) =>
            barSprite.fillAmount = currentHealth / maxHealth;
    }
}
