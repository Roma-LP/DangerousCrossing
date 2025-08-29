using _DangerousCrossing.Scripts.Player;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemySpawnLinks
    {
        public PlayerPerson PlayerPerson { get; }
        public float AttackDistance { get; }
        public float AttackCooldown { get; }
        public float AttackDamage { get; }

        public EnemySpawnLinks(float attackDistance, float attackCooldown, float attackDamage,
            PlayerPerson playerPerson)
        {
            AttackDistance = attackDistance;
            AttackCooldown = attackCooldown;
            AttackDamage = attackDamage;
            PlayerPerson = playerPerson;
        }
    }
}