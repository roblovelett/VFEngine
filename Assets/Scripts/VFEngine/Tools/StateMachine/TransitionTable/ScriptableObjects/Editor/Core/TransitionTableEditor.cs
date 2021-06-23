/*using UnityEditor;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core
{
    using static TransitionTableEditorDataSO;
    using static Undo;

    [CustomEditor(typeof(TransitionTableSO))]
    public class TransitionTableEditor : EditorUnity
    {
        #region properties

        #endregion

        #region fields

        private TransitionTableEditorDataSO data;
        private TransitionTableEditorDependencies dependencies;
        //private TransitionTableEditorUtilities transitionTable;

        #endregion

        #region Unity Events

        #region OnEnable

        private void OnEnable()
        {
            Initialize();
            OnEnabledEnd();

            void OnEnabledEnd()
            {
                undoRedoPerformed += Reset;
                Reset();
            }
        }

        private void Initialize()
        {
            data = CreateInstance<TransitionTableEditorDataSO>();
            dependencies = new TransitionTableEditorDependencies
            {
                TransitionTableEditor = this, SerializedObject = serializedObject
            };
            data.Initialize(dependencies);
            //transitionTable = new TransitionTableEditorUtilities();
        }

        #endregion

        #region Reset

        internal void Reset()
        {
            //transitionTable.Reset(ref data);
        }

        #endregion

        #endregion
    }
}

//data.OnResetStart(/*data for reset*///);
//transitionTable.Reset(ResetDataIn, out var resetDataOut);
//data.OnResetEnd(resetDataOut);
/*private void OnResetToggledIndex()
{
    ToggledIndex = ResetToggledIndex;
}*/
//private ResetDataIn ResetDataIn => data;//.ResetDataIn;
//private Rect currentRow;
//private Header header;
//using static TransitionTableEditor.Text;
//using static TransitionTableEditor.Text.IconContent;
//using static TransitionTableEditor.Layout;
////using static TransitionTableEditor.Text.Directions;
//using static TransitionTableEditor.Text.ConditionsText;
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
private TransitionTableEditorDataSO data;

private int ToggledIndex
{
    get => data.ToggledIndex;
    set => data.OnSetToggledIndex(value);
}

private Rect State
{
    get => data.State;
    set => data.OnSetState(value);
}

private TransitionTableEditor TransitionTableEditorInternal => data.TransitionTableEditor;
private List<DisplayTransition> GroupedProperties => data.GroupedProperties;

private List<List<DisplayTransition>> TransitionsByFromStates
{
    get => data.TransitionsByFromStates;
    set => data.OnSetTransitionsByFromStates(value);
}

private Dictionary<Object, List<DisplayTransition>> GroupedTransitions => data.GroupedTransitions;
private bool MoveTransitionUp => data.MoveTransitionUp;

private int CurrentCondition
{
    get => data.CurrentCondition;
    set => data.OnSetCurrentCondition(value);
}

private int CurrentTransitions
{
    get => data.CurrentTransitions;
    set => data.OnSetCurrentTransitions(value);
}

private int CurrentSourceCondition
{
    get => data.CurrentSourceCondition;
    set => data.OnSetCurrentSourceCondition(value);
}

private int DeleteIndex
{
    get => data.DeleteIndex;
    set => data.OnSetDeleteIndex(value);
}

private Rect CurrentRow
{
    get => data.CurrentRow;
    set => data.OnSetCurrentRow(value);
}

private Object SourceTransitionsInternal
{
    get => data.SourceTransitions;
    set => data.OnSetSourceTransitions(value);
}

private EditorUnity CachedStateEditor => data.CachedStateEditor;

private DisplayTransition CurrentTransition
{
    get => data.CurrentTransition;
    set => data.OnSetCurrentTransition(value);
}

private AddTransition AddTransitionHelper => data.AddTransitionHelper;

private SerializedTransition SerializedTransition
{
    get => data.SerializedTransition;
    set => data.OnSetSerializedTransition(value);
}

private SerializedTransition SourceTransition
{
    get => data.SourceTransition;
    set => data.OnSetSourceTransition(value);
}

private SerializedTransition AddedTransition
{
    get => data.AddedTransition;
    set => data.OnSetAddedTransition(value);
}*/
/*
        #endregion

        #region OnDisable

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
            header?.Dispose();
            AddTransitionHelper?.Dispose();
        }

        #endregion

        #region OnInspectorGUI

        private bool DisplayingStateEditor
        {
            get => data.DisplayingStateEditor;
            set => data.OnSetDisplayingStateEditor(value);
        }

        private bool DisplayTransitionTable => !DisplayingStateEditor;

        public override void OnInspectorGUI()
        {
            if (DisplayTransitionTable) TransitionTableGUI();
            else StateEditorGUI();
        }

        #endregion

        #region StateEditorGUI

        private bool ButtonExists => Button(IconContent(ScrollLeft), Width(35), Height(20));
        private bool CanCreateCachedStateEditor => CachedStateEditor == null;
        private bool HasBackButton => ButtonExists || CanCreateCachedStateEditor;
        private string CachedStateEditorName => CachedStateEditor.target.name;

        private void StateEditorGUI()
        {
            if (!SetBackButton()) return;
            SetStateEditorHelpBox();
            SetStateNameLabel();
            CachedStateEditor.OnInspectorGUI();

            bool SetBackButton()
            {
                Separator();
                if (!HasBackButton) return true;
                DisplayingStateEditor = false;
                return false;
            }

            void SetStateEditorHelpBox()
            {
                Separator();
                HelpBox(StateEditorHelp, Info);
                Separator();
            }

            void SetStateNameLabel()
            {
                LabelField(CachedStateEditorName, boldLabel);
                Separator();
            }
        }

        #endregion

        #region TransitionTableGUI

        private int FromStatesAmount => FromStates.Count;

        private int CurrentFromState
        {
            get => data.CurrentFromState;
            set => data.OnSetCurrentFromState(value);
        }

        private bool CanSetTransitionTable => CurrentFromState < FromStatesAmount;
        private Rect TransitionButton => BeginHorizontal();
        private float TransitionButtonSpaceWidth => TransitionButton.width - 55;

        private float RowY
        {
            get => State.y;
            set
            {
                var rect = State;
                rect.y = value;
                State = rect;
            }
        }

        private IEnumerable<DisplayTransition> CurrentTransitionsByFromStates =>
            TransitionsByFromStates[CurrentFromState];

        private bool ToggledIndexIsCurrentFromState => ToggledIndex == CurrentFromState;
        private bool CheckTransitionChanges => ToggledIndexIsCurrentFromState;
        private bool CanApplyModifiedProperties => EndChangeCheck();
        private float RowYOnChangeCheck => singleLineHeight * 2;

        private bool TransitionChanged => CurrentTransition.Display(ref currentRow);

        private void TransitionTableGUI()
        {
            TransitionHelpBox();
            TransitionTable();

            void TransitionHelpBox()
            {
                Separator();
                HelpBox(SeeTransitionText, Info);
                Separator();
            }
        }

        private void TransitionTable()
        {
            {
                for (CurrentFromState = 0; CanSetTransitionTable; CurrentFromState++)
                {
                    State = BeginVertical(WithPaddingAndMargins);
                    DrawRect(State, LightGray);
                    Header();
                    if (!HasTransition()) return;
                }

                TransitionFooter();
            }

            bool HasTransition()
            {
                if (CheckTransitionChanges)
                {
                    OnCheckTransitionChangesStart();
                    if (!DisplayTransition()) return false;
                    if (CanApplyModifiedProperties) ApplyModifiedProperties();
                }

                OnCheckTransitionChangesEnd();
                return true;
            }

            void OnCheckTransitionChangesStart()
            {
                BeginChangeCheck();
                RowY += RowYOnChangeCheck;
            }

            bool DisplayTransition()
            {
                foreach (var currentTransitionByFromStates in CurrentTransitionsByFromStates)
                {
                    SetCurrentTransition(currentTransitionByFromStates);
                    SetTransitionRow();
                    if (TransitionsChanged()) return false;
                }

                return true;
            }

            void OnCheckTransitionChangesEnd()
            {
                OnEndVertical();
                Separator();
            }

            void SetCurrentTransition(DisplayTransition transition)
            {
                CurrentTransition = transition;
            }

            void SetTransitionRow()
            {
                CurrentRow = State;
            }

            bool TransitionsChanged()
            {
                currentRow = CurrentRow;
                if (TransitionChanged)
                {
                    EndChangeCheck();
                    OnEndVertical();
                    Layout.EndHorizontal();
                    return true;
                }

                Separator();
                return false;
            }

            void OnEndVertical()
            {
                Layout.EndFoldoutHeaderGroup();
                Layout.EndVertical();
            }

            void TransitionFooter()
            {
                Layout.Space(TransitionButtonSpaceWidth);
                AddTransitionHelper.Display(TransitionButton);
                Layout.EndHorizontal();
            }
        }

        private StateHeaderDataIn HeaderDataIn => data.HeaderDataIn;

        private void Header()
        {
            data.OnSetHeader(GroupedTransitions, TransitionsByFromStates, CurrentFromState);
            header.Display(HeaderDataIn, out var headerDataOut);
            data.OnSetData(headerDataOut);
            
        }

        #endregion

        #region ApplyModifications

        private void ApplyModifications(string message)
        {
            RecordObject(TargetObject, message);
            ApplyModifiedProperties();
            Reset();
        }

        private void ApplyModifiedProperties()
        {
            SerializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region AddTransition

        private Object SourceFromStateObject => SourceTransition.FromState.objectReferenceValue;
        private Object SourceToStateObject => SourceTransition.ToState.objectReferenceValue;
        private bool GotExistingTransition => SourceToIndex >= 0;
        private bool CanAddTransition => !FailedGetExistingTransition && GotExistingTransition;
        private SerializedProperty SourceFromTransition => SourceTransition.FromState;
        private SerializedProperty SourceToTransition => SourceTransition.ToState;
        private Object SourceToTransitionObject => SourceToTransition.objectReferenceValue;
        private int SourceFromIndex => FromStates.IndexOf(SourceFromTransition.objectReferenceValue);
        private bool FailedGetExistingTransition => SourceFromIndex < 0;
        private List<DisplayTransition> SourceTransitionByFromStates => TransitionsByFromStates[SourceFromIndex];

        private int GetSourceToIndex => SourceTransitionByFromStates.FindIndex(t =>
            t.SerializedTransition.ToState.objectReferenceValue == SourceToTransitionObject);

        private int SourceToIndex => FailedGetExistingTransition ? -1 : GetSourceToIndex;
        private DisplayTransition ExistingTransition => TransitionsByFromStates[SourceFromIndex][SourceToIndex];
        private SerializedTransition ExistingTransitionSerialized => ExistingTransition.SerializedTransition;
        private SerializedProperty TransitionsAtLastIndex => Transitions.GetArrayElementAtIndex(TransitionsAmount);
        private SerializedProperty Conditions => AddedTransition.Conditions;
        private SerializedProperty SourceConditions => SourceTransition.Conditions;
        private int SourceConditionsAmount => SourceConditions.arraySize;
        private bool CopyingConditions => CurrentCondition < SourceConditionsAmount;
        private SerializedProperty Condition => Conditions.GetArrayElementAtIndex(CurrentSourceCondition);
        private SerializedProperty SourceCondition => SourceConditions.GetArrayElementAtIndex(CurrentSourceCondition);
        private int SourceConditionOperator => SourceCondition.FindPropertyRelative(Operator).enumValueIndex;
        private int SourceConditionCondition => SourceCondition.FindPropertyRelative(ConditionInternal).enumValueIndex;

        private Object AddedTransitionFromStateObject
        {
            set => AddedTransition.FromState.objectReferenceValue = value;
        }

        private Object AddedTransitionToStateObject
        {
            set => AddedTransition.ToState.objectReferenceValue = value;
        }

        private int ConditionExpectedResult
        {
            set => Condition.FindPropertyRelative(ExpectedResult).enumValueIndex = value;
        }

        private int SourceConditionExpectedResult =>
            SourceCondition.FindPropertyRelative(ExpectedResult).enumValueIndex;

        private int ConditionOperator
        {
            set => Condition.FindPropertyRelative(Operator).enumValueIndex = value;
        }

        private int ConditionCondition
        {
            set => Condition.FindPropertyRelative(ConditionInternal).enumValueIndex = value;
        }

        internal void AddTransition(SerializedTransition transition)
        {
            SourceTransition = transition;
            if (CanAddTransition)
            {
                AddedTransition = ExistingTransitionSerialized;
            }
            else
            {
                Transitions.InsertArrayElementAtIndex(TransitionsAmount);
                AddedTransition = new SerializedTransition(TransitionsAtLastIndex);
                AddedTransition.ClearProperties();
                AddedTransitionFromStateObject = SourceFromStateObject;
                AddedTransitionToStateObject = SourceToStateObject;
            }

            CopyConditions();
            ApplyModifications(AddedTransitionMessage(AddedTransition.FromState, AddedTransition.ToState));
            OnResetToggledIndex();

            void CopyConditions()
            {
                for (CurrentCondition = 0, CurrentSourceCondition = SourceConditionsAmount;
                    CopyingConditions;
                    CurrentCondition++, CurrentSourceCondition++)
                {
                    Conditions.InsertArrayElementAtIndex(CurrentSourceCondition);
                    ConditionExpectedResult = SourceConditionExpectedResult;
                    ConditionOperator = SourceConditionOperator;
                    ConditionCondition = SourceConditionCondition;
                }
            }
        }

        #endregion

        #region ReorderTransition

        private int StateIndex => FromStates.IndexOf(SourceFromStateObject);
        private List<DisplayTransition> StateTransitions => TransitionsByFromStates[StateIndex];
        private int Index => StateTransitions.FindIndex(t => t.SerializedTransition.Index == SourceTransition.Index);

        private int CurrentIndex => MoveTransitionUp
            ? SourceTransition.Index
            : StateTransitions[Index + 1].SerializedTransition.Index;

        private int TargetIndex => MoveTransitionUp
            ? StateTransitions[Index - 1].SerializedTransition.Index
            : SourceTransition.Index;

        private string SourceToState => SourceToStateObject.name;
        private string ReorderTransitionMessage => MovedTransitionMessage(SourceToState, MoveTransitionUp);

        internal void ReorderTransition(SerializedTransition transition)
        {
            SourceTransition = transition;
            Transitions.MoveArrayElement(CurrentIndex, TargetIndex);
            ApplyModifications(ReorderTransitionMessage);
            OnResetToggledIndex();
        }

        #endregion

        #region RemoveTransition

        private int StateTransitionsAmount => StateTransitions.Count;
        private bool MoveTransitionsArrayElement => Index == 0 && StateTransitionsAmount > 1;
        private string SourceFromState => SourceFromStateObject.name;
        private string RemovedTransitionMessage => DeletedTransitionMessage(SourceFromState, SourceToState);

        internal void RemoveTransition(SerializedTransition transition)
        {
            SourceTransition = transition;
            DeleteIndex = SourceTransition.Index;
            if (MoveTransitionsArrayElement)
                Transitions.MoveArrayElement(StateTransitions[1].SerializedTransition.Index, DeleteIndex++);
            Transitions.DeleteArrayElementAtIndex(DeleteIndex);
            ApplyModifications(RemovedTransitionMessage);
            if (StateTransitionsAmount > 1) ToggledIndex = StateIndex;
        }

        #endregion

        #region GetStateTransitions

        private int SourceTransitionsIndex => FromStates.IndexOf(SourceTransitionsInternal);
        private IEnumerable<DisplayTransition> SourceTransitions => TransitionsByFromStates[SourceTransitionsIndex];

        internal List<SerializedTransition> GetStateTransitions(Object transitions)
        {
            SourceTransitionsInternal = transitions;
            return SourceTransitions.Select(t => t.SerializedTransition).ToList();
        }

        #endregion

        #region Layout

        public static class Layout
        {
            public static bool Button(Rect position, string icon)
            {
                return GUI.Button(position, IconContent(icon));
            }

            public static void BeginVertical()
            {
                EditorGUILayout.BeginVertical();
            }

            public static Rect BeginVertical(GUIStyle style)
            {
                return EditorGUILayout.BeginVertical(style);
            }

            public static void EndVertical()
            {
                EditorGUILayout.EndVertical();
            }

            public static Rect BeginHorizontal()
            {
                return EditorGUILayout.BeginHorizontal();
            }

            public static void EndHorizontal()
            {
                EditorGUILayout.EndVertical();
            }

            public static void GetRect(float width, float height)
            {
                GUILayoutUtility.GetRect(width, height);
            }

            public static void EndFoldoutHeaderGroup()
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            public static void Space(float width)
            {
                EditorGUILayout.Space(width);
            }
        }

        #endregion

        #region Text

        public static class Text
        {
            public const string StateEditorHelp =
                "Edit the Actions that a State performs per frame. The order represent the order of execution.";

            private const string MovedStateText = "Moved @State State @Direction";
            private const string AddedTransitionText = "Added transition from @FromState to @ToState";
            private const string MovedTransitionText = "Moved transition to @ToState @Direction";
            private const string DeletedTransitionText = "Deleted transition from @FromState to @ToState";

            

            
            
            public const string InvalidTransitionDeleted = "Invalid transition deleted";
            public const string TransitionsProperty = "Transitions";

            public struct Directions
            {
                public const string Up = "up";
                public const string Down = "down";
            }

            public struct IconContent
            {
                public const string ScrollLeft = "scrollleft";
                public const string ScrollUp = "scrollup";
                public const string ScrollDown = "scrolldown";
                public const string SceneViewTools = "SceneViewTools";
            }

            public struct ConditionsText
            {
                public const string ConditionInternal = "Condition";
                public const string ExpectedResult = "ExpectedResult";
                public const string Operator = "Operator";
            }

            public static string MovedStateMessage(string state, bool up)
            {
                var direction = NewDirection(up);
                return MovedStateText.Replace("@State", state).Replace("@Direction", direction);
            }

            private static string NewDirection(bool up)
            {
                return up ? Up : Down;
            }

            public static string AddedTransitionMessage(SerializedProperty fromState, SerializedProperty toState)
            {
                var from = $"{fromState}";
                var to = $"{toState}";
                return AddedTransitionText.Replace("@FromState", from).Replace("@ToState", to);
            }

            public static string MovedTransitionMessage(string toState, bool up)
            {
                var direction = NewDirection(up);
                return MovedTransitionText.Replace("@ToState", toState).Replace("@Direction", direction);
            }

            public static string DeletedTransitionMessage(string fromState, string toState)
            {
                return DeletedTransitionText.Replace("@FromState", fromState).Replace("@ToState", toState);
            }
        }

        #endregion
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////







//set => _data.OnSetTransitionTableEditor(value);
//set => _data.OnSetGroupedProperties(value);
//set => _data.OnSetGroupedTransitions(value);
//set => _data.OnSetMoveTransitionUp(value);
//set => _data.OnSetCachedStateEditor(value);
/*private static StateHeaderDisplay Header
{
    get => _data.Header;
    set => _data.OnSetHeader(value);
}*/
//set => _data.OnSetAddTransitionHelper(value);
/*
#region ReorderState

private int InitialTransitionsIndex { get; set; }
private bool AddToInitialTransitionsIndex => !MoveTransitionUp;
private List<TransitionDisplayHelper> ReorderedTransitions => TransitionsByFromStates[InitialTransitionsIndex];
private int TransitionsSourceIndex => ReorderedTransitions[0].SerializedTransition.Index;
private int TargetTransitionsIndex => InitialTransitionsIndex - 1;
private TransitionDisplayHelper ReorderedTransition => TransitionsByFromStates[TargetTransitionsIndex][0];
private SerializedTransition ReorderedSerializedTransition => ReorderedTransition.SerializedTransition;
private int ReorderedTransitionsIndex => ReorderedSerializedTransition.Index;


private string CurrentFromStateName => FromStates[CurrentFromState].name;

private void ReorderState()
{
    OnReorderStateStart();
    SetInitialReorderedTransitionsIndex();
    OnReorderStateEnd();

    void OnReorderStateStart()
    {
        InitialTransitionsIndex = CurrentFromState;
    }

    void SetInitialReorderedTransitionsIndex()
    {
        if (AddToInitialTransitionsIndex) InitialTransitionsIndex++;
    }

    void OnReorderStateEnd()
    {
        Transitions.MoveArrayElement(TransitionsSourceIndex, ReorderedTransitionsIndex);
        ApplyModifications(MovedStateMessage(CurrentFromStateName, MoveTransitionUp));
        OnResetToggledIndex();
    }
}

#endregion
*/

//private static SerializedProperty InitialFromState => CurrentTransitionsByFromStates[0].SerializedTransition.FromState;
//private static Object CurrentTransitionsObject => InitialFromState.objectReferenceValue;
//#region DisplayStateEditor

//private static Type StateEditorType => typeof(StateEditor);
/*private void DisplayStateEditor()
{
    if (CanCreateCachedStateEditor) CreateCachedStateEditor();
    else SetCachedStateEditor();
    OnDisplayStateEditor();

    void CreateCachedStateEditor()
    {
        CachedStateEditor = CreateEditor(CurrentTransitionsObject, StateEditorType);
    }

    void SetCachedStateEditor()
    {
        var cached = CachedStateEditor;
        CreateCachedEditor(CurrentTransitionsObject, StateEditorType, ref cached);
    }

    void OnDisplayStateEditor()
    {
        DisplayingStateEditor = true;
    }
}*/

//#endregion