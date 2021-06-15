using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Reset;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Transition;
using TransitionEditor = VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.TransitionTableEditor;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor
{
    public class TransitionTableEditorDataSO : ScriptableObject
    {
        #region properties

        public TransitionTableEditorData Data => data;

        #endregion

        #region fields

        private TransitionTableEditorData data;

        #endregion

        #region private methods

        #endregion

        #region public methods

        public void Initialize(TransitionTableEditorDependencies @in)
        {
            data = new TransitionTableEditorData(@in);
        }

        private SerializedProperty Transitions
        {
            set => data.Transitions = value;
        }

        private bool ApplyingModifications
        {
            set => data.ApplyingModifications = value;
        }

        private string AppliedModificationsMessage
        {
            set => data.AppliedModificationsMessage = value;
        }

        private List<Object> FromStates
        {
            set => data.FromStates = value;
        }

        private Dictionary<Object, List<DisplayTransition>> GroupedTransitions
        {
            set => data.GroupedTransitions = value;
        }

        private int ToggledIndex
        {
            set => data.ToggledIndex = value;
        }

        private List<List<DisplayTransition>> TransitionsByFromStates
        {
            set => data.TransitionsByFromStates = value;
        }

        private TransitionTableEditorDependencies Dependencies
        {
            get => data.Dependencies;
            set => data.Dependencies = value;
        }

        private TransitionEditor TransitionTableEditor
        {
            set
            {
                var dep = Dependencies;
                dep.TransitionTableEditor = value;
                Dependencies = dep;
            }
        }

        private SerializedObject SerializedObject
        {
            set
            {
                var dep = Dependencies;
                dep.SerializedObject = value;
                Dependencies = dep;
            }
        }

        public void OnResetEnd(ResetDependencies @in)
        {
            Transitions = @in.Transitions;
            ApplyingModifications = @in.ApplyingModifications;
            FromStates = @in.FromStates;
            GroupedTransitions = @in.GroupedTransitions;
            ToggledIndex = @in.ToggledIndex;
            AppliedModificationsMessage = @in.AppliedModificationsMessage;
            TransitionsByFromStates = @in.TransitionsByFromStates;
            SerializedObject = @in.SerializedObject;
            TransitionTableEditor = @in.TransitionTableEditor;
        }

        public struct TransitionTableEditorData
        {
            public TransitionEditor TransitionTableEditor => Dependencies.TransitionTableEditor;
            public SerializedObject SerializedObject => Dependencies.SerializedObject;
            public TransitionTableEditorDependencies Dependencies { get; internal set; }
            public int ToggledIndex { get; internal set; }
            public int CurrentTransitions { get; internal set; }
            public int CurrentFromState { get; internal set; }
            public int CurrentCondition { get; internal set; }
            public int CurrentSourceCondition { get; internal set; }
            public int DeleteIndex { get; internal set; }
            public bool ReorderState { get; internal set; }
            public bool MoveTransitionUp { get; internal set; }
            public bool DisplayStateEditor { get; internal set; }
            public bool ApplyingModifications { get; set; }
            public string AppliedModificationsMessage { get; set; }
            public Rect State { get; internal set; }
            public Rect CurrentRow { get; internal set; }
            public Object SourceTransitions { get; internal set; }
            public List<Object> FromStates { get; internal set; }
            public AddTransition AddTransitionHelper { get; internal set; }
            public DisplayTransition CurrentTransition { get; internal set; }
            public SerializedProperty Transitions { get; internal set; }
            public SerializedTransition SourceTransition { get; internal set; }
            public SerializedTransition SerializedTransition { get; internal set; }
            public SerializedTransition AddedTransition { get; internal set; }
            public List<DisplayTransition> GroupedProperties { get; internal set; }
            public List<List<DisplayTransition>> TransitionsByFromStates { get; internal set; }
            public Dictionary<Object, List<DisplayTransition>> GroupedTransitions { get; internal set; }

            public TransitionTableEditorData(TransitionTableEditorDependencies dep) : this()
            {
                Initialize(this);

                void Initialize(TransitionTableEditorData @in)
                {
                    @in.ToggledIndex = -1;
                    @in.CurrentCondition = 0;
                    @in.CurrentTransitions = 0;
                    @in.CurrentFromState = 0;
                    @in.CurrentSourceCondition = 0;
                    @in.DeleteIndex = 0;
                    @in.ReorderState = false;
                    @in.MoveTransitionUp = false;
                    @in.DisplayStateEditor = false;
                    @in.ApplyingModifications = false;
                    @in.AppliedModificationsMessage = "";
                    @in.State = new Rect();
                    @in.CurrentRow = new Rect();
                    @in.SourceTransitions = new Object();
                    @in.SourceTransition = new SerializedTransition();
                    @in.FromStates = new List<Object>();
                    @in.AddTransitionHelper = null;
                    @in.CurrentTransition = null;
                    @in.Transitions = null;
                    @in.SerializedTransition = new SerializedTransition();
                    @in.AddedTransition = new SerializedTransition();
                    @in.GroupedProperties = new List<DisplayTransition>();
                    @in.TransitionsByFromStates = new List<List<DisplayTransition>>();
                    @in.GroupedTransitions = new Dictionary<Object, List<DisplayTransition>>();
                    @in.Dependencies = dep;
                }
            }
        }

        public struct TransitionTableEditorDependencies
        {
            public TransitionEditor TransitionTableEditor { get; internal set; }
            public SerializedObject SerializedObject { get; internal set; }
        }

        #endregion
    }
}

// ===============================================================================================
/*
    using static StateHeaderDataSO;
    
    public class TransitionTableEditorDataOutSO : ScriptableObject
    {

        public StateHeaderDataIn HeaderData { get; private set; }

        private static string SetCurrentTransitionsName(IReadOnlyList<List<DisplayTransition>> transitionsByFromStates,
            int currentFromState)
        {
            var currentTransitionsByFromStates = transitionsByFromStates[currentFromState];
            return currentTransitionsByFromStates[0].SerializedTransition.FromState.objectReferenceValue.name;
        }

        private static bool SetSetScrollDownButton(Dictionary<Object, List<DisplayTransition>> groupedTransitions,
            int currentFromState)
        {
            var fromStates = groupedTransitions.Keys.ToList();
            return currentFromState < fromStates.Count - 1;
        }

        private void SetHeaderData(StateHeaderDataIn headerDataInternal)
        {
            HeaderData = headerDataInternal;
        }

        private void SetHeader(Dictionary<Object, List<DisplayTransition>> groupedTransitions,
            IReadOnlyList<List<DisplayTransition>> transitionsByFromStates, int currentFromState)
        {
            var addAdditionalStateToLabel = currentFromState == 0;
            var setScrollDownButton = SetSetScrollDownButton(groupedTransitions, currentFromState);
            var setScrollUpButton = currentFromState > 0;
            var currentTransitionsName = SetCurrentTransitionsName(transitionsByFromStates, currentFromState);
            HeaderData.OnSet(addAdditionalStateToLabel, setScrollDownButton, setScrollUpButton, currentFromState,
                currentTransitionsName);
        }

        private void Initialize()
        {
            InitializeHeaderData();

            void InitializeHeaderData()
            {
                SetHeaderData(new StateHeaderDataIn());
                HeaderData.OnInitialize();
            }
        }

        public void OnSetHeader(Dictionary<Object, List<DisplayTransition>> groupedTransitions,
            IReadOnlyList<List<DisplayTransition>> transitionsByFromStates, int currentFromState)
        {
            SetHeader(groupedTransitions, transitionsByFromStates, currentFromState);
        }

        public void OnInitialize()
        {
            Initialize();
        }
    }*/
// ============================================================================================

// public void OnReset()
// {
/*ResetDependencies = new ResetDependencies()
{
    ToggledIndex = Data.ToggledIndex,
    CurrentTransitions = Data.CurrentTransitions,
    SerializedObject = Dependencies.SerializedObject,
    Transitions = Data.Transitions,
    SerializedTransition = Data.SerializedTransition,
    GroupedTransitions = Data.GroupedTransitions
};*/
//  }
/*public struct ResetDependencies
{
    public int ToggledIndex { get; set; }
    public int CurrentTransitions { get; set; }
    public SerializedObject SerializedObject { get; set; }
    public SerializedProperty Transitions { get; set; }
    public SerializedTransition SerializedTransition { get; set; }
    public Dictionary<Object, List<DisplayTransition>> GroupedTransitions { get; set; }
    public string TargetState => ResetText.TargetState;
    public string FromState => ResetText.FromState;
}*/
/*private void Dependencies(TTransitionTableEditorDependencies dependencies, out TransitionTableEditorData dataInternal)
{
    dependenciesInternal = dependencies;
    //dependenciesInternal.TransitionTableEditor = dependencies.transitionTableEditor;
    //dependenciesInternal.SerializedObject = serializedObject;
    dataInternal = Data(dependenciesInternal);
}*/
/*private TransitionTableEditorData Data(TransitionTableEditorDependencies dependencies)
{
    var dataInternal = data;
    dataInternal.Dependencies = dependencies;
    return dataInternal;
}*/

//#region properties

//#endregion

//#region fields

//private TransitionTableEditorDataIn @in;
//private TransitionTableEditorDataOut @out;

//#endregion

//#region constructors

//public TransitionTableEditorData(Editor.TransitionTableEditor transitionTableEditor, SerializedObject serializedObject) : this()
//{
//Initialize(transitionTableEditor,serializedObject);
//}

//#endregion

//#region private methods

//private void Initialize(Editor.TransitionTableEditor transitionTableEditor, SerializedObject serializedObject)
//{
//@in = new TransitionTableEditorDataIn(transitionTableEditor, serializedObject);
//@out = new TransitionTableEditorDataOut(serializedObject);
//}

//#endregion

//#region types

//#endregion
//#region constructors
/*public Dependencies(Editor.TransitionTableEditor transitionTableEditor, SerializedObject serializedObject) : this()
{
    Initialize(transitionTableEditor, serializedObject);
}*/
/*public Dependencies(SerializedObject serializedObject) : this()
{
    Initialize(serializedObject);
}*/

//#endregion
/*private void Initialize(Editor.TransitionTableEditor transitionTableEditor, SerializedObject serializedObject)
{
    TransitionTableEditor = transitionTableEditor;
    Initialize(serializedObject);
}*/
/*private void Initialize(SerializedObject serializedObject)
{
    ToggledIndex = -1;
    CurrentTransitions = 0;
    CurrentFromState = 0;
    CurrentCondition = 0;
    CurrentSourceCondition = 0;
    DeleteIndex = 0;
    ReorderState = false;
    MoveTransitionUp = false;
    DisplayStateEditor = false;
    State = new Rect();
    CurrentRow = new Rect();
    SourceTransitions = new Object();
    AddTransitionHelper = null;
    CurrentTransition = null;
    SerializedObject = serializedObject;
    Transitions = null;
    SourceTransition = new SerializedTransition();
    SerializedTransition = new SerializedTransition();
    AddedTransition = new SerializedTransition();
    GroupedProperties = new List<DisplayTransition>();
    TransitionsByFromStates = new List<List<DisplayTransition>>();
    GroupedTransitions = new Dictionary<Object, List<DisplayTransition>>();
}

public void OnSetReorderState(bool reorderState)
{
    ReorderState = reorderState;
}

public void OnSetMoveTransitionUp(bool moveTransitionUp)
{
    MoveTransitionUp = moveTransitionUp;
}

public void OnSetDisplayStateEditor(bool displayStateEditor)
{
    DisplayStateEditor = displayStateEditor;
}

public void OnSetCurrentFromState(int currentFromState)
{
    CurrentFromState = currentFromState;
}

public void OnSetCurrentCondition(int currentCondition)
{
    CurrentCondition = currentCondition;
}

public void OnSetCurrentSourceCondition(int currentSourceCondition)
{
    CurrentSourceCondition = currentSourceCondition;
}

public void OnSetDeleteIndex(int deleteIndex)
{
    DeleteIndex = deleteIndex;
}

public void OnSetState(Rect state)
{
    State = state;
}

public void OnSetCurrentRow(Rect currentRow)
{
    CurrentRow = currentRow;
}

public void OnSetSourceTransitions(Object sourceTransitions)
{
    SourceTransitions = sourceTransitions;
}

public void OnSetAddTransitionHelper(AddTransition addTransitionHelper)
{
    AddTransitionHelper = addTransitionHelper;
}

public void OnSetSourceTransition(SerializedTransition sourceTransition)
{
    SourceTransition = sourceTransition;
}

public void OnSetAddedTransition(SerializedTransition addedTransition)
{
    AddedTransition = addedTransition;
}

public void OnSetCurrentTransition(DisplayTransition currentTransition)
{
    CurrentTransition = currentTransition;
}

public void OnSetGroupedProperties(List<DisplayTransition> groupedProperties)
{
    GroupedProperties = groupedProperties;
}

public void OnSetTransitionTableEditor(Editor.TransitionTableEditor transitionTableEditor)
{
    TransitionTableEditor = transitionTableEditor;
}

public void OnSetTransitionsByFromStates(List<List<DisplayTransition>> transitionsByFromStates)
{
    TransitionsByFromStates = transitionsByFromStates;
}

public void OnSetTransitions(SerializedProperty transitions)
{
    Transitions = transitions;
}

public void OnSetSerializedObject(SerializedObject serializedObject)
{
    SerializedObject = serializedObject;
}

public void OnSetSerializedTransition(SerializedTransition serializedTransition)
{
    SerializedTransition = serializedTransition;
}

public void OnSetGroupedTransitions(Dictionary<Object, List<DisplayTransition>> groupedTransitions)
{
    GroupedTransitions = groupedTransitions;
}*/

//public struct TransitionTableEditorDataOut
//{
/*#region properties



#endregion

#region fields

private ResetDataIn reset;

#endregion

#region constructors

public TransitionTableEditorDataOut(SerializedObject serializedObject) : this()
{
    reset = new ResetDataIn(serializedObject);
}

#endregion*/
//}

//private StateHeaderDataIn HeaderDataIn { get; private set; }
/*private void SetHeaderData(StateHeaderDataIn headerData)
{
    HeaderDataIn = headerData;
}

private void SetResetData(ResetDataIn resetData)
{
    resetDataIn = resetData;
}

private void SetResetDataIn(ResetDataIn.Dependencies dataIn)
{
    resetDataIn.OnSet(dataIn);
}

private void Initialize()
{
    var data = this;
    InitializeHeaderData();
    InitializeResetData();

    void InitializeHeaderData()
    {
        data.SetHeaderData(new StateHeaderDataIn());
        data.HeaderDataIn.OnInitialize();
    }

    void InitializeResetData()
    {
        data.SetResetData(new ResetDataIn());
        data.resetDataIn.OnInitialize();
        ;
    }
}*/
//[CanBeNull] public EditorUnity CachedStateEditor { get; private set; }
//public TransitionTableEditorDataIn In { get; private set; }
//public TransitionTableEditorDataOut Out { get; private set; }

//private TransitionTableEditorDataIn @in;
/**/
/*private void SetDataIn(TransitionTableEditorDataIn dataIn)
{
    In = dataIn;
}

private void SetDataOut(TransitionTableEditorDataOut dataOut)
{
    Out = dataOut;
}

private void SetReorderState(bool reorderState)
{
    ReorderState = reorderState;
}

private void SetMoveTransitionUp(bool moveTransitionUp)
{
    MoveTransitionUp = moveTransitionUp;
}

private void SetDisplayStateEditor(bool displayStateEditor)
{
    DisplayStateEditor = displayStateEditor;
}

private void SetCurrentFromState(int currentFromState)
{
    CurrentFromState = currentFromState;
}

private void SetCurrentCondition(int currentCondition)
{
    CurrentCondition = currentCondition;
}

private void SetTransitions(SerializedProperty transitions)
{
    Transitions = transitions;
}

private void SetCurrentTransitions(int currentTransitions)
{
    CurrentTransitions = currentTransitions;
}

private void SetCurrentSourceCondition(int currentSourceCondition)
{
    CurrentSourceCondition = currentSourceCondition;
}

private void SetDeleteIndex(int deleteIndex)
{
    DeleteIndex = deleteIndex;
}

private void SetToggledIndex(int toggledIndex)
{
    ToggledIndex = toggledIndex;
}

private void SetState(Rect state)
{
    State = state;
}

private void SetCurrentRow(Rect currentRow)
{
    CurrentRow = currentRow;
}

private void SetSourceTransitions(Object sourceTransitions)
{
    SourceTransitions = sourceTransitions;
}

private void SetCachedStateEditor(EditorUnity cachedStateEditor)
{
    CachedStateEditor = cachedStateEditor;
}

private void SetCurrentTransition(DisplayTransition currentTransition)
{
    CurrentTransition = currentTransition;
}

private void SetAddTransitionHelper(AddTransition addTransitionHelper)
{
    AddTransitionHelper = addTransitionHelper;
}

private void SetSerializedObject(SerializedObject serializedObject)
{
    SerializedObject = serializedObject;
}

private void SetSerializedTransition(SerializedTransition serializedTransition)
{
    SerializedTransition = serializedTransition;
}

private void SetSourceTransition(SerializedTransition sourceTransition)
{
    SourceTransition = sourceTransition;
}

private void SetAddedTransition(SerializedTransition addedTransition)
{
    AddedTransition = addedTransition;
}

private void SetTransitionTableEditor(Editor.TransitionTableEditor transitionTableEditor)
{
    TransitionTableEditor = transitionTableEditor;
}

private void SetGroupedProperties(List<DisplayTransition> groupedProperties)
{
    GroupedProperties = groupedProperties;
}

private void SetTransitionsByFromStates(List<List<DisplayTransition>> transitionsByFromStates)
{
    TransitionsByFromStates = transitionsByFromStates;
}

private void SetGroupedTransitions(Dictionary<Object, List<DisplayTransition>> groupedTransitions)
{
    GroupedTransitions = groupedTransitions;
}

private void SetResetDataIn()
{
    Out.OnSetResetDataIn(); //SerializedObject, );
    //Out.SetResetDataIn();
    //Out.ResetData.OnSet();
}*/

//private void Initialize()
//{
//var data = this;
/*InitializeDataIn();
InitializeDataOut();
InitializeReorderState();
InitializeMoveTransitionUp();
InitializeDisplayStateEditor();
InitializeCurrentFromState();
InitializeCurrentCondition();
InitializeTransitions();
InitializeCurrentTransitions();
InitializeCurrentSourceCondition();
InitializeDeleteIndex();
InitializeToggledIndex();
InitializeState();
InitializeCurrentRow();
InitializeSourceTransitions();
InitializeCachedStateEditor();
InitializeCurrentTransition();
InitializeAddTransitionHelper();
InitializeSerializedObject();
InitializeSerializedTransition();
InitializeSourceTransition();
InitializeAddedTransition();
InitializeTransitionTableEditor();
InitializeGroupedProperties();
InitializeTransitionsByFromStates();
InitializeGroupedTransitions();

void InitializeDataIn()
{
    data.SetDataIn(new TransitionTableEditorDataIn());
    data.In.OnInitialize();
}

void InitializeDataOut()
{
    data.SetDataOut(new TransitionTableEditorDataOut());
    data.Out.OnInitialize();
}

void InitializeReorderState()
{
    data.SetReorderState(false);
}

void InitializeMoveTransitionUp()
{
    data.SetMoveTransitionUp(false);
}

void InitializeDisplayStateEditor()
{
    data.SetDisplayStateEditor(false);
}

void InitializeCurrentFromState()
{
    data.SetCurrentFromState(0);
}

void InitializeCurrentCondition()
{
    data.SetCurrentCondition(0);
}

void InitializeTransitions()
{
    data.SetTransitions(null);
}

void InitializeCurrentTransitions()
{
    data.SetCurrentTransitions(0);
}

void InitializeCurrentSourceCondition()
{
    data.SetCurrentSourceCondition(0);
}

void InitializeDeleteIndex()
{
    data.SetDeleteIndex(0);
}

void InitializeToggledIndex()
{
    data.SetToggledIndex(-1);
}

void InitializeState()
{
    data.SetState(new Rect());
}

void InitializeCurrentRow()
{
    data.SetCurrentRow(new Rect());
}

void InitializeSourceTransitions()
{
    data.SetSourceTransitions(new Object());
}

void InitializeCachedStateEditor()
{
    data.SetCachedStateEditor(null);
}

void InitializeCurrentTransition()
{
    data.SetCurrentTransition(null);
}

void InitializeAddedTransition()
{
    data.SetAddedTransition(new SerializedTransition());
}

void InitializeTransitionTableEditor()
{
    data.SetTransitionTableEditor(null);
}

void InitializeAddTransitionHelper()
{
    data.SetAddTransitionHelper(null);
}

void InitializeSerializedObject()
{
    data.SetSerializedObject(null);
}

void InitializeSerializedTransition()
{
    data.SetSerializedTransition(new SerializedTransition());
}

void InitializeSourceTransition()
{
    data.SetSourceTransition(new SerializedTransition());
}

void InitializeGroupedProperties()
{
    data.SetGroupedProperties(new List<DisplayTransition>());
}

void InitializeTransitionsByFromStates()
{
    data.SetTransitionsByFromStates(new List<List<DisplayTransition>>());
}

void InitializeGroupedTransitions()
{
    data.SetGroupedTransitions(new Dictionary<Object, List<DisplayTransition>>());
}*/
//}
/*public void OnInitialize()
{
    Initialize();
}

public void OnSetToggledIndex(int toggledIndex)
{
    SetToggledIndex(toggledIndex);
}

public void OnSetCurrentFromState(int currentFromState)
{
    SetCurrentFromState(currentFromState);
}

public void OnSetCurrentCondition(int currentCondition)
{
    SetCurrentCondition(currentCondition);
}

public void OnSetCurrentTransitions(int currentTransitions)
{
    SetCurrentTransitions(currentTransitions);
}

public void OnSetCurrentSourceCondition(int currentSourceCondition)
{
    SetCurrentSourceCondition(currentSourceCondition);
}

public void OnSetDeleteIndex(int deleteIndex)
{
    SetDeleteIndex(deleteIndex);
}

public void OnSetState(Rect state)
{
    SetState(state);
}

public void OnSetCurrentRow(Rect currentRow)
{
    SetCurrentRow(currentRow);
}

public void OnSetSourceTransitions(Object sourceTransitions)
{
    SetSourceTransitions(sourceTransitions);
}

public void OnSetCachedStateEditor(EditorUnity cachedStateEditor)
{
    SetCachedStateEditor(cachedStateEditor);
}

public void OnSetCurrentTransition(DisplayTransition currentTransition)
{
    SetCurrentTransition(currentTransition);
}

public void OnSetSerializedTransition(SerializedTransition serializedTransition)
{
    SetSerializedTransition(serializedTransition);
}

public void OnSetSourceTransition(SerializedTransition sourceTransition)
{
    SetSourceTransition(sourceTransition);
}

public void OnSetAddedTransition(SerializedTransition addedTransition)
{
    SetAddedTransition(addedTransition);
}

public void OnSetTransitionsByFromStates(List<List<DisplayTransition>> transitionsByFromStates)
{
    SetTransitionsByFromStates(transitionsByFromStates);
}

public void OnSetAddTransitionHelper(AddTransition addTransitionHelper)
{
    SetAddTransitionHelper(addTransitionHelper);
}

public void OnSetTransitionTableEditor(Editor.TransitionTableEditor transitionTableEditor)
{
    SetTransitionTableEditor(transitionTableEditor);
}

public void OnSetSerializedObject(SerializedObject serializedObject)
{
    SetSerializedObject(serializedObject);
}

public void OnSetResetDataIn() //Reset()
{
    SetResetDataIn();
    //SetResetDataIn();//ForReset();
}*/
//}
/*

    public void OnInitialize()
    {
        Initialize();
    }

    public void
        OnSetResetDataIn(
            ResetDataIn.Dependencies dataIn) //SerializedTransition serializedTransition, SerializedProperty transitions, SerializedObject serializedObject, int toggledIndex, int currentTransitions, Dictionary<Object, List<DisplayTransition>> groupedTransitions)
    {
        //var dataIn = new ResetDataIn.Dependencies();
        //var dataIn = new TransitionTableEditorResetDataIn(serializedTransition, transitions, serializedObject, toggledIndex, currentTransitions, groupedTransitions);
        SetResetDataIn(dataIn); //serializedObject);
        //SetResetDataIn
        //SetResetDataIn();
    }

    public struct TransitionTableEditorResetDataIn
    {
        public SerializedObject SerializedObject { get; }
        public int ToggledIndex { get; }
        public int CurrentTransitions { get; }
        public Dictionary<Object, List<DisplayTransition>> GroupedTransitions { get; }
        public SerializedProperty Transitions { get; }
        public SerializedTransition SerializedTransition { get; }

        public TransitionTableEditorResetDataIn(SerializedTransition serializedTransition,
            SerializedProperty transitions, SerializedObject serializedObject, int toggledIndex,
            int currentTransitions, Dictionary<Object, List<DisplayTransition>> groupedTransitions)
        {
            SerializedTransition = serializedTransition;
            Transitions = transitions;
            SerializedObject = serializedObject;
            ToggledIndex = toggledIndex;
            CurrentTransitions = currentTransitions;
            GroupedTransitions = groupedTransitions;
        }
    }*/
//}
//private TransitionTableEditorDataOutSO dataOut;
//public StateHeaderDataIn HeaderDataIn => dataOut.HeaderData;
/*private void SetData(StateHeaderDataOut headerDataOut)
{
    SetReorderState(headerDataOut.ReorderState);
    //SetToggledIndex(headerDataOut.ToggledIndex);
    //SetDisplayingStateEditor(headerDataOut.DisplayStateEditor);
    //SetMoveTransitionUp(headerDataOut.MoveTransitionUp);
}*/
/*public void OnSetData(StateHeaderDataOut headerDataOut)
{
    SetData(headerDataOut);
}*/
/*private void SetDataOut(TransitionTableEditorDataOutSO dataOutInternal)
{
    dataOut = dataOutInternal;
}*/
/**/
/**/
/*private void SetHeader(Dictionary<Object, List<DisplayTransition>> groupedTransitions,
    IReadOnlyList<List<DisplayTransition>> transitionsByFromStates, int currentFromState)
{
    dataOut.OnSetHeader(groupedTransitions, transitionsByFromStates, currentFromState);
}*/
/*public void OnSetDisplayingStateEditor(bool displayingStateEditor)
{
    SetDisplayingStateEditor(displayingStateEditor);
}*/
/*public void OnInitialize(TransitionTableEditor transitionTableEditor,
    SerializedObject serializedObject)
{
    Initialize(transitionTableEditor, serializedObject);
}*/
/*public void OnSetHeader(Dictionary<Object, List<DisplayTransition>> groupedTransitions, IReadOnlyList<List<DisplayTransition>> transitionsByFromStates, int currentFromState)
{
    SetHeader(groupedTransitions, transitionsByFromStates, currentFromState);
}*/
/*private void SetHeaderData(StateHeaderDataSO headerDataInternal)
        {
            headerData = headerDataInternal;
        }*/
/*public void OnSetGroupedProperties(List<TransitionDisplay> groupedPropertiesInternal)
{
    SetGroupedProperties(groupedPropertiesInternal);
}*/
//private StateHeaderDataSO headerData;
//private TransitionTableEditorDataOut dataOut;
//public void OnSetMoveTransitionUp(bool moveTransitionUpInternal)
//{
//    SetMoveTransitionUp(moveTransitionUpInternal);
//}

//public void OnSetAddTransitionHelper(TransitionAddHelper addTransitionHelperInternal)
//{
//    SetAddTransitionHelper(addTransitionHelperInternal);
//}

//public void OnSetSerializedObject(SerializedObject serializedObjectInternal)
//{
//    SetSerializedObject(serializedObjectInternal);
//}
/*public void OnSetTransitionTableEditor(TransitionTableEditor transitionTableEditorInternal)
{
    SetTransitionTableEditor(transitionTableEditorInternal);
}*/
/*public void OnSetGroupedTransitions(Dictionary<Object, List<TransitionDisplay>> groupedTransitionsInternal)
{
    SetGroupedTransitions(groupedTransitionsInternal);
}*/
/*public struct TransitionTableEditorDataIn
{
public TransitionTableEditorDataIn(int currentFromState)//, List<TransitionDisplay>> groupedTransitions, List<List<TransitionDisplay>> transitionsByFromStates)//Dictionary<Object, List<TransitionDisplay>> groupedTransitions)
{
    //CurrentFromState = currentFromState;
    //SetCurrentTransitionsName(groupedTransitions.Keys.ToList());
}

private void SetCurrentTransitionsName()//IReadOnlyList<Object> fromStates, Dictionary<Object, List<TransitionDisplay>> groupedTransitions, List<List<TransitionDisplay>> transitionsByFromStates)
{
    


    //private static List<TransitionDisplay> CurrentTransitionsByFromStates => _headerData.TransitionsByFromStates[CurrentFromState];
    //private static SerializedProperty InitialFromState => CurrentTransitionsByFromStates[0].SerializedTransition.FromState;
    //private static Object CurrentTransitionsObject => InitialFromState.objectReferenceValue;
    //private static string CurrentTransitionsName => CurrentTransitionsObject.name;


    //CurrentTransitionsName = fromStates[CurrentFromState].name;
}
}*/
//public void OnSetHeaderData(int currentFromStateInternal, Dictionary<Object, List<TransitionDisplay>> groupedTransitionsInternal)
//{
//    SetHeader(currentFromStateInternal, groupedTransitionsInternal);
//}
//public void OnInitializeTransitionsByFromStates()
//{
//    InitializeTransitionsByFromStates();
//}
//public void OnUpdateSerializedObject(SerializedObject serializedObject)
//{
//SetSerializedObject(serializedObject);
//UpdateSerializedObject();
//}
//public void OnApplyModifiedPropertiesSerializedObject(SerializedObject serializedObject)
//{
//SetSerializedObject(serializedObject);
//ApplyModifiedPropertiesSerializedObject();
//}
//public void OnRecordObject(SerializedObject serializedObject)
//{
//    SetSerializedObject(serializedObject);
//}
//public void OnDeleteInvalidTransition(SerializedObject serializedObject)
//{
//    SetSerializedObject(serializedObject);
//}