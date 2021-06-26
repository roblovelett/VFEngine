using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    using static AssemblyReloadEvents;
    using static StateMachineText;
    using static UniTask;
    using static ScriptableObject;

    public class StateMachine : MonoBehaviour
    {
        [Tooltip(InitialState)] [SerializeField]
        private TransitionTableSO transitionTableSO;

        [SerializeField] private bool enableDebug;
        private Dictionary<Type, Component> cachedComponents;
        private CancellationToken ct;
        private CancellationTokenSource cts;
        private Component cachedComponent;
        private State transitionState;
        private Type type;
#if UNITY_EDITOR
        [Space] [SerializeField] internal StateMachineDebug debug;
#endif
        internal State CurrentState { get; private set; }

        private void Awake()
        {
            if (transitionTableSO == null) transitionTableSO = CreateInstance<TransitionTableSO>();
            CurrentState = transitionTableSO.GetInitialState(this);
            cachedComponents = new Dictionary<Type, Component>();
            ct = this.GetCancellationTokenOnDestroy();
            cts = new CancellationTokenSource();
#if UNITY_EDITOR
            EnableDebug();
#endif
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            afterAssemblyReload += OnAfterAssemblyReload;
        }

        private void OnDisable()
        {
            afterAssemblyReload -= OnAfterAssemblyReload;
        }

        private void OnAfterAssemblyReload()
        {
            CurrentState = transitionTableSO.GetInitialState(this);
            EnableDebug();
        }
#endif

        private void EnableDebug()
        {
            if (!enableDebug) return;
            debug = default(StateMachineDebug);
            debug?.Awake(this);
        }

        private new bool TryGetComponent<T>(out T component) where T : Component
        {
            type = typeof(T);
            if (!cachedComponents.TryGetValue(type, out cachedComponent))
            {
                if (base.TryGetComponent(out component)) cachedComponents.Add(type, component);
                return component != null;
            }

            component = (T) cachedComponent;
            return true;
        }

        private async void Start()
        {
            ((IState) CurrentState).Enter();
            await RunStateMachine();
        }

        private async UniTask RunStateMachine()
        {
            try
            {
                while (!cts.IsCancellationRequested || !ct.IsCancellationRequested)
                {
                    if (CurrentState.TryGetTransition(out transitionState))
                    {
                        ((IState) CurrentState).Exit();
                        CurrentState = transitionState;
                        if (transitionState == null) cts.Cancel();
                        ((IState) CurrentState).Enter();
                    }

                    await WaitForEndOfFrame();
                }
            }
            catch (OperationCanceledException error)
            {
                throw new InvalidOperationException(TransitionStateError + $" {error.Message}");
            }
        }

        internal T GetOrAddComponent<T>() where T : Component
        {
            if (TryGetComponent<T>(out var component)) return component;
            component = gameObject.AddComponent<T>();
            cachedComponents.Add(typeof(T), component);
            return component;
        }

        internal new T GetComponent<T>() where T : Component
        {
            return TryGetComponent(out T component)
                ? component
                : throw new InvalidOperationException(GetComponentError(typeof(T).Name, name));
        }
    }
}