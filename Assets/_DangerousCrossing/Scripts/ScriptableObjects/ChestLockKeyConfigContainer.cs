using System.Collections.Generic;
using System.Linq;
using _DangerousCrossing.Scripts.Enums;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "_ChestLockKeyConfigContainer", menuName = "Setups/ChestLock/ChestLockKeyConfigContainer")]
    public class ChestLockKeyConfigContainer : ScriptableObject
    {
        [SerializeField] private List<ChestLockKeyConfig> _chestLockKeyConfigs;

        public IReadOnlyList<ChestLockKeyConfig> ChestLockKeyConfigs => _chestLockKeyConfigs;

        public ChestLockKeyType GetKeyTypeByIndex(int index)
        {
            return ChestLockKeyType.KeyType_01;
        }

        public ChestLockKeyConfig GetKeyConfigByKeyType(ChestLockKeyType chestLockKeyType)
        {
            ChestLockKeyConfig foundKeyConfig = _chestLockKeyConfigs.FirstOrDefault(item => item.ChestLockKeyType == chestLockKeyType);
            
            if (foundKeyConfig == null)
                Debug.LogError($"[{nameof(ChestLockKeyConfigContainer)}] [{nameof(ChestLockKeyConfig)}] for '{chestLockKeyType}' not found!");

            return foundKeyConfig;
        }
    }
}