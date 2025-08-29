using System.Collections;
using _DangerousCrossing.Scripts.Player;
using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyTransitionAttack : FSMTransition
    {
        [SerializeField] private EnemyPerson enemyPerson;

        private PlayerPerson _playerPerson;
        private Coroutine _checkRoutine;
        private WaitForSeconds _waitForSeconds;
        
        public override void Init()
        {
            base.Init();

            _playerPerson = enemyPerson.PlayerPerson;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }
        
        private void OnDisable()
        {
           
        }
        
        
        
        private void OnDrawGizmosSelected()
        {
            if (_playerPerson != null)
            {
                Gizmos.color = Color.yellow;
                //Gizmos.DrawWireSphere(transform.position, _activationRadius);
            }
        }
    }
}