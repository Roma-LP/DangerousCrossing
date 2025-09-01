using System;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        Transform TargetTransform { get; }

        /// <summary>
        /// Action&lt; currentHealth, maxHealth &gt;.
        /// </summary>
        event Action<float, float> OnHealthChanged;
        event Action<float> OnTakeDamage;
        event Action OnHealthZero;

        void TakeDamage(float amount);
        void TakeHealthZero();
    }
}