using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ScriptableObjects;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockInitiator
    {
        private ChestLockConfig _chestLockConfig;
        private ChestLockKeysGridPanel _chestLockKeysGridPanel;
        private ChestLockDataProvider  _chestLockDataProvider;
        private ChestLockKeyType _keyToChestUnlock;
        private GenericArrayProvider<ChestLockKeyType> _keysToSpawnProvider;

        public ChestLockKeyType KeyToChestUnlock => _keyToChestUnlock;

        public ChestLockInitiator(ChestLockConfig chestLockConfig, ChestLockKeysGridPanel chestLockKeysGridPanel, IDataStorage iDataStorage)
        {
            _chestLockConfig = chestLockConfig;
            _chestLockKeysGridPanel = chestLockKeysGridPanel;
            _chestLockDataProvider =  new ChestLockDataProvider(_chestLockConfig, iDataStorage);
        }

        private ChestLockKeyType GetKeyToChestUnlock()
        {
            return _chestLockDataProvider.GetNewLockKeyType();
        }
        
        public IArrayProvider<ChestLockKeyType> GenerateKeysGrid()
        {
            int totalKeys = _chestLockConfig.CountGridKeysRows * _chestLockConfig.CountGridKeysColumns;
            _keyToChestUnlock = GetKeyToChestUnlock();

            ChestLockKeyType[] keysToSpawn = new ChestLockKeyType[totalKeys];
            
            for (int i = 0; i < _chestLockConfig.CountKeysToOpenLock; i++)
                keysToSpawn[i] = _keyToChestUnlock;
            
            for (int i = _chestLockConfig.CountKeysToOpenLock; i < totalKeys; i++)
            {
                IReadOnlyList<ChestLockKeyConfig> possibleLocks = _chestLockConfig.ChestLockKeyConfigContainer.ChestLockKeyConfigs;
                ChestLockKeyType randomConfig = possibleLocks[Random.Range(0, possibleLocks.Count)].ChestLockKeyType;
                keysToSpawn[i] = randomConfig;
            }
            
            keysToSpawn.Shuffle();
            
            _keysToSpawnProvider = new GenericArrayProvider<ChestLockKeyType>(keysToSpawn);
            
            return _keysToSpawnProvider;
        }

        public void FillGridPanelWithKeys(IArrayProvider<ChestLockKeyType> keysProvider)
        {
            _chestLockKeysGridPanel.InstantiateGridWithKeys(keysProvider);
        }
    }
}