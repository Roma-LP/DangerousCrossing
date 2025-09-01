using _DangerousCrossing.Scripts.StateMachineCore;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyTransitionAttack : FSMTransition
    {
        [SerializeField] private EnemyPerson _enemyPerson;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            this.StartCoroutineUniversalWait(_enemyPerson.EnemySpawnLinks.EnemyPersonConfig.DelayBeforeTransitInAttack, SetNeedTransit);
        }
    }
}