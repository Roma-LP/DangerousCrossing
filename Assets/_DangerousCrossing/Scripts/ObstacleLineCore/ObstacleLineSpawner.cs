using System.Collections;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ObstacleLineCore
{
    public class ObstacleLineSpawner
    {
        private ObjectPool<ObstacleInstance> _pool;
        private Coroutine _spawnRoutine;
        private ObstacleLineReferencesAndSettings _obstacleLineReferencesAndSettings;
        private WaitForSeconds _waitForSecondsspawnInterval;
        private Transform _startPoint;
        private Transform _endPoint;

        public ObstacleLineSpawner(ObstacleLineReferencesAndSettings obstacleLineReferencesAndSettings)
        {
            _obstacleLineReferencesAndSettings = obstacleLineReferencesAndSettings;
            
            Init();
        }

        private void Init()
        {
            _pool = new ObjectPool<ObstacleInstance>(_obstacleLineReferencesAndSettings.ObstacleInstances, _obstacleLineReferencesAndSettings.PoolSize, _obstacleLineReferencesAndSettings.Container);
            _waitForSecondsspawnInterval = new WaitForSeconds(_obstacleLineReferencesAndSettings.SpawnInterval);
            SetStartEndPoints();
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnPedestrian();
                yield return _waitForSecondsspawnInterval;
            }
        }

        private void SpawnPedestrian()
        {
            ObstacleInstance ped = _pool.Get();
            ped.SetPool(_pool);

            ped.transform.position = _startPoint.position;

            ped.transform
                .DOMove(_endPoint.position, _obstacleLineReferencesAndSettings.MoveDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => ped.Despawn());
        }

        private void SetStartEndPoints()
        {
            switch (_obstacleLineReferencesAndSettings.LineDirection)
            {
                case LineDirection.LeftToRight:
                    _startPoint = _obstacleLineReferencesAndSettings.LeftPoint;
                    _endPoint = _obstacleLineReferencesAndSettings.RightPoint;
                    break;
                case LineDirection.RightToLeft:
                    _startPoint = _obstacleLineReferencesAndSettings.RightPoint;
                    _endPoint = _obstacleLineReferencesAndSettings.LeftPoint;
                    break;
            }
        }

        public void LaunchLine()
        {
            _spawnRoutine = _obstacleLineReferencesAndSettings.StartCoroutine(SpawnLoop());
        }
    }
}