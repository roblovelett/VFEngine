using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Editor.Data;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Editor
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

        // ReSharper disable UnusedParameter.Local
        private void OnEnable()
        {
            undoRedoPerformed += DoUndo;
            SerializedProperty reorderableListSerializedProperty;
            var actions = serializedObject.FindProperty(ActionsProperty);
            list = new ReorderableList(serializedObject, actions, true, true, true, true);
            list.elementHeight *= 1.5f;
            list.drawHeaderCallback += rect => Label(rect, Actions);
            list.onAddCallback += l =>
            {
                var reorderableListItemsAmount = l.count;
                l.serializedProperty.InsertArrayElementAtIndex(reorderableListItemsAmount);
                reorderableListSerializedProperty =
                    l.serializedProperty.GetArrayElementAtIndex(reorderableListItemsAmount);
                reorderableListSerializedProperty.objectReferenceValue = null;
            };
            list.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var reorderableListRect = rect;
                reorderableListRect.height = singleLineHeight;
                reorderableListRect.y += 5;
                reorderableListRect.x += 5;
                reorderableListSerializedProperty = list.serializedProperty.GetArrayElementAtIndex(index);
                if (reorderableListSerializedProperty.objectReferenceValue != null)
                {
                    var reorderableListLabel = reorderableListSerializedProperty.objectReferenceValue.name;
                    reorderableListRect.width = 35;
                    PropertyField(reorderableListRect, reorderableListSerializedProperty, none);
                    reorderableListRect.width = rect.width - 50;
                    reorderableListRect.x += 42;
                    Label(reorderableListRect, reorderableListLabel, boldLabel);
                }
                else
                {
                    PropertyField(reorderableListRect, reorderableListSerializedProperty, none);
                }
            };
            OnListChanged(ref list);
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