using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.TestStateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "Tools/SM/StateSO")]
    public class StateSO : ScriptableObject
    {
        private bool CanExit;
        private string Name;
        private Hash128 Hash;
        private UnityGameObject gameObject;
        private Tools.StateMachine.StateMachine stateMachine;
        private Component[] cachedComponents;
        private IEnumerable<State> ToStates;
        private IEnumerable<Input> Inputs;
        private enum Input { Initialize, Open, Close, Destroy }
        private enum State { Ready, Open, Closed, Destroyed }

        async UniTask<State> Transition(State current, Input input) => (current, input) switch
        {
            // Initialization (Enter)
            
            // StateActions (Update)
            
            // Physics Update (FixedUpdate)
            
            // Render
            
            // Pause if needed
            
            // Transition (Exit)
            
            (State.Ready, Input.Open) =>
                await ((Func<UniTask<State>>)(async () =>
                {
                    await OpenAction();
                    return State.Open;
                }))(),
            (State.Ready, Input.Close) => State.Closed,
            (State.Ready, Input.Destroy) => State.Destroyed,
            _ => throw new NotSupportedException($"{current} has no transition on {input}")
        };

        private static async UniTask OpenAction()
        {
            Debug.Log("open.");
            await UniTask.DelayFrame(10);
        }

        private void Exit()
        {
            
        }
        private void Update(){}
        private void PhysicsUpdate(){}
        private void Render(){}
        private void Pause(){}
    }
}