using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VFEngine.Tools.TestStateMachine
{
    public class Sandbox : MonoBehaviour
    {
        private class StateMachineBasic
        {
            private enum State
            {
                Open,
                Closed,
                Locked
            }

            private enum Input
            {
                Open,
                Close,
                Lock,
                Unlock
            }

            private State Enter(State current, Input input)
            {
                return (current, input) switch
                {
                    (State.Closed, Input.Open) => State.Open,
                    (State.Open, Input.Close) => State.Closed,
                    (State.Closed, Input.Lock) => State.Locked,
                    (State.Locked, Input.Unlock) => State.Closed,
                    _ => throw new NotSupportedException($"{current} has no transition on {input}")
                };
            }

            private State currentState = State.Closed;

            private void Run()
            {
                currentState = Enter(currentState, Input.Open);
            }
        }

        // ================================================================================================ //

        private class StateMachineProcessScheduler
        {
            private enum State
            {
                Created,
                Ready,
                Running,
                Waiting,
                Terminated,
                New
            }

            private enum Input
            {
                Admit,
                ScheduleDispatch,
                Interrupt,
                EventWait,
                EventComplete,
                Exit
            }

            private readonly bool hasPermission;

            public StateMachineProcessScheduler(bool hasPermission)
            {
                this.hasPermission = hasPermission;
            }

            private State Enter(State current, Input input)
            {
                return (current, input) switch
                {
                    (State.Created, Input.Admit) => State.Ready,
                    (State.Ready, Input.ScheduleDispatch) => State.Running,
                    (State.Running, Input.EventWait) => State.Waiting,
                    (State.Waiting, Input.EventComplete) => State.Ready,
                    (State.Running, Input.Interrupt) => State.Ready,
                    (State.Running, Input.Exit) => State.Terminated,
                    // conditional transition
                    (State.New, Input.Admit) when hasPermission => State.Ready,
                    (State.New, Input.Admit) => ((Func<State>) (() =>
                    {
                        ExecuteSomeAction();
                        return State.Ready;
                    }))(),
                    _ => throw new NotSupportedException($"{current} has no transition on {input}")
                };
            }

            private void ExecuteSomeAction()
            {
                Debug.Log("foobar");
            }

            private State currentState = State.New;

            private void Run()
            {
                currentState = Enter(currentState, Input.Admit);
            }
        }

        // ================================================================================================ //

        private class AsyncStateMachine
        {
            private enum State
            {
                Created,
                Ready,
                Running,
                Waiting,
                Terminated,
                New
            }

            private enum Input
            {
                Admit,
                ScheduleDispatch,
                Interrupt,
                EventWait,
                EventComplete,
                Exit
            }

            private async UniTask<State> Enter(State current, Input input)
            {
                return (current, input) switch
                {
                    (State.New, Input.Admit) => await ((Func<UniTask<State>>) (async () =>
                    {
                        await ExecuteSomeActionAsync();
                        return State.Ready;
                    }))(),
                    (State.Running, Input.Exit) => State.Terminated,
                    (State.Ready, Input.ScheduleDispatch) => await UniTask.FromResult(State.Running),
                    _ => throw new NotSupportedException($"{current} has no transition on {input}")
                };
            }

            private async UniTask ExecuteSomeActionAsync()
            {
                Debug.Log("foobar");
                await UniTask.DelayFrame(10);
            }

            private async void Run()
            {
                var currentState = await Enter(State.New, Input.Admit);
            }
        }

        // ===========
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
}