using System;
using _DangerousCrossing.Scripts.ObstacleLineCore;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.Utilities;

namespace _DangerousCrossing.Scripts.GameSystems
{
    public class GamePlaySceneHandler : IDisposable
    {
        private readonly PlayerPerson _playerPersonInstance;
        private readonly PlayerSpawnContext _playerSpawnContext;
        private readonly ObstacleLineContext _obstacleLineContext;
        private const float _waitTimeToSpawnPlayer = 2f;

        public GamePlaySceneHandler(PlayerSpawnContext playerSpawnContext, ObstacleLineContext obstacleLineContext)
        {
            _playerPersonInstance = playerSpawnContext.PlayerPersonInstance;
            _playerSpawnContext = playerSpawnContext;
            _obstacleLineContext = obstacleLineContext;
            
            _playerPersonInstance.OnHealthZero += HealthPlayerZeroHandler;
        }

        public void StartGamePlayScene()
        {
            NeedSpawnPlayer();
            _obstacleLineContext.Launch();
        }

        private void HealthPlayerZeroHandler()
        {
            _playerPersonInstance.StartCoroutineUniversalWait(_waitTimeToSpawnPlayer, NeedSpawnPlayer);
        }

        private void NeedSpawnPlayer()
        {
            _playerSpawnContext.SpawnPlayer();
        }

        public void Dispose()
        {
            _playerPersonInstance.OnHealthZero -= HealthPlayerZeroHandler;
        }
    }
}