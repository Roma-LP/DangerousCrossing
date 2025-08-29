using UnityEngine;

namespace _DangerousCrossing.Scripts.StateMachineCore
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField] private FSMState _firstState;

        private FSMState _currentState;

        public FSMState CurrentState => _currentState;

        public virtual void StartFSM()
        {
            Transit(_firstState);
        }

        public virtual void UpdateFSM()
        {
            if (_currentState == null)
                return;

            var nextState = _currentState.GetNextState();
            if(nextState != null)
                Transit(nextState);

            _currentState.UpdateState();
        }

        private void Transit(FSMState nextState)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = nextState;

            if (_currentState != null)
                _currentState.Enter();
        }
    }
}
