﻿using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static MathsExtensions;
    using static LayerMask;
    using static Vector3;
    using static Single;
    using static UniTaskExtensions;

    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastController downRaycastController;
        private LayerMaskController layerMaskController;
        private DownRaycastHitColliderData d;
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
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            downRaycastController = GetComponent<DownRaycastController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void InitializeData()
        {
            d = new DownRaycastHitColliderData();

            /*
            d.StandingOnLastFrame = d.StandingOn;
            d.HasStandingOnLastFrame = d.StandingOnLastFrame != null;
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
            */
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            downRaycast = downRaycastController.Data;
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            InitializeDownHitsStorage();
            //InitializeDownHitsStorageSmallestDistanceIndex();
            //InitializeDownHitsStorageIndex();
            //StopMovingPlatformCurrentGravity();
            //InitializeFriction();
            //d.DownHitsStorageLength = d.DownHitsStorage.Length;
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

        public async UniTaskVoid OnSetOnMovingPlatform()
        {
            SetOnMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetMovingPlatformCurrentGravity()
        {
            SetMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetWasGroundedLastFrame()
        {
            SetWasGroundedLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetStandingOnLastFrame()
        {
            SetStandingOnLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCurrentDownHitsStorage()
        {
            SetCurrentDownHitsStorage();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeFriction()
        {
            InitializeFriction();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnInitializeDownHitsStorage()
        {
            InitializeDownHitsStorage();
        }

        public async UniTaskVoid OnInitializeDownHitsStorageSmallestDistanceIndex()
        {
            InitializeDownHitsStorageSmallestDistanceIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeDownHitConnected()
        {
            InitializeDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeDownHitsStorageIndex()
        {
            InitializeDownHitsStorageIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnAddDownHitsStorageIndex()
        {
            AddDownHitsStorageIndex();
        }

        public async UniTaskVoid OnSetRaycastDownHitAt()
        {
            SetRaycastDownHitAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetDownHitConnected()
        {
            SetDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetBelowSlopeAngleAt()
        {
            SetBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCrossBelowSlopeAngleAt()
        {
            SetCrossBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetSmallestDistanceIndexAt()
        {
            SetSmallestDistanceIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetNegativeBelowSlopeAngle()
        {
            SetNegativeBelowSlopeAngle();
        }

        public async UniTaskVoid OnSetDownHitWithSmallestDistance()
        {
            SetDownHitWithSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetIsCollidingBelow()
        {
            SetIsCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetIsNotCollidingBelow()
        {
            SetIsNotCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetFrictionToDownHitWithSmallestDistancesFriction()
        {
            SetFrictionToDownHitWithSmallestDistancesFriction();
        }

        public void OnSetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            SetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
        }

        public async UniTaskVoid OnSetMovingPlatformToNull()
        {
            SetMovingPlatformToNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnStopMovingPlatformCurrentGravity()
        {
            StopMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnStopMovingPlatformCurrentSpeed()
        {
            StopMovingPlatformCurrentSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCurrentDownHitSmallestDistance()
        {
            SetCurrentDownHitSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeSmallestDistanceToDownHit()
        {
            InitializeSmallestDistanceToDownHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetSmallestDistanceToDownHitDistance()
        {
            SetSmallestDistanceToDownHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        public async UniTaskVoid OnSetStandingOn()
        {
            SetStandingOn();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetStandingOnCollider()
        {
            SetStandingOnCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetNotOnMovingPlatform()
        {
            SetNotOnMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetMovingPlatformCurrentSpeed()
        {
            SetMovingPlatformCurrentSpeed();
        }

        #endregion

        #endregion
    }
}