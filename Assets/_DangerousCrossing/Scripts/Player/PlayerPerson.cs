using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Person;
using _DangerousCrossing.Scripts.Player.Player_FSM;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerPerson : PersonBase
    {
        [SerializeField] private PlayerInputHandler _playerInputHandler;
        [SerializeField] private PlayerAnimationController _personAnimationController;
        [SerializeField] private PlayerFSM _playerFsm;

        public PlayerPersonConfig PlayerPersonConfig { get; private set; }

        public void Init(IInputReader input, Transform cameraTransform, PlayerPersonConfig playerPersonConfig)
        {
            PlayerPersonConfig = playerPersonConfig;
            _playerInputHandler.Init(input,  cameraTransform, PlayerPersonConfig.MovementSpeed, playerPersonConfig.MovementRotation);
            InitHealthBar(cameraTransform);
        }

        public void OnSpawnedPlayer()
        {
            _playerFsm.StartFSM();
            _personAnimationController.SetRespawn();
            ResetCurrentHealth(PlayerPersonConfig.CurrentHealth, PlayerPersonConfig.MaxHealth);
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