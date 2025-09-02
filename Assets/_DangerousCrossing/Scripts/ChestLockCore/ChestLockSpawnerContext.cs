using _DangerousCrossing.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockSpawnerContext : MonoBehaviour
    {
        [SerializeField] private ChestLockProps _chestLockProps;
        [SerializeField] private Transform _chestLockSpawnPoint;

        private ChestLockProps _chestLockPropsInstance;
        private IDialogService _dialogService;
        private IDataStorage _dataStorage;
        
        public ChestLockProps ChestLockProps => _chestLockPropsInstance;

        public void Init(IDialogService dialogService, IDataStorage dataStorage)
        {
            _dialogService = dialogService;
            _dataStorage = dataStorage;
        }
        
        [Button]
        public void SpawnChestLock()
        {
            _chestLockPropsInstance = Instantiate(_chestLockProps, _chestLockSpawnPoint.position, Quaternion.identity, _chestLockSpawnPoint);
            _chestLockPropsInstance.Init(_dialogService, _dataStorage);
        }

        public void DestroyChestLock()
        {
            if(_chestLockPropsInstance == null)
                return;
            
            Destroy(_chestLockPropsInstance.gameObject);
        }
    }
}