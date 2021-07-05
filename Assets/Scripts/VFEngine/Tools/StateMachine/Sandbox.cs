using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VFEngine.Tools.StateMachine
{
    public class Sandbox : MonoBehaviour
    {

        private class StateMachineBasic
        {
            enum State { Open, Closed, Locked }
            enum Input { Open, Close, Lock, Unlock };

            State Enter(State current, Input input) => (current, input) switch
            {
                (State.Closed, Input.Open) => State.Open,
                (State.Open, Input.Close) => State.Closed,
                (State.Closed, Input.Lock) => State.Locked,
                (State.Locked, Input.Unlock) => State.Closed,
                _ => throw new NotSupportedException($"{current} has no transition on {input}")
            };

            private State currentState = State.Closed;

            private void Run()
            {
                currentState = Enter(currentState, Input.Open);
            } 
        }
        
        
        // ================================================================================================ //

        private class StateMachineProcessScheduler
        {
            enum State { Created, Ready, Running, Waiting, Terminated, New }
            enum Input { Admit, ScheduleDispatch, Interrupt, EventWait, EventComplete, Exit}

            private bool hasPermission;

            public StateMachineProcessScheduler(bool hasPermission)
            {
                this.hasPermission = hasPermission;
            }

            State Enter(State current, Input input) => (current, input) switch
            {
                (State.Created, Input.Admit) => State.Ready,
                (State.Ready, Input.ScheduleDispatch) => State.Running,
                (State.Running, Input.EventWait) => State.Waiting,
                (State.Waiting, Input.EventComplete) => State.Ready,
                (State.Running, Input.Interrupt) => State.Ready,
                (State.Running, Input.Exit) => State.Terminated,
                // conditional transition
                (State.New, Input.Admit) when hasPermission => State.Ready,
                (State.New, Input.Admit) => ((Func<State>)(() => {
                    ExecuteSomeAction();
                    return State.Ready;
                }))(),
                _ => throw new NotSupportedException($"{current} has no transition on {input}")
            };

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
            enum State { Created, Ready, Running, Waiting, Terminated, New }
            enum Input { Admit, ScheduleDispatch, Interrupt, EventWait, EventComplete, Exit}

            async UniTask<State> Enter(State current, Input input) => (current, input) switch
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
            }
        }
    }
}
