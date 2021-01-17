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

// ReSharper disable UnusedMember.Local
namespace VFEngine.Platformer
{
    using static UniTask;
    using static Debug;
    using static Mathf;
    using static Time;
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
                await HorizontalDeltaMoveDetection();
                await StopHorizontalSpeedControl();
                await VerticalCollision();
                await SlopeChangeCollisionControl();
                Log($"Frame={frameCount} FixedTime={fixedTime}");
                await WaitForFixedUpdate(ct);
            }
        }

        private async UniTask InitializeFrame()
        {
            var raycast = raycastController.OnPlatformerInitializeFrame();
            var layerMask = layerMaskController.OnPlatformerInitializeFrame();
            var physics = physicsController.OnPlatformerInitializeFrame();
            await (raycast, layerMask, physics);
        }

        private async UniTask GroundCollision()
        {
            await raycastController.OnPlatformerGroundCollisionRaycast();
        }

        private async UniTask UpdateForces()
        {
            await physicsController.OnPlatformerUpdateForces();
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
        }

        private async UniTask OnClimbMildSlopeHit()
        {
            await physicsController.OnPlatformerClimbMildSlopeHit();
        }

        private async UniTask DescendSlopeChangeCollision()
        {
            await DescendMildSlope();
            //if (HitMildSlope)
            {
            }
            //else await DescendSteepSlope();
            await Yield();
        }

        private async UniTask DescendMildSlope()
        {
            await Yield();
        }

        private async UniTask DescendSteepSlope()
        {
            await Yield();
        }

        private void CastRayTowardsMovement()
        {
            //
        }

        private void Move()
        {
            //
        }

        private void ResetJumpCollision()
        {
            //
        }

        private void OnFrameExit()
        {
            /*
            setLayerMaskToSaved.Invoke();
            setRaycastFrictionCollisionDetection.Invoke();
            */
        }

        #endregion

        #region event handlers

        #endregion
    }
}