using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
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

        [SerializeField] private GameObject character;
        private DistanceToGroundRaycastData d;

        #endregion

        #region private methods
        
        private void InitializeData()
        {
            d = new DistanceToGroundRaycastData {Character = character};
            d.RuntimeData = DistanceToGroundRaycastRuntimeData.CreateInstance(d.DistanceToGroundRaycastHit);
        }

        private void InitializeModel()
        {
            d.PhysicsRuntimeData = d.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            d.RaycastRuntimeData = d.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            //d.StickyRaycastHitColliderRuntimeData = d.Character.GetComponentNoAllocation<RaycastHitColliderController>()
            //    .StickyRaycastHitColliderRuntimeData;
            //d.LayerMaskRuntimeData = d.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            d.Transform = d.PhysicsRuntimeData.Transform;
            d.DrawRaycastGizmosControl = d.RaycastRuntimeData.DrawRaycastGizmosControl;
            d.DistanceToGroundRayMaximumLength = d.RaycastRuntimeData.DistanceToGroundRayMaximumLength;
            d.BoundsCenter = d.RaycastRuntimeData.BoundsCenter;
            d.BoundsBottomLeftCorner = d.RaycastRuntimeData.BoundsBottomLeftCorner;
            d.BoundsBottomRightCorner = d.RaycastRuntimeData.BoundsBottomRightCorner;
            //d.BelowSlopeAngle = d.StickyRaycastHitColliderRuntimeData.stickyRaycastHitCollider.BelowSlopeAngle;
            //d.RaysBelowLayerMaskPlatforms = d.LayerMaskRuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
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

        public DistanceToGroundRaycastRuntimeData RuntimeData => d.RuntimeData;

        #region public methods
        
        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public void OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
        }

        /*public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        #endregion

        #endregion
    }
}