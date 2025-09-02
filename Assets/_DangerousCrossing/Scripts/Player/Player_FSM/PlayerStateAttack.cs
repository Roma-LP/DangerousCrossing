using System.Collections;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.StateMachineCore;
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
        [SerializeField] private float _timeToWaitNextFrame = 0.1f;
        [ShowInInspector, ReadOnly] private float _attackRadius;
        [ShowInInspector, ReadOnly] private float _attackCooldown;
        [ShowInInspector, ReadOnly] private int _attackDamage;

        private Collider[] _scanBuffer;
        private Coroutine _coroutineAttack;
        private WaitForSeconds _waitCooldown;
        private WaitForSeconds _waitNextFrame;

        private IDamageable _currentTarget;

        private void Start()
        {
            _attackRadius = _playerPerson.PlayerPersonConfig.AttackDistance;
            _attackCooldown = _playerPerson.PlayerPersonConfig.AttackCooldown;
            _attackDamage = _playerPerson.PlayerPersonConfig.AttackDamage;

            _waitCooldown = new WaitForSeconds(_attackCooldown);
            _waitNextFrame = new WaitForSeconds(_timeToWaitNextFrame);
        }

        private void OnEnable()
        {
            _scanBuffer = new Collider[_maxScanTargets];
            if (_coroutineAttack == null)
                _coroutineAttack = StartCoroutine(nameof(TryAttack));

            _personAnimationController.OnAttackMoment += AttackMoment;
            _personAnimationController.OnAttackAnimationEnd += AttackAnimationEnd;
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutineAttack);
            _coroutineAttack = null;
            _currentTarget = null;

            _personAnimationController.OnAttackMoment -= AttackMoment;
            _personAnimationController.OnAttackAnimationEnd -= AttackAnimationEnd;
        }

        private IEnumerator TryAttack()
        {
            while (true)
            {
                if (_rigidbody.velocity.magnitude <= _stopThreshold)
                {
                    if (_currentTarget == null)
                    {
                        if (ScanForTarget(out _currentTarget))
                        {
                            StartAttack();
                            yield return _waitCooldown;
                        }
                        else
                        {
                            yield return _waitNextFrame;
                        }
                    }
                    else
                    {
                        StartAttack();
                        yield return _waitCooldown;
                    }
                }
                else
                {
                    yield return _waitNextFrame;
                }
            }
        }

        private void StartAttack()
        {
            _personAnimationController.SetAttack();
            _playerPerson.RotateTowardsTarget(_currentTarget.TargetTransform);
        }

        private bool ScanForTarget(out IDamageable target)
        {
            target = null;

            int hits = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _scanBuffer);

            for (int i = 0; i < hits; i++)
            {
                if (_scanBuffer[i].TryGetComponent(out IDamageable iDamageable))
                {
                    if (iDamageable == _playerPerson)
                        continue;

                    if (iDamageable.CurrentHealth > 0f)
                    {
                        target = iDamageable;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsCurrentTargetDied()
        {
            return _currentTarget != null && _currentTarget.CurrentHealth <= 0f;
        }

        private void AttackMoment()
        {
            if (IsCurrentTargetDied())
                return;

            _currentTarget.TakeDamage(_attackDamage);
        }

        private void AttackAnimationEnd()
        {
            if (IsCurrentTargetDied())
                _currentTarget = null;
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