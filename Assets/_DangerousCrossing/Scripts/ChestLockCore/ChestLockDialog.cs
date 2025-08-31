using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
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

        public override void Show()
        {
            base.Show();
            
            _chestLockInitiator = new ChestLockInitiator(_chestLockConfig,  _chestLockKeysGridPanel);
            ChestLockKeyType[] keysToSpawn = _chestLockInitiator.GenerateKeysGrid();
            _chestLockKeysGridPanel.Init(_chestLockConfig.ChestLockKeyConfigContainer);
            _chestLockDragHandler = new ChestLockDragHandler(_chestLockPanel, _chestLockInitiator.KeyToChestUnlock, _chestLockConfig.CountKeysToOpenLock);
            _chestLockPanel.Init(_chestLockInitiator.KeyToChestUnlock, _chestLockConfig.ChestLockKeyConfigContainer, _chestLockDragHandler);
            _chestLockInitiator.FillGridPanelWithKeys(keysToSpawn);
        }
    }
}