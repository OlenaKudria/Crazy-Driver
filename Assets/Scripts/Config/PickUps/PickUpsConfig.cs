using UnityEngine;

namespace Config.PickUps
{
    [CreateAssetMenu(fileName = "PickUps", menuName = "Config/PickUps")]
    public class PickUpsConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set;}
        [field: SerializeField] public float Health { get; private set;}
        [field: SerializeField] public int Value { get; private set;}
        [field: SerializeField] public float MaxAmount { get; private set;}
        [field: Range(0f, 100f)] [field: SerializeField] public float Probability { get; private set;}
        [field: SerializeField] public float Weight { get; set;}
    }
}
