using _DangerousCrossing.Scripts.UI;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler _playerInputHandler;
        [SerializeField] private PersonAnimationController _personAnimationController;
        [SerializeField] private TouchInputReader _inputSource;

        private void Awake()
        {
            _playerInputHandler.Init(_inputSource);
        }

        private void Update()
        {
            _personAnimationController.SetSpeed(_inputSource.Direction);
        }

        private void FixedUpdate()
        {
            _playerInputHandler.UpdateInput();
        }
    }
}