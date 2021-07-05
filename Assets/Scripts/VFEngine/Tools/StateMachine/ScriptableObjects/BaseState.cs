using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    /*
    using static Enum;

    public abstract class BaseState
    {
        private protected bool CanExit;
        private protected string Name;
        private protected Hash128 Hash;
        private protected UnityGameObject GameObject;
        private protected StateMachine StateMachine;
        private protected IEnumerable<ToState> ToStates;
        private protected IEnumerable<Transition> Transitions;

        protected enum Transition
        {
        }

        public enum ToState
        {
        }

        protected BaseState(IEnumerable<string> toStates, IEnumerable<string> transitions, bool canExit, string name,
            Hash128 hash, UnityGameObject gameObject, StateMachine stateMachine)
        {
            ToStates = ProcessEnum<ToState>(toStates);
            Transitions = ProcessEnum<Transition>(transitions);
            CanExit = canExit;
            Name = name;
            Hash = hash;
            GameObject = gameObject;
            StateMachine = stateMachine;
        }

        private static IEnumerable<T> ProcessEnum<T>(IEnumerable<string> strings)
        {
            return strings.Select(@string => (T) Parse(typeof(T), @string));
        }
        
        
        //enum State { Created, Ready, Running, Waiting, Terminated, New }
        //enum Input { Admit, ScheduleDispatch, Interrupt, EventWait, EventComplete, Exit}

        /*async UniTask<State> Enter(State current, Input input) => (current, input) switch
        {
            (State.New, Input.Admit) =>
                await ((Func<UniTask<State>>)(async () =>
                {
                    await ExecuteSomeActionAsync();
                    return State.Ready;
                }))(),
            (State.Running, Input.Exit) => State.Terminated,
            (State.Ready, Input.ScheduleDispatch) =>
                await UniTask.FromResult(State.Running),
            _ => throw new NotSupportedException($"{current} has no transition on {input}")
        };

        private async UniTask ExecuteSomeActionAsync()
        {
            Debug.Log("foobar");
            await UniTask.DelayFrame(10);
        }

        private async void Run()
        {
            var currentState = await Enter(State.New, Input.Admit);
        }*/

    /*
        public async UniTask<ToState> Enter()
        {
        }
        public abstract void Input();
        public abstract void Update();
        public abstract void PhysicsUpdate();
        public abstract void Render();
        public abstract void Pause();
        public abstract void Exit();
    }*/
}