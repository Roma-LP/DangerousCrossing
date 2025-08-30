using System;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.ObstacleLineCore;
using _DangerousCrossing.Scripts.Player;
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
        private const float _waitTimeToSpawnPlayer = 2f;

        public GamePlaySceneHandler(PlayerSpawnContext playerSpawnContext, ObstacleLineContext obstacleLineContext, EndObstacleLineZone  endObstacleLineZone, EnemySpawnContext enemySpawnContext)
        {
            _playerPersonInstance = playerSpawnContext.PlayerPersonInstance;
            _playerSpawnContext = playerSpawnContext;
            _enemySpawnContext = enemySpawnContext;
            _obstacleLineContext = obstacleLineContext;
            _endObstacleLineZone = endObstacleLineZone;
            
            _playerPersonInstance.OnHealthZero += HealthPlayerZeroHandler;
            _endObstacleLineZone.OnPlayerInEndZone += PlayerInEndZoneHandler;
        }

        public void StartGamePlayScene()
        {
            NeedSpawnPlayer();
            _obstacleLineContext.Launch();
        }

        private void HealthPlayerZeroHandler()
        {
            _playerPersonInstance.StartCoroutineUniversalWait(_waitTimeToSpawnPlayer, NeedSpawnPlayer);
            _enemySpawnContext.DeSpawnPerson();
            _endObstacleLineZone.OnPlayerInEndZone += PlayerInEndZoneHandler;
        }

        private void NeedSpawnPlayer()
        {
            _playerSpawnContext.SpawnPlayer();
        }

        private void PlayerInEndZoneHandler()
        {
            _endObstacleLineZone.OnPlayerInEndZone -= PlayerInEndZoneHandler;
            
            _enemySpawnContext.SpawnPerson();
        }

        public void Dispose()
        {
            _playerPersonInstance.OnHealthZero -= HealthPlayerZeroHandler;
        }
    }
}