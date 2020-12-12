using System;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;
using Debug = System.Diagnostics.Debug;
// ReSharper disable PossibleNullReferenceException

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static LayerMask;
    using static Vector3;
    using static Single;
    using static PhysicsExtensions;
    using static MathsExtensions;
    using static RaycastDirection;
    using static Debug;

    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PlatformerController platformerController;
        private PhysicsController physicsController;
        private PhysicsMaterialController physicsMaterialController;
        private RaycastController raycastController;
        private DownRaycastController downRaycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private LayerMaskController layerMaskController;
        private DownRaycastHitColliderData d;
        private PlatformerData platformer;
        private PhysicsData physics;
        private PhysicsMaterialData physicsMaterial;
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
                PathMovementControllerClosestToHit = null,
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
            InitializeSmallestDistanceMet();
            InitializeSmallestDistanceRaycast();
            //InitializePhysicsMaterialControllerClosestToHit();
            InitializeFrictionTest();
            //InitializePathMovementControllerClosestToHit();
            InitializeMovingPlatformTest();
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
                SetNotCollidingBelow();
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
        }

        private void PlatformerAddToCurrentHitsStorageIndex()
        {
            if (!CastingDown) return;
            AddToHitsStorageIndex();
        }

        private bool HitConnected => d.HitConnected = true;
        private bool NotGroundedLastFrame => !d.WasGroundedLastFrame;
        private bool SmallestDistanceLessThanHalfBoundsHeight => d.SmallestDistance < raycast.BoundsHeight / 2;
        private bool OneWayPlatformHasStandingOn => layerMask.OneWayPlatform.Contains(d.StandingOn.layer);
        private bool MovingOneWayPlatformHasStandingOn => layerMask.MovingOneWayPlatform.Contains(d.StandingOn.layer);
        private bool AerialAndNotHighEnoughForOneWayPlatform =>
            NotGroundedLastFrame && SmallestDistanceLessThanHalfBoundsHeight && OneWayPlatformHasStandingOn ||
            MovingOneWayPlatformHasStandingOn;
        private bool ApplyingExternalForce => physics.ExternalForce.y > 0 && physics.Speed.y > 0;
        private bool FrictionTest => d.FrictionTest = true;
        private bool MovingPlatformTest => d.MovingPlatformTest = true;
        private void PlatformerOnHitConnected()
        {
            if (!CastingDown) return;
            if (HitConnected)
            {
                SetStandingOn();
                SetStandingOnCollider();
                if (AerialAndNotHighEnoughForOneWayPlatform)
                {
                    SetNotCollidingBelow();
                    return;
                }
                SetIsCollidingBelow();
                if (ApplyingExternalForce)
                {
                    SetNotCollidingBelow();
                }

                if (FrictionTest)
                {
                    d.Friction = d.PhysicsMaterialControllerClosestToHit.Data.Friction;
                }

                if (MovingPlatformTest && d.IsGrounded)
                {
                    d.MovingPlatform = d.PathMovementControllerClosestToHit;
                }
                else
                {
                    DetachFromMovingPlatform();
                }    
            }
            else
            {
                SetNotCollidingBelow();
                if (d.OnMovingPlatform) DetachFromMovingPlatform();
            }
        }

        private bool NoMovingPlatform => d.MovingPlatform == null;
        private void DetachFromMovingPlatform()
        {
            if (NoMovingPlatform) return;
            SetDetachedFromMovingPlatform();
            SetNotOnMovingPlatform();
            ResetMovingPlatform();
        }

        private void SetDetachedFromMovingPlatform()
        {
            d.DetachedFromMovingPlatformEvent = true;
        }

        private void ResetMovingPlatform()
        {
            d.MovingPlatform = null;
            d.MovingPlatformGravity = 0;
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
            d.HitsStorage[d.HitsStorageIndex] = downRaycast.CurrentRaycast;
        }

        private void InitializeCurrentRaycast()
        {
            d.CurrentRaycast = d.HitsStorage[d.HitsStorageIndex];
        }

        private void InitializeSmallestDistanceRaycast()
        {
            d.SmallestDistanceRaycast = d.HitsStorage[d.SmallestDistanceIndex];
        }
        
        private void InitializeDistanceBetweenSmallestPointAndVerticalRaycast()
        {
            d.DistanceBetweenSmallestPointAndVerticalRaycast = DistanceBetweenPointAndLine(
                d.SmallestDistanceRaycast.point, downRaycast.RaycastFromLeftOrigin,
                downRaycast.RaycastToRightOrigin);
        }

        private void InitializeSmallestDistanceMet()
        {
            d.SmallestDistanceMet = d.DistanceBetweenSmallestPointAndVerticalRaycast < d.SmallValue;
        }

        private void InitializePhysicsMaterialControllerClosestToHit()
        {
            d.PhysicsMaterialControllerClosestToHit = d.SmallestDistanceRaycast.collider.gameObject.GetComponent<PhysicsMaterialController>();
        }

        private void InitializeFrictionTest()
        {
            d.FrictionTest = d.PhysicsMaterialControllerClosestToHit != null;
        }

        private void InitializePathMovementControllerClosestToHit()
        {
            d.PathMovementControllerClosestToHit = d.SmallestDistanceRaycast.collider.gameObject.GetComponent<PathMovementController>();
        }
        
        private void InitializeMovingPlatformTest()
        {
            d.MovingPlatformTest = d.PathMovementControllerClosestToHit != null;
        }

        private void ResetState()
        {
            SetNoGroundedEvent();
            SetNoDetachedFromMovingPlatformEvent();
        }

        private void SetNoDetachedFromMovingPlatformEvent()
        {
            d.DetachedFromMovingPlatformEvent = false;
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

        private void SetNotCollidingBelow()
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
            d.SmallestDistanceIndex = d.HitsStorageIndex;
        }

        private void SetSmallestDistanceToCurrentDistance()
        {
            d.SmallestDistance = d.CurrentRaycast.distance;
        }
        
        private void AddToHitsStorageIndex()
        {
            d.HitsStorageIndex++;
        }
        
        private void SetStandingOn()
        {
            d.StandingOn = d.HitsStorage[d.SmallestDistanceIndex].collider.gameObject;
        }
        
        private void SetStandingOnCollider()
        {
            d.StandingOnCollider = d.HitsStorage[d.SmallestDistanceIndex].collider;
        }
        
        /*==========================================================================================================*/

        private void SetIsCollidingBelow()
        {
            d.IsCollidingBelow = true;
        }
        private void SetGroundedEvent()
        {
            d.GroundedEvent = true;
        }
        private void SetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            d.StandingOnLastFrame.layer = layerMask.SavedBelowLayer;
        }
        
        private void SetNotOnMovingPlatform()
        {
            d.OnMovingPlatform = false;
        }

        private void SetMovingPlatformCurrentSpeed()
        {
            //d.MovingPlatformCurrentSpeed = d.MovingPlatform.CurrentSpeed;
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

        public void OnPlatformerCastCurrentRay()
        {
            PlatformerCastCurrentRay();
        }

        public void OnPlatformerAddToCurrentHitsStorageIndex()
        {
            PlatformerAddToCurrentHitsStorageIndex();
        }

        public void OnPlatformerHitConnected()
        {
            PlatformerOnHitConnected();
        }

        #endregion

        #endregion

        #endregion
    }
}