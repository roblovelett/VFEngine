using System.Collections.Generic;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    internal class State : IState
    {
        internal StateSO OriginSO;
        internal StateMachine StateMachine;
        internal StateTransition[] Transitions;

        internal State(StateSO originSO, StateMachine stateMachine, StateTransition[] transitions)
        {
            OriginSO = originSO;
            StateMachine = stateMachine;
            Transitions = transitions;
        }

        internal State()
        {
        }

        void IState.Enter()
        {
            foreach (var transition in Transitions as IEnumerable<IState>) transition.Enter();
        }

        void IState.Exit()
        {
            foreach (var transition in Transitions as IEnumerable<IState>) transition.Exit();
        }

        internal bool TryGetTransition(out State state)
        {
            state = null;
            foreach (var transition in Transitions)
                if (transition.TryGetTransition(out state))
                    break;
            foreach (var transition in Transitions) transition.ClearCache();
            return state != null;
        }
    }
}