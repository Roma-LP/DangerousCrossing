using System;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.Interfaces;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDragHandler : IDisposable
    {
        private readonly ChestLockPanel _chestLockPanel;
        private readonly ChestLockKeyType _keyToChestUnlock;
        private readonly int _countKeysToOpenLock;
        private readonly ChestLockDialog _chestLockDialog;
        private int _currentAcceptKeysCount;
        
        private readonly Action OnChestLockUnlocked;
        
        public event Action<int, int> OnCurrentAcceptKeysCountChanged;
        
        
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
        
        public ChestLockDragHandler(ChestLockPanel chestLockPanel, ChestLockKeyType keyToChestUnlock, int countKeysToOpenLock, ChestLockDialog chestLockDialog, Action onChestLockUnlocked)
        {
            _chestLockPanel = chestLockPanel;
            _keyToChestUnlock = keyToChestUnlock;
            _countKeysToOpenLock =  countKeysToOpenLock;
            _chestLockDialog =  chestLockDialog;
            OnChestLockUnlocked = onChestLockUnlocked;
            
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