using System.Collections.Generic;
using System.Linq;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.GameSystems.DataStorageCore;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDataProvider
    {
        private const string USED_KEYS_FILE_NAME = "used_chest_keys";
        
        private readonly ChestLockConfig _chestLockConfig;
        private readonly IDataStorage _dataStorage;
        private List<ChestLockKeyType> _availableKeys;
        private UsedKeysData _usedKeysData;

        public ChestLockDataProvider(ChestLockConfig chestLockConfig, IDataStorage dataStorage)
        {
            _chestLockConfig = chestLockConfig;
            _dataStorage = dataStorage;
            
            InitializeKeys();
        }

        public ChestLockKeyType GetNewLockKeyTypeOld()
        {
            IReadOnlyList<ChestLockKeyConfig> possibleLocks = _chestLockConfig.ChestLockKeyConfigContainer.ChestLockKeyConfigs;
            ChestLockKeyType newChestLock = possibleLocks[Random.Range(0, possibleLocks.Count)].ChestLockKeyType;
            return newChestLock;
        } 
        
        private void InitializeKeys()
        {
            _usedKeysData = _dataStorage.Load<UsedKeysData>(USED_KEYS_FILE_NAME);
            
            List<ChestLockKeyType> allKeys = _chestLockConfig.ChestLockKeyConfigContainer.ChestLockKeyConfigs
                .Select(config => config.ChestLockKeyType)
                .ToList();
            
            _availableKeys = allKeys.Except(_usedKeysData.UsedKeys).ToList();
            
            if (_availableKeys.Count == 0)
            {
                ResetUsedKeys();
                _availableKeys = new List<ChestLockKeyType>(allKeys);
            }
        }


        private void ResetUsedKeys()
        {
            _usedKeysData.UsedKeys.Clear();
            SaveUsedKeys();
        }

        private void SaveUsedKeys()
        {
            _dataStorage.Save(_usedKeysData, USED_KEYS_FILE_NAME);
        }
        
        public ChestLockKeyType GetNewLockKeyType()
        {
            if (_availableKeys.Count == 0)
            {
                ResetUsedKeys();
                InitializeKeys();
            }
            
            int randomIndex = UnityEngine.Random.Range(0, _availableKeys.Count);
            ChestLockKeyType newKey = _availableKeys[randomIndex];
            
            _availableKeys.RemoveAt(randomIndex);
            _usedKeysData.UsedKeys.Add(newKey);
            
            SaveUsedKeys();

            return newKey;
        }
    }
}