using UnityEngine;

namespace Level.Road
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform startPoint;

        public Vector3 EndPointPosition => endPoint.position;
        
        public Vector3 StartPointPosition => startPoint.position;
    }
}
