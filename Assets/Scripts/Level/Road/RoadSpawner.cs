using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level.Road
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> roads;
        [SerializeField] private SpriteRenderer roadPart;
        private float _offset;
        
        private void Start()
        {
            RoadTrigger.OnRoadEntered += HandleRoadEntered;
            ReorderList();
            GetOffset();
        }

        private void ReorderList()
        {
            if (roads is { Count: > 0 })
                roads = roads.OrderBy(p => p.transform.position.y).ToList();
        }

        private void GetOffset()
        {
            if (roadPart != null)
            { 
                roadPart.TryGetComponent(out SpriteRenderer spriteRenderer);
                _offset = spriteRenderer.bounds.size.y;
            }
        }
        
        private void HandleRoadEntered()
        {
            GameObject movedRoad = roads[0];
            roads.Remove(movedRoad);
            float position = roads[^1].transform.position.y + _offset;
            movedRoad.transform.position = new Vector3(0f, position, 0f);
            roads.Add(movedRoad);
        }
        
        private void OnDestroy() => RoadTrigger.OnRoadEntered -= HandleRoadEntered;
    }
}