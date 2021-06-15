using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.State
{
    using static EditorGUIUtility;
    using static EditorGUILayout;
    using static StateHeaderData.Text;

    public class StateHeaderData
    {
        public StateHeaderDataOut DataOut { get; private set; }
        public Rect Header { get; private set; }
        public Rect Toggled { get; private set; }
        public string Label { get; private set; }
        public Rect Button { get; private set; }
        public bool ToggledIsCurrentFromState => ToggledIndex == CurrentFromState;
        public bool SetScrollButtons => SetScrollDownButton || SetScrollUpButton;
        public int CurrentFromState => dataIn.CurrentFromState;
        public int ToggledIndexOnHeader => ToggledIsCurrentFromState ? -1 : ToggledIndex;
        private StateHeaderDataIn dataIn;
        private Rect initialHeader;
        private Rect initialButton;
        private bool SetScrollDownButton => dataIn.SetScrollDownButton;
        private bool SetScrollUpButton => dataIn.SetScrollUpButton;
        private string CurrentTransitionsName => dataIn.CurrentTransitionsName;
        private bool AddInitialStateToLabel => dataIn.AddInitialStateToLabel;

        private bool MoveTransitionUp
        {
            set => DataOut.OnSetMoveTransitionUp(value);
        }

        public bool ReorderState
        {
            get => DataOut.ReorderState;
            private set => DataOut.OnSetReorderState(value);
        }

        public bool DisplayStateEditor
        {
            get => DataOut.DisplayStateEditor;
            private set => DataOut.OnSetDisplayStateEditor(value);
        }

        public bool EarlyOut
        {
            get => DataOut.EarlyOut;
            private set => DataOut.OnSetEarlyOut(value);
        }

        private int ToggledIndex
        {
            get => dataIn.ToggledIndex;
            set => DataOut.OnSetToggledIndex(value);
        }

        private void Initialize()
        {
            SetToggled(new Rect());
            SetButton(new Rect());
            SetToggled(new Rect());
            SetHeader(new Rect());
            SetInitialHeader(new Rect());
            SetInitialButton(new Rect());
            SetDataIn(new StateHeaderDataIn());
            SetDataOut(new StateHeaderDataOut());
            dataIn.OnInitialize();
            DataOut.OnInitialize();
        }

        private void SetDataIn(StateHeaderDataIn dataInInternal)
        {
            dataIn = dataInInternal;
        }

        private void SetDataOut(StateHeaderDataOut dataOut)
        {
            DataOut = dataOut;
        }

        private void Set(StateHeaderDataIn dataInInternal)
        {
            SetDataIn(dataInInternal);
        }

        private void SetToggled(Rect toggled)
        {
            Toggled = toggled;
        }

        private void SubtractFromToggledWidth(float subtractFromToggled)
        {
            var toggled = Toggled;
            toggled.width -= subtractFromToggled;
            SetToggled(toggled);
        }

        private void SetButton(Rect button)
        {
            Button = button;
        }

        private void SetHeader()
        {
            SetInitialHeader(BeginVertical());
            SetHeader(initialHeader);
            SetHeaderHeight(singleLineHeight);
        }

        private void SetHeader(Rect header)
        {
            Header = header;
        }

        private void AddToHeaderRectX(float x)
        {
            var header = Header;
            header.x += x;
            SetHeader(header);
        }

        private void SetInitialButton(Rect initialButtonInternal)
        {
            initialButton = initialButtonInternal;
        }

        private void SetInitialHeader(Rect initialHeaderInternal)
        {
            initialHeader = initialHeaderInternal;
        }

        private void SetHeaderHeight(float height)
        {
            var header = Header;
            header.height = height;
            SetHeader(header);
        }

        private void SetHeaderTitle()
        {
            AddToHeaderRectX(5);
            SetLabel(CurrentTransitionsName);
            if (AddInitialStateToLabel) AddToLabel(AddInitialState);
        }

        private void SetToggled()
        {
            SetToggled(Header);
            SubtractFromToggledWidth(140);
        }

        private void SetLabel(string label)
        {
            Label = label;
        }

        private void AddToLabel(string addToLabel)
        {
            Label += addToLabel;
        }

        private void SetHeaderButtons()
        {
            SetInitialButton(new Rect(Header.width - 25, Header.y, 35, 20));
            SetButton(initialButton);

            //
        }

        private void SubtractFromButtonX(float x)
        {
            var button = Button;
            button.x -= x;
            SetButton(button);
        }

        private void SetToggledIndex(int toggledIndex)
        {
            ToggledIndex = toggledIndex;
        }

        private void SetMoveTransitionUp(bool moveTransitionUp)
        {
            MoveTransitionUp = moveTransitionUp;
        }

        private void SetReorderState(bool reorderState)
        {
            ReorderState = reorderState;
        }

        private void SetDisplayStateEditor(bool displayStateEditor)
        {
            DisplayStateEditor = displayStateEditor;
        }

        private void SetEarlyOut(bool earlyOut)
        {
            EarlyOut = earlyOut;
        }

        private void ScrollDownButtonPressed()
        {
            SetMoveTransitionUp(false);
            OnScrollButtonPressed();
        }

        private void OnScrollButtonPressed()
        {
            SetReorderState(true);
            OnButtonPressed();
        }

        private void ScrollUpButtonPressed()
        {
            SetMoveTransitionUp(true);
            OnScrollButtonPressed();
        }

        private void SetScrollButtonsX()
        {
            SubtractFromButtonX(40);
        }

        private void SceneViewButtonPressed()
        {
            SetDisplayStateEditor(true);
            OnButtonPressed();
        }

        private void OnButtonPressed()
        {
            SetEarlyOut(true);
        }

        public void OnInitialize()
        {
            Initialize();
        }

        public void OnSet(StateHeaderDataIn dataInInternal)
        {
            Set(dataInInternal);
        }

        public void OnSetHeader()
        {
            SetHeader();
        }

        public void OnSetHeaderTitle()
        {
            SetHeaderTitle();
        }

        public void OnSetToggled()
        {
            SetToggled();
        }

        public void OnSetToggledIndex(int toggledIndex)
        {
            SetToggledIndex(toggledIndex);
        }

        public void OnSetHeaderButtons()
        {
            SetHeaderButtons();
        }

        public void OnScrollDownButtonPressed()
        {
            ScrollDownButtonPressed();
        }

        public void OnScrollUpButtonPressed()
        {
            ScrollUpButtonPressed();
        }

        public void OnSetScrollButtonsX()
        {
            SetScrollButtonsX();
        }

        public void OnSceneViewButtonPressed()
        {
            SceneViewButtonPressed();
        }

        public struct StateHeaderDataIn
        {
            public bool AddInitialStateToLabel { get; private set; }
            public bool SetScrollDownButton { get; private set; }
            public bool SetScrollUpButton { get; private set; }
            public int CurrentFromState { get; private set; }
            public int ToggledIndex { get; private set; }
            public string CurrentTransitionsName { get; private set; }

            private void SetAddInitialStateToLabel(bool addInitialStateToLabel)
            {
                AddInitialStateToLabel = addInitialStateToLabel;
            }

            private void SetSetScrollDownButton(bool setScrollDownButton)
            {
                SetScrollDownButton = setScrollDownButton;
            }

            private void SetSetScrollUpButton(bool setScrollUpButton)
            {
                SetScrollUpButton = setScrollUpButton;
            }

            private void SetToggledIndexInternal(int toggledIndex)
            {
                ToggledIndex = toggledIndex;
            }

            private void SetCurrentFromStateInternal(int currentFromState)
            {
                CurrentFromState = currentFromState;
            }

            private void SetCurrentTransitionsName(string currentTransitionsName)
            {
                CurrentTransitionsName = currentTransitionsName;
            }

            private static void Initialize(StateHeaderDataIn data)
            {
                InitializeCurrentFromState();
                InitializeToggledIndex();
                InitializeCurrentTransitionsName();
                InitializeAddInitialStateToLabel();
                InitializeSetScrollDownButton();
                InitializeSetScrollUpButton();

                void InitializeCurrentFromState()
                {
                    data.SetCurrentFromStateInternal(0);
                }

                void InitializeToggledIndex()
                {
                    data.SetToggledIndexInternal(0);
                }

                void InitializeCurrentTransitionsName()
                {
                    data.SetCurrentTransitionsName("");
                }

                void InitializeAddInitialStateToLabel()
                {
                    data.SetAddInitialStateToLabel(false);
                }

                void InitializeSetScrollDownButton()
                {
                    data.SetSetScrollDownButton(false);
                }

                void InitializeSetScrollUpButton()
                {
                    data.SetSetScrollUpButton(false);
                }
            }

            private void Set(bool addAdditionStateToLabel, bool setScrollDownButton, bool setScrollUpButton, int currentFromState, string currentTransitionsName)
            {
                SetAddInitialStateToLabel(addAdditionStateToLabel);
                SetSetScrollDownButton(setScrollDownButton);
                SetSetScrollUpButton(setScrollUpButton);
                SetCurrentFromStateInternal(currentFromState);
                SetCurrentTransitionsName(currentTransitionsName);
            }

            public void OnInitialize()
            {
                Initialize(this);
            }

            public void OnSet(bool addAdditionStateToLabel, bool setScrollDownButton, bool setScrollUpButton, int currentFromState, string currentTransitionsName)
            {
                Set(addAdditionStateToLabel, setScrollDownButton, setScrollUpButton, currentFromState, currentTransitionsName);
            }
        }

        public struct StateHeaderDataOut
        {
            public bool ReorderState { get; private set; }
            public bool MoveTransitionUp { get; private set; }
            public bool DisplayStateEditor { get; private set; }
            public bool EarlyOut { get; private set; }
            public int ToggledIndex { get; private set; }

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

            private void SetEarlyOut(bool earlyOut)
            {
                EarlyOut = earlyOut;
            }

            private void SetToggledIndex(int toggledIndex)
            {
                ToggledIndex = toggledIndex;
            }

            private static void Initialize(StateHeaderDataOut data)
            {
                InitializeReorderState();
                InitializeMoveTransitionUp();
                InitializeDisplayStateEditor();
                InitializeEarlyOut();
                InitializeToggledIndex();

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

                void InitializeEarlyOut()
                {
                    data.SetEarlyOut(false);
                }

                void InitializeToggledIndex()
                {
                    data.SetToggledIndex(0);
                }
            }

            public void OnInitialize()
            {
                Initialize(this);
            }

            public void OnSetReorderState(bool reorderState)
            {
                SetReorderState(reorderState);
            }

            public void OnSetMoveTransitionUp(bool moveTransitionUp)
            {
                SetMoveTransitionUp(moveTransitionUp);
            }

            public void OnSetDisplayStateEditor(bool displayStateEditor)
            {
                SetDisplayStateEditor(displayStateEditor);
            }

            public void OnSetEarlyOut(bool earlyOut)
            {
                SetEarlyOut(earlyOut);
            }

            public void OnSetToggledIndex(int toggledIndex)
            {
                SetToggledIndex(toggledIndex);
            }
        }

        public struct Text
        {
            public const string AddInitialState = " (Initial State)";
            public const string ScrollDown = "scrolldown";
            public const string ScrollUp = "scrollup";
            public const string SceneViewTools = "SceneViewTools";
        }
    }
}

//private bool HasButton(Rect position, string icon) => Button(position, IconContent(icon));
//public bool SetButtons => SetButtonsInternal;
//private bool SetButtonsInternal => SetScrollDownButton || SetScrollUpButton || HasSceneViewToolsButton;
//private bool HasSceneViewToolsButton => HasButton(Button, SceneViewTools);
//private bool HasScrollDownButton => HasButton(Button, ScrollDown);
//private bool HasScrollUpButton => HasButton(Button, ScrollUp);
//private bool HasScrollButtons => HasScrollDownButton || HasScrollUpButton;
//private bool ScrollUpButtonPressed => HasScrollButtons && HasScrollUpButton;
//private bool ScrollDownButtonPressed => HasScrollButtons && HasScrollDownButton;
//private bool ReorderStateInternal => ScrollUpButtonPressed || ScrollDownButtonPressed;
//private bool MoveTransitionUpInternal => ReorderStateInternal && ScrollUpButtonPressed;
//private bool DisplayStateEditorInternal => HasSceneViewToolsButton;
//button = InitialButton;
//if (!SetButtons) return;
//if (HasScrollButtons) button.x -= 40;
//ReorderState = ReorderStateInternal;
//MoveTransitionUp = MoveTransitionUpInternal;
//DisplayStateEditor = DisplayStateEditorInternal;
//{ get; private set }
// for use
//public bool EarlyOut { get; private set; }
//public bool displayStateEditor;// { get; private set; }
//public bool ScrollDownButtonPressed { get; private set; }
//public bool ScrollUpButtonPressed { get; private set; }

//{ get; private set; }

//public StateHeaderDataOut Out => @out;

//public int ToggledIndex { get; private set; }

//private Rect toggled;
//private Rect header;
//private Rect button;

// internal
/*private float HeaderHeight
{
    set
    {
        var headerInternal = Header;
        headerInternal.height = value;
        Header = headerInternal;
    }
}*/
/*private float HeaderX
{
    get => Header.x;
    set
    {
        var headerInternal = Header;
        headerInternal.x = value;
        Header = headerInternal;
    }
}*/
/*
        private float ToggledWidth
        {
            get => Toggled.width;
            set
            {
                var toggledInternal = Toggled;
                toggledInternal.width = value;
                Toggled = toggledInternal;
            }
        }*/
// { get; private set; }
//{
//get { //return new Rect(header.width - 25, header.y, 35, 20); }
//}

// => BeginHorizontal();

//private int toggledIndexOnGroupedHeader;
//private string ;
//private bool addInitialStateToLabel;
//public int CurrentFromState { get; private set; }
//private bool setScrollUpButton;
//private bool setScrollDownButton;
//private bool reorderState;
//private bool moveTransitionUp;

//private bool moveTransitionUp;
//private bool earlyOut;
//public bool AddInitialStateToLabel { get; private set; }
//public int ToggledIndex { get; private set; }
//public bool ToggledIndexIsCurrentFromState { get; private set; }
//public int ToggledIndexOnGroupedHeader { get; private set; }

//public int CurrentFromState => @in.CurrentFromState;
//private readonly TransitionTableEditorDataIn @in;
//public void OnSetHeader(StateHeaderDataIn stateHeaderDataIn)
//{
//SetHeader(stateHeaderDataIn);
//}
//private bool setScrollDownButton;
//private bool setScrollUpButton;
//private bool addInitialStateToLabel;
//private string currentTransitionsName;
//private void SetHeader(StateHeaderDataIn dataIn)
//{
//SetCurrentFromState(dataIn.CurrentFromState);
//SetSetScrollDownButton(dataIn.SetScrollDownButton);
//SetSetScrollUpButton(dataIn.SetScrollUpButton);
//SetAddInitialStateToLabel(dataIn.AddInitialStateToLabel);
//SetCurrentTransitionsName(dataIn.CurrentTransitionsName);
//CurrentFromState = dataIn.CurrentFromState;
//setScrollDownButton = dataIn.SetScrollDownButton;
//setScrollUpButton = dataIn.SetScrollUpButton;
//addInitialStateToLabel = dataIn.AddInitialStateToLabel;
//currentTransitionsName = dataIn.CurrentTransitionsName;
//SetHeader(InitialHeader);
//header = InitialHeader;
//SetHeaderHeight(singleLineHeight);
//header.height = singleLineHeight;
//}
/*private void SetSetScrollDownButton(bool setScrollDownButton)
{
    //SetScrollDownButton = setScrollDownButton;
}*/
/*private void SetHeader(Rect header)
{
    Header = header;
}*/
/*private void SetCurrentFromState(int currentFromState)
{
    CurrentFromState = currentFromState;
}*/