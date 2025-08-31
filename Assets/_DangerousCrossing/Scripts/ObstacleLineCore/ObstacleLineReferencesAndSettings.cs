using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enums;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ObstacleLineCore
{
    public class ObstacleLineReferencesAndSettings : MonoBehaviour
    {
        [Header("ObstacleInstances")]
        [SerializeField] private List<ObstacleInstance> _obstacleInstances;
        
        [Header("References")]
        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;
        [SerializeField] private Transform _container;
        
        [Header("Settings")]
        [SerializeField] private ObstacleLineDirection obstacleLineDirection = ObstacleLineDirection.LeftToRight;
        [SerializeField] private int _poolSize = 5;
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _moveDuration = 5f;

        public List<ObstacleInstance> ObstacleInstances => _obstacleInstances;
        
        public Transform LeftPoint => _leftPoint;
        public Transform RightPoint => _rightPoint;
        public Transform Container => _container;

        public ObstacleLineDirection ObstacleLineDirection => obstacleLineDirection;
        public int PoolSize => _poolSize;
        public float SpawnInterval => _spawnInterval;
        public float MoveDuration => _moveDuration;
    }
}