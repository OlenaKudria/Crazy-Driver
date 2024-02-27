using System;
using System.Collections.Generic;
using System.Linq;
using Config.Level;
using Config.PickUps;
using Level.Road;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PickUps
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField] private PickUpsConfig[] pickUpsConfigs;
        [SerializeField] private RoadConfig roadConfig;
        [SerializeField] private GameObject holderPrefab;
        [SerializeField] private float minDistance;
        private float _accumulatedWeights;
        private float _extent;
        private Dictionary<GameObject, GameObject> _roadToHolderMap;
        private List<Vector3> _puckUpsPositions;
        private System.Random _random;

        private void Start()
        {
            RoadSpawner.OnPickUpsCreation += CreatePickUps;
            RoadSpawner.OnPickUpsDestruction += DestroyPickUps;
            
            _random = new System.Random();
            _roadToHolderMap = new Dictionary<GameObject, GameObject>();
            _puckUpsPositions = new List<Vector3>();
            CalculateWeights();
        }
        
        private void CalculateWeights()
        {
            _accumulatedWeights = 0f;
            foreach (PickUpsConfig pickUp in pickUpsConfigs)
            {
                _accumulatedWeights += pickUp.Probability;
                pickUp.Weight = _accumulatedWeights;
            }
        }

        private void CreatePickUps(GameObject road, float offset)
        {
            _extent = offset / 2f;
            CreateHolder(road);
            GameObject holder = _roadToHolderMap[road];
            PickUpsConfig pickUp = ChooseRandomPickUp();

            for (int i = 0; i < pickUp.MaxAmount; i++)
            {
                Vector3 randomPosition;
                
                do
                {
                    randomPosition = new Vector3(
                        Random.Range(-roadConfig.RoadRange, roadConfig.RoadRange),
                        Random.Range(-_extent, _extent),
                        -2);

                } while (_puckUpsPositions.Any(pos => Vector3.Distance(randomPosition, pos) < minDistance));
                
                SpawnPickUps(randomPosition, holder, pickUp.Prefab);
                _puckUpsPositions.Add(randomPosition);
            }
            _puckUpsPositions.Clear();
        }

        private PickUpsConfig ChooseRandomPickUp()
        {
            int index = GetRandomObstacleIndex();
            PickUpsConfig pickUp = pickUpsConfigs[index];
            
            return pickUp;
        }
        
        private int GetRandomObstacleIndex()
        {
            var randomNumber = _random.NextDouble() * _accumulatedWeights;
            for (int i = 0; i < pickUpsConfigs.Length; i++)
                if (pickUpsConfigs[i].Weight >= randomNumber)
                    return i;
            
            return 0;
        }
        
        private void CreateHolder(GameObject road)
        {
            GameObject holder = Instantiate(holderPrefab, road.transform.position, Quaternion.identity, road.transform);
            _roadToHolderMap.Add(road, holder);
        }
        
        private void SpawnPickUps(Vector3 randomPosition, GameObject holder, GameObject pickUp) =>
            Instantiate(pickUp, holder.transform.position + randomPosition, Quaternion.identity, holder.transform);
        
        private void DestroyPickUps(GameObject road)
        {
            if (_roadToHolderMap.ContainsKey(road))
            {
                GameObject holder = _roadToHolderMap[road];
                Destroy(holder.gameObject);
                _roadToHolderMap.Remove(road);
            }
        }
        
        private void OnDestroy()
        {
            RoadSpawner.OnPickUpsCreation -= CreatePickUps;
            RoadSpawner.OnPickUpsDestruction -= DestroyPickUps;
        }
    }
}