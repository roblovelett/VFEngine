/*using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;
using TransitionEditor = VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.TransitionTableEditor;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Reset
{
    using static Text.Text.Reset;
    using static Text.Text.Reset.Debug;
    using static UnityEngine.Debug;

    public class ResetTransitionTableData : ScriptableObject
    {
        #region properties

        #endregion

        #region fields

        private ResetData data;

        #endregion

        #region private methods

        #endregion

        #region public methods

        public void Initialize()
        {
            data = new ResetData
            {
                CurrentTransition = 0,
                GroupedProperties = new List<DisplayTransition>(),
                Dependencies = new ResetDependencies
                {
                    ToggledIndex = 0,
                    FromStates = new List<Object>(),
                    SerializedObject = null,
                    Transitions = null,
                    GroupedTransitions = new Dictionary<Object, List<DisplayTransition>>(),
                    TransitionTableEditor = null,
                    TransitionsByFromStates = new List<List<DisplayTransition>>(),
                    ApplyingModifications = false,
                    AppliedModificationsMessage = ""
                }
            };
        }

        #region properties

        private int ToggledIndex
        {
            get => data.Dependencies.ToggledIndex;
            set => data.ToggledIndex = value;
        }

        private List<Object> FromStates
        {
            get => data.Dependencies.FromStates;
            set => data.FromStates = value;
        }

        private SerializedProperty Transitions
        {
            get => data.Dependencies.Transitions;
            set => data.Transitions = value;
        }

        private Dictionary<Object, List<DisplayTransition>> GroupedTransitions
        {
            get => data.Dependencies.GroupedTransitions;
            set => data.GroupedTransitions = value;
        }

        private List<List<DisplayTransition>> TransitionsByFromStates
        {
            get => data.Dependencies.TransitionsByFromStates;
            set => data.TransitionsByFromStates = value;
        }

        private int CurrentTransition
        {
            get => data.CurrentTransition;
            set => data.CurrentTransition = value;
        }

        private List<DisplayTransition> GroupedProperties
        {
            get => data.GroupedProperties;
            set => data.GroupedProperties = value;
        }

        private TransitionEditor TransitionTableEditor => data.Dependencies.TransitionTableEditor;
        private SerializedObject SerializedObject => data.Dependencies.SerializedObject;
        private SerializedTransition CurrentSerializedTransition => TransitionInternal(Transitions, CurrentTransition);

        private static SerializedTransition TransitionInternal(SerializedProperty transitions, int currentTransition)
        {
            var transition = new SerializedTransition(transitions, currentTransition);
            return transition;
        }

        private bool DeleteFromState => CurrentSerializedTransition.FromState?.objectReferenceValue == null;
        private bool DeleteToState => CurrentSerializedTransition.ToState?.objectReferenceValue == null;
        private Object ToggledState => ToggledIndex > -1 ? FromStates[ToggledIndex] : null;
        private int ResetToggledIndex => ToggledState ? FromStates.IndexOf(ToggledState) : -1;
        private Object CurrentTransitionFromState => CurrentSerializedTransition.FromState?.objectReferenceValue;

        private bool AddGroupedTransition
        {
            get
            {
                var @in = new PropertiesData {Transitions = GroupedTransitions, State = CurrentTransitionFromState};
                if (!AddProperties(@in)) return false;
                GroupedProperties = new List<DisplayTransition>();
                return true;
            }
        }

        private static bool AddProperties(PropertiesData @in)
        {
            var addProperties = !@in.Transitions.TryGetValue(@in.State, out _);
            return addProperties;
        }

        private struct PropertiesData
        {
            public IReadOnlyDictionary<Object, List<DisplayTransition>> Transitions { get; internal set; }
            public Object State { get; internal set; }
        }

        private DisplayTransition AddedCurrentTransition
        {
            get
            {
                var added = new DisplayTransition(CurrentSerializedTransition, TransitionTableEditor);
                return added;
            }
        }

        #endregion

        public void OnReset(ref TransitionTableEditorDataSO @in)
        {
            data.OnReset(@in);
            SerializedObject.Update();
            Transitions = SerializedObject.FindProperty(TransitionsProperty);
            GroupByFromState();
            ToggledIndex = ResetToggledIndex;
            DataForTransitionTableEditor(@in, out var @out);
            @in = @out;
            ResetInternalData();
        }

        private void DataForTransitionTableEditor(TransitionTableEditorDataSO @in, out TransitionTableEditorDataSO @out)
        {
            @out = @in;
            @out.OnResetEnd(data.Dependencies);
        }

        private void ResetInternalData()
        {
            ApplyingModifications = false;
            AppliedModificationsMessage = "";
            GroupedProperties = null;
        }

        private bool DeleteInvalidTransition => DeleteFromState || DeleteToState;
        private string TargetStateName => SerializedObject.targetObject.name;
        private string DeleteText => DeleteStateMessage(DeleteFromState, DeleteToState, TargetStateName);
        private int TransitionsAmount => Transitions.arraySize;
        private bool CanGroupByFromStates => CurrentTransition < TransitionsAmount;

        private bool ApplyingModifications
        {
            set => data.ApplyingModifications = value;
        }

        private string AppliedModificationsMessage
        {
            set => data.AppliedModificationsMessage = value;
        }

        private void GroupByFromState()
        {
            GroupedTransitions = new Dictionary<Object, List<DisplayTransition>>();
            for (CurrentTransition = 0; CanGroupByFromStates; CurrentTransition++)
            {
                if (DeleteInvalidTransition)
                {
                    LogError(DeleteText);
                    Transitions.DeleteArrayElementAtIndex(CurrentTransition);
                    ApplyingModifications = true;
                    AppliedModificationsMessage = DeletedText;
                    return;
                }

                if (AddGroupedTransition) GroupedTransitions.Add(CurrentTransitionFromState, GroupedProperties);
                GroupedProperties.Add(AddedCurrentTransition);
            }

            FromStates = GroupedTransitions.Keys.ToList();
            TransitionsByFromStates = new List<List<DisplayTransition>>();
            foreach (var fromState in FromStates) TransitionsByFromStates.Add(GroupedTransitions[fromState]);
        }
    }

    #endregion

    public struct ResetData
    {
        public bool ApplyingModifications
        {
            set
            {
                var dep = Dependencies;
                dep.ApplyingModifications = value;
                Dependencies = dep;
            }
        }

        public string AppliedModificationsMessage
        {
            set
            {
                var dep = Dependencies;
                dep.AppliedModificationsMessage = value;
                Dependencies = dep;
            }
        }

        public int ToggledIndex
        {
            get => Dependencies.ToggledIndex;
            set
            {
                var dep = Dependencies;
                dep.ToggledIndex = value;
                Dependencies = dep;
            }
        }

        public List<Object> FromStates
        {
            get => Dependencies.FromStates;
            set
            {
                var dep = Dependencies;
                dep.FromStates = value;
                Dependencies = dep;
            }
        }

        public SerializedProperty Transitions
        {
            get => Dependencies.Transitions;
            set
            {
                var dep = Dependencies;
                dep.Transitions = value;
                Dependencies = dep;
            }
        }

        public Dictionary<Object, List<DisplayTransition>> GroupedTransitions
        {
            get => Dependencies.GroupedTransitions;
            set
            {
                var dep = Dependencies;
                dep.GroupedTransitions = value;
                Dependencies = dep;
            }
        }

        public List<List<DisplayTransition>> TransitionsByFromStates
        {
            get => Dependencies.TransitionsByFromStates;
            set
            {
                var dep = Dependencies;
                dep.TransitionsByFromStates = value;
                Dependencies = dep;
            }
        }

        public int CurrentTransition { get; set; }
        public List<DisplayTransition> GroupedProperties { get; set; }
        public ResetDependencies Dependencies { get; set; }

        public void OnReset(TransitionTableEditorDataSO @in)
        {
            Dependencies = new ResetDependencies
            {
                ToggledIndex = @in.Data.ToggledIndex,
                FromStates = @in.Data.FromStates,
                SerializedObject = @in.Data.SerializedObject,
                Transitions = @in.Data.Transitions,
                GroupedTransitions = @in.Data.GroupedTransitions,
                TransitionTableEditor = @in.Data.TransitionTableEditor,
                TransitionsByFromStates = @in.Data.TransitionsByFromStates,
                ApplyingModifications = @in.Data.ApplyingModifications,
                AppliedModificationsMessage = @in.Data.AppliedModificationsMessage
            };
        }
    }

    public struct ResetDependencies
    {
        public bool ApplyingModifications { get; set; }
        public string AppliedModificationsMessage { get; set; }
        public int ToggledIndex { get; set; }
        public List<Object> FromStates { get; set; }
        public SerializedObject SerializedObject { get; set; }
        public SerializedProperty Transitions { get; set; }
        public Dictionary<Object, List<DisplayTransition>> GroupedTransitions { get; set; }
        public TransitionEditor TransitionTableEditor { get; set; }
        public List<List<DisplayTransition>> TransitionsByFromStates { get; set; }
    }
}*/