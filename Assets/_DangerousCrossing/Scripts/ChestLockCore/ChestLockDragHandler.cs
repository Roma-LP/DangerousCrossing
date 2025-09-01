using System;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.Interfaces;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDragHandler : IDisposable,  IChestLockUnlocked
    {
        private ChestLockPanel _chestLockPanel;
        private ChestLockKeyType _keyToChestUnlock;
        private int _currentAcceptKeysCount;
        private int _countKeysToOpenLock;
        private ChestLockDialog _chestLockDialog;
        
        public event Action<int, int> OnCurrentAcceptKeysCountChanged;
        public event Action OnChestLockUnlocked;
        
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
        
        public ChestLockDragHandler(ChestLockPanel chestLockPanel, ChestLockKeyType keyToChestUnlock, int countKeysToOpenLock, ChestLockDialog chestLockDialog)
        {
            _chestLockPanel = chestLockPanel;
            _keyToChestUnlock = keyToChestUnlock;
            _countKeysToOpenLock =  countKeysToOpenLock;
            _chestLockDialog =  chestLockDialog;
            
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
                    OnChestLockUnlocked?.Invoke();
                    _chestLockDialog.SetActiveBlockInputArea(true);
                }
            }
        }

        public void Dispose()
        {
            _chestLockPanel.OnDropKeyUIElement -= DropKeyUIElementHandler;
        }
    }
}