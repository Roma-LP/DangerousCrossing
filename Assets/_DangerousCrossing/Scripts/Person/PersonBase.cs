using System;
using _DangerousCrossing.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Person
{
    public abstract class PersonBase : MonoBehaviour, IDamageable
    {
        [ShowInInspector, ReadOnly] protected float _currentHealth;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Transform _pivotUI;
        //[SerializeField] private HitFlashEffect _hitFlashEffect;

        //private HealthBarUI _healthBarUI;
        //private WorldToUIFollower _worldToUIFollower;

        public event Action<float, float> OnHealthChanged;
        public event Action<float> OnTakeDamage;
        public event Action OnHealthZero;

        public float CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

                OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
               // _healthBarUI.SetHealth(_currentHealth);
            }
        }

        public float MaxHealth => _maxHealth;
        public Transform TargetTransform => transform;

        protected virtual void Awake()
        {
            //_hitFlashEffect.Init();
            //_healthBarUI = SceneContext.Instance.WorldHpBarSpawner.CreateHpBar(_currentHealth, _maxHealth);
            //_worldToUIFollower = new WorldToUIFollower(_healthBarUI.RectTransformToMove, _pivotUI);
        }
        
        protected virtual void SetInitialHealth()
        {
            _currentHealth = _maxHealth;
        }

        protected virtual void LateUpdate()
        {
            //_worldToUIFollower.LateUpdateUIFollower();
        }

        protected virtual void OnDestroy()
        {
           // if (_healthBarUI != null)
               // Destroy(_healthBarUI.gameObject);
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
            OnTakeDamage?.Invoke(amount);
        }

        public void TakeHealthZero()
        {
            CurrentHealth = 0;
            OnHealthZero?.Invoke();
        }
    }
}