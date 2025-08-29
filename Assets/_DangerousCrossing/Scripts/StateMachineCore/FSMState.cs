using System.Collections.Generic;
using UnityEngine;

namespace _DangerousCrossing.Scripts.StateMachineCore
{
    public abstract class FSMState : MonoBehaviour
    {
        [SerializeField] private List<FSMTransition> _transitions;
        
        public void Enter()
        {
            if(enabled == false)
            {
                enabled = true;
                foreach(var transition in _transitions)
                {
                    transition.enabled = true;
                    transition.Init();
                }
            }
        }

        public void Exit()
        {
            if (enabled == true)
            {
                foreach (var transition in _transitions)
                    transition.enabled = false;

                enabled = false;
            }
        }

        public virtual void UpdateState()
        {
        }

        public FSMState GetNextState()
        {
            foreach (var transition in _transitions)
            {
                if(transition.NeedTransit)
                    return transition.TargetState;
            }

            return null;
        }
    }
}