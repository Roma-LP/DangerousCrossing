using System;
using _DangerousCrossing.Scripts.Enums;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDragHandler : IDisposable
    {
        private ChestLockPanel _chestLockPanel;
        private ChestLockKeyType _keyToChestUnlock;
        private int _currentAcceptKeysCount;
        private int _countKeysToOpenLock;


        public event Action<int, int> OnCurrentAcceptKeysCountChanged;
        public event Action<int> OnChestLockUnlocked;
        
        public int CountKeysToOpenLock => _countKeysToOpenLock;
        public int AcceptKeysCount
        {
            get => _currentAcceptKeysCount;
            private set
            {
                _currentAcceptKeysCount = value;

                OnCurrentAcceptKeysCountChanged?.Invoke(_currentAcceptKeysCount, _countKeysToOpenLock);
            }
        }
        
        public ChestLockDragHandler(ChestLockPanel chestLockPanel, ChestLockKeyType keyToChestUnlock, int countKeysToOpenLock)
        {
            _chestLockPanel = chestLockPanel;
            _keyToChestUnlock = keyToChestUnlock;
            _countKeysToOpenLock =  countKeysToOpenLock;
            
            _chestLockPanel.OnDropKeyUIElement += DropKeyUIElementHandler;
        }

        private void DropKeyUIElementHandler(ChestLockKeyUIElement keyUIElement)
        {
            if (keyUIElement.KeyType == _keyToChestUnlock)
            {
                AcceptKeysCount++;
            
                keyUIElement.MarkAsUsed();
            
                if (_currentAcceptKeysCount >= _countKeysToOpenLock)
                {
                    Debug.Log("Замок открыт!");
                    OnChestLockUnlocked?.Invoke(_currentAcceptKeysCount);
                }
            }
            else
            {
                Debug.Log("Неподходящий ключ!");
            }
        }

        public void Dispose()
        {
            _chestLockPanel.OnDropKeyUIElement -= DropKeyUIElementHandler;
        }
    }
}