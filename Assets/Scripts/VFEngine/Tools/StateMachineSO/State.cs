using System;
using System.Collections.Generic;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

namespace VFEngine.Tools.StateMachineSO
{
    public class State : IState
    {
        internal StateSO OriginSO;
        internal StateMachine StateMachine;
        internal StateTransition[] Transitions;
        internal StateAction[] Actions;

        internal State()
        {
        }

        void IState.Enter()
        {
            StateComponents(Transitions, Input.Enter);
            StateComponents(Actions, Input.Enter);
        }

        void IState.Update()
        {
            StateComponents(Actions, Input.Update);
        }

        void IState.Exit()
        {
            StateComponents(Transitions, Input.Exit);
            StateComponents(Actions, Input.Exit);
        }

        private static void StateComponents(IEnumerable<IState> stateComponents, Input input)
        {
            foreach (var stateComponent in stateComponents)
                switch (input)
                {
                    case Input.Enter:
                        stateComponent.Enter();
                        break;
                    case Input.Update:
                        stateComponent.Update();
                        break;
                    case Input.Exit:
                        stateComponent.Exit();
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(input), input, null);
                }
        }

        private enum Input
        {
            Enter,
            Update,
            Exit
        }

        public bool TryGetTransition(out State state)
        {
            state = null;
            for (var i = 0; i < Transitions.Length; i++)
                if (Transitions[i].TryGetTransition(out state))
                    break;
            for (var i = 0; i < Transitions.Length; i++) Transitions[i].ClearConditionsCache();
            return state != null;
        }
    }
}