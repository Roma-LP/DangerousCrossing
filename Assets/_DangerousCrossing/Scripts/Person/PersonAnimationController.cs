using UnityEngine;

namespace _DangerousCrossing.Scripts.Person
{
    public abstract class PersonAnimationController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private readonly int SPEED = Animator.StringToHash("Speed");
        private readonly int DEATH = Animator.StringToHash("Death");
        
        public void SetSpeed(Vector2 moveInput)
        {
            _animator.SetFloat(SPEED, moveInput.magnitude);
        }

        public void SetDeath()
        {
           _animator.SetTrigger(DEATH);
        }
    }
}