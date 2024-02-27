using UnityEngine;

namespace Config.Enemies
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Config/Enemies")]
    public class EnemiesConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set;}
        [field: SerializeField] public float WaitTime { get; private set;}
        [field: SerializeField] public float ChaseTime { get; private set;}
        [field: SerializeField] public float Damage { get; private set;}
    }
}