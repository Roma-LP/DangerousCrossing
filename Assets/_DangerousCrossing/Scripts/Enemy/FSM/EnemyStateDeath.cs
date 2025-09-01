using _DangerousCrossing.Scripts.StateMachineCore;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyStateDeath : FSMState
    {
        [SerializeField] private EnemyPerson _enemyPerson;
        [SerializeField] private float _delayBeforeInvokeRemove = 3f;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _enemyPerson.EnemyAnimationController.SetDeath();
            _enemyPerson.StopMoving();
            _rigidbody.isKinematic = true;
            _capsuleCollider.enabled = false;
            this.StartCoroutineUniversalWait(_delayBeforeInvokeRemove, _enemyPerson.NeedRemove);
        }
    }
}