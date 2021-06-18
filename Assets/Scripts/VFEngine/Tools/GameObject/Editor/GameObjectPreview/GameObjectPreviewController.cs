using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.GameObject.Editor.GameObjectPreview
{
    /*internal class GameObjectPreviewController
    {
        private static GameObjectPreviewModel _gameObjectPreview;
        private static bool CanInitializeGameObjectPreviewModel => _gameObjectPreview == null;
        internal RenderTexture OutputTexture => _gameObjectPreview.OutputTexture;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            InitializeDefault();
        }

        private static void InitializeDefault()
        {
            if (CanInitializeGameObjectPreviewModel) _gameObjectPreview = new GameObjectPreviewModel();
        }

        internal void CreatePreviewForTarget(UnityGameObject target)
        {
            _gameObjectPreview.CreatePreviewForTarget(target);
        }

        internal void RenderInteractivePreview(Rect rect)
        {
            _gameObjectPreview.RenderInteractivePreview(rect);
        }

        internal void DrawPreviewTexture(Rect rect)
        {
            _gameObjectPreview.DrawPreviewTexture(rect);
        }

        internal bool CanRenderAssets(UnityGameObject gameObject)
        {
            return _gameObjectPreview.CanRenderAssets(gameObject.GetComponentsInChildren<Renderer>());
        }

        internal GameObjectPreviewController()
        {
            InitializeDefault();
        }
    }*/
}