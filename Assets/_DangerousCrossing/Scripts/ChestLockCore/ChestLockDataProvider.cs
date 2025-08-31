using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDataProvider
    {
        private readonly ChestLockConfig _chestLockConfig;

        public ChestLockDataProvider(ChestLockConfig chestLockConfig)
        {
            _chestLockConfig = chestLockConfig;
        }

        public ChestLockKeyType GetNewLockKeyType()
        {
            IReadOnlyList<ChestLockKeyConfig> possibleLocks = _chestLockConfig.ChestLockKeyConfigContainer.ChestLockKeyConfigs;
            ChestLockKeyType newChestLock = possibleLocks[Random.Range(0, possibleLocks.Count)].ChestLockKeyType;
            return newChestLock;
        } 
    }
}