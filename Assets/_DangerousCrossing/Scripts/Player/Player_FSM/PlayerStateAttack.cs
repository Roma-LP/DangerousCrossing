using System;
using System.Collections;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.StateMachineCore;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerStateAttack : FSMState
    {
        [SerializeField] private PlayerPerson _playerPerson;
        [SerializeField] private PlayerAnimationController _personAnimationController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _maxScanTargets = 5;
        [SerializeField] private float _stopThreshold = 0.1f;
        [ShowInInspector, ReadOnly] private float _attackRadius;
        [ShowInInspector, ReadOnly] private float _attackCooldown;
        [ShowInInspector, ReadOnly] private int _attackDamage;

        private Collider[] _scanBuffer; 
        private Coroutine _coroutineAttack;

        private IDamageable _currentTarget;

        private void Start()
        {
            _attackRadius = _playerPerson.PlayerPersonConfig.AttackDistance;
            _attackCooldown = _playerPerson.PlayerPersonConfig.AttackCooldown;
            _attackDamage = _playerPerson.PlayerPersonConfig.AttackDamage;
        }

        private void OnEnable()
        {
            _scanBuffer = new Collider[_maxScanTargets];
            //_isAttackAnimationIsPlaying =  false;
            if (_coroutineAttack == null)
                _coroutineAttack = StartCoroutine(nameof(TryAttack));

            _personAnimationController.OnAttackMoment += AttackMoment;
            _personAnimationController.OnAttackAnimationEnd += AttackAnimationEnd;
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutineAttack);
            _coroutineAttack = null;

            _personAnimationController.OnAttackMoment -= AttackMoment;
            _personAnimationController.OnAttackAnimationEnd -= AttackAnimationEnd;
        }

        private IEnumerator TryAttack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackCooldown);

                if (_rigidbody.velocity.magnitude > _stopThreshold)
                    continue;

                if (_currentTarget == null)
                {
                    if (ScanForTarget(out _currentTarget))
                    {
                        _personAnimationController.SetAttack();
                        RotateTowardsTarget(_currentTarget.TargetTransform);
                    }
                }
                else
                {
                    _personAnimationController.SetAttack();
                    RotateTowardsTarget(_currentTarget.TargetTransform);
                }
            }
        }

        private bool ScanForTarget(out IDamageable target)
        {
            target = null;

            int hits = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _scanBuffer);

            for (int i = 0; i < hits; i++)
            {
                if (_scanBuffer[i].TryGetComponent(out EnemyPerson enemyPerson))
                {
                    if (enemyPerson.TryGetComponent(out IDamageable iDamageable))
                    {
                        if (iDamageable.CurrentHealth > 0f)
                        {
                            target = iDamageable;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void AttackMoment()
        {
            if (_currentTarget == null)
                return;
            
            _currentTarget.TakeDamage(_attackDamage);
        }

        private void AttackAnimationEnd()
        {
            if (_currentTarget != null && _currentTarget.CurrentHealth <= 0f)
            {
                _currentTarget = null;
            }
        }
        
        private void RotateTowardsTarget(Transform target, float duration = 0.3f)
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

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
#endif
    }
}