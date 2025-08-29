using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerTransitionDeath : FSMTransition
    {
        [SerializeField] private PlayerPerson _playerPerson;

        protected override void OnEnable()
        {
            base.OnEnable();

            _playerPerson.OnHealthZero += HealthZeroHandler;
        }

        private void OnDisable()
        {
            _playerPerson.OnHealthZero -= HealthZeroHandler;
        }

        private void HealthZeroHandler()
        {
            SetNeedTransit();
        }
    }
}