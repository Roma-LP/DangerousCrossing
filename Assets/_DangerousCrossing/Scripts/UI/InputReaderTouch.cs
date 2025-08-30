using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _DangerousCrossing.Scripts.UI
{
    public class InputReaderTouch : MonoBehaviour, IInputReader, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private bool _isPressed;
        private Vector2 _direction;

        public bool IsPressed => _isPressed;
        public Vector2 Direction => _direction;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _direction = Vector2.up;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isPressed)
            {
                _isPressed = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            _direction = Vector2.zero;
        }
    }
}
