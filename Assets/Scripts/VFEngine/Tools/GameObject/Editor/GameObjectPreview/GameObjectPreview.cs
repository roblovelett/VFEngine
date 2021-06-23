using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using EditorUnity = UnityEditor.Editor;
using Text = VFEngine.Tools.GameObject.Editor.GameObjectPreview.Data.GameObjectPreviewText;

// ReSharper disable PossibleNullReferenceException
namespace VFEngine.Tools.GameObject.Editor.GameObjectPreview
{
    using static BindingFlags;
    using static CameraClearFlags;
    using static GUI;
    using static ScaleMode;
    using static EditorUnity;

    internal class GameObjectPreview
    {
        #region fields

        #region static fields

        private static object _previewData;
        private static Type _gameObjectInspectorType;
        private static Type _previewDataType;
        private static Color _light00;
        private static Color _light01;
        private static Color _guiColor;
        private static Color _backgroundColor;
        private static FieldInfo _renderUtilityField;
        private static MeshFilter _filter;
        private static MethodInfo _previewDataMethod;
        private static EditorUnity _cachedEditor;
        internal static PreviewRenderUtility RenderUtility;
        private static Renderer[] _renderers;

        #endregion

        #endregion

        #region properties

        internal RenderTexture OutputTexture { get; private set; }

        #region private static properties

        #endregion

        #endregion

        #region private methods

        #region static private methods

        #region initialization
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            _gameObjectInspectorType = typeof(EditorUnity).Assembly.GetType(Text.GameObjectInspector);
            _previewDataType = _gameObjectInspectorType.GetNestedType(Text.PreviewData, NonPublic);
            _previewDataMethod = _previewDataType.GetMethod(Text.GetPreviewData, NonPublic | Instance);
            _renderUtilityField = _previewDataType.GetField(Text.RenderUtility, Public | Instance);
            RenderUtility = null;
        }

        #endregion

        #endregion

        #endregion

        #region internal methods

        internal void RenderInteractivePreview(Rect rect)
        {
            if (!_cachedEditor) return;
            if (RenderUtility == null)
            {
                _previewData = _previewDataMethod.Invoke(_cachedEditor, null);
                RenderUtility = _renderUtilityField.GetValue(_previewData) as PreviewRenderUtility;
                _light00 = RenderUtility.lights[0].color;
                _light01 = RenderUtility.lights[1].color;
            }
            RenderUtility.lights[0].color = _light00 * 1.6f;
            RenderUtility.lights[1].color = _light01 * 6f;
            _backgroundColor = RenderUtility.camera.backgroundColor;
            RenderUtility.camera.backgroundColor =
                new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 0);
            RenderUtility.camera.clearFlags = Depth;
            _guiColor = color;
            color = new Color(1, 1, 1, 0);
            _cachedEditor.OnPreviewGUI(rect, null);
            color = _guiColor;
            OutputTexture = RenderUtility.camera.targetTexture;
        }

        internal void DrawPreviewTexture(Rect rect)
        {
            DrawTexture(rect, OutputTexture, ScaleToFit, true, 0);
        }

        internal static bool CanRender(UnityGameObject gameObject)
        {
            _renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in _renderers)
                switch (renderer)
                {
                    case MeshRenderer meshRenderer:
                        _filter = meshRenderer.gameObject.GetComponent<MeshFilter>();
                        return _filter && _filter.sharedMesh;
                    case SkinnedMeshRenderer skinnedMesh when skinnedMesh.sharedMesh:
                    case SpriteRenderer sprite when sprite.sprite:
                    case BillboardRenderer billboard when billboard.billboard && billboard.sharedMaterial: return true;
                }

            return false;
        }

        internal static void CreatePreview(UnityGameObject target)
        {
            if (_cachedEditor && _cachedEditor.target == target) return;
            RenderUtility = null;
            CreateCachedEditor(target, _gameObjectInspectorType, ref _cachedEditor);
        }

        #endregion
    }
}