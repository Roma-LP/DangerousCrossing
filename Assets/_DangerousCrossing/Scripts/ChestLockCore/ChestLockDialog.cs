using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDialog : DialogView
    {
        [SerializeField] private ChestLockConfig _chestLockConfig;
        [SerializeField] private ChestLockPanel _chestLockPanel;
        [SerializeField] private ChestLockKeysGridPanel _chestLockKeysGridPanel;
        
        private ChestLockInitiator _chestLockInitiator;
        private ChestLockDragHandler _chestLockDragHandler;
        private IChestLockUnlocked  _chestLockUnlocked;

        public IChestLockUnlocked ChestLockUnlocked => _chestLockUnlocked;

        public override void Show()
        {
            _chestLockInitiator = new ChestLockInitiator(_chestLockConfig,  _chestLockKeysGridPanel);
            ChestLockKeyType[] keysToSpawn = _chestLockInitiator.GenerateKeysGrid();
            _chestLockKeysGridPanel.Init(_chestLockConfig.ChestLockKeyConfigContainer);
            _chestLockDragHandler = new ChestLockDragHandler(_chestLockPanel, _chestLockInitiator.KeyToChestUnlock, _chestLockConfig.CountKeysToOpenLock, this);
            _chestLockUnlocked = _chestLockDragHandler;
            _chestLockPanel.Init(_chestLockInitiator.KeyToChestUnlock, _chestLockConfig.ChestLockKeyConfigContainer, _chestLockDragHandler);
            _chestLockInitiator.FillGridPanelWithKeys(keysToSpawn);
            
            base.Show();
        }

        private void OnDestroy()
        {
            _chestLockDragHandler.Dispose();
            _chestLockPanel.Dispose();
        }
    }
}