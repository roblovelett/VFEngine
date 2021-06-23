/*using System;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;
using Object = UnityEngine.Object;

// ReSharper disable RedundantAssignment
namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.State
{
    using static EditorGUI;
    using static EditorGUILayout;
    using static ContentStyle;
    using static StateHeaderData;
    using static GUI;
    using static GC;
    using static Object;
    using static EditorGUIUtility;
    using static StateHeaderData.Text;

    public class StateHeader : IDisposable
    {
        private StateHeader header;
        private StateHeaderData data;

        public StateHeader()
        {
            Initialize();
        }

        private void Initialize()
        {
            header = this;
            data = new StateHeaderData();//CreateInstance<VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.State.StateHeaderData>();
            data.OnInitialize();
        }
        
        private bool EarlyOut => data.EarlyOut;
        
        //private bool EarlyOut => ReorderState || DisplayStateEditor;
        //private bool EarlyOut => ReorderState || DisplayStateEditor
        internal void Display(StateHeaderDataIn dataIn, out StateHeaderDataOut dataOut)
        {
            data.OnSet(dataIn);
            SetHeader();
            SetHeaderTitle();
            SetHeaderButtons();
            if (!EarlyOut)
            {
                //Layout.EndHorizontal();
            }
            dataOut = data.DataOut;
        }

        internal void OnEarlyOut()
        {
            EarlyOut();

            static void EarlyOut()
            {/*
                Layout.EndHorizontal();
                Layout.EndFoldoutHeaderGroup();
                Layout.EndVertical();
                Layout.EndHorizontal();
            */
/*}
        }

        internal void OnDisplayEnd()
        {
            
        }

        //private bool OnEarlyOut(out StateHeaderDataOut dataOut)
        //{
            /*if (EarlyOut)
            {
                if (ReorderState) state.Reorder(CurrentFromState, MoveTransitionUp);
                if (DisplayStateEditor) stateEditor.Display();
                OnEarlyOut();
                dataOut = data.DataOut;
                return true;
            }

            dataOut = data.DataOut;
            return false;*/
        //}

        /*private static void OnEarlyOut()
        {
            
        }*/

        //private Rect Header => data.Header;
/*
        private void SetHeader()
        {
            BeginVertical();
            data.OnSetHeader();
            //GetRect(Header.width, Header.height);
        }

        private Rect Toggled => data.Toggled;
        private bool ToggledIsCurrentFromState => data.ToggledIsCurrentFromState;
        private string Label => data.Label;
        private int ToggledIndexOnHeader => data.ToggledIndexOnHeader;
        private int CurrentFromState => data.CurrentFromState;
        private bool GroupedHeader => BeginFoldoutHeaderGroup(Toggled, ToggledIsCurrentFromState, Label, StateListStyle);
        private int ToggledIndexOnGroupedHeader => GroupedHeader ? CurrentFromState : ToggledIndexOnHeader;

        private void SetHeaderTitle()
        {
            data.OnSetHeaderTitle();
            SetToggled();
            Separator();
            //Layout.EndVertical();
        }

        private void SetToggled()
        {
            data.OnSetToggled();
            data.OnSetToggledIndex(ToggledIndexOnGroupedHeader);
        }

        private bool SetScrollButtons => data.SetScrollButtons;
        private Rect Button => data.Button;
        private bool SceneViewButton => ButtonOnPress(Button, SceneViewTools);
        private bool ScrollDownButton => ButtonOnPress(Button, ScrollDown);
        private bool ScrollUpButton => ButtonOnPress(Button, ScrollUp);

        private static bool ButtonOnPress(Rect position, string icon)
        {
            return Button(position, IconContent(icon));
        }

        private void SetHeaderButtons()
        {
            data.OnSetHeaderButtons();
            if (SetScrollButtons)
            {
                if (ScrollDownButton)
                {
                    data.OnScrollDownButtonPressed();
                    return;
                }

                if (ScrollUpButton)
                {
                    data.OnScrollUpButtonPressed();
                    return;
                }
                data.OnSetScrollButtonsX();
            }

            if (!SceneViewButton) return;
            data.OnSceneViewButtonPressed();
        }

        public void Dispose()
        {
           // DestroyImmediate(data);
            SuppressFinalize(header);
        }
    }
}

//using static TransitionTableEditor.Text;
//using static TransitionTableEditor.Text.IconContent;
// #region fields

// #region static

//private static Rect _headerRect;
//private static Rect _initialHeaderRect;
//private static TransitionTableEditorDataSO _headerData;

//#endregion

//#endregion
//private bool reorderState;
//private bool displayStateEditor;
/*
private bool scrollDownButtonPressed;
private bool scrollUpButtonPressed;
private string headerLabel;
private Rect toggledHeaderRect;
private Rect initialHeaderButtonRect;
private Rect headerButtonRect;*/
//private StateReorderHelper state;
//private StateEditorDisplay stateEditor;
//#region initialization
//private Rect HeaderRect => _data.Header;

//_headerData = CreateInstance<TransitionTableEditorDataSO>();
//_headerRect = new Rect();
//state = new StateReorderHelper();
//stateEditor = new StateEditorDisplay();
//reorderState = false;
//displayStateEditor = false;
//scrollUpButtonPressed = false;
//scrollDownButtonPressed = false;
//headerButtonRect = new Rect();
//_initialHeaderRect = BeginHorizontal();
//initialHeaderButtonRect = new Rect(_headerRect.width - 25, _headerRect.y, 35, 20);

//#endregion

//#region internal methods

//#region Display
/*
private bool ReorderingState => SetScrollButtonsOnButtonsPressed();
private bool DisplayingStateEditor => SetStateEditorButtonOnButtonPressed();
private bool ReturnEarly => ReorderingState || DisplayingStateEditor;

//
private static int CurrentFromState => _headerData.CurrentFromState;
private static bool AddInitialStateToLabel => CurrentFromState == 0;

private static List<TransitionDisplay> CurrentTransitions => _headerData.TransitionsByFromStates[CurrentFromState];
private static SerializedProperty InitialFromState => CurrentTransitions[0].SerializedTransition.FromState;
private static Object CurrentTransitionsObject => InitialFromState.objectReferenceValue;
private static string CurrentTransitionsName => CurrentTransitionsObject.name;

//

private static bool ToggledIndexIsCurrentFromState => ToggledIndex == CurrentFromState;
private bool GroupedHeader => BeginFoldoutHeaderGroup(toggledHeaderRect, ToggledIndexIsCurrentFromState, headerLabel, StateListStyle);
private static int ToggledIndexOnHeader => ToggledIndexIsCurrentFromState ? -1 : ToggledIndex;
private int ToggledIndexOnGroupedHeader => GroupedHeader ? CurrentFromState : ToggledIndexOnHeader;

//

private static int ToggledIndex
{
    get => _headerData.ToggledIndex;
    set => _headerData.OnSetToggledIndex(value);
}

//

private static bool HeaderButton(Rect position, string icon)
{
    return Button(position, icon);
}

//

private static List<Object> FromStates => _headerData.GroupedTransitions.Keys.ToList();
private static int FromStatesAmount => FromStates.Count;
private static int LastFromStateIndex => FromStatesAmount - 1;
private static bool SetScrollDownButton => CurrentFromState < LastFromStateIndex;
private static bool SetScrollUpButton => CurrentFromState > 0;
private static bool SetScrollButtons => SetScrollDownButton || SetScrollUpButton;
private bool ScrollButtonPressed => ScrollDownButtonPressed() || ScrollUpButtonPressed();

//

private bool StateEditorButtonPressed => HeaderButton(headerButtonRect, SceneViewTools);

//
*/
/*private bool SetHeaderButtonsOnPressed(ref TransitionTableEditorDataSO data)
{
    //SetHeaderButtonRect();
    if (!ReturnEarly) return false;
    OnReturnEarlyStart(ref data);
    ReorderState(ref data);
    DisplayStateEditor(ref data);
    OnReturnEarlyEnd(ref data);
    return true;
}*/
/*private void DisplayStateEditor(ref TransitionTableEditorDataSO data)
{
    if (!displayStateEditor) return;
    StateEditorDisplay.Display(ref data);
    displayStateEditor = false;
}

private void ReorderState(ref TransitionTableEditorDataSO data)
{
    if (!reorderState) return;
    state.Reorder(ref data);
    reorderState = false;
}

private static void OnReturnEarlyStart(ref TransitionTableEditorDataSO data)
{
    SetData(ref data);
}

private static void OnReturnEarlyEnd(ref TransitionTableEditorDataSO data)
{
    EarlyOut();
    SetData(ref data);
}

private static void OnDisplayEnd(ref TransitionTableEditorDataSO data)
{
    SetData(ref data);
}

private static void SetData(ref TransitionTableEditorDataSO data)
{
    data = _data;
}*/

// #endregion

//#endregion

//#region private methods

//#region OnDisplayStart

// #endregion

//#region SetHeaderTitle
/*private static int ToggledIndex
{
    get => _headerData.ToggledIndex;
    set => _headerData.OnSetToggledIndex(value);
}*/

//#region OnSetHeaderTitleStart

//#endregion

//#region SetHeaderLabel
/*
private static int CurrentFromState => _headerData.CurrentFromState;
private static bool AddInitialStateToLabel => CurrentFromState == 0;

private static List<TransitionDisplay> CurrentTransitions => _headerData.TransitionsByFromStates[CurrentFromState];

private static SerializedProperty InitialFromState => CurrentTransitions[0].SerializedTransition.FromState;
private static Object CurrentTransitionsObject => InitialFromState.objectReferenceValue;
private static string CurrentTransitionsName => CurrentTransitionsObject.name;
*/

//#endregion

//#region SetToggled
/*
private static bool ToggledIndexIsCurrentFromState => ToggledIndex == CurrentFromState;
private bool GroupedHeader => BeginFoldoutHeaderGroup(toggledHeaderRect, ToggledIndexIsCurrentFromState, headerLabel, StateListStyle);
private static int ToggledIndexOnHeader => ToggledIndexIsCurrentFromState ? -1 : ToggledIndex;
private int ToggledIndexOnGroupedHeader => GroupedHeader ? CurrentFromState : ToggledIndexOnHeader;
*/
/*
private void SetToggled()
{
    toggledHeaderRect = _headerRect;
    toggledHeaderRect.width -= 140;
    ToggledIndex = ToggledIndexOnGroupedHeader;
}

private static int ToggledIndex
{
    get => _headerData.ToggledIndex;
    set => _headerData.OnSetToggledIndex(value);
}*/
/*
        #endregion

        #region OnSetHeaderTitleEnd

        #endregion

        #endregion

        #region EarlyOut

        private static void EarlyOut()
        {
            Layout.EndHorizontal();
            Layout.EndFoldoutHeaderGroup();
            Layout.EndVertical();
            Layout.EndHorizontal();
        }

        #endregion*/

//#region SetScrollButtonsOnButtonsPressed
/*private bool ScrollDownButtonPressed()
{
    if (!HeaderButton(headerButtonRect, ScrollDown)) return false;
    scrollDownButtonPressed = true;
    return true;
}



private bool ScrollUpButtonPressed()
{
    if (!HeaderButton(headerButtonRect, ScrollUp)) return false;
    scrollUpButtonPressed = true;
    return true;
}*/
/*private static bool HeaderButton(Rect position, string icon)
{
    return Button(position, icon);
}*/
/*
private static List<Object> FromStates => _headerData.GroupedTransitions.Keys.ToList();
private static int FromStatesAmount => FromStates.Count;
private static int LastFromStateIndex => FromStatesAmount - 1;
private static bool SetScrollDownButton => CurrentFromState < LastFromStateIndex;
private static bool SetScrollUpButton => CurrentFromState > 0;
private static bool SetScrollButtons => SetScrollDownButton || SetScrollUpButton;
private bool ScrollButtonPressed => ScrollDownButtonPressed() || ScrollUpButtonPressed();
*/
/*private bool SetScrollButtonsOnButtonsPressed()
{
    if (!SetScrollButtons) return false;
    if (ScrollButtonPressed)
    {
        if (scrollDownButtonPressed)
        {
            MoveTransitionUp = false;
            scrollDownButtonPressed = false;
        }

        if (scrollUpButtonPressed)
        {
            MoveTransitionUp = true;
            scrollUpButtonPressed = false;
        }

        reorderState = true;
        return true;
    }

    headerButtonRect.x -= 40;
    return false;
}*/
/*#region MoveTransitionUp

/*private static bool MoveTransitionUp
{
    set => _data.OnSetMoveTransitionUp(value);
}

#endregion

#endregion

#region SetStateEditorButtonOnButtonPressed

//private bool StateEditorButtonPressed => HeaderButton(headerButtonRect, SceneViewTools);

private bool SetStateEditorButtonOnButtonPressed()
{
    if (!StateEditorButtonPressed) return false;
    displayStateEditor = true;
    return true;
}*/

//#endregion

//#endregion