using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;
using VFEngine.Tools.StateMachine.State;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine
{
    using static UniTask;
    using static CancellationToken;
    using static UnityEngine.Debug;

    public class Model
    {
        private readonly StateMachineData data;
        private bool CanInitializeData => data == null;

        internal Model(StateMachineController stateMachineController, TransitionTableSO transitionTableSO,
            CancellationToken cancellationToken)
        {
            if (CanInitializeData)
                data = new StateMachineData(stateMachineController, transitionTableSO, cancellationToken);
        }

        private TransitionTableSO TransitionTableSO => data.TransitionTableSO;
        private StateMachineController StateMachineController => data.StateMachineController;

        internal void OnAfterAssemblyReload()
        {
            StateController = TransitionTableSO.GetInitialState(StateMachineController);
        }

        private CancellationToken CancellationToken
        {
            get => data.CancellationToken;
            set => data.CancellationToken = value;
        }

        private string NoComponentErrorText => data.NoComponentErrorText;
        private Dictionary<Type, Component> CachedComponents => data.CachedComponents;

        internal bool AddedReceivedToCachedComponents
        {
            get => data.AddedReceivedToCachedComponents;
            private set => data.AddedReceivedToCachedComponents = value;
        }

        internal bool CanAddToCachedComponents
        {
            get => data.CanAddToCachedComponents;
            private set => data.CanAddToCachedComponents = value;
        }

        internal Component CachedComponent
        {
            get => data.CachedComponent;
            private set => data.CachedComponent = value;
        }

        private StateController StateController
        {
            get => data.StateController;
            set => data.StateController = value;
        }

        private InvalidOperationException NoComponentError
        {
            get => data.NoComponentError;
            set => data.NoComponentError = value;
        }

        private bool RunStateMachine => CancellationToken != None && StateMachineController != null;

        private void UpdateStateController()
        {
            if (StateController.GetNextStateController(out var nextStateController)) TransitionTo(nextStateController);
        }

        private void TransitionTo(StateController nextStateController)
        {
            (StateController as IStateController).OnExit();
            StateController = nextStateController;
            (StateController as IStateController).OnEnter();
        }

        private InvalidOperationException CannotRunStateMachineError
        {
            get => data.CannotRunStateMachineError;
            set => data.CannotRunStateMachineError = value;
        }

        private string CannotRunStateMachineErrorText => data.CannotRunStateMachineErrorText;

        internal async UniTask Run()
        {
            try
            {
                if (RunStateMachine) await StateMachine(true);
                else ThrowCannotRunStateMachineError();
            }
            catch (OperationCanceledException e)
            {
                ThrowStoppedStateMachineError(e.Message);
            }
        }

        private bool StopStateMachine => data.CancellationToken.IsCancellationRequested;

        private async UniTask StateMachine(bool running)
        {
            while (running)
            {
                if (StopStateMachine) running = false;
                UpdateStateController();
                await WaitForFixedUpdate(CancellationToken);
            }
        }

        internal void AddToCachedComponents(Type type, Component receivedComponent)
        {
            CachedComponents.Add(type, receivedComponent);
            AddedReceivedToCachedComponents = true;
        }

        internal void TryGetComponent(Type type)
        {
            CanAddToCachedComponents = !CachedComponents.TryGetValue(type, out var cachedComponent);
            CachedComponent = cachedComponent;
        }

        private string StateMachineControllerName => data.StateMachineController.name;

        private void ThrowCannotRunStateMachineError()
        {
            CannotRunStateMachineError = new InvalidOperationException(
                CannotRunStateMachineErrorText.Replace("@", StateMachineControllerName));
            LogError(CannotRunStateMachineError);
        }

        private void ThrowStoppedStateMachineError(string error)
        {
            StoppedStateMachineError = new InvalidOperationException(StoppedStateMachineErrorText
                .Replace("@", StateMachineControllerName).Replace("@Error", error));
            LogError(StoppedStateMachineError);
        }

        internal void ThrowNoComponentError(string componentName, string name)
        {
            NoComponentError = new InvalidOperationException(NoComponentErrorText
                .Replace("@", componentName).Replace("@Name", name));
            LogError(NoComponentError);
        }

        private string StoppedStateMachineErrorText => data.StoppedStateMachineErrorText;

        private InvalidOperationException StoppedStateMachineError
        {
            get => data.StoppedStateMachineError;
            set => data.StoppedStateMachineError = value;
        }

        private void LogError(Exception error)
        {
            CancellationToken = new CancellationToken(true);
            Log(error);
            throw error;
        }
    }
}