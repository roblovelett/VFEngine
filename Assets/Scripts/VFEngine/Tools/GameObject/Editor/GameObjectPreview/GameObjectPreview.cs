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

    internal class GameObjectPreview
    {
        #region fields

        #region static fields

        private static Type _gameObjectInspectorType;
        private static Type _previewDataType;
        private static MethodInfo _previewDataMethod;
        private static FieldInfo _renderUtilityField;
        private static PreviewRenderUtility _renderUtility;
        private static MeshFilter _filter;
        private static Renderer[] _renderers;

        #endregion

        private object previewData;
        private Color light00;
        private Color light01;
        private Color guiColor;
        private Color backgroundColor;
        private EditorUnity cachedEditor;
        
        #endregion

        #region properties

        internal RenderTexture OutputTexture { get; private set; }

        #region private static properties

        private static bool HasRenderUtility => _renderUtility != null;

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
        }

        #endregion

        #endregion

        #endregion

        #region internal methods

        internal void RenderInteractivePreview(Rect rect)
        {
            if (!cachedEditor) return;
            if (HasRenderUtility || _renderUtility.lights[0] == null)
            {
                previewData = _previewDataMethod.Invoke(cachedEditor, null);
                _renderUtility = _renderUtilityField.GetValue(previewData) as PreviewRenderUtility;
                if (HasRenderUtility)
                {
                    light00 = _renderUtility.lights[0].color;
                    light01 = _renderUtility.lights[1].color;
                }
            }

            if (!HasRenderUtility) return;
            _renderUtility.lights[0].color = light00 * 1.6f;
            _renderUtility.lights[1].color = light01 * 6f;
            backgroundColor = _renderUtility.camera.backgroundColor;
            _renderUtility.camera.backgroundColor =
                new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 0);
            _renderUtility.camera.clearFlags = Depth;
            guiColor = color;
            color = new Color(1, 1, 1, 0);
            cachedEditor.OnPreviewGUI(rect, null);
            color = guiColor;
            OutputTexture = _renderUtility.camera.targetTexture;
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

        internal void CreatePreview(UnityGameObject target)
        {
            if (cachedEditor && cachedEditor.target == target) return;
            _renderUtility = null;
            EditorUnity.CreateCachedEditor(target, _gameObjectInspectorType, ref cachedEditor);
        }

        #endregion
    }
}