using UnityEngine;

namespace Config.PowerUps
{
    [CreateAssetMenu(fileName = "PowerUps", menuName = "Config/PowerUps")]
    public class PowerUps : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set;}
        [field: SerializeField] public Sprite PowerUp { get; private set;}
        [field: SerializeField] public float Time { get; private set;}
        [field: SerializeField] public Sprite PowerUpGlow { get; private set;}
        [field: SerializeField] public Sprite PowerUpCar { get; private set;}
    }
}
