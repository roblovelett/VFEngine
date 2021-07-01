using System.Collections.Generic;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

namespace VFEngine.Tools.StateMachineSO
{
    internal class State : IState
    {
        internal StateSO OriginSO;
        internal StateMachine StateMachine;
        internal StateTransition[] Transitions;
        private readonly StateAction[] actions;

        internal State(StateSO originSO, StateMachine stateMachine, StateTransition[] transitions, StateAction[] actionsInternal)
        {
            OriginSO = originSO;
            StateMachine = stateMachine;
            Transitions = transitions;
            actions = actionsInternal;
        }

        internal State()
        {
        }

        void IState.Enter()
        {
            foreach (var transition in Transitions as IEnumerable<IState>) transition.Enter();
        }

        internal void Update()
        {
            foreach (var action in actions)
            {
                action.Update();
            }
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