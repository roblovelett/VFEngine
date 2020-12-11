using System;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static LayerMask;
    using static Vector3;
    using static Single;
    using static PhysicsExtensions;
    using static MathsExtensions;
    using static RaycastDirection;

    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PlatformerController platformerController;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastController downRaycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private LayerMaskController layerMaskController;
        private DownRaycastHitColliderData d;
        private PlatformerData platformer;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastData downRaycast;
        private RaycastHitColliderData raycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region internal

        private bool TestMovingPlatform => d.HasMovingPlatform && platformer.TestPlatform;
        private bool IsNotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool CastingDown => raycast.CurrentRaycastDirection == Down;
        private bool IncorrectStorageLength => d.HitsStorage.Length != raycast.NumberOfVerticalRays;

        private bool ColliderAndMidHeightOneWayPlatformHasStandingOnLastFrame => d.HasStandingOnLastFrame &&
            layerMask.MidHeightOneWayPlatformHasStandingOnLastFrame;

        private bool HasCurrentRaycast => d.CurrentRaycast;
        private bool HitIgnoredCollider => d.CurrentRaycast.collider == raycastHitCollider.IgnoredCollider;
        private bool NegativeCrossBelowSlopeAngle => d.CrossBelowSlopeAngle.z < 0;
        private bool RaycastLessThanSmallestDistance => d.CurrentRaycast.distance < d.SmallestDistance;
        private bool SmallestDistanceMet => d.DistanceBetweenSmallestPointAndVerticalRaycast < d.SmallValue;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            platformerController = GetComponent<PlatformerController>();
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            downRaycastController = GetComponent<DownRaycastController>();
            raycastHitColliderController = GetComponent<RaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void InitializeData()
        {
            d = new DownRaycastHitColliderData
            {
                PhysicsMaterialClosestToHit = null,
                PathMovementClosestToHit = null,
                MovingPlatform = null,
                MovingPlatformGravity = -500f,
                SmallValue = 0.0001f
            };
            /*
            d.StandingOnLastFrame = d.StandingOn;
            d.StandingOnWithSmallestDistance = d.DownHitWithSmallestDistance.collider.gameObject;
            d.PhysicsMaterialClosestToDownHit = d.StandingOnWithSmallestDistance.gameObject
                .GetComponentNoAllocation<PhysicsMaterialData>();
            d.HasPhysicsMaterialClosestToDownHit = d.PhysicsMaterialClosestToDownHit != null;
            d.PathMovementClosestToDownHit =
                d.StandingOnWithSmallestDistance.gameObject.GetComponentNoAllocation<PathMovementData>();
            d.HasPathMovementClosestToDownHit = d.PathMovementClosestToDownHit != null;
            d.StandingOnWithSmallestDistanceLayer = d.StandingOnWithSmallestDistance.gameObject.layer;
            d.HasMovingPlatform = d.MovingPlatform != null;
            */
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            platformer = platformerController.Data;
            physics = physicsController.Data;
            raycast = raycastController.Data;
            downRaycast = downRaycastController.Data;
            raycastHitCollider = raycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            InitializeDownHitsStorage();
            InitializeHasMovingPlatform();
            InitializeMovingPlatformHasSpeed();
            InitializeMovingPlatformHasSpeedOnAxis();
            InitializeHasStandingOnLastFrame();
            InitializeCurrentHitsStorage();
            InitializeCurrentRaycast();
            InitializeDistanceBetweenSmallestPointAndVerticalRaycast();
            ResetState();
        }

        #endregion

        #region platformer

        private void PlatformerInitializeFrame()
        {
            SetHasGroundedLastFrameToCollidingBelow();
            SetStandingOnLastFrameToStandingOn();
            ResetState();
        }

        private void PlatformerTestMovingPlatform()
        {
            if (!TestMovingPlatform) return;
            SetOnMovingPlatform();
            SetMovingPlatformCurrentGravity();
        }

        private void PlatformerCastRaysDown()
        {
            if (!CastingDown) return;
            InitializeFriction();
            if (IsNotCollidingBelow)
            {
                SetIsNotCollidingBelow();
                return;
            }

            if (IncorrectStorageLength) InitializeDownHitsStorage();
            if (ColliderAndMidHeightOneWayPlatformHasStandingOnLastFrame) SetStandingOnLastFrameLayerToPlatform();
            InitializeSmallestDistance();
            InitializeSmallestDistanceIndex();
            InitializeHitConnected();
        }

        private void PlatformerCastCurrentRay()
        {
            if (!CastingDown || !HasCurrentRaycast || HitIgnoredCollider) return;
            SetHitConnected();
            SetBelowSlopeAngle();
            SetCrossBelowSlopeAngle();
            if (NegativeCrossBelowSlopeAngle) SetNegativeBelowSlopeAngle();
            if (!RaycastLessThanSmallestDistance) return;
            SetSmallestDistanceIndexToCurrentHitsStorageIndex();
            SetSmallestDistanceToCurrentDistance();
            if (SmallestDistanceMet) return;

            // on hit connected
        }

        #endregion

        private void InitializeDownHitsStorage()
        {
            d.HitsStorage = new RaycastHit2D[raycast.NumberOfVerticalRaysPerSide];
        }

        private void InitializeHasMovingPlatform()
        {
            d.HasMovingPlatform = d.MovingPlatform != null;
        }

        private void InitializeMovingPlatformHasSpeed()
        {
            d.MovingPlatformHasSpeed = !SpeedNan(d.MovingPlatformCurrentSpeed);
        }

        private void InitializeMovingPlatformHasSpeedOnAxis()
        {
            d.MovingPlatformHasSpeedOnAxis = !AxisSpeedNan(d.MovingPlatformCurrentSpeed);
        }

        private void InitializeHasStandingOnLastFrame()
        {
            d.HasStandingOnLastFrame = d.StandingOnLastFrame != null;
        }

        private void InitializeCurrentHitsStorage()
        {
            d.HitsStorage[d.CurrentHitsStorageIndex] = downRaycast.CurrentRaycast;
        }

        private void InitializeCurrentRaycast()
        {
            d.CurrentRaycast = d.HitsStorage[d.CurrentHitsStorageIndex];
        }

        private void InitializeDistanceBetweenSmallestPointAndVerticalRaycast()
        {
            d.DistanceBetweenSmallestPointAndVerticalRaycast = DistanceBetweenPointAndLine(
                d.HitsStorage[d.SmallestDistanceIndex].point, downRaycast.RaycastFromLeftOrigin,
                downRaycast.RaycastToRightOrigin);
        }

        private void ResetState()
        {
            SetNoGroundedEvent();
        }

        private void SetNoGroundedEvent()
        {
            d.GroundedEvent = false;
        }

        private void SetHasGroundedLastFrameToCollidingBelow()
        {
            d.WasGroundedLastFrame = d.IsCollidingBelow;
        }

        private void SetStandingOnLastFrameToStandingOn()
        {
            d.StandingOnLastFrame = d.StandingOn;
        }

        private void SetOnMovingPlatform()
        {
            d.OnMovingPlatform = true;
        }

        private void SetMovingPlatformCurrentGravity()
        {
            d.MovingPlatformCurrentGravity = d.MovingPlatformGravity;
        }

        private void InitializeFriction()
        {
            d.Friction = 0;
        }

        private void SetIsNotCollidingBelow()
        {
            d.IsCollidingBelow = false;
        }

        private void SetStandingOnLastFrameLayerToPlatform()
        {
            d.StandingOnLastFrame.layer = NameToLayer("Platform");
        }

        private void InitializeSmallestDistance()
        {
            d.SmallestDistance = MaxValue;
        }

        private void InitializeSmallestDistanceIndex()
        {
            d.SmallestDistanceIndex = 0;
        }

        private void InitializeHitConnected()
        {
            d.HitConnected = false;
        }

        private void SetHitConnected()
        {
            d.HitConnected = true;
        }

        private void SetBelowSlopeAngle()
        {
            d.BelowSlopeAngle = Vector2.Angle(d.CurrentRaycast.normal, physics.Transform.up);
        }

        private void SetCrossBelowSlopeAngle()
        {
            d.CrossBelowSlopeAngle = Cross(physics.Transform.up, d.CurrentRaycast.normal);
        }

        private void SetNegativeBelowSlopeAngle()
        {
            d.BelowSlopeAngle = -d.BelowSlopeAngle;
        }

        private void SetSmallestDistanceIndexToCurrentHitsStorageIndex()
        {
            d.SmallestDistanceIndex = d.CurrentHitsStorageIndex;
        }

        private void SetSmallestDistanceToCurrentDistance()
        {
            d.SmallestDistance = d.CurrentRaycast.distance;
        }

        /*==========================================================================================================*/

        private void AddDownHitsStorageIndex()
        {
            d.CurrentHitsStorageIndex++;
        }

        //private void SetRaycastDownHitAt()
        //{
        //    d.RaycastHitAt = d.HitsStorage[d.SmallestDistanceToHitStorageIndex];
        //}

        private void SetSmallestDistanceIndexAt()
        {
            d.SmallestDistanceIndex = d.CurrentHitsStorageIndex;
        }

        private void SetDownHitWithSmallestDistance()
        {
            //d.DownHitWithSmallestDistance = d.HitsStorage[d.SmallestDistanceToHitStorageIndex];
        }

        private void SetFrictionToDownHitWithSmallestDistancesFriction()
        {
            if (d.PhysicsMaterialClosestToHit is null) return;
            //d.Friction = d.PhysicsMaterialClosestToDownHit.Friction;
        }

        private void SetIsCollidingBelow()
        {
            d.IsCollidingBelow = true;
        }

        private void SetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            if (d.PathMovementClosestToHit is null) return;
            d.MovingPlatform = d.PathMovementClosestToHit;
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
            /*d.CurrentDownHitSmallestDistance = DistanceBetweenPointAndLine(
                d.DownHitsStorage[d.DownHitsStorageSmallestDistanceIndex].point, downRaycast.DownRaycastFromLeft,
                downRaycast.RaycastToRightOrigin);*/
        }

        private void SetGroundedEvent()
        {
            d.GroundedEvent = true;
        }

        private void SetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            d.StandingOnLastFrame.layer = layerMask.SavedBelowLayer;
        }

        private void SetStandingOn()
        {
            d.StandingOn = d.HitsStorage[d.SmallestDistanceIndex].collider.gameObject;
        }

        private void SetStandingOnCollider()
        {
            d.StandingOnCollider = d.HitsStorage[d.SmallestDistanceIndex].collider;
        }

        private void SetNotOnMovingPlatform()
        {
            d.OnMovingPlatform = false;
        }

        private void SetMovingPlatformCurrentSpeed()
        {
            //d.MovingPlatformCurrentSpeed = d.MovingPlatform.CurrentSpeed;
        }

        /*private void SetSmallestDistanceToDownHitDistance()
        {
            d.SmallestDistance = d.RaycastHitAt.distance;
        }*/

        #endregion

        #endregion

        #region properties

        public DownRaycastHitColliderData Data => d;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnPlatformerTestMovingPlatform()
        {
            PlatformerTestMovingPlatform();
        }

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        public void OnPlatformerCastCurrentRay()
        {
            PlatformerCastCurrentRay();
        }

        #endregion

        #endregion

        #endregion
    }
}