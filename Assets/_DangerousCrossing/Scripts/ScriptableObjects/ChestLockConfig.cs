using _DangerousCrossing.Scripts.Enums;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChestLockConfig", menuName = "Setups/ChestLock/ChestLockConfig")]
    public class ChestLockConfig : ScriptableObject
    {
        [SerializeField] private int _countKeysToOpenLock = 3;
        [SerializeField] private int _countGridKeysRows = 6;
        [SerializeField] private int _countGridKeysColumns = 6;
        [SerializeField] private ChestLockKeyConfigContainer _chestLockKeyConfigContainer;

        public int CountKeysToOpenLock => _countKeysToOpenLock;

        public int CountGridKeysRows => _countGridKeysRows;

        public int CountGridKeysColumns => _countGridKeysColumns;

        public ChestLockKeyConfigContainer ChestLockKeyConfigContainer => _chestLockKeyConfigContainer;

        public ChestLockKeyType GetKeyTypeByIndex(int index)
        {
           return _chestLockKeyConfigContainer.GetKeyTypeByIndex(5);
        }
    }
}