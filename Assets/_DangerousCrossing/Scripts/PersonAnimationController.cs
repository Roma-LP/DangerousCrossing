using UnityEngine;

namespace _DangerousCrossing.Scripts
{
    public abstract class PersonAnimationController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private readonly int SPEED = Animator.StringToHash("Speed");
        
        public void SetSpeed(Vector2 moveInput)
        {
            _animator.SetFloat(SPEED, moveInput.magnitude);
        }
    }
}