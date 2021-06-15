using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview.Data;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.GameObjectPreview
{
    using static ScaleMode;
    using static BindingFlags;
    using static CameraClearFlags;
    using static GUI;
    using static ScriptableObject;
    using static GameObjectPreviewText;
    using static EditorUnity;

    internal class GameObjectPreviewModel
    {
        private static GameObjectPreviewData _data;

        private static bool CreatedCachedEditor
        {
            get => _data.CreatedCachedEditor;
            set => _data.CreatedCachedEditor = value;
        }

        private static EditorUnity CachedEditor
        {
            get => _data.CachedEditor;
            set => _data.CachedEditor = value;
        }

        internal RenderTexture OutputTexture
        {
            get => _data.OutputTexture;
            private set => _data.OutputTexture = value;
        }

        private static PreviewRenderUtility RenderUtility
        {
            get => _data.RenderUtility;
            set => _data.RenderUtility = value;
        }

        private static BillboardRenderer BillboardRenderer
        {
            get => _data.BillboardRenderer;
            set => _data.BillboardRenderer = value;
        }

        private static SpriteRenderer SpriteRenderer
        {
            get => _data.SpriteRenderer;
            set => _data.SpriteRenderer = value;
        }

        private static Component Component
        {
            get => _data.Component;
            set => _data.Component = value;
        }

        private static SkinnedMeshRenderer SkinnedMeshRenderer
        {
            get => _data.SkinnedMeshRenderer;
            set => _data.SkinnedMeshRenderer = value;
        }

        private static MeshFilter MeshFilter
        {
            get => _data.MeshFilter;
            set => _data.MeshFilter = value;
        }

        private static UnityGameObject Renderer
        {
            get => _data.Renderer;
            set => _data.Renderer = value;
        }

        private static Color? Light0Color
        {
            get => RenderUtility.lights[0].color;
            set => RenderUtilityLight0Color(value);
        }

        private static Color? Light1Color
        {
            get => RenderUtility.lights[1].color;
            set => RenderUtilityLight1Color(value);
        }

        private static CameraClearFlags RenderUtilityCameraClearFlags
        {
            set => RenderUtility.camera.clearFlags = value;
        }

        private static Color RenderUtilityCameraBackgroundColor
        {
            set => RenderUtility.camera.backgroundColor = value;
        }

        private static bool HasSprite => SpriteInitialized;
        private static bool CanInitializeData => _data == null;
        private static bool HasBillboard => BillboardInitialized;
        private static bool HasSharedMesh => SharedMeshInitialized;
        private static bool SpriteInitialized => SpriteRenderer.sprite != null;
        private static bool HasRenderUtilityLights => RenderUtility.lights[0] != null;
        private static bool HasRenderUtility => RenderUtility != null;
        private static bool InitializedComponent => Component != null;
        private static bool HasSharedMaterial => SharedMaterialInitialized;
        private static bool BillboardInitialized => BillboardRenderer.billboard != null;
        private static bool SharedMaterialInitialized => BillboardRenderer.billboard != null;
        private static bool SharedMeshInitialized => HasMeshFilter || HasSkinnedMeshRenderer;
        private static bool HasMeshFilter => HasComponent<MeshFilter>() && MeshFilterInitialized();
        private static bool CanInitializeRenderUtility => !HasRenderUtility || !HasRenderUtilityLights;
        private static bool CanInitializeRenderUtilityLights => Light0Color == null && Light1Color == null;
        private static bool CanRenderInteractivePreview => CreatedCachedEditor == false && CanInitializeRenderUtility;

        private static bool HasMeshRenderer =>
            HasComponent<MeshRenderer>() && HasComponent<MeshFilter>() && HasSharedMesh;

        private static bool HasSpriteRenderer =>
            HasComponent<SpriteRenderer>() && SpriteRendererInitialized() && HasSprite;

        private static bool HasSkinnedMeshRenderer =>
            HasComponent<SkinnedMeshRenderer>() && SkinnedMeshRendererInitialized();

        private static bool RenderersInitialized =>
            HasMeshRenderer || HasSkinnedMeshRenderer || HasSpriteRenderer || HasBillboardRenderer;

        private static bool HasBillboardRenderer => HasComponent<BillboardRenderer>() &&
                                                    BillboardRendererInitialized() && HasBillboard && HasSharedMaterial;

        private static Type GameObjectInspectorType => typeof(EditorUnity).Assembly.GetType(GameObjectInspector);
        private static Color GUIColor => color;
        private static Color CameraBackgroundColor => RenderUtility.camera.backgroundColor;
        private static RenderTexture RenderUtilityCameraTargetTexture => RenderUtility.camera.targetTexture;

        private static void InitializeData()
        {
            if (CanInitializeData) _data = new GameObjectPreviewData();
            else _data.Initialize();
        }

        private static void ResetRenderUtility(out EditorUnity cachedEditor)
        {
            cachedEditor = CreateInstance<EditorUnity>();
            RenderUtility = null;
        }

        private void HideDefaultPreviewTexture(Rect rect)
        {
            color = new Color(1, 1, 1, 0);
            CachedEditor.OnPreviewGUI(rect, null);
            color = GUIColor;
            OutputTexture = RenderUtilityCameraTargetTexture;
        }

        private static void InitializeRenderUtilityCamera()
        {
            RenderUtilityCameraBackgroundColor = new Color(CameraBackgroundColor.r, CameraBackgroundColor.g,
                CameraBackgroundColor.b, 0);
            RenderUtilityCameraClearFlags = Depth;
        }

        private static void RenderUtilityLight0Color(Color? value)
        {
            if (value != null) RenderUtility.lights[0].color = (Color) value;
        }

        private static void RenderUtilityLight1Color(Color? value)
        {
            if (value != null) RenderUtility.lights[1].color = (Color) value;
        }

        private static void InitializeRenderUtilityLights()
        {
            if (!CanInitializeRenderUtilityLights) return;
            Light0Color *= 1.6f;
            Light1Color *= 6f;
        }

        private static void InitializeRenderUtility()
        {
            if (!CanRenderInteractivePreview) return;
            RenderUtility =
                GameObjectInspectorType.GetMethod(GetPreviewData, NonPublic | Instance)?.Invoke(CachedEditor, null) as
                    PreviewRenderUtility;
        }

        private static void InitializeCachedEditor(UnityObject target, ref EditorUnity cachedEditor)
        {
            CreateCachedEditor(target, GameObjectInspectorType, ref cachedEditor);
            CachedEditor = cachedEditor;
            CreatedCachedEditor = true;
        }

        private static bool BillboardRendererInitialized()
        {
            BillboardRenderer = Component as BillboardRenderer;
            return BillboardRenderer != null;
        }

        private static bool MeshFilterInitialized()
        {
            MeshFilter = Component as MeshFilter;
            return MeshFilter != null;
        }

        private static bool SkinnedMeshRendererInitialized()
        {
            SkinnedMeshRenderer = Component as SkinnedMeshRenderer;
            return SkinnedMeshRenderer != null;
        }

        private static bool SpriteRendererInitialized()
        {
            SpriteRenderer = Component as SpriteRenderer;
            return SpriteRenderer != null;
        }

        private static bool CanCreatePreviewForTarget(UnityObject target)
        {
            return !CachedEditor && CreatedCachedEditor == false || CachedEditor.target != target;
        }

        private static bool HasRenderers(UnityGameObject renderer)
        {
            Renderer = renderer;
            return RenderersInitialized;
        }

        private static bool HasComponent<T>() where T : Component
        {
            //Component = Renderer.GetComponentNoAlloc<T>();
            return InitializedComponent;
        }

        internal void CreatePreviewForTarget(UnityGameObject target)
        {
            if (!CanCreatePreviewForTarget(target)) return;
            ResetRenderUtility(out var cachedEditor);
            InitializeCachedEditor(target, ref cachedEditor);
        }

        internal void RenderInteractivePreview(Rect rect)
        {
            InitializeRenderUtility();
            InitializeRenderUtilityLights();
            InitializeRenderUtilityCamera();
            HideDefaultPreviewTexture(rect);
        }

        internal void DrawPreviewTexture(Rect rect)
        {
            DrawTexture(rect, OutputTexture, ScaleToFit, true, 0);
        }

        internal bool CanRenderAssets(IEnumerable<Renderer> renderers)
        {
            return renderers.Any(renderer => HasRenderers(renderer.gameObject));
        }
        
        internal GameObjectPreviewModel()
        {
            InitializeData();
        }
    }
}