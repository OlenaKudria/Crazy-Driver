using UnityEngine;

namespace Config.Level
{
    [CreateAssetMenu(fileName = "Road", menuName = "Config/Road")]
    public class RoadConfig : ScriptableObject
    {
        public GameObject road;
        public float roadRange;
        public float pavementRange;
    }
}
