using Config.Player;
using UnityEngine;

namespace Player
{
    public class SpeedManager : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;

        public void SlowDown(float newMaxSpeed)
        {
            playerConfig.MaxSpeed = newMaxSpeed;
        }
        
        public void SpeedUp(float newMaxSpeed, float newMinSpeed)
        {
            
        }
    }
}
