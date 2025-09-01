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

        public event Action OnAllEnemiesDied;

        public void Init(PlayerPerson playerPerson, Transform cameraTransform)
        {
            _enemySpawnLinks = new EnemySpawnLinks(playerPerson, _enemyPersonConfig, cameraTransform);
        }

        protected override void OnRemovableHandler(EnemyPerson spawnedObject)
        {
            base.OnRemovableHandler(spawnedObject);

            if (_spawnedObjects.Count == 0)
            {
                OnAllEnemiesDied?.Invoke();
            }
        }

        public override void SpawnPerson()
        {
            SpawnFromConfig(_enemySpawnLinks);
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