using System;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        Transform TargetTransform { get; }

        event Action<float, float> OnHealthChanged; // (currentHealth, maxHealth)
        event Action<float> OnTakeDamage;
        event Action OnHealthZero;

        void TakeDamage(float amount);
        void TakeHealthZero();
    }
}