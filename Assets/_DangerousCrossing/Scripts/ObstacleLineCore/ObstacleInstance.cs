using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ObstacleLineCore
{
    public class ObstacleInstance : MonoBehaviour
    {
        private ObjectPool<ObstacleInstance> _pool;

        public void SetPool(ObjectPool<ObstacleInstance> pool)
        {
            _pool = pool;
        }

        public void Despawn()
        {
            _pool.ReturnToPool(this);
        }
    }
}