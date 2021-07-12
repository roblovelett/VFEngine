using System;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.StateMachineSO
{
    using static MessageType;
    using static AttributeTargets;
    using static EditorGUI;
    using static GUI;
    using static EditorGUIUtility;
    using static EditorApplication;
    using static AssemblyReloadEvents;

    internal class StateMachine : MonoBehaviour
    {
        [Tooltip("Set the initial state of this StateMachine")] [SerializeField]
        private TransitionTableSO transitionTableSO = default(TransitionTableSO);

#if UNITY_EDITOR
        [Space] [SerializeField] internal StateMachineDebugger debugger = default(StateMachineDebugger);
#endif
        //private readonly Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();
        internal State CurrentState;

        private void Awake()
        {
            CurrentState = transitionTableSO.GetInitialState(this);
#if UNITY_EDITOR
            debugger.Awake(this);
#endif
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            afterAssemblyReload += OnAfterAssemblyReload;
        }

        private void OnAfterAssemblyReload()
        {
            CurrentState = transitionTableSO.GetInitialState(this);
            debugger.Awake(this);
        }

        private void OnDisable()
        {
            afterAssemblyReload -= OnAfterAssemblyReload;
        }
#endif

        private void Start()
        {
            (CurrentState as IState).Enter();
        }

        /*private new bool TryGetComponent<T>(out T component) where T : Component
        {
            var type = typeof(T);
            if (!cachedComponents.TryGetValue(type, out var value))
            {
                if (base.TryGetComponent(out component)) cachedComponents.Add(type, component);
                return component != null;
            }

            component = (T) value;
            return true;
        }*/
        /*public T GetOrAddComponent<T>() where T : Component
        {
            if (TryGetComponent<T>(out var component)) return component;
            component = gameObject.AddComponent<T>();
            cachedComponents.Add(typeof(T), component);
            return component;
        }*/
        /*public new T GetComponent<T>() where T : Component
        {
            return TryGetComponent(out T component)
                ? component
                : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
        }*/

        private void Update()
        {
            if (CurrentState.GetTransition(out var transitionState))
            {
                #region Update Transition

                (CurrentState as IState).Exit();
                CurrentState = transitionState;
                (CurrentState as IState).Enter();

                #endregion
            }

            (CurrentState as IState).Update();
        }

        [AttributeUsage(Field)]
        private class InitOnlyAttribute : PropertyAttribute
        {
        }

        [CustomPropertyDrawer(typeof(InitOnlyAttribute))]
        private class InitOnlyAttributeDrawer : PropertyDrawer
        {
            private const string Text =
                "Changes to this parameter during Play mode won't be reflected on existing StateMachines";

            private static readonly GUIStyle Style = new GUIStyle(skin.GetStyle("helpbox"))
            {
                padding = new RectOffset(5, 5, 5, 5)
            };

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (isPlaying)
                {
                    position.height = Style.CalcHeight(new GUIContent(Text), currentViewWidth);
                    HelpBox(position, Text, Info);
                    position.y += position.height + standardVerticalSpacing;
                    position.height = EditorGUI.GetPropertyHeight(property, label);
                }

                PropertyField(position, property, label);
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                var height = EditorGUI.GetPropertyHeight(property, label);
                if (isPlaying)
                    height += Style.CalcHeight(new GUIContent(Text), currentViewWidth) + standardVerticalSpacing * 4;
                return height;
            }
        }
    }
}