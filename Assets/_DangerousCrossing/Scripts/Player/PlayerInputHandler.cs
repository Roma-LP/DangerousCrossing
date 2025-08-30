using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Rigidbody _rigidbody;
        
        private IInputReader _input;
        private Transform _cameraTransform;
        private Vector2 _moveInput;
        private bool _isCanMove = true;
        
        public IInputReader InputReader => _input;

        public void Init(IInputReader input, Transform cameraTransform)
        {
            _input = input;
            _cameraTransform = cameraTransform;
        }

        public void UpdateInput()
        {
            if (!_isCanMove)
                return;
        
            _moveInput =  _input.Direction * _speed;;
            _rigidbody.velocity = new Vector3(_moveInput.x, _rigidbody.velocity.y, _moveInput.y);
            
            // _moveInput =  _input.Direction * _speed;;
            // _movementVector = new Vector3(_moveInput.x, _rigidbody.velocity.y, _moveInput.y);
            // //_movementVector = Quaternion.Euler(0, _cameraTransform.transform.eulerAngles.y,0) * _movementVector;
            // _rigidbody.velocity = _movementVector;
            
            Vector3 lookDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        public void SetMovePossible(bool isCanMove)
        {
            _isCanMove = isCanMove;
        }
    }
}