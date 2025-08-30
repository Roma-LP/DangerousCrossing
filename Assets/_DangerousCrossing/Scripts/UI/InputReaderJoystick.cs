using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.UI
{
    public class InputReaderJoystick : MonoBehaviour, IInputReader
    {
        [SerializeField] private Joystick _joystick;
        
        public Vector2 Direction => _joystick.Direction;
    }
}