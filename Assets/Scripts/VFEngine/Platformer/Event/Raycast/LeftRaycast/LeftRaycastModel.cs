using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastModel", menuName = PlatformerLeftRaycastModelPath, order = 0)]
    [InlineEditor]
    public class LeftRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Left Raycast Data")] [SerializeField]
        private LeftRaycastData l = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            l.RuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().LeftRaycastRuntimeData;
            l.RuntimeData.SetLeftRaycast(l.LeftRayLength, l.LeftRaycastFromBottomOrigin, l.LeftRaycastToTopOrigin,
                l.CurrentLeftRaycastHit);
        }

        private void InitializeModel()
        {
            l.PlatformerRuntimeData = l.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            l.RaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            l.LeftRaycastHitColliderRuntimeData = l.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .LeftRaycastHitColliderRuntimeData;
            l.PhysicsRuntimeData = l.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            l.LayerMaskRuntimeData = l.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            l.Transform = l.PlatformerRuntimeData.platformer.Transform;
            l.DrawRaycastGizmosControl = l.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            l.NumberOfHorizontalRaysPerSide = l.RaycastRuntimeData.raycast.NumberOfHorizontalRaysPerSide;
            l.RayOffset = l.RaycastRuntimeData.raycast.RayOffset;
            l.ObstacleHeightTolerance = l.RaycastRuntimeData.raycast.ObstacleHeightTolerance;
            l.BoundsWidth = l.RaycastRuntimeData.raycast.BoundsWidth;
            l.BoundsBottomLeftCorner = l.RaycastRuntimeData.raycast.BoundsBottomLeftCorner;
            l.BoundsBottomRightCorner = l.RaycastRuntimeData.raycast.BoundsBottomRightCorner;
            l.BoundsTopLeftCorner = l.RaycastRuntimeData.raycast.BoundsTopLeftCorner;
            l.BoundsTopRightCorner = l.RaycastRuntimeData.raycast.BoundsTopRightCorner;
            l.CurrentLeftHitsStorageIndex =
                l.LeftRaycastHitColliderRuntimeData.leftRaycastHitCollider.CurrentLeftHitsStorageIndex;
            l.Speed = l.PhysicsRuntimeData.Speed;
            l.PlatformMask = l.LayerMaskRuntimeData.layerMask.PlatformMask;
            l.OneWayPlatformMask = l.LayerMaskRuntimeData.layerMask.OneWayPlatformMask;
            l.MovingOneWayPlatformMask = l.LayerMaskRuntimeData.layerMask.MovingOneWayPlatformMask;
        }

        private void SetLeftRaycastFromBottomOrigin()
        {
            l.LeftRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(l.BoundsBottomRightCorner,
                l.BoundsBottomLeftCorner, l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetLeftRaycastToTopOrigin()
        {
            l.LeftRaycastToTopOrigin = OnSetRaycastToTopOrigin(l.BoundsTopLeftCorner, l.BoundsTopRightCorner,
                l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetCurrentLeftRaycastOrigin()
        {
            l.CurrentLeftRaycastOrigin = OnSetCurrentRaycastOrigin(l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, l.CurrentLeftHitsStorageIndex, l.NumberOfHorizontalRaysPerSide);
        }

        private void InitializeLeftRaycastLength()
        {
            l.LeftRayLength = OnSetHorizontalRayLength(l.Speed.x, l.BoundsWidth, l.RayOffset);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength,
                l.PlatformMask, red, l.DrawRaycastGizmosControl);
        }

        private void SetCurrentLeftRaycast()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength,
                l.PlatformMask & ~l.OneWayPlatformMask & ~l.MovingOneWayPlatformMask, red, l.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastRuntimeData RuntimeData => l.RuntimeData;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        
        public void OnSetLeftRaycastFromBottomOrigin()
        {
            SetLeftRaycastFromBottomOrigin();
        }

        public void OnSetLeftRaycastToTopOrigin()
        {
            SetLeftRaycastToTopOrigin();
        }

        public void OnInitializeLeftRaycastLength()
        {
            InitializeLeftRaycastLength();
        }

        public void OnSetCurrentLeftRaycastOrigin()
        {
            SetCurrentLeftRaycastOrigin();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
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