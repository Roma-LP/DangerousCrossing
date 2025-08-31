using _DangerousCrossing.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChestLockKeyConfig", menuName = "Setups/ChestLock/ChestLockKeyConfig")]
    public class ChestLockKeyConfig : ScriptableObject
    {
        [SerializeField] private ChestLockKeyType _chestLockKeyType;
        [SerializeField, PreviewField(100)] private Sprite _sprite;
        
        public ChestLockKeyType ChestLockKeyType => _chestLockKeyType;
        public Sprite Sprite => _sprite;
    }
}