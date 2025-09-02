using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerTransitionAttack : FSMTransition
    {
        [SerializeField] private PlayerInteractionDetector _playerInteractionDetector;

        protected override void OnEnable()
        {
            base.OnEnable();

            _playerInteractionDetector.OnEnemyDetected += OnEnemyDetectedHandler;
        }

        private void OnDisable()
        {
            _playerInteractionDetector.OnEnemyDetected -= OnEnemyDetectedHandler;
        }

        private void OnEnemyDetectedHandler()
        {
                SetNeedTransit();
        }
    }
}