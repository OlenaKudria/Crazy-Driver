using System.Collections.Generic;
using System.Linq;
using Config.Level;
using Config.Obstacles;
using Level.Road;
using UnityEngine;

namespace Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private ObstaclesConfig[] obstaclesConfigs;
        [SerializeField] private RoadConfig roadConfig;
        [SerializeField] private GameObject holderPrefab;
        [SerializeField] private float maxAmount;
        [SerializeField] private float minDistance;
        private float _accumulatedWeights;
        private float _extent;
        private Dictionary<GameObject, GameObject> _roadToHolderMap;
        private List<Vector3> _obstaclePositions;
        private System.Random _random;

        private void Start()
        {
            RoadSpawner.OnObstaclesCreation += CreateObstacles;
            RoadSpawner.OnObstaclesDestruction += DestroyObstacles;
            
            _random = new System.Random();
            _roadToHolderMap = new Dictionary<GameObject, GameObject>();
            _obstaclePositions = new List<Vector3>();
            CalculateWeights();
        }
        
        private void CalculateWeights()
        {
            _accumulatedWeights = 0f;
            foreach (ObstaclesConfig obstacle in obstaclesConfigs)
            {
                _accumulatedWeights += obstacle.Probability;
                obstacle.Weight = _accumulatedWeights;
            }
        }
        
        private void CreateObstacles(GameObject road, float offset)
        {
            _extent = offset / 2f;
            CreateHolder(road);
            GameObject holder = _roadToHolderMap[road];
            
            for (int i = 0; i < maxAmount; i++)
            {
                Vector3 randomPosition;

                do
                {
                    randomPosition = new Vector3(
                        Random.Range(-roadConfig.RoadRange, roadConfig.RoadRange),
                        Random.Range(-_extent, _extent),
                        -1);

                } while (_obstaclePositions.Any(pos => Vector3.Distance(randomPosition, pos) < minDistance));
                
                SpawnRandomObstacle(randomPosition, holder);
                _obstaclePositions.Add(randomPosition);
            }
            _obstaclePositions.Clear();
        }
        
        private void CreateHolder(GameObject road)
        {
            GameObject holder = Instantiate(holderPrefab, road.transform.position, Quaternion.identity, road.transform);
            _roadToHolderMap.Add(road, holder);
        }

        private void SpawnRandomObstacle(Vector3 randomPosition, GameObject holder)
        {
            int index = GetRandomObstacleIndex();
            GameObject obstacle = obstaclesConfigs[index].Prefab;
            Instantiate(obstacle, holder.transform.position + randomPosition, Quaternion.identity, holder.transform);
        }

        private int GetRandomObstacleIndex()
        {
            var randomNumber = _random.NextDouble() * _accumulatedWeights;
            for (int i = 0; i < obstaclesConfigs.Length; i++)
                if (obstaclesConfigs[i].Weight >= randomNumber)
                    return i;
            
            return 0;
        }
        
        private void DestroyObstacles(GameObject road)
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
            RoadSpawner.OnObstaclesCreation -= CreateObstacles;
            RoadSpawner.OnObstaclesDestruction -= DestroyObstacles;
        }
    }
}