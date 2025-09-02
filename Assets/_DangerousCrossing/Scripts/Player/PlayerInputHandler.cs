using _DangerousCrossing.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private float _speed;
        [ShowInInspector, ReadOnly] private float _rotationSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        
        private IInputReader _input;
        private Transform _cameraTransform;
        private Vector2 _moveInput;
        private bool _isCanMove = true;
        
        public IInputReader InputReader => _input;

        public void Init(IInputReader input, Transform cameraTransform, float speed, float rotationSpeed)
        {
            _input = input;
            _cameraTransform = cameraTransform;
            _speed = speed;
            _rotationSpeed =  rotationSpeed;
        }


        void test()
        {
            // Получаем ввод от джойстика
            Vector2 input = _input.Direction;
    
            if (input.sqrMagnitude < 0.01f)
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
                return;
            }

            // Получаем направления камеры в мировых координатах
            Vector3 cameraForward = _cameraTransform.forward;
            Vector3 cameraRight = _cameraTransform.right;

            // Игнорируем наклон камеры (работаем только в горизонтальной плоскости)
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Создаем вектор движения относительно камеры
            Vector3 moveDirection = (cameraForward * input.y) + (cameraRight * input.x);
            moveDirection.Normalize();

            // Применяем скорость
            Vector3 velocity = moveDirection * _speed;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);

            // Поворот персонажа в направлении движения
            if (moveDirection.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _rigidbody.rotation = Quaternion.Slerp(
                    _rigidbody.rotation, 
                    targetRotation, 
                    10 * Time.fixedDeltaTime
                );
            }
        }
        

        public void UpdateInput()
        {
            if (!_isCanMove)
                return;

            _moveInput = _input.Direction;
            
            Vector3 cameraForward = _cameraTransform.forward;
            Vector3 cameraRight = _cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            Vector3 moveDirection = (cameraForward * _moveInput.y) + (cameraRight * _moveInput.x);
            
            Vector3 velocity = moveDirection * _speed;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
            
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed);
            }
        }

        public void SetMovePossible(bool isCanMove)
        {
            _isCanMove = isCanMove;
        }
    }
}