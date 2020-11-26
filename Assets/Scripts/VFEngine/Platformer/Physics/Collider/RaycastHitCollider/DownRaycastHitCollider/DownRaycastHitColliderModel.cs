using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static MathsExtensions;
    using static LayerMask;
    using static Vector3;
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;
    using static Single;

    [CreateAssetMenu(fileName = "DownRaycastHitColliderModel", menuName = PlatformerDownRaycastHitColliderModelPath,
        order = 0)]
    [InlineEditor]
    public class DownRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private DownRaycastHitColliderData d;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastData downRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!d) d = CreateInstance<DownRaycastHitColliderData>();
            d.StandingOnWithSmallestDistance = d.DownHitWithSmallestDistance.collider.gameObject;
            d.PhysicsMaterialClosestToDownHit = d.StandingOnWithSmallestDistance.gameObject
                .GetComponentNoAllocation<PhysicsMaterialData>();
            d.HasPhysicsMaterialClosestToDownHit = d.PhysicsMaterialClosestToDownHit != null;
            d.PathMovementClosestToDownHit =
                d.StandingOnWithSmallestDistance.gameObject.GetComponentNoAllocation<PathMovementData>();
            d.HasPathMovementClosestToDownHit = d.PathMovementClosestToDownHit != null;
            d.StandingOnWithSmallestDistanceLayer = d.StandingOnWithSmallestDistance.gameObject.layer;
            d.HasStandingOnLastFrame = d.StandingOnLastFrame != null;
            d.HasMovingPlatform = d.MovingPlatform != null;
            d.DownHitsStorageLength = d.DownHitsStorage.Length;
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            downRaycast = raycastController.DownRaycastModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
            InitializeFriction();
            InitializeDownHitsStorage();
            InitializeDownHitsStorageSmallestDistanceIndex();
            InitializeDownHitsStorageIndex();
            ResetState();
        }

        private void SetCurrentDownHitsStorage()
        {
            d.DownHitsStorage[d.CurrentDownHitsStorageIndex] = downRaycast.CurrentDownRaycastHit;
        }

        private void InitializeFriction()
        {
            d.Friction = 0;
        }

        private void InitializeDownHitsStorage()
        {
            d.DownHitsStorage = new RaycastHit2D[raycast.NumberOfVerticalRaysPerSide];
        }

        private void InitializeDownHitsStorageSmallestDistanceIndex()
        {
            d.DownHitsStorageSmallestDistanceIndex = 0;
        }

        private void InitializeDownHitConnected()
        {
            d.DownHitConnected = false;
        }

        private void InitializeDownHitsStorageIndex()
        {
            d.CurrentDownHitsStorageIndex = 0;
        }

        private void AddDownHitsStorageIndex()
        {
            d.CurrentDownHitsStorageIndex++;
        }

        private void SetRaycastDownHitAt()
        {
            d.RaycastDownHitAt = d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex];
        }

        private void SetDownHitConnected()
        {
            d.DownHitConnected = true;
        }

        private void SetBelowSlopeAngleAt()
        {
            d.BelowSlopeAngle = Vector2.Angle(d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].normal,
                physics.Transform.up);
        }

        private void SetCrossBelowSlopeAngleAt()
        {
            d.CrossBelowSlopeAngle = Cross(physics.Transform.up,
                d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].normal);
        }

        private void SetSmallestDistanceIndexAt()
        {
            d.DownHitsStorageSmallestDistanceIndex = d.CurrentDownHitsStorageIndex;
        }

        private void SetNegativeBelowSlopeAngle()
        {
            d.CrossBelowSlopeAngle = -d.CrossBelowSlopeAngle;
        }

        private void SetDownHitWithSmallestDistance()
        {
            d.DownHitWithSmallestDistance = d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex];
        }

        private void SetFrictionToDownHitWithSmallestDistancesFriction()
        {
            if (d.PhysicsMaterialClosestToDownHit is null) return;
            d.Friction = d.PhysicsMaterialClosestToDownHit.Friction;
        }

        private void SetIsCollidingBelow()
        {
            d.IsCollidingBelow = true;
        }

        private void SetIsNotCollidingBelow()
        {
            d.IsCollidingBelow = false;
        }

        private void SetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            if (d.PathMovementClosestToDownHit is null) return;
            d.MovingPlatform = d.PathMovementClosestToDownHit;
        }

        private void SetMovingPlatformToNull()
        {
            d.MovingPlatform = null;
        }

        private void StopMovingPlatformCurrentGravity()
        {
            d.MovingPlatformCurrentGravity = 0;
        }

        private void StopMovingPlatformCurrentSpeed()
        {
            d.MovingPlatformCurrentSpeed = Vector2.zero;
        }

        private void SetCurrentDownHitSmallestDistance()
        {
            d.CurrentDownHitSmallestDistance = DistanceBetweenPointAndLine(
                d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].point, downRaycast.DownRaycastFromLeft,
                downRaycast.DownRaycastToRight);
        }

        private void SetGroundedEvent()
        {
            d.GroundedEvent = true;
        }

        private void SetStandingOnLastFrameLayerToPlatform()
        {
            d.StandingOnLastFrame.layer = NameToLayer("Platform");
        }

        private void SetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            d.StandingOnLastFrame.layer = layerMask.SavedBelowLayer;
        }

        private void SetStandingOn()
        {
            d.StandingOn = d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].collider.gameObject;
        }

        private void SetStandingOnCollider()
        {
            d.StandingOnCollider = d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].collider;
        }

        private void ResetState()
        {
            d.IsCollidingBelow = false;
            d.GroundedEvent = false;
            d.BelowSlopeAngle = 0f;
            d.CrossBelowSlopeAngle = zero;
            d.StandingOn = null;
            d.OnMovingPlatform = false;
            d.StandingOnCollider = null;
            d.DownHitConnected = false;
            StopMovingPlatformCurrentGravity();
            StopMovingPlatformCurrentSpeed();
        }

        private void SetWasGroundedLastFrame()
        {
            d.HasGroundedLastFrame = d.IsCollidingBelow;
        }

        private void SetStandingOnLastFrame()
        {
            d.StandingOnLastFrame = d.StandingOn;
        }

        private void SetOnMovingPlatform()
        {
            d.OnMovingPlatform = true;
        }

        private void SetNotOnMovingPlatform()
        {
            d.OnMovingPlatform = false;
        }

        private void SetMovingPlatformCurrentGravity()
        {
            d.MovingPlatformCurrentGravity = d.MovingPlatformGravity;
        }

        private void SetMovingPlatformCurrentSpeed()
        {
            d.MovingPlatformCurrentSpeed = d.MovingPlatform.CurrentSpeed;
        }

        private void InitializeSmallestDistanceToDownHit()
        {
            d.SmallestDistanceToDownHit = MaxValue;
        }

        private void SetSmallestDistanceToDownHitDistance()
        {
            d.SmallestDistanceToDownHit = d.RaycastDownHitAt.distance;
        }

        #endregion

        #endregion

        #region properties

        public DownRaycastHitColliderData Data => d;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetOnMovingPlatform()
        {
            SetOnMovingPlatform();
        }

        public void OnSetMovingPlatformCurrentGravity()
        {
            SetMovingPlatformCurrentGravity();
        }

        public void OnSetWasGroundedLastFrame()
        {
            SetWasGroundedLastFrame();
        }

        public void OnSetStandingOnLastFrame()
        {
            SetStandingOnLastFrame();
        }

        public void OnSetCurrentDownHitsStorage()
        {
            SetCurrentDownHitsStorage();
        }

        public void OnInitializeFriction()
        {
            InitializeFriction();
        }

        public void OnInitializeDownHitsStorage()
        {
            InitializeDownHitsStorage();
        }

        public void OnInitializeDownHitsStorageSmallestDistanceIndex()
        {
            InitializeDownHitsStorageSmallestDistanceIndex();
        }

        public void OnInitializeDownHitConnected()
        {
            InitializeDownHitConnected();
        }

        public void OnInitializeDownHitsStorageIndex()
        {
            InitializeDownHitsStorageIndex();
        }

        public void OnAddDownHitsStorageIndex()
        {
            AddDownHitsStorageIndex();
        }

        public void OnSetRaycastDownHitAt()
        {
            SetRaycastDownHitAt();
        }

        public void OnSetDownHitConnected()
        {
            SetDownHitConnected();
        }

        public void OnSetBelowSlopeAngleAt()
        {
            SetBelowSlopeAngleAt();
        }

        public void OnSetCrossBelowSlopeAngleAt()
        {
            SetCrossBelowSlopeAngleAt();
        }

        public void OnSetSmallestDistanceIndexAt()
        {
            SetSmallestDistanceIndexAt();
        }

        public void OnSetNegativeBelowSlopeAngle()
        {
            SetNegativeBelowSlopeAngle();
        }

        public void OnSetDownHitWithSmallestDistance()
        {
            SetDownHitWithSmallestDistance();
        }

        public void OnSetIsCollidingBelow()
        {
            SetIsCollidingBelow();
        }

        public void OnSetIsNotCollidingBelow()
        {
            SetIsNotCollidingBelow();
        }

        public void OnSetFrictionToDownHitWithSmallestDistancesFriction()
        {
            SetFrictionToDownHitWithSmallestDistancesFriction();
        }

        public void OnSetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            SetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
        }

        public void OnSetMovingPlatformToNull()
        {
            SetMovingPlatformToNull();
        }

        public void OnStopMovingPlatformCurrentGravity()
        {
            StopMovingPlatformCurrentGravity();
        }

        public void OnStopMovingPlatformCurrentSpeed()
        {
            StopMovingPlatformCurrentSpeed();
        }

        public void OnSetCurrentDownHitSmallestDistance()
        {
            SetCurrentDownHitSmallestDistance();
        }

        public void OnInitializeSmallestDistanceToDownHit()
        {
            InitializeSmallestDistanceToDownHit();
        }

        public void OnSetSmallestDistanceToDownHitDistance()
        {
            SetSmallestDistanceToDownHitDistance();
        }

        public void OnSetGroundedEvent()
        {
            SetGroundedEvent();
        }

        public void OnSetStandingOnLastFrameLayerToPlatform()
        {
            SetStandingOnLastFrameLayerToPlatform();
        }

        public void OnSetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            SetStandingOnLastFrameLayerToSavedBelowLayer();
        }

        public void OnSetStandingOn()
        {
            SetStandingOn();
        }

        public void OnSetStandingOnCollider()
        {
            SetStandingOnCollider();
        }

        public void OnSetNotOnMovingPlatform()
        {
            SetNotOnMovingPlatform();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public void OnSetMovingPlatformCurrentSpeed()
        {
            SetMovingPlatformCurrentSpeed();
        }

        #endregion

        #endregion
    }
}