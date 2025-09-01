using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockSpawnerContext : MonoBehaviour
    {
        [SerializeField] private ChestLockProps _chestLockProps;
        [SerializeField] private Transform _chestLockSpawnPoint;

        private ChestLockProps _chestLockPropsInstance;
        private IDialogService _dialogService;
        
        public ChestLockProps ChestLockProps => _chestLockPropsInstance;

        public void Init(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        
        public void SpawnChestLock()
        {
            _chestLockPropsInstance = Instantiate(_chestLockProps, _chestLockSpawnPoint.position, Quaternion.identity, _chestLockSpawnPoint);
            _chestLockPropsInstance.Init(_dialogService);
        }
    }
}