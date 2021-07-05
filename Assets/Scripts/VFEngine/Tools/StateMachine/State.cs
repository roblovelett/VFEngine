using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.StateMachine
{
    public abstract class State : IState
    {
        //private StateSO currentState;
        private bool CanExit;
        private string Name;
        //private Hash128 Hash;
        private UnityGameObject gameObject;
        private StateMachine stateMachine;
        private Component[] cachedComponents;
        private IEnumerable<ToState> ToStates;
        private IEnumerable<Input> Inputs;
        private enum Input { }
        private enum ToState { }

        private State()
        {
            
        }
        void IState.Enter() { }
        void IState.Exit() { }
        private void Update(){}
        private void PhysicsUpdate(){}
        private void Render(){}
        private void Pause(){}
        private void Exit(){}
        
        
        /*internal StateSO OriginSO;
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
        }*/
        
    }
}