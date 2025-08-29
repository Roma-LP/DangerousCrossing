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

            enemyPerson.OnHealthChanged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            enemyPerson.OnHealthChanged -= HealthChangedHandler;
        }

        private void HealthChangedHandler(float currentHealth, float maxHealth)
        {
            if (currentHealth <= 0f)
                SetNeedTransit();
        }
    }
}