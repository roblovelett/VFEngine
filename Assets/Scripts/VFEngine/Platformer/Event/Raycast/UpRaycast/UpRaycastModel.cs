using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "UpRaycastModel", menuName = PlatformerUpRaycastModelPath, order = 0)]
    [InlineEditor]
    public class UpRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Up Raycast Data")] [SerializeField]
        private UpRaycastData u = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            u.RuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().UpRaycastRuntimeData;
            u.RuntimeData.SetUpRaycast(u.UpRaycastSmallestDistance, u.CurrentUpRaycastOrigin, u.CurrentUpRaycastHit);
        }

        private void InitializeModel()
        {
            u.PlatformerRuntimeData = u.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            u.RaycastRuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            u.UpRaycastHitColliderRuntimeData = u.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .UpRaycastHitColliderRuntimeData;
            u.DownRaycastHitColliderRuntimeData = u.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .DownRaycastHitColliderRuntimeData;
            u.PhysicsRuntimeData = u.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            u.LayerMaskRuntimeData = u.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            u.Transform = u.PlatformerRuntimeData.platformer.Transform;
            u.DrawRaycastGizmosControl = u.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            u.NumberOfVerticalRaysPerSide = u.RaycastRuntimeData.raycast.NumberOfVerticalRaysPerSide;
            u.RayOffset = u.RaycastRuntimeData.raycast.RayOffset;
            u.BoundsBottomLeftCorner = u.RaycastRuntimeData.raycast.BoundsBottomLeftCorner;
            u.BoundsBottomRightCorner = u.RaycastRuntimeData.raycast.BoundsBottomRightCorner;
            u.BoundsTopLeftCorner = u.RaycastRuntimeData.raycast.BoundsTopLeftCorner;
            u.BoundsTopRightCorner = u.RaycastRuntimeData.raycast.BoundsTopRightCorner;
            u.CurrentUpHitsStorageIndex =
                u.UpRaycastHitColliderRuntimeData.upRaycastHitCollider.CurrentUpHitsStorageIndex;
            u.RaycastUpHitAt = u.UpRaycastHitColliderRuntimeData.upRaycastHitCollider.RaycastUpHitAt;
            u.GroundedEvent = u.DownRaycastHitColliderRuntimeData.downRaycastHitCollider.GroundedEvent;
            u.NewPosition = u.PhysicsRuntimeData.NewPosition;
            u.PlatformMask = u.LayerMaskRuntimeData.layerMask.PlatformMask;
            u.OneWayPlatformMask = u.LayerMaskRuntimeData.layerMask.OneWayPlatformMask;
            u.MovingOneWayPlatformMask = u.LayerMaskRuntimeData.layerMask.MovingOneWayPlatformMask;
        }

        private void InitializeUpRaycastLength()
        {
            u.UpRayLength = u.GroundedEvent ? u.RayOffset : u.NewPosition.y;
        }

        private void InitializeUpRaycastStart()
        {
            u.UpRaycastStart = SetPoint(u.BoundsBottomLeftCorner, u.BoundsTopLeftCorner, u.Transform, u.NewPosition.x);
        }

        private void InitializeUpRaycastEnd()
        {
            u.UpRaycastEnd = SetPoint(u.BoundsBottomRightCorner, u.BoundsTopRightCorner, u.Transform, u.NewPosition.y);
        }

        private static Vector2 SetPoint(Vector2 bounds1, Vector2 bounds2, Transform t, float axis)
        {
            return OnSetBounds(bounds1, bounds2) * 2 + (Vector2) t.right * axis;
        }

        private void InitializeUpRaycastSmallestDistance()
        {
            u.UpRaycastSmallestDistance = MaxValue;
        }

        private void SetCurrentUpRaycastOrigin()
        {
            u.CurrentUpRaycastOrigin = OnSetCurrentRaycastOrigin(u.UpRaycastStart, u.UpRaycastEnd,
                u.CurrentUpHitsStorageIndex, u.NumberOfVerticalRaysPerSide);
        }

        private void SetCurrentUpRaycast()
        {
            u.CurrentUpRaycastHit = Raycast(u.CurrentUpRaycastOrigin, u.Transform.up, u.UpRayLength,
                u.PlatformMask & ~ u.OneWayPlatformMask & ~ u.MovingOneWayPlatformMask, cyan,
                u.DrawRaycastGizmosControl);
        }

        private void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            u.UpRaycastSmallestDistance = u.RaycastUpHitAt.distance;
        }

        #endregion

        #endregion

        #region properties

        public UpRaycastRuntimeData RuntimeData => u.RuntimeData;
        
        #region public methods
        
        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnInitializeUpRaycastLength()
        {
            InitializeUpRaycastLength();
        }

        public void OnInitializeUpRaycastStart()
        {
            InitializeUpRaycastStart();
        }

        public void OnInitializeUpRaycastEnd()
        {
            InitializeUpRaycastEnd();
        }

        public void OnInitializeUpRaycastSmallestDistance()
        {
            InitializeUpRaycastSmallestDistance();
        }

        public void OnSetCurrentUpRaycastOrigin()
        {
            SetCurrentUpRaycastOrigin();
        }

        public void OnSetCurrentUpRaycast()
        {
            SetCurrentUpRaycast();
        }

        public void OnSetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            SetUpRaycastSmallestDistanceToRaycastUpHitAt();
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