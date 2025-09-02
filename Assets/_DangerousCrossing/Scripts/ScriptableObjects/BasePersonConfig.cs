using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ScriptableObjects
{
    public abstract class BasePersonConfig: ScriptableObject
    {
        [Header("Health")]
        [SerializeField, MinValue(1)] private int _currentHealth = 100;
        [SerializeField, MinValue(1)] private int _maxHealth = 100;
        [Header("Attack")]
        [SerializeField, MinValue(1)] private float _attackCooldown = 3;
        [SerializeField, MinValue(1)] private int _attackDamage = 20;
        [SerializeField, MinValue(0.2)] private float _attackDistance = 0.8f;
        [Header("Movement")]
        [SerializeField, MinValue(1)] private int _movementSpeed = 3;
        [SerializeField, MinValue(1)] private int _movementRotation = 10;

        public int MovementSpeed => _movementSpeed;
        public int MovementRotation => _movementRotation;
        public float AttackDistance => _attackDistance;
        public int AttackDamage => _attackDamage;
        public float AttackCooldown => _attackCooldown;
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
    }
}