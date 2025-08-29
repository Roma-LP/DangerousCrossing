using UnityEngine;

namespace _DangerousCrossing.Scripts.StateMachineCore
{
    public abstract class FSMTransition : MonoBehaviour
    {
        [SerializeField] private FSMState _targetState;
        
        public FSMState TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        public virtual void Init()
        {
        }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }
    }
}