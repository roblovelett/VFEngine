using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private const string ModelAssetPath = "StickyRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public StickyRaycastSettings Settings { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public RightStickyRaycastRuntimeData RightStickyRaycastRuntimeData { get; set; }
        public LeftStickyRaycastRuntimeData LeftStickyRaycastRuntimeData { get; set; }
        public StickyRaycastHitColliderRuntimeData StickyRaycastHitColliderRuntimeData { get; set; }
        public LeftStickyRaycastHitColliderRuntimeData LeftStickyRaycastHitColliderRuntimeData { get; set; }
        public RightStickyRaycastHitColliderRuntimeData RightStickyRaycastHitColliderRuntimeData { get; set; }
        public bool HasSettings => Settings;
        public bool StickToSlopesControl { get; set; }
        public bool DisplayWarningsControl { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float BoundsWidth { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public float BoundsHeight { get; set; }
        public float RayOffset { get; set; }
        public float BelowSlopeAngleLeft { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }

        #endregion

        public StickyRaycastRuntimeData RuntimeData { get; set; }
        public bool IsCastingLeft { get; set; }
        public float StickToSlopesOffsetY { get; set; }
        public float StickyRaycastLength { get; set; }
        public float StickToSlopesOffsetYSetting => Settings.stickToSlopesOffsetY;
        public bool DisplayWarningsControlSetting => Settings.displayWarningsControl;
        public static string StickyRaycastPath { get; } = $"{RaycastPath}StickyRaycast/";

        public static readonly string StickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{StickyRaycastPath}{ModelAssetPath}";

        #endregion
    }
}