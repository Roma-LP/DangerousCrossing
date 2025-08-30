using System.Collections.Generic;
using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public abstract class SpawnerBase<T,P> : MonoBehaviour where T : MonoBehaviour, ISpawnable<P>
    {
        [SerializeField] private T _spawnObject;
        [SerializeField] private Transform _container;
        [SerializeField, Range(1, 10)] private int _spawnCount = 2;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private bool _isUseRandomSpawnPoints;

        protected List<T> _spawnedObjects = new List<T>();
        
        public abstract void SpawnPerson();

        protected void SpawnFromConfig(P linksFowSpawned)
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                Transform spawnPoint = _isUseRandomSpawnPoints
                    ? _spawnPoints[Random.Range(0, _spawnPoints.Count)]
                    : _spawnPoints[i % _spawnPoints.Count];

                T instance = Instantiate(_spawnObject, spawnPoint.position, Quaternion.identity, _container);
                instance.OnSpawned(linksFowSpawned);
                _spawnedObjects.Add(instance);
                
                if(instance.TryGetComponent(out IRemovable<T> removableObject))
                {
                    removableObject.OnRemoveble += OnRemovableHandler;
                }
            }
        }

        protected virtual void OnRemovableHandler(T spawnedObject)
        {
            _spawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject.gameObject);
        }
    }
}