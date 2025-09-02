using System;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.ScriptableObjects;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemySpawnContext : SpawnerBase<EnemyPerson, EnemySpawnLinks>
    {
        [SerializeField] private EnemyPersonConfig _enemyPersonConfig;

        private EnemySpawnLinks _enemySpawnLinks;
        private Transform _playerTransform;

        public event Action OnAllEnemiesDied;

        public void Init(PlayerPerson playerPerson, Transform cameraTransform)
        {
            _enemySpawnLinks = new EnemySpawnLinks(playerPerson, _enemyPersonConfig, cameraTransform);
            _playerTransform = playerPerson.transform;
        }

        protected override void OnRemovableHandler(EnemyPerson spawnedObject)
        {
            base.OnRemovableHandler(spawnedObject);

            if (_spawnedObjects.Count == 0)
            {
                OnAllEnemiesDied?.Invoke();
            }
        }

        private void TurnEnemiesOnThePlayer()
        {
            for (var i = 0; i < _spawnedObjects.Count; i++)
            {
                Vector3 directionToPlayer = _playerTransform.position - _spawnedObjects[i].transform.position;
                directionToPlayer.y = 0;
                _spawnedObjects[i].transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }

        public override void SpawnPerson()
        {
            SpawnFromConfig(_enemySpawnLinks);
            TurnEnemiesOnThePlayer();
        }

        public void DeSpawnPerson()
        {
            for (var i = 0; i < _spawnedObjects.Count; i++)
            {
                if (_spawnedObjects[i] != null)
                    Destroy(_spawnedObjects[i].gameObject);
            }

            _spawnedObjects.Clear();
        }
    }
}