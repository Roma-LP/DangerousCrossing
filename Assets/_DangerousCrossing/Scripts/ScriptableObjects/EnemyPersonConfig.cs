using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyPersonConfig", menuName = "Setups/PersonConfig/EnemyPersonConfig")]
    public class EnemyPersonConfig : BasePersonConfig
    {
        [Header("Settings")]
        [SerializeField, MinValue(1)] private float _delayBeforeTransitInAttack = 3f;
        [SerializeField, MinValue(1)] private float _delayBeforeInvokeRemoveInDeath = 3f;

        public float DelayBeforeInvokeRemoveInDeath => _delayBeforeInvokeRemoveInDeath;
        public float DelayBeforeTransitInAttack => _delayBeforeTransitInAttack;
    }
}