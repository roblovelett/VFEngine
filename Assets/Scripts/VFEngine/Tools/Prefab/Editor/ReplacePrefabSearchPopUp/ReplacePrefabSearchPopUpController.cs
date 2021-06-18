using System;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool;
using UnityGameObject = UnityEngine.GameObject;
//using ModelView = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ReplacePrefabSearchPopUpModelView;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp
{
    /*internal class ReplacePrefabSearchPopUpController : EditorWindow
    {
        private ModelView replacePrefabSearchPopUp;
        private bool Initialized(ReplaceToolController replaceToolController)
        {
            return CreatedModelView(replaceToolController);
        }

        private bool CreatedModelView(ReplaceToolController replaceToolController)
        {
            Initialize(replaceToolController);
            return true;
        }

        internal ReplacePrefabSearchPopUpController()
        {
            Initialize();
        }

        private void Initialize()
        {
            replacePrefabSearchPopUp = new ModelView();
        }

        private void Initialize(ReplaceToolController replaceToolController)
        {
            replacePrefabSearchPopUp = new ModelView(this, replaceToolController);
        }

        private InvalidOperationException ControllerError { get; } =
            new InvalidOperationException("Cannot initialize Replace Prefab Search Pop Up's Controller.");

        private void OnEnable()
        {
            var replaceToolController = CreateInstance<ReplaceToolController>();
            if (!Initialized(replaceToolController)) throw ControllerError;
            replacePrefabSearchPopUp.Initialize();
        }

        internal void Show(Rect rect)
        {
            replacePrefabSearchPopUp.Show(rect);
        }

        private void OnDisable()
        {
            Close();
        }

        private void OnDestroy()
        {
            Close();
        }

        private bool CanCloseWindow => replacePrefabSearchPopUp.CloseWindow(focusedWindow != this);

        private void OnGUI()
        {
            if (CanCloseWindow) Close();
            replacePrefabSearchPopUp.OnGUI();
        }
        private new void Close()
        {
            replacePrefabSearchPopUp.SaveState();
            base.Close();
            replacePrefabSearchPopUp.Dispose();
        }
    }*/
}