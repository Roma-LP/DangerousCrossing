using System;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Person
{
    public abstract class PersonAnimationController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private readonly int SPEED = Animator.StringToHash("Speed");
        private readonly int DEATH = Animator.StringToHash("Death");
        private readonly int ATTACK = Animator.StringToHash("Attack");
        
        public event Action OnAttackMoment;
        public event Action OnAttackAnimationEnd;
        
        public void SetSpeed(Vector2 moveInput)
        {
            _animator.SetFloat(SPEED, moveInput.magnitude);
        }

        public void SetDeath()
        {
           _animator.SetTrigger(DEATH);
        }
        
        public void SetAttack()
        {
            _animator.SetTrigger(ATTACK);
        }

        public void TriggerAttackMoment()
        {
            OnAttackMoment?.Invoke();
        }
        
        public void TriggerAttackAnimationEnd()
        {
            OnAttackAnimationEnd?.Invoke();
        }
    }
}