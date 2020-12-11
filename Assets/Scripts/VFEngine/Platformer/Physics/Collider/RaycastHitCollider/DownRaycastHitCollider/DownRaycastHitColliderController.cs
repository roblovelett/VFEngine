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

    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PlatformerController platformerController;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastController downRaycastController;
        private LayerMaskController layerMaskController;
        private DownRaycastHitColliderData d;
        private PlatformerData platformer;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastData downRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

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
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void InitializeData()
        {
            d = new DownRaycastHitColliderData
            {
                PhysicsMaterialClosestToHit = null, PathMovementClosestToHit = null, MovingPlatform = null
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
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            InitializeDownHitsStorage();
            InitializeHasMovingPlatform();
            InitializeMovingPlatformHasSpeed();
            InitializeMovingPlatformHasSpeedOnAxis();
            InitializeHasStandingOnLastFrame();
            ResetState();
        }

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

        private void ResetState()
        {
            d.GroundedEvent = false;
        }

        private void PlatformerInitializeFrame()
        {
            SetHasGroundedLastFrameToCollidingBelow();
            SetStandingOnLastFrameToStandingOn();
            ResetState();
        }

        private void SetHasGroundedLastFrameToCollidingBelow()
        {
            d.WasGroundedLastFrame = d.IsCollidingBelow;
        }

        private void SetStandingOnLastFrameToStandingOn()
        {
            d.StandingOnLastFrame = d.StandingOn;
        }

        private bool TestMovingPlatform => d.HasMovingPlatform && platformer.TestPlatform;

        private void PlatformerTestMovingPlatform()
        {
            if (!TestMovingPlatform) return;
            SetOnMovingPlatform();
            SetMovingPlatformCurrentGravity();
        }

        private void SetOnMovingPlatform()
        {
            d.OnMovingPlatform = true;
        }

        private void SetMovingPlatformCurrentGravity()
        {
            d.MovingPlatformCurrentGravity = d.MovingPlatformGravity;
        }

        private bool IsNotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool IncorrectStorageLength => d.HitsStorage.Length != raycast.NumberOfVerticalRays;

        private bool ColliderAndMidHeightOneWayPlatformHasStandingOnLastFrame => d.HasStandingOnLastFrame &&
            layerMask.MidHeightOneWayPlatformHasStandingOnLastFrame;

        private void PlatformerCastRaysDown()
        {
            InitializeFriction();
            if (IsNotCollidingBelow)
            {
                SetIsNotCollidingBelow();
                return;
            }

            if (IncorrectStorageLength) InitializeDownHitsStorage();
            if (ColliderAndMidHeightOneWayPlatformHasStandingOnLastFrame) SetStandingOnLastFrameLayerToPlatform();
            InitializeSmallestDistanceToHit();
            InitializeSmallestDistanceToHitStorageIndex();
            InitializeHitConnected();
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

        private void InitializeSmallestDistanceToHit()
        {
            d.SmallestDistanceToHit = MaxValue;
        }

        private void InitializeSmallestDistanceToHitStorageIndex()
        {
            d.SmallestDistanceToHitStorageIndex = 0;
        }

        private void InitializeHitConnected()
        {
            d.HitConnected = false;
        }

        /*==========================================================================================================*/

        private void SetCurrentDownHitsStorage()
        {
            d.HitsStorage[d.CurrentHitsStorageIndex] = downRaycast.CurrentRaycastHit;
        }

        private void InitializeDownHitsStorageIndex()
        {
            d.CurrentHitsStorageIndex = 0;
        }

        private void AddDownHitsStorageIndex()
        {
            d.CurrentHitsStorageIndex++;
        }

        private void SetRaycastDownHitAt()
        {
            d.RaycastHitAt = d.HitsStorage[d.SmallestDistanceToHitStorageIndex];
        }

        private void SetDownHitConnected()
        {
            d.HitConnected = true;
        }

        private void SetBelowSlopeAngleAt()
        {
            d.BelowSlopeAngle = Vector2.Angle(d.HitsStorage[d.SmallestDistanceToHitStorageIndex].normal,
                physics.Transform.up);
        }

        private void SetCrossBelowSlopeAngleAt()
        {
            d.CrossBelowSlopeAngle = Cross(physics.Transform.up,
                d.HitsStorage[d.SmallestDistanceToHitStorageIndex].normal);
        }

        private void SetSmallestDistanceIndexAt()
        {
            d.SmallestDistanceToHitStorageIndex = d.CurrentHitsStorageIndex;
        }

        private void SetNegativeBelowSlopeAngle()
        {
            d.CrossBelowSlopeAngle = -d.CrossBelowSlopeAngle;
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
            d.StandingOn = d.HitsStorage[d.SmallestDistanceToHitStorageIndex].collider.gameObject;
        }

        private void SetStandingOnCollider()
        {
            d.StandingOnCollider = d.HitsStorage[d.SmallestDistanceToHitStorageIndex].collider;
        }

        private void SetNotOnMovingPlatform()
        {
            d.OnMovingPlatform = false;
        }

        private void SetMovingPlatformCurrentSpeed()
        {
            //d.MovingPlatformCurrentSpeed = d.MovingPlatform.CurrentSpeed;
        }

        private void SetSmallestDistanceToDownHitDistance()
        {
            d.SmallestDistanceToHit = d.RaycastHitAt.distance;
        }

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

        #endregion

        #endregion

        #endregion
    }
}