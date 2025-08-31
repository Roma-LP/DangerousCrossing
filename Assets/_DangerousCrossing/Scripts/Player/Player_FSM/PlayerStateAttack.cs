using System.Collections;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerStateAttack : FSMState
    {
        [SerializeField] private PlayerPerson _playerPerson;
        [SerializeField] private PlayerAnimationController _personAnimationController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _maxScanTargets = 5;

        [Header("Attack Settings")]
        [SerializeField] private float _attackRadius = 2f;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private float _stopThreshold = 0.1f;

        private Collider[] _scanBuffer; 
        private Coroutine _coroutineAttack;

        private IDamageable _currentTarget;

        //private bool _isAttackAnimationIsPlaying;

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
                    }
                }
                else
                {
                    _personAnimationController.SetAttack();
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

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
#endif
    }
}