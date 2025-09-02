using System;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockDialog : DialogView, IChestLockHandler
    {
        [SerializeField] private ChestLockConfig _chestLockConfig;
        [SerializeField] private ChestLockPanel _chestLockPanel;
        [SerializeField] private ChestLockKeysGridPanel _chestLockKeysGridPanel;
        
        private ChestLockInitiator _chestLockInitiator;
        private ChestLockDragHandler _chestLockDragHandler;

        public event Action OnChestLockUnlocked;

        public override void Show()
        {
            ChestLockDialogArgs chestLockDialogArgs = GetArgs<ChestLockDialogArgs>();
            _chestLockInitiator = new ChestLockInitiator(_chestLockConfig,  _chestLockKeysGridPanel, chestLockDialogArgs.IDataStorage);
            IArrayProvider<ChestLockKeyType> keysProviderToSpawn = _chestLockInitiator.GenerateKeysGrid();
            _chestLockKeysGridPanel.Init(_chestLockConfig.ChestLockKeyConfigContainer);
            _chestLockDragHandler = new ChestLockDragHandler(_chestLockPanel, _chestLockInitiator.KeyToChestUnlock, _chestLockConfig.CountKeysToOpenLock, this, ChestLockUnlocked);
            _chestLockPanel.Init(_chestLockInitiator.KeyToChestUnlock, _chestLockConfig.ChestLockKeyConfigContainer, _chestLockDragHandler);
            _chestLockInitiator.FillGridPanelWithKeys(keysProviderToSpawn);
            
            base.Show();
        }

        private void ChestLockUnlocked()
        {
            OnChestLockUnlocked?.Invoke();
        }

        private void OnDestroy()
        {
            _chestLockDragHandler.Dispose();
            _chestLockPanel.Dispose();
        }
    }
}