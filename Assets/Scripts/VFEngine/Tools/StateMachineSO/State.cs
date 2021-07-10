using System.Collections.Generic;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.StateMachineSO
{
    using static State.Input;

    internal class State : IState
    {
        internal StateSO OriginSO;
        internal StateMachine StateMachine;
        internal StateTransition[] Transitions;
        internal StateAction[] Actions;

        internal enum Input
        {
            Enter,
            Update,
            Exit
        }

        internal State()
        {
        }

        void IState.Awake(StateMachine stateMachine)
        {
        }

        void IState.Enter()
        {
            StateComponents(Transitions, Enter);
            StateComponents(Actions, Enter);
        }

        void IState.Update()
        {
            StateComponents(Actions, Update);
        }

        void IState.Exit()
        {
            StateComponents(Transitions, Exit);
            StateComponents(Actions, Exit);
        }

        private static void StateComponents(IEnumerable<IState> stateComponents, Input input)
        {
            foreach (var stateComponent in stateComponents)
                switch (input)
                {
                    case Enter:
                        stateComponent.Enter();
                        break;
                    case Update:
                        stateComponent.Update();
                        break;
                    case Exit:
                        stateComponent.Exit();
                        break;
                }
        }

        private void Transition(StateTransition.Input input, out State state)
        {
            state = null;
            var hasTransition = false;
            foreach (var transition in Transitions)
            {
                switch (input)
                {
                    case StateTransition.Input.Get:
                        hasTransition = transition.Get(out state);
                        break;
                    case StateTransition.Input.ClearConditionsCache:
                        transition.ClearConditionsCache();
                        break;
                }

                if (hasTransition) break;
            }
        }

        internal bool GetTransition(out State state)
        {
            state = null;
            Transition(StateTransition.Input.Get, out state);
            Transition(StateTransition.Input.ClearConditionsCache, out _);
            return state != null;
        }
    }
}