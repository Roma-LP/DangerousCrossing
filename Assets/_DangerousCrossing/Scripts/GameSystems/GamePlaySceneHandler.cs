using System;
using _DangerousCrossing.Scripts.ChestLockCore;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ObstacleLineCore;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.UI;
using _DangerousCrossing.Scripts.Utilities;

namespace _DangerousCrossing.Scripts.GameSystems
{
    public class GamePlaySceneHandler : IDisposable
    {
        private readonly PlayerPerson _playerPersonInstance;
        private readonly PlayerSpawnContext _playerSpawnContext;
        private readonly EnemySpawnContext _enemySpawnContext;
        private readonly ObstacleLineContext _obstacleLineContext;
        private readonly EndObstacleLineZone _endObstacleLineZone;
        private readonly ChestLockSpawnerContext _chestLockSpawnerContext;
        private readonly IDialogService _dialogService;
        private const float _waitTimeToSpawnPlayer = 2f;

        public GamePlaySceneHandler(PlayerSpawnContext playerSpawnContext, ObstacleLineContext obstacleLineContext, EndObstacleLineZone  endObstacleLineZone, EnemySpawnContext enemySpawnContext, IDialogService dialogService, ChestLockSpawnerContext  chestLockSpawnerContext)
        {
            _playerPersonInstance = playerSpawnContext.PlayerPersonInstance;
            _playerSpawnContext = playerSpawnContext;
            _enemySpawnContext = enemySpawnContext;
            _obstacleLineContext = obstacleLineContext;
            _endObstacleLineZone = endObstacleLineZone;
            _dialogService = dialogService;
            _chestLockSpawnerContext = chestLockSpawnerContext;
            
            _playerPersonInstance.OnHealthZero += HealthPlayerZeroHandler;
            _endObstacleLineZone.OnPlayerInEndZone += PlayerInEndZoneHandler;
            _dialogService.OnDialogShown += OnDialogShownHandler;
            _enemySpawnContext.OnAllEnemiesDied += OnAllEnemiesDiedHandler;
        }

        public void StartGamePlayScene()
        {
            RestartLevel();
            _obstacleLineContext.Launch();
        }

        private void RestartLevel()
        {
            _playerSpawnContext.ResetPlayerOnStartPoint();
            _endObstacleLineZone.ActivateZone();
            _chestLockSpawnerContext.DestroyChestLock();
            _enemySpawnContext.DeSpawnPerson();
        }

        private void HealthPlayerZeroHandler()
        {
            _playerPersonInstance.StartCoroutineUniversalWait(_waitTimeToSpawnPlayer, RestartLevel);
        }

        private void PlayerInEndZoneHandler()
        {
            _enemySpawnContext.SpawnPerson();
        }

        private void OnDialogShownHandler(DialogView dialogView)
        {
            switch (dialogView)
            {
                case IChestLockHandler chestLockDialog:
                {
                    chestLockDialog.OnChestLockUnlocked += ChestLockUnlockedHandler;
                    
                    break;
                }
                case WinPanelDialog winPanelDialog:
                {
                    winPanelDialog.OnRestartClicked += OnRestartClickedHandler;
                    break;
                }
            }
        }

        private void OnAllEnemiesDiedHandler()
        {
            _chestLockSpawnerContext.SpawnChestLock();
        }

        private void ChestLockUnlockedHandler()
        {
            if (_dialogService.TryGetDialog(out ChestLockDialog chestLockDialog))
            {
                chestLockDialog.AddHiddenHandler((dialogView) =>
                {
                    _dialogService.CallDialog(typeof(WinPanelDialog));
                });
                chestLockDialog.Hide();
            }
        }

        private void OnRestartClickedHandler(WinPanelDialog winPanelDialog)
        {
            winPanelDialog.AddHiddenHandler((dialogView) =>
            {
                RestartLevel();
;            });
            winPanelDialog.Hide();
        }
        
        public void Dispose()
        {
            _playerPersonInstance.OnHealthZero -= HealthPlayerZeroHandler;
            _dialogService.OnDialogShown -= OnDialogShownHandler;
            _endObstacleLineZone.OnPlayerInEndZone -= PlayerInEndZoneHandler;
            _enemySpawnContext.OnAllEnemiesDied -= OnAllEnemiesDiedHandler;
        }
    }
}