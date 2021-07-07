using System.Collections.Generic;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    internal class State : IState
    {
        private readonly StateAction[] actions;
        internal readonly StateSO OriginSO;
        internal readonly StateMachine StateMachine;
        internal StateTransition[] Transitions;
        

        public State(StateSO stateSO, StateMachine stateMachine, StateAction[] stateActions)
        {
            OriginSO = stateSO;
            StateMachine = stateMachine;
            Transitions = new StateTransition[0];
            actions = stateActions;
        }

        void IState.Enter()
        {
            foreach (var transition in Transitions as IEnumerable<IState>) transition.Enter();
        }

        internal void Update()
        {
            foreach (var action in actions) action.Update();
        }

        void IState.Exit()
        {
            foreach (var transition in Transitions as IEnumerable<IState>) transition.Exit();
            foreach (var action in actions) ((IState) action).Exit();
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