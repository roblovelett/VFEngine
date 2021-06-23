/*

// ReSharper disable RedundantAssignment
namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.State
{
    internal class DisplayStateEditor
    {
        /*private  TransitionTableEditorDataSO _stateEditorData;
        private  EditorUnity _cachedStateEditor;
        private StateHeaderDataIn headerDataIn;

        internal DisplayStateEditor()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _stateEditorData = CreateInstance<TransitionTableEditorDataSO>();
            _cachedStateEditor = CreateInstance<EditorUnity>();
        }

        #region DisplayStateEditor

        private static List<List<DisplayTransition>> TransitionsByFromStates => TransitionTableEditorDataSO.TransitionsByFromStates;

        private static int CurrentFromState => TransitionTableEditorDataSO.CurrentFromState;

        private static EditorUnity CachedStateEditor
        {
            get => _stateEditorData.CachedStateEditor;
            set => _stateEditorData.OnSetCachedStateEditor(value);
        }

        private static bool DisplayingStateEditor
        {
            set => _stateEditorData.OnSetDisplayingStateEditor(value);
        }

        private static List<DisplayTransition> CurrentTransitionsByFromStates =>
            TransitionsByFromStates[CurrentFromState];

        private static bool CanCreateCachedStateEditor => CachedStateEditor == null;

        private static SerializedProperty InitialFromState =>
            CurrentTransitionsByFromStates[0].SerializedTransition.FromState;

        private static Object CurrentTransitionsObject => InitialFromState.objectReferenceValue;
        private static Type StateEditorType => typeof(Editor.StateEditor);

        public static void Display(ref TransitionTableEditorDataSO data)
        {
            SetStateEditorData(data);
            if (CanCreateCachedStateEditor) CreateCachedStateEditor();
            else SetCachedStateEditor();
            OnDisplayStateEditor(ref data);

            static void CreateCachedStateEditor()
            {
                CachedStateEditor = CreateEditor(CurrentTransitionsObject, StateEditorType);
            }

            static void SetCachedStateEditor()
            {
                _cachedStateEditor = CachedStateEditor;
                CreateCachedEditor(CurrentTransitionsObject, StateEditorType, ref _cachedStateEditor);
            }

            static void OnDisplayStateEditor(ref TransitionTableEditorDataSO dataInternal)
            {
                DisplayingStateEditor = true;
                SetData(ref dataInternal);
            }
        }

        private static void SetStateEditorData(TransitionTableEditorDataSO data)
        {
            _stateEditorData = data;
        }

        private static void SetData(ref TransitionTableEditorDataSO data)
        {
            data = _stateEditorData;
        }

        #endregion*/
  /*  }
}*/