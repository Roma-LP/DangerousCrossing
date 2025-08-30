using _DangerousCrossing.Scripts.StateMachineCore;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyTransitionAttack : FSMTransition
    {
        [SerializeField] private float _delayBeforeTransition = 3f;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            this.StartCoroutineUniversalWait(_delayBeforeTransition, SetNeedTransit);
        }
    }
}