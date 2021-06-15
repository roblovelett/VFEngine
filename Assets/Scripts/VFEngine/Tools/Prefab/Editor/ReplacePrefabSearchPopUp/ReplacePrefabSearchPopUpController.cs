using System;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp
{
    using static Selection;
    using static ReplacePrefabSearchPopUpText;

    internal class ReplacePrefabSearchPopUpController : EditorWindow
    {
        private static Model _replacePrefabSearchPopup;

        internal ReplacePrefabSearchPopUpController()
        {
            if (!Initialized(_replacePrefabSearchPopup))
                throw new InvalidOperationException(CannotInitializeController);
        }

        private bool Initialized(Model model)
        {
            return InitializedModel() && InitializedExistingModel(model);
        }

        private bool InitializedModel()
        {
            _replacePrefabSearchPopup ??= new Model();
            return true;
        }

        private bool InitializedExistingModel(Model model)
        {
            _replacePrefabSearchPopup.Initialize(this, model);
            return true;
        }

        internal void StartPosition(Vector2 startPosition)
        {
            _replacePrefabSearchPopup.InitializeStartPosition(startPosition);
        }

        internal void StartSize(Vector2 startSize)
        {
            _replacePrefabSearchPopup.InitializeStartSize(startSize);
        }

        internal void Position(Rect positionInternal)
        {
            _replacePrefabSearchPopup.InitializePosition(positionInternal);
        }

        internal void Show(Rect rect)
        {
            _replacePrefabSearchPopup.Show(rect, new Rect(rect.position, rect.size));
        }

        internal void Initialize()
        {
            _replacePrefabSearchPopup.Initialize(gameObjects);
        }

        internal void InitialSize()
        {
            _replacePrefabSearchPopup.InitialSize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            _replacePrefabSearchPopup.OnDisable();
        }

        internal new void Close()
        {
            _replacePrefabSearchPopup.Close();
            base.Close();
        }

        private bool CloseWindow => _replacePrefabSearchPopup.CloseWindow;

        private void OnGUI()
        {
            _replacePrefabSearchPopup.OnGUI(focusedWindow, this);
            if (CloseWindow) Close();
        }
    }
}