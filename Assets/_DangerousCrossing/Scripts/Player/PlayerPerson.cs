using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Person;
using _DangerousCrossing.Scripts.Player.Player_FSM;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerPerson : PersonBase
    {
        [SerializeField] private PlayerInputHandler _playerInputHandler;
        [SerializeField] private PlayerAnimationController _personAnimationController;
        [SerializeField] private PlayerFSM _playerFsm;

        public void Init(IInputReader input, Transform cameraTransform)
        {
            _playerInputHandler.Init(input,  cameraTransform);
        }

        public void SpawnPlayer()
        {
            SetInitialHealth();
            _playerFsm.StartFSM();
            _personAnimationController.SetRespawn();
        }

        private void Update()
        {
            _personAnimationController.SetSpeed(_playerInputHandler.InputReader.Direction);
            _playerFsm.UpdateFSM();
        }

        private void FixedUpdate()
        {
            _playerInputHandler.UpdateInput();
        }
    }
}