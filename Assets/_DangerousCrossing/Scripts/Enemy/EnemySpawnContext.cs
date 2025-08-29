using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemySpawnContext : SpawnerBase<EnemyPerson, EnemySpawnLinks>
    {
        [Header("AttackSettings")]
        [SerializeField] private float _attackDistance = 0.3f;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _attackDamage = 25f;

        private EnemySpawnLinks _enemySpawnLinks;

        public void Init(PlayerPerson playerPerson)
        {
            _enemySpawnLinks = new EnemySpawnLinks(_attackDistance, _attackCooldown, _attackDamage, playerPerson);
        }

        public override void SpawnPerson()
        {
            SpawnFromConfig(_enemySpawnLinks);
        }
    }
}