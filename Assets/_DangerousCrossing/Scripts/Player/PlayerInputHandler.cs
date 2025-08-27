using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Rigidbody _rigidbody;
        
        private IInputReader _input;
        private Vector2 _moveInput;
        
        public IInputReader InputReader => _input;

        public void Init(IInputReader input)
        {
            _input = input;

            if (_input == null)
                Debug.LogError("InputSource не реализует IPlayerInput!");
        }

        public void UpdateInput()
        {
            Vector2 inputDir = _input.Direction;
            Vector3 move = new Vector3(inputDir.x, 0, inputDir.y);

            Vector3 velocity = move.normalized * _speed;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
    }
}