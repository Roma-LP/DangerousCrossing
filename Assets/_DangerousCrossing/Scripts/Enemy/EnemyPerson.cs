using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Person;
using _DangerousCrossing.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

namespace _DangerousCrossing.Scripts.Enemy
{
    public class EnemyPerson : PersonBase, ISpawnable<EnemySpawnLinks>
    {
        [SerializeField] private EnemyAnimationController _enemyAnimationController;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyFSM _enemyFsm;

        private PlayerPerson _playerPerson;
        private EnemySpawnLinks _enemySpawnLinks;
        
        public NavMeshAgent Agent => _agent;
        public PlayerPerson PlayerPerson => _playerPerson;
        public EnemyAnimationController EnemyEnemyAnimationController => _enemyAnimationController;
        public EnemySpawnLinks EnemySpawnLinks => _enemySpawnLinks;

        protected override void Awake()
        {
            base.Awake();

            //_playerPerson = SceneContext.Instance.PlayerUnit;
        }

        private void Update()
        {
            _enemyFsm.UpdateFSM();
            _enemyAnimationController.SetSpeed(new Vector2(_agent.velocity.x, _agent.velocity.y));
        }

        public void MoveTo(Vector3 target)
        {
            _agent.SetDestination(target);
        }

        public void StopMoving()
        {
            _agent.ResetPath();
            _enemyAnimationController.SetSpeed(Vector2.zero);
        }

        public void OnSpawned(EnemySpawnLinks parametrs)
        {
            _enemySpawnLinks = parametrs;
        }
    }
}