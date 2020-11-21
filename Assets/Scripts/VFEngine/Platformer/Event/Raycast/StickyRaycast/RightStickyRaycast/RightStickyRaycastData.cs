using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    [CreateAssetMenu(fileName = "RightStickyRaycastData", menuName = PlatformerRightStickyRaycastDataPath, order = 0)]
    [InlineEditor]
    public class RightStickyRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        private static readonly string RightStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{RightStickyRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public StickyRaycastRuntimeData StickyRaycastRuntimeData { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public float BoundsWidth { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public float BoundsHeight { get; set; }
        public float RayOffset { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public RightStickyRaycastRuntimeData RuntimeData { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }
        public float RightStickyRaycastLength { get; set; }
        public Vector2 RightStickyRaycastOrigin { get; } = Vector2.zero;

        public float RightStickyRaycastOriginX
        {
            set => value = RightStickyRaycastOrigin.x;
        }

        public float RightStickyRaycastOriginY
        {
            set => value = RightStickyRaycastOrigin.y;
        }

        public static readonly string RightStickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}