using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerTransitionEnemyDetection : FSMTransition
    {
        private enum DetectionState
        {
            NoEnemiesDetected,
            EnemiesDetected
        }

        [SerializeField] private PlayerInteractionDetector _playerInteractionDetector;
        [SerializeField] private DetectionState detectionState;

        protected override void OnEnable()
        {
            base.OnEnable();

            _playerInteractionDetector.OnEnemyDetected += OnEnemyDetectedHandler;

            OnEnemyDetectedHandler(_playerInteractionDetector.EnemyCountDetected);
        }

        private void OnDisable()
        {
            _playerInteractionDetector.OnEnemyDetected -= OnEnemyDetectedHandler;
        }

        private void OnEnemyDetectedHandler(int enemyCount)
        {
            if (Compare(enemyCount, detectionState))
            {
                SetNeedTransit();
            }
        }

        private bool Compare(int enemyCount, DetectionState comparison)
        {
            switch (comparison)
            {
                case DetectionState.NoEnemiesDetected:
                    return enemyCount == 0;
                case DetectionState.EnemiesDetected:
                    return enemyCount > 0;
                default:
                    return false;
            }
        }
    }
}