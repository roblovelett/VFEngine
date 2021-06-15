using UnityEditor;
using UnityEngine;
using EditorUnity = UnityEditor.Editor;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.GameObjectPreview.Data
{
    internal class GameObjectPreviewData
    {
        internal bool CreatedCachedEditor { get; set; }
        internal UnityGameObject Renderer { get; set; }
        internal MeshFilter MeshFilter { get; set; }
        internal SkinnedMeshRenderer SkinnedMeshRenderer { get; set; }
        internal EditorUnity CachedEditor { get; set; }
        internal RenderTexture OutputTexture { get; set; }
        internal SpriteRenderer SpriteRenderer { get; set; }
        internal BillboardRenderer BillboardRenderer { get; set; }
        internal PreviewRenderUtility RenderUtility { get; set; }
        internal Component Component { get; set; }

        internal GameObjectPreviewData()
        {
            Initialize();
        }

        internal void Initialize()
        {
            CreatedCachedEditor = false;
            Renderer = null;
            MeshFilter = null;
            CachedEditor = null;
            OutputTexture = null;
            SpriteRenderer = null;
            BillboardRenderer = null;
            RenderUtility = null;
        }
    }
}