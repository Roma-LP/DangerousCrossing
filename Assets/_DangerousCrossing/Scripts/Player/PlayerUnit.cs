using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler _playerInputHandler;
        [SerializeField] private PersonAnimationController _personAnimationController;

        public void Init(IInputReader input)
        {
            _playerInputHandler.Init(input);
        }

        private void Update()
        {
            _personAnimationController.SetSpeed(_playerInputHandler.InputReader.Direction);
        }

        private void FixedUpdate()
        {
            _playerInputHandler.UpdateInput();
        }
    }
}