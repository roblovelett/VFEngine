using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.Debug.ScriptableObjects;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects;
using UnityDebug = UnityEngine.Debug;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineDebugController = VFEngine.Tools.StateMachine.Debug.Controller;

namespace VFEngine.Tools.StateMachine
{
    using static ScriptableObject;
    using static AssemblyReloadEvents;

    public class Controller : MonoBehaviour
    {
        private Model stateMachine;
        [SerializeField] private TransitionTableSO transitionTableSO;
#if UNITY_EDITOR
        [SerializeField] private StateMachineDebugSettingsSO debugSettings;
        [SerializeField] private StateMachineDebugController debugController;
#endif
        private bool AddedReceivedToCachedComponents => stateMachine.AddedReceivedToCachedComponents;
        private bool CanAddToCachedComponents => stateMachine.CanAddToCachedComponents;
        private Component CachedComponent => stateMachine.CachedComponent;
#if UNITY_EDITOR
        private void OnEnable()
        {
            Initialize();
            afterAssemblyReload += OnAfterAssemblyReload;
        }
#endif
        private void OnAfterAssemblyReload()
        {
            stateMachine.OnAfterAssemblyReload();
        }

        private bool CanInitializeStateMachineModel => stateMachine == null;
        private bool CanInitializeTransitionTableSO => transitionTableSO == null;
        private bool CanInitializeDebugSettings => debugSettings == null;
        private bool CanInitializeDebugController => debugController == null;

        private void Initialize()
        {
            if (CanInitializeDebugSettings) InitializeDebugSettings();
            if (CanInitializeDebugController) InitializeDebugController();
            if (CanInitializeTransitionTableSO) InitializeTransitionTableSO();
            if (CanInitializeStateMachineModel) InitializeStateMachineModel();
        }

        private void InitializeStateMachineModel()
        {
            stateMachine = new Model(this, transitionTableSO, this.GetCancellationTokenOnDestroy());
        }

        private void InitializeTransitionTableSO()
        {
            transitionTableSO = CreateInstance<TransitionTableSO>();
        }

        private void InitializeDebugSettings()
        {
            debugSettings = CreateInstance<StateMachineDebugSettingsSO>();
        }

        private void InitializeDebugController()
        {
            debugController = new StateMachineDebugController(true, this, debugSettings);
        }

        private void OnDisable()
        {
            afterAssemblyReload -= OnAfterAssemblyReload;
        }

        private bool HasDebugStateMachineControl => debugController.HasDebugStateMachineControl;

        private void Awake()
        {
            Initialize();
            if (HasDebugStateMachineControl) debugController.Awake();
        }

        private async void Start()
        {
            await stateMachine.Run();
        }

        private void AddToCachedComponents<T>(T component) where T : Component
        {
            stateMachine.AddToCachedComponents(typeof(T), component);
        }

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            debugController.TransitionConditionResult(conditionName, result, isMet);
        }

        internal T GetOrAddComponent<T>() where T : Component
        {
            if (TryGetComponent<T>(out var component)) return component;
            component = gameObject.AddComponent<T>();
            AddToCachedComponents(component);
            return component;
        }

        internal new T GetComponent<T>() where T : Component
        {
            if (TryGetComponent(out T component)) return component;
            stateMachine.ThrowNoComponentError(typeof(T).Name, name);
            return null;
        }

        private new bool TryGetComponent<T>(out T component) where T : Component
        {
            stateMachine.TryGetComponent(typeof(T));
            if (CanAddToCachedComponents)
                if (base.TryGetComponent(out component))
                {
                    AddToCachedComponents(component);
                    return AddedReceivedToCachedComponents;
                }

            component = (T) CachedComponent;
            return true;
        }
    }
}