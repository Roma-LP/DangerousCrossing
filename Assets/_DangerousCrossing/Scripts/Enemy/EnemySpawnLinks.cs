using _DangerousCrossing.Scripts.Player;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemySpawnLinks
    {
        public PlayerPerson PlayerPerson { get; }
        public float AttackDistance { get; }
        public float AttackCooldown { get; }
        public float AttackDamage { get; }
        public Transform CameraTransform { get; }

        public EnemySpawnLinks(float attackDistance, float attackCooldown, float attackDamage,
            PlayerPerson playerPerson,  Transform cameraTransform)
        {
            AttackDistance = attackDistance;
            AttackCooldown = attackCooldown;
            AttackDamage = attackDamage;
            PlayerPerson = playerPerson;
            CameraTransform = cameraTransform;
        }
    }
}