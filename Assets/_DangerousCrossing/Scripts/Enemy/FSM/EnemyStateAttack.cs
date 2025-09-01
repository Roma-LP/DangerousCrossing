using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyStateAttack : FSMState
    {
        [SerializeField] private EnemyPerson _enemyPerson;

        private float _attackDistance;
        private float _attackCooldown;
        private float _attackDamage;

        private float _cooldownTimer;
        private bool _isAttackAnimationIsPlaying;
        private IDamageable _damageablePerson;

        private void OnEnable()
        {
            _cooldownTimer = 0;
            _isAttackAnimationIsPlaying = false;

            _attackDistance = _enemyPerson.EnemySpawnLinks.EnemyPersonConfig.AttackDistance;
            _attackCooldown = _enemyPerson.EnemySpawnLinks.EnemyPersonConfig.AttackCooldown;
            _attackDamage = _enemyPerson.EnemySpawnLinks.EnemyPersonConfig.AttackDamage;

            _damageablePerson = _enemyPerson.PlayerPerson.GetComponent<IDamageable>();

            _enemyPerson.EnemyAnimationController.OnAttackAnimationEnd += AttackEnemyAnimationControllerEndHandler;
            _enemyPerson.EnemyAnimationController.OnAttackMoment += AttackMomentHandler;
        }

        private void OnDisable()
        {
            _enemyPerson.EnemyAnimationController.OnAttackAnimationEnd -= AttackEnemyAnimationControllerEndHandler;
            _enemyPerson.EnemyAnimationController.OnAttackMoment -= AttackMomentHandler;
        }

        public override void UpdateState()
        {
            if (_isAttackAnimationIsPlaying)
                return;

            if (IsAttackDistance())
            {
                _enemyPerson.StopMoving();

                if (_cooldownTimer <= 0f)
                {
                    StartAttack();
                }
                else
                {
                    _cooldownTimer -= Time.deltaTime;
                }
            }
            else
            {
                _enemyPerson.MoveTo(_enemyPerson.PlayerPerson.transform.position);
            }
        }

        private bool IsAttackDistance()
        {
            float distance = Vector3.Distance(_enemyPerson.transform.position,
                _enemyPerson.PlayerPerson.transform.position);

            //Debug.Log($"dist: {distance}");

            return distance <= _attackDistance;
        }

        private void StartAttack()
        {
            _isAttackAnimationIsPlaying = true;
            _enemyPerson.EnemyAnimationController.SetAttack();
        }

        private void AttackEnemyAnimationControllerEndHandler()
        {
            _cooldownTimer = _attackCooldown;
            _isAttackAnimationIsPlaying = false;
        }

        private void AttackMomentHandler()
        {
            if (IsAttackDistance() == false)
                return;

            _damageablePerson.TakeDamage(_attackDamage);
        }
    }
}