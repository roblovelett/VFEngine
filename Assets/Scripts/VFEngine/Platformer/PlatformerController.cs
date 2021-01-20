using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;

namespace VFEngine.Platformer
{
    using static UniTask;
    using static Debug;
    using static Mathf;
    using static ScriptableObject;

    public class PlatformerController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public PlatformerData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private RaycastController raycastController;
        [OdinSerialize] private LayerMaskController layerMaskController;
        [OdinSerialize] private PhysicsController physicsController;
        private RaycastData raycastData;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!raycastController) raycastController = GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (!physicsController) physicsController = GetComponent<PhysicsController>();
            if (!Data) Data = CreateInstance<PlatformerData>();
        }

        private void SetDependencies()
        {
            raycastData = raycastController.Data;
            physicsData = physicsController.Data;
            layerMaskData = layerMaskController.Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private async void Start()
        {
            SetDependencies();
            await Run();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask Run()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            try
            {
                await RunPlatformer(ct);
            }
            catch (OperationCanceledException e)
            {
                Log(e.Message);
            }
        }

        private async UniTask RunPlatformer(CancellationToken ct)
        {
            var run = true;
            while (run)
            {
                if (ct.IsCancellationRequested) run = false;
                await InitializeFrame();
                await GroundCollision();
                await UpdateForces();
                //await UpdateForces();
                //await HorizontalDeltaMoveDetection();
                //await StopHorizontalSpeedControl();
                //await VerticalCollision();
                //await SlopeChangeCollisionControl();
                await Move();
                await OnFrameExit();
                Log($"Frame={Time.frameCount} FixedTime={Time.fixedTime}");
                await WaitForFixedUpdate(ct);
            }
        }

        private async UniTask InitializeFrame()
        {
            await ResetCollision();
            await InitializeDeltaMove();
            await SetSavedLayer();
            await UpdateOrigins();
        }

        private async UniTask ResetCollision()
        {
            await raycastController.OnPlatformerResetCollision();
        }

        private async UniTask InitializeDeltaMove()
        {
            await physicsController.OnPlatformerInitializeDeltaMove();
        }

        private async UniTask SetSavedLayer()
        {
            await layerMaskController.OnPlatformerSetSavedLayer();
        }

        private async UniTask UpdateOrigins()
        {
            await raycastController.OnPlatformerUpdateOrigins();
        }

        private int VerticalRays => raycastData.VerticalRays;
        private float IgnorePlatformsTime => raycastData.IgnorePlatformsTime;
        private bool CastGroundCollisionForOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private bool CastNextGroundCollisionRay => (Hit.distance <= 0);
        private async UniTask GroundCollision()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                Data.OnGroundCollision(i);
                await SetGroundCollisionRaycast();
                if (CastGroundCollisionForOneWayPlatform)
                {
                    await SetGroundCollisionRaycastForOneWayPlatform();
                    if (CastNextGroundCollisionRay) continue;
                }

                if (!Hit) continue;
                await SetCollisionOnGroundCollisionRaycastHit();
                await CastGroundCollisionRay();
                break;
            }

            await Yield();
        }

        private async UniTask SetGroundCollisionRaycast()
        {
            await raycastController.OnPlatformerSetGroundCollisionRaycast();
        }

        private async UniTask SetGroundCollisionRaycastForOneWayPlatform()
        {
            await raycastController.OnPlatformerSetGroundCollisionRaycastForOneWayPlatform();
        }

        private async UniTask SetCollisionOnGroundCollisionRaycastHit()
        {
            await raycastController.OnPlatformerSetCollisionOnGroundCollisionRaycastHit();
        }

        private async UniTask CastGroundCollisionRay()
        {
            await raycastController.OnPlatformerCastGroundCollisionRay();
        }

        private async UniTask UpdateForces()
        {
            await UpdateExternalForce();
            await UpdateGravity();
            await UpdateExternalForceX();
        }

        private bool IgnoreFriction => raycastData.IgnoreFriction;
        private Vector2 ExternalForce => physicsData.ExternalForce;
        private float MinimumMoveThreshold => physicsData.MinimumMoveThreshold;
        private bool StopExternalForce => ExternalForce.magnitude <= MinimumMoveThreshold;
        private async UniTask UpdateExternalForce()
        {
            if (!IgnoreFriction)
            {
                await UpdateExternalForceInternal();
                if (StopExternalForce) await StopExternalForceInternal();
            }
            await Yield();
        }

        private async UniTask UpdateExternalForceInternal()
        {
            await physicsController.OnPlatformerUpdateExternalForce();
        }

        private async UniTask StopExternalForceInternal()
        {
            await physicsController.OnPlatformerStopExternalForce();
        }

        private bool ApplyGravity => Speed.y > 0;
        private async UniTask UpdateGravity()
        {
            if (ApplyGravity) await ApplyGravityToSpeed();
            else await ApplyExternalForceToGravity();
            await Yield();
        }

        private async UniTask ApplyGravityToSpeed()
        {
            await physicsController.OnPlatformerApplyGravityToSpeed();
        }

        private async UniTask ApplyExternalForceToGravity()
        {
            await physicsController.OnPlatformerApplyExternalForceToGravity();
        }

        private float MaximumSlopeAngle => physicsData.MaximumSlopeAngle;
        private bool ApplyForcesToExternalForceX => OnSlope && GroundAngle > MaximumSlopeAngle &&
                                                    (GroundAngle < MinimumWallAngle || Speed.x == 0);
        private async UniTask UpdateExternalForceX()
        {
            if (ApplyForcesToExternalForceX) await UpdateExternalForceXInternal();
            await Yield();
        }

        private async UniTask UpdateExternalForceXInternal()
        {
            await physicsController.OnPlatformerUpdateExternalForceX();
        }
        
        private Vector2 DeltaMove => physicsData.DeltaMove;
        private bool HorizontalDeltaMove => DeltaMove.x != 0;

        private async UniTask HorizontalDeltaMoveDetection()
        {
            if (HorizontalDeltaMove)
            {
                await SlopeCollision();
                await HorizontalCollision();
            }

            await Yield();
        }

        private bool OnSlope => raycastData.OnSlope;
        private bool SlopeBehavior => DeltaMove.y <= 0 && OnSlope;
        private int GroundDirectionAxis => raycastData.GroundDirectionAxis;
        private int DeltaMoveXDirectionAxis => physicsData.DeltaMoveXDirectionAxis;
        private bool DescendingSlope => GroundDirectionAxis == DeltaMoveXDirectionAxis;
        private float GroundAngle => raycastData.GroundAngle;
        private float MinimumWallAngle => physicsData.MinimumWallAngle;
        private float DeltaMoveDistanceX => physicsData.DeltaMoveDistanceX;
        private float SlopeMoveDistance => Sin(GroundAngle * Deg2Rad) * DeltaMoveDistanceX;
        private bool ClimbingSlope => GroundAngle < MinimumWallAngle && DeltaMove.x <= SlopeMoveDistance;

        private async UniTask SlopeCollision()
        {
            if (SlopeBehavior)
            {
                if (DescendingSlope) await DescendSlope();
                else if (ClimbingSlope) await ClimbSlope();
            }

            await Yield();
        }

        private async UniTask DescendSlope()
        {
            var raycast = raycastController.OnPlatformerSlopeBehavior();
            var physics = physicsController.OnPlatformerDescendSlope();
            await (raycast, physics);
        }

        private async UniTask ClimbSlope()
        {
            var raycast = raycastController.OnPlatformerSlopeBehavior();
            var physics = physicsController.OnPlatformerClimbSlope();
            await (raycast, physics);
        }

        private async UniTask HorizontalCollision()
        {
            await raycastController.OnPlatformerHorizontalCollision();
        }

        private float Tolerance => Data.Tolerance;
        private Vector2 Speed => physicsData.Speed;

        private bool StoppingHorizontalSpeed => OnSlope && GroundAngle >= MinimumWallAngle &&
                                                Abs(GroundAngle - DeltaMoveXDirectionAxis) > Tolerance && Speed.y < 0;

        private async UniTask StopHorizontalSpeedControl()
        {
            if (StoppingHorizontalSpeed) await StopHorizontalSpeed();
            await Yield();
        }

        private async UniTask StopHorizontalSpeed()
        {
            var raycast = raycastController.OnPlatformerStopHorizontalSpeed();
            var physics = physicsController.OnPlatformerStopHorizontalSpeed();
            await (raycast, physics);
        }

        private async UniTask VerticalCollision()
        {
            await raycastController.OnPlatformerVerticalCollision();
        }

        private bool OnGround => raycastData.OnGround;
        private bool SlopeChanging => OnGround && DeltaMove.x != 0 && Speed.y <= 0;

        private async UniTask SlopeChangeCollisionControl()
        {
            if (SlopeChanging) await SlopeChangeCollision();
            await Yield();
        }

        private bool Climbing => DeltaMove.y > 0;

        private async UniTask SlopeChangeCollision()
        {
            if (Climbing) await ClimbSlopeChangeCollision();
            else await DescendSlopeChangeCollision();
            await Yield();
        }

        private RaycastHit2D Hit => raycastData.Hit;
        private float HitAngle => raycastData.HitAngle;
        private bool ClimbSteepSlopeHit => Hit && Abs(HitAngle - GroundAngle) > Tolerance;

        private async UniTask ClimbSlopeChangeCollision()
        {
            await ClimbSteepSlope();
            if (ClimbSteepSlopeHit) await OnClimbSteepSlopeHit();
            else await ClimbMildSlope();
        }

        private async UniTask OnClimbSteepSlopeHit()
        {
            var physics = physicsController.OnPlatformerClimbSteepSlopeHit();
            var raycast = raycastController.OnPlatformerClimbSteepSlopeHit();
            await (physics, raycast);
        }

        private async UniTask ClimbSteepSlope()
        {
            await raycastController.OnPlatformerClimbSteepSlope();
        }

        private LayerMask GroundLayer => layerMaskData.Ground;
        private bool ClimbMildSlopeHit => Hit && Hit.collider.gameObject.layer == GroundLayer && HitAngle < GroundAngle;

        private async UniTask ClimbMildSlope()
        {
            await raycastController.OnPlatformerClimbMildSlope();
            if (ClimbMildSlopeHit) await OnClimbMildSlopeHit();
            await Yield();
        }

        private async UniTask OnClimbMildSlopeHit()
        {
            await physicsController.OnPlatformerClimbMildSlopeHit();
        }

        private bool DescendMildSlopeHit => Hit && HitAngle < GroundAngle;
        private bool FacingRight => physicsData.FacingRight;

        private bool DescendSteepSlopeHit => Hit && Abs(Sign(Hit.normal.x) - DeltaMoveXDirectionAxis) < Tolerance &&
                                             Hit.collider.gameObject.layer == GroundLayer && HitAngle > GroundAngle &&
                                             Abs(Sign(Hit.normal.x) - (FacingRight ? 1 : -1)) < Tolerance;

        private async UniTask DescendSlopeChangeCollision()
        {
            await DescendMildSlope();
            if (DescendMildSlopeHit) await OnDescendMildSlopeHit();
            else await DescendSteepSlope();
            if (DescendSteepSlopeHit) await OnDescendSteepSlopeHit();
            await Yield();
        }

        private async UniTask DescendMildSlope()
        {
            await raycastController.OnPlatformerDescendMildSlope();
        }

        private async UniTask OnDescendMildSlopeHit()
        {
            var physics = physicsController.OnPlatformerDescendMildSlopeHit();
            var raycast = raycastController.OnPlatformerDescendMildSlopeHit();
            await (physics, raycast);
        }

        private async UniTask DescendSteepSlope()
        {
            await raycastController.OnPlatformerDescendSteepSlope();
        }

        private async UniTask OnDescendSteepSlopeHit()
        {
            await physicsController.OnPlatformerDescendSteepSlopeHit();
        }

        private async UniTask Move()
        {
            await CastRayOnMove();
            await MoveCharacter();
        }

        private async UniTask CastRayOnMove()
        {
            await raycastController.OnPlatformerCastRayOnMove();
        }

        private async UniTask MoveCharacter()
        {
            await physicsController.OnPlatformerMoveCharacter();
        }

        private async UniTask OnFrameExit()
        {
            await ResetJumpCollision();
            await ResetLayerMask();
            await ResetFriction();
        }

        private RaycastHit2D VerticalHit => raycastData.VerticalHit;
        private bool CollidingBelow => raycastData.CollidingBelow;
        private Vector2 TotalSpeed => physicsData.TotalSpeed;
        private bool CollidingAbove => raycastData.CollidingAbove;

        private bool StopForcesY => VerticalHit && CollidingBelow && TotalSpeed.y < 0 ||
                                    CollidingAbove && TotalSpeed.y > 0 && (!OnSlope || GroundAngle < MinimumWallAngle);

        private async UniTask ResetJumpCollision()
        {
            if (StopForcesY) await physicsController.OnPlatformerResetJumpCollision();
            await Yield();
        }

        private async UniTask ResetLayerMask()
        {
            await layerMaskController.OnPlatformerResetLayerMask();
        }

        private async UniTask ResetFriction()
        {
            await raycastController.OnPlatformerResetFriction();
        }

        #endregion

        #region event handlers

        #endregion
    }
}