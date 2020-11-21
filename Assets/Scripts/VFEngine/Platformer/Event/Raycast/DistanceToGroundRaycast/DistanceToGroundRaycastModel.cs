using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastModel", menuName = PlatformerDistanceToGroundRaycastModelPath,
        order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Data")] [SerializeField]
        private DistanceToGroundRaycastData d = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            d.RuntimeData = d.Character.GetComponentNoAllocation<RaycastController>()
                .DistanceToGroundRaycastRuntimeData;
            d.RuntimeData.SetDistanceToGroundRaycast(d.DistanceToGroundRaycastHit);
        }

        private void InitializeModel()
        {
            d.PlatformerRuntimeData = d.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            d.RaycastRuntimeData = d.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            d.StickyRaycastHitColliderRuntimeData = d.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .StickyRaycastHitColliderRuntimeData;
            d.LayerMaskRuntimeData = d.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            d.Transform = d.PlatformerRuntimeData.platformer.Transform;
            d.DrawRaycastGizmosControl = d.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            d.DistanceToGroundRayMaximumLength = d.RaycastRuntimeData.raycast.DistanceToGroundRayMaximumLength;
            d.BoundsCenter = d.RaycastRuntimeData.raycast.BoundsCenter;
            d.BoundsBottomLeftCorner = d.RaycastRuntimeData.raycast.BoundsBottomLeftCorner;
            d.BoundsBottomRightCorner = d.RaycastRuntimeData.raycast.BoundsBottomRightCorner;
            d.BelowSlopeAngle = d.StickyRaycastHitColliderRuntimeData.stickyRaycastHitCollider.BelowSlopeAngle;
            d.RaysBelowLayerMaskPlatforms = d.LayerMaskRuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
        }

        private void SetDistanceToGroundRaycastOrigin()
        {
            d.DistanceToGroundRaycastOrigin = new Vector2
            {
                x = d.BelowSlopeAngle < 0 ? d.BoundsBottomLeftCorner.x : d.BoundsBottomRightCorner.x,
                y = d.BoundsCenter.y
            };
        }

        private void SetDistanceToGroundRaycast()
        {
            d.DistanceToGroundRaycastHit = Raycast(d.DistanceToGroundRaycastOrigin, -d.Transform.up,
                d.DistanceToGroundRayMaximumLength, d.RaysBelowLayerMaskPlatforms, blue, d.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public void OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}