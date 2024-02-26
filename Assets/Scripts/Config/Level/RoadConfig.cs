using UnityEngine;

namespace Config.Level
{
    [CreateAssetMenu(fileName = "Road", menuName = "Config/Road")]
    public class RoadConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Road { get; private set; }
        [field: SerializeField] public float RoadRange { get; private set; }
        [field: SerializeField] public float PavementRange { get; private set; }
    }
}
