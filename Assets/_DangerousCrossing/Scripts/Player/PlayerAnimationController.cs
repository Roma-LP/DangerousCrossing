using _DangerousCrossing.Scripts.Person;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerAnimationController : PersonAnimationController
    {
        private readonly int RESPAWN = Animator.StringToHash("Respawn");

        public void SetRespawn()
        {
            _animator.SetTrigger(RESPAWN);
        }
    }
}