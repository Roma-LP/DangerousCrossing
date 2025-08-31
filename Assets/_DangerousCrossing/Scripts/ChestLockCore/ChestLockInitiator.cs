using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enums;
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
        private ChestLockKeyType[] keysToSpawn;

        public ChestLockKeyType KeyToChestUnlock => _keyToChestUnlock;

        public ChestLockInitiator(ChestLockConfig chestLockConfig, ChestLockKeysGridPanel chestLockKeysGridPanel)
        {
            _chestLockConfig = chestLockConfig;
            _chestLockKeysGridPanel = chestLockKeysGridPanel;
            _chestLockDataProvider =  new ChestLockDataProvider(_chestLockConfig);
        }

        private ChestLockKeyType GetKeyToChestUnlock()
        {
            return _chestLockDataProvider.GetNewLockKeyType();
        }
        
        public ChestLockKeyType[] GenerateKeysGrid()
        {
            int totalKeys = _chestLockConfig.CountGridKeysRows * _chestLockConfig.CountGridKeysColumns;
            _keyToChestUnlock = GetKeyToChestUnlock();

            ChestLockKeyType[] keysToSpawn = new ChestLockKeyType[totalKeys];

            // 1. Минимум 3 ключа нужного типа
            for (int i = 0; i < _chestLockConfig.CountKeysToOpenLock; i++)
                keysToSpawn[i] = _keyToChestUnlock;

            // 2. Остальные случайные
            for (int i = _chestLockConfig.CountKeysToOpenLock; i < totalKeys; i++)
            {
                IReadOnlyList<ChestLockKeyConfig> possibleLocks = _chestLockConfig.ChestLockKeyConfigContainer.ChestLockKeyConfigs;
                ChestLockKeyType randomConfig = possibleLocks[Random.Range(0, possibleLocks.Count)].ChestLockKeyType;
                keysToSpawn[i] = randomConfig;
            }

            // 3. Перемешиваем
            keysToSpawn.Shuffle();

            // 4. Создаём ячейки и ключи
            //_chestLockKeysGridPanel.InstantiateGridWithKeys(keysToSpawn);
            return keysToSpawn;
        }

        public void FillGridPanelWithKeys(ChestLockKeyType[] chestLockKeyTypes)
        {
            _chestLockKeysGridPanel.InstantiateGridWithKeys(chestLockKeyTypes);
        }
    }
}