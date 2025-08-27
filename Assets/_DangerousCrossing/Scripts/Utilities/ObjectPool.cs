using System.Collections.Generic;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public class ObjectPool<T> where T : Component
    {
        private Queue<T> _pool = new Queue<T>();
        private List<T> _prefabs;
        private Transform _parent;

        public ObjectPool(List<T> prefabs, int initialCount, Transform parent)
        {
            _prefabs = prefabs;
            _parent = parent;
            
            for (int i = 0; i < initialCount; i++)
            {
                var prefab = GetRandomPrefab();
                T obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }
        
        private T GetRandomPrefab()
        {
            //TODO if (_prefabs == null || _prefabs.Count == 0)
            //{
            //    Debug.LogError("ObjectPool: список префабов пуст!");
            //    return null;
            //}
            return _prefabs[Random.Range(0, _prefabs.Count)];
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var prefab = GetRandomPrefab();
                T obj = GameObject.Instantiate(prefab, _parent);
                return obj;
            }
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}