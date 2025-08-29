using System;
using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerStateDeath : FSMState
    {
        [SerializeField] private PlayerAnimationController _playerAnimationController;
        [SerializeField] private PlayerInputHandler _playerInputHandler;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _playerInputHandler.SetMovePossible(false);
            _playerAnimationController.SetDeath();
            SetStaticState(true);
        }

        private void OnDisable()
        {
            _playerInputHandler.SetMovePossible(true);
            SetStaticState(false);
        }

        private void SetStaticState(bool value)
        {
            _rigidbody.isKinematic = value;
            _capsuleCollider.enabled = !value;
        }
    }
}