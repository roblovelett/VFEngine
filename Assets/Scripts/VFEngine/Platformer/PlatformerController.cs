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

        [OdinSerialize] private PlatformerSettings settings;
        [OdinSerialize] private RaycastController raycastController;
        [OdinSerialize] private LayerMaskController layerMaskController;
        [OdinSerialize] private PhysicsController physicsController;
        private RaycastData raycastData;
        private PhysicsData physicsData;
        private LayerMaskData layerMaskData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            if (!raycastController) raycastController = GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (!physicsController) physicsController = GetComponent<PhysicsController>();
            if (!Data) Data = CreateInstance<PlatformerData>();
            Data.OnInitialize();
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

        private int HorizontalRays => raycastData.HorizontalRays;
        private async UniTask HorizontalCollision()
        {
            await raycastController.OnPlatformerHorizontalCollision();
        }

        private void StopSpeedControl()
        {
            //
        }

        private void VerticalCollision()
        {
            //
        }

        private void SlopeChangeCollision()
        {
            // 
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