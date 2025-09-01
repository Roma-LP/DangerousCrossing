using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemySpawnLinks
    {
        public PlayerPerson PlayerPerson { get; }
        public EnemyPersonConfig EnemyPersonConfig { get; }
        public Transform CameraTransform { get; }
        
        public EnemySpawnLinks(PlayerPerson playerPerson,EnemyPersonConfig enemyPersonConfig, Transform cameraTransform)
        {
            EnemyPersonConfig = enemyPersonConfig;
            PlayerPerson = playerPerson;
            CameraTransform = cameraTransform;
        }
    }
}