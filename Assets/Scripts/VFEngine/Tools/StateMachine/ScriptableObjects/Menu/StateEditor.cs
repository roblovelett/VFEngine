using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Menu
{
    using static EditorGUI;
    using static EditorStyles;
    using static Undo;
    using static EditorGUIUtility;
    using static EditorText;
    using static GUI;
    using static GUIContent;
    using static TransitionTableEditor;

    [CustomEditor(typeof(StateSO))]
    internal class StateEditor : EditorUnity
    {
        private ReorderableList list;
        private SerializedProperty actions;

        // ReSharper disable UnusedParameter.Local
        private void OnEnable()
        {
            undoRedoPerformed += DoUndo;
            actions = serializedObject.FindProperty(ActionsProperty);
            list = new ReorderableList(serializedObject, actions, true, true, true, true);
            list.elementHeight *= 1.5f;
            list.drawHeaderCallback += rect => Label(rect, Actions);
            list.onAddCallback += l =>
            {
                var count = l.count;
                l.serializedProperty.InsertArrayElementAtIndex(count);
                var prop = l.serializedProperty.GetArrayElementAtIndex(count);
                prop.objectReferenceValue = null;
            };
            list.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var r = rect;
                r.height = singleLineHeight;
                r.y += 5;
                r.x += 5;
                var prop = list.serializedProperty.GetArrayElementAtIndex(index);
                if (prop.objectReferenceValue != null)
                {
                    var label = prop.objectReferenceValue.name;
                    r.width = 35;
                    PropertyField(r, prop, none);
                    r.width = rect.width - 50;
                    r.x += 42;
                    Label(r, label, boldLabel);
                }
                else
                {
                    PropertyField(r, prop, none);
                }
            };
            OnChangedCallback(ref list);
        }

        private void OnDisable()
        {
            undoRedoPerformed -= DoUndo;
        }

        public override void OnInspectorGUI()
        {
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DoUndo()
        {
            serializedObject.UpdateIfRequiredOrScript();
        }
    }
}