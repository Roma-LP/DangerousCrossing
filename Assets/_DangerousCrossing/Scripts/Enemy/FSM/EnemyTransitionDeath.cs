using _DangerousCrossing.Scripts.StateMachineCore;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Enemy.FSM
{
    public class EnemyTransitionDeath : FSMTransition
    {
        [SerializeField] private EnemyPerson enemyPerson;

        protected override void OnEnable()
        {
            base.OnEnable();

            enemyPerson.OnHealthZero += HealthChangedHandler;
        }

        private void OnDisable()
        {
            enemyPerson.OnHealthZero -= HealthChangedHandler;
        }

        private void HealthChangedHandler()
        {
            SetNeedTransit();
        }
    }
}