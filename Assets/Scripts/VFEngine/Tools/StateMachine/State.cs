using VFEngine.Tools.StateMachine.Data.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    internal class State : IState
    {
        internal StateSO OriginSO;
        internal StateMachine StateMachine;
        internal StateTransition[] Transitions;

        internal State()
        {
        }

        public State(StateSO originSO, StateMachine stateMachine, StateTransition[] transitions)
        {
            OriginSO = originSO;
            StateMachine = stateMachine;
            Transitions = transitions;
        }

        void IState.Enter()
        {
            foreach (var transition in Transitions) ((IState) transition).Enter();
        }

        void IState.Exit()
        {
            foreach (var transition in Transitions) ((IState) transition).Exit();
        }

        internal bool TryGetTransition(out State state)
        {
            state = null;
            foreach (var transition in Transitions)
                if (transition.TryGetTransition(out state))
                    break;
            foreach (var transition in Transitions) transition.ClearConditionsCache();
            return state != null;
        }
    }
}