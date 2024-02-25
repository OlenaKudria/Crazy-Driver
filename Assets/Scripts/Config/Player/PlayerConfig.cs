using UnityEngine;

namespace Config.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "Config/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public float DefaultSpeed { get; private set;}
        [field: SerializeField] public float TurnSpeed { get; private set;}
        [field: SerializeField] public float MaxSpeed { get; private set;}
        [field: SerializeField] public float MinSpeed { get; private set;}
        [field: SerializeField] public float StepSpeed { get; private set;}
        [field: SerializeField] public float MaxHealth { get; private set;}
    }
}