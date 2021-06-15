using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateController = VFEngine.Tools.StateMachine.State.Controller;

namespace VFEngine.Tools.StateMachine.Data
{
    using static String;
    using static CancellationToken;

    internal class StateMachineData
    {
        internal bool CanAddToCachedComponents { get; set; }
        internal bool AddedReceivedToCachedComponents { get; set; }
        internal string NoComponentErrorText { get; private set; }
        internal string CannotRunStateMachineErrorText { get; private set; }
        internal string StoppedStateMachineErrorText { get; private set; }
        internal Component CachedComponent { get; set; }
        internal StateController StateController { get; set; }
        internal CancellationToken CancellationToken { get; set; }
        internal StateMachineController StateMachineController { get; private set; }
        internal InvalidOperationException NoComponentError { get; set; }
        internal InvalidOperationException CannotRunStateMachineError { get; set; }
        internal InvalidOperationException StoppedStateMachineError { get; set; }
        internal Dictionary<Type, Component> CachedComponents { get; private set; }
        internal TransitionTableSO TransitionTableSO { get; }

        internal StateMachineData(StateMachineController stateMachineController, TransitionTableSO transitionTableSO,
            CancellationToken cancellationToken)
        {
            Initialize();
            CancellationToken = cancellationToken;
            StateMachineController = stateMachineController;
            TransitionTableSO = transitionTableSO;
            NoComponentErrorText = "@ not found in @Name.";
            CannotRunStateMachineErrorText = "@ cannot run. Initialize the @ and/or its model for @";
            StoppedStateMachineErrorText = "Stopped @ with error: @Error";
        }

        private void Initialize()
        {
            CanAddToCachedComponents = false;
            AddedReceivedToCachedComponents = false;
            NoComponentErrorText = Empty;
            CannotRunStateMachineErrorText = Empty;
            StoppedStateMachineErrorText = Empty;
            CachedComponent = null;
            StateController = null;
            CancellationToken = None;
            StateMachineController = null;
            CachedComponents = null;
            NoComponentError = null;
            CannotRunStateMachineError = null;
            StoppedStateMachineError = null;
        }
    }
}