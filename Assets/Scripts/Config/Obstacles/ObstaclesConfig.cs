using UnityEngine;

namespace Config.Obstacles
{
    [CreateAssetMenu(fileName = "Obstacles", menuName = "Config/Obstacles")]
    public class ObstaclesConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set;}
        [field: SerializeField] public float Time { get; private set;}
        [field: SerializeField] public float Damage { get; private set;}
        [field: Range(0f, 100f)] [field: SerializeField] public float Probability { get; private set;}
        [field: SerializeField] public float Weight { get; set;}
    }
}
