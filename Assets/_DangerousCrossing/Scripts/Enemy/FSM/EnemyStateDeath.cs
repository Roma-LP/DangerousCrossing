using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyStateDeath : FSMState
    {
        [SerializeField] private EnemyPerson enemyPerson;

        private void OnEnable()
        {
            
            
            Destroy(gameObject);
        }
    }
}