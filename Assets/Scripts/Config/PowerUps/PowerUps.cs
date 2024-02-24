using UnityEngine;

namespace Config.PowerUps
{
    [CreateAssetMenu(fileName = "PowerUps", menuName = "Config/PowerUps")]
    public class PowerUps : ScriptableObject
    {
        public Sprite powerUp;
        public Sprite powerUpGlow;
        public Sprite powerUpCar;
    }
}
