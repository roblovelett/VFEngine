using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.GameObject.Editor.GameObjectPreview
{
    public class GameObjectPreviewController
    {
        private static GameObjectPreviewModel _gameObjectPreview;
        private static bool CanInitializeGameObjectPreviewModel => _gameObjectPreview == null;
        public RenderTexture OutputTexture => _gameObjectPreview.OutputTexture;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            InitializeDefault();
        }

        private static void InitializeDefault()
        {
            if (CanInitializeGameObjectPreviewModel) _gameObjectPreview = new GameObjectPreviewModel();
        }

        public void CreatePreviewForTarget(UnityGameObject target)
        {
            _gameObjectPreview.CreatePreviewForTarget(target);
        }

        public void RenderInteractivePreview(Rect rect)
        {
            _gameObjectPreview.RenderInteractivePreview(rect);
        }

        public void DrawPreviewTexture(Rect rect)
        {
            _gameObjectPreview.DrawPreviewTexture(rect);
        }

        public bool CanRenderAssets(UnityGameObject gameObject)
        {
            return _gameObjectPreview.CanRenderAssets(gameObject.GetComponentsInChildren<Renderer>());
        }

        public GameObjectPreviewController()
        {
            InitializeDefault();
        }
    }
}