using System;
using _DangerousCrossing.Scripts.Interfaces;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Person
{
    public abstract class PersonBase : MonoBehaviour, IDamageable
    {
        [ShowInInspector, ReadOnly] protected float _currentHealth;
        [ShowInInspector, ReadOnly] private float _maxHealth;
        [SerializeField] private HealthBarUI _healthBarUI;
        //[SerializeField] private HitFlashEffect _hitFlashEffect;

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

                if (_currentHealth == 0)
                    OnHealthZero?.Invoke();
                
                _healthBarUI.SetHealth(_currentHealth, _maxHealth);
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

        protected virtual void InitHealthBar(Transform cameraTransform)
        {
            _healthBarUI.Init(cameraTransform);
        }
        
        protected virtual void ResetCurrentHealth(float currentHealth, float maxHealth)
        {
            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
            _healthBarUI.SetHealth(_currentHealth, _maxHealth, false);
        }

        protected virtual void LateUpdate()
        {
            _healthBarUI.OnLateUpdate();
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
            //CurrentHealth = 0;
            //OnHealthZero?.Invoke();
            TakeDamage(CurrentHealth);
        }
        
        public void RotateTowardsTarget(Transform target, float duration = 0.2f)
        {
            if (target == null) return;
        
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
        
            if (direction != Vector3.zero)
            {
                transform.DOLookAt(transform.position + direction, duration)
                    .SetEase(Ease.OutQuad).SetLink(gameObject);
            }
        }
    }
}