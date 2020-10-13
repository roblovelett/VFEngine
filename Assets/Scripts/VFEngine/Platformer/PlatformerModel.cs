using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static PhysicsExtensions;
    using static TimeExtensions;
    using static Mathf;

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = "VFEngine/Platformer/Platformer Model", order = 0)]
    public class PlatformerModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Platformer Data")] [SerializeField]
        private PlatformerData p;

        /* fields: methods */
        private async UniTaskVoid GetWarningMessages()
        {
            if (!p.DisplayWarnings) return;
            const string rc = "Raycast";
            const string ctr = "Controller";
            const string ch = "Character";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!p.Physics) warningMessage += FieldParentGameObjectString($"Physics {ctr}", $"{ch}");
            if (!p.Raycast) warningMessage += FieldParentGameObjectString($"{rc} {ctr}", $"{ch}");
            if (!p.RaycastHitCollider)
                warningMessage += FieldParentGameObjectString($"{rc} Hit Collider {ctr}", $"{ch}");
            if (!p.LayerMask) warningMessage += FieldParentGameObjectString($"Layer Mask {ctr}", $"{ch}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldParentGameObjectString(string field, string gameObject)
            {
                warningMessageCount++;
                return FieldParentGameObjectMessage(field, gameObject);
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid RunPlatformer()
        {
            var pTask1 = Async(ApplyGravity());
            var pTask2 = Async(InitializeFrame());
            
            var task1 = await (pTask1, pTask2);
            
            await Async(TestMovingPlatform());
            await Async(SetMovementDirection());
            // CastRaysAsync();
            // OnHit Raycast
            // MoveTransform
            // Set Rays Params
            // Set New Speed
            // Set States
            // Set Distance To Ground
            // Reset External Force
            // On FrameExit
            // Update World Speed
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid ApplyGravity()
        {
            p.Physics.SetCurrentGravity();
            if (p.Speed.y > 0) p.Physics.ApplyAscentMultiplierToCurrentGravity();
            if (p.Speed.y < 0) p.Physics.ApplyFallMultiplierToCurrentGravity();
            if (p.GravityActive) p.Physics.ApplyGravityToVerticalSpeed();
            if (p.FallSlowFactor != 0) p.Physics.ApplyFallSlowFactorToVerticalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeFrame()
        {
            var phTask1 = Async(p.Physics.SetNewPosition());
            var phTask2 = Async(p.Physics.ResetState());
            var rhcTask1 = Async(p.RaycastHitCollider.ClearContactList());
            var rhcTask2 = Async(p.RaycastHitCollider.SetWasGroundedLastFrame());
            var rhcTask3 = Async(p.RaycastHitCollider.SetStandingOnLastFrame());
            var rhcTask4 = Async(p.RaycastHitCollider.SetWasTouchingCeilingLastFrame());
            var rhcTask5 = Async(p.RaycastHitCollider.SetCurrentWallCollider());
            var rhcTask6 = Async(p.RaycastHitCollider.ResetState());
            var rTask1 = Async(p.Raycast.SetRaysParameters());
            var pTask = await (phTask1, phTask2, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rTask1);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (p.IsCollidingWithMovingPlatform)
            {
                if (!SpeedNan(p.MovingPlatformCurrentSpeed)) p.Physics.TranslatePlatformSpeedToTransform();
                var platformTest = MovingPlatformTest(TimeLteZero(), AxisSpeedNan(p.MovingPlatformCurrentSpeed),
                    p.WasTouchingCeilingLastFrame);
                if (platformTest)
                {
                    var rchTask1 = Async(p.RaycastHitCollider.SetOnMovingPlatform());
                    var rchTask2 = Async(p.RaycastHitCollider.SetMovingPlatformCurrentGravity());
                    var phTask1 = Async(p.Physics.DisableGravity());
                    var phTask2 = Async(p.Physics.ApplyMovingPlatformSpeedToNewPosition());
                    var rTask1 = Async(p.Raycast.SetRaysParameters());
                    var pTask = await (rchTask1, rchTask2, phTask1, phTask2, rTask1);
                    p.Physics.StopHorizontalSpeed();
                    p.Physics.SetForcesApplied();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static bool MovingPlatformTest(bool timeLteZero, bool axisSpeedNan, bool wasTouchingCeilingLastFrame)
        {
            return !timeLteZero && !axisSpeedNan && !wasTouchingCeilingLastFrame;
        }

        private async UniTaskVoid SetMovementDirection()
        {
            p.Physics.SetMovementDirectionToStored();
            if (p.Speed.x < -p.MovementDirectionThreshold || p.ExternalForce.x < -p.MovementDirectionThreshold)
                p.Physics.SetNegativeMovementDirection();
            else if (p.Speed.x > p.MovementDirectionThreshold || p.ExternalForce.x > p.MovementDirectionThreshold)
                p.Physics.SetPositiveMovementDirection();
            if (p.IsCollidingWithMovingPlatform && Abs(p.MovingPlatformCurrentSpeed.x) > Abs(p.Speed.x))
                p.Physics.ApplyPlatformSpeedToMovementDirection();
            p.Physics.SetStoredMovementDirection();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties: methods */
        public void OnInitialize()
        {
            Async(GetWarningMessages());
        }

        public void OnRunPlatformer()
        {
            Async(RunPlatformer());
        }
    }
}

/* fields */
/*
private async UniTaskVoid TestMovingPlatformAsyncInternal()
{
    if (p.IsCollidingWithMovingPlatform)
    {
        if (!SpeedNan(p.MovingPlatformCurrentSpeed)) await physics.TranslatePlatformSpeedToTransformAsync();
        var platformTest = MovingPlatformTest(TimeLteZero(), AxisSpeedNan(p.MovingPlatformCurrentSpeed),
            p.WasTouchingCeilingLastFrame);
        if (platformTest)
        {
            await physics.ApplyPlatformSpeedToNewPositionAsync();
            await physics.StopSpeedAsync();
            var pTask1 = physics.DisableGravityAsync();
            var pTask2 = physics.SetAppliedForcesAsync();
            /*var rchTask = raycastHitCollider.SetMovingPlatformGravityAsync();
            var rTask = raycast.SetRaysParametersAsync();
            var task = await (pTask1, pTask2, rchTask, rTask);*/
/*        }
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetMovementDirectionAsyncInternal()
{
    await physics.SetMovementDirectionAsync();
    if (p.Speed.x < -p.MovementDirectionThreshold || p.ExternalForce.x < -p.MovementDirectionThreshold)
        await physics.SetMovementDirectionNegativeAsync();
    else if (p.Speed.x > p.MovementDirectionThreshold || p.ExternalForce.x > p.MovementDirectionThreshold)
        await physics.SetMovementDirectionPositiveAsync();
    if (p.IsCollidingWithMovingPlatform && Abs(p.MovingPlatformCurrentSpeed.x) > Abs(p.Speed.x))
        await physics.ApplyPlatformSpeedToMovementDirectionAsync();
    await physics.SetStoredMovementDirectionAsync();
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysAsyncInternal()
{
    var taskRight = CastRaysRightAsync();
    var taskLeft = CastRaysLeftAsync();
    var taskDown = CastRaysDownAsync();
    var taskUp = CastRaysUpAsync();
    
    if (p.CastRaysOnBothSides)
    {
        var task = await (taskRight, taskLeft, taskDown, taskUp);
    }
    else if (Abs(p.MovementDirection - -1) < p.Tolerance)
    {
        var task = await (taskLeft, taskDown, taskUp);
    }
    else
    {
        var task = await (taskRight, taskDown, taskUp);
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysRightAsyncInternal()
{
    //HorizontalRaycast rightRaycast = new HorizontalRaycast();
    /*
    var task1 = raycast.SetRightRaycastFromBottomAsync();
    var task2 = raycast.SetRightRaycastFromTopAsync();
    var task3 = raycast.SetRightRaycastLengthAsync();

    var task = await (task1, task2, task3);
        
    if (p.RightHitsStorage.Length != p.NumberOfHorizontalRays)
    {
        await raycastHitCollider.SetRightHitsStorageAsync();
    }

    for (var i = 0; i < p.NumberOfHorizontalRays; i++)
    {
        await raycastHit.SetRightRaycastOriginPointAsync();

        if (p.WasGroundedLastFrame && i == 0)
        {
            await raycastHitCollider.SetRightHitsStorageElementToIgnoreOnewayPlatformAsync();
        }
        else
        {
            await raycastHitCollider.SetRightHitsStorageElementAsync();
        }
    }
    */
/*    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysHorizontallyAsyncInternal(UniTask<UniTaskVoid> setRaycastFromBottom,
    UniTask<UniTaskVoid> setRaycastFromTop, UniTask<UniTaskVoid> setRaycastLength, int hitStorageLength,
    UniTask<UniTaskVoid> setSideHitsStorage, UniTask<UniTaskVoid> setRaycastOriginPoint, 
    UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOnewayPlatform,
    UniTask<UniTaskVoid> setSideHitsStorageElement)
{
    var task = await (setRaycastFromBottom, setRaycastFromTop, setRaycastLength);
    //if (hitStorageLength != p.NumberOfHorizontalRays) await setSideHitsStorage;
    /*for (var i = 0; i < p.NumberOfHorizontalRays; i++)
    {
        await setRaycastOriginPoint;
        if (p.wasGroundedLastFrame && i == 0) await setSideHitsStorageElementToIgnoreOnewayPlatform;
        else await setSideHitsStorageElement;
    }*/
/*    await SetYieldOrSwitchToThreadPoolAsync();
}

private UniTask<UniTaskVoid> CastRaysHorizontallyAsync(UniTask<UniTaskVoid> setRaycastFromBottom,
    UniTask<UniTaskVoid> setRaycastFromTop, UniTask<UniTaskVoid> setRaycastLength, int hitStorageLength,
    UniTask<UniTaskVoid> setSideHitsStorage, UniTask<UniTaskVoid> setRaycastOriginPoint,
    bool wasGroundedLastFrame, UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOnewayPlatform,
    UniTask<UniTaskVoid> setSideHitsStorageElement)
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysHorizontallyAsyncInternal(setRaycastFromBottom,
            setRaycastFromTop, setRaycastLength, hitStorageLength, setSideHitsStorage, setRaycastOriginPoint, setSideHitsStorageElementToIgnoreOnewayPlatform, setSideHitsStorageElement));
    }
    finally
    {
        CastRaysHorizontallyAsyncInternal(setRaycastFromBottom,
            setRaycastFromTop, setRaycastLength, hitStorageLength, setSideHitsStorage, setRaycastOriginPoint,setSideHitsStorageElementToIgnoreOnewayPlatform, setSideHitsStorageElement).Forget();
    }
}

private class HorizontalRaycast
{
    public UniTask<UniTaskVoid> SetRaycastFromBottom { get; private set; }
    public UniTask<UniTaskVoid> SetRaycastFromTop { get; private set; }
    public UniTask<UniTaskVoid> SetRaycastLength { get; private set; }
    public UniTask<UniTaskVoid> SetRaycastOriginPoint { get; private set; }
    public int HitStorageLength { get; private set; }
    public UniTask<UniTaskVoid> SetSideHitsStorage { get; private set; } 
    
    public UniTask<UniTaskVoid> SetSideHitsStorageElementToIgnoreOnewayPlatform { get; private set; }
    public UniTask<UniTaskVoid> SetSideHitsStorageElement { get; private set; }

    public HorizontalRaycast(UniTask<UniTaskVoid> setRaycastFromBottom,UniTask<UniTaskVoid> setRaycastFromTop,UniTask<UniTaskVoid> setRaycastLength,
        int hitStorageLength,UniTask<UniTaskVoid> setSideHitsStorage,UniTask<UniTaskVoid> setRaycastOriginPoint,
        UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOneWayPlatform, UniTask<UniTaskVoid> setSideHitsStorageElement)
    {
        setRaycastFromBottom = SetRaycastFromBottom;
        setRaycastFromTop = SetRaycastFromTop;
        setRaycastLength = SetRaycastLength;
        hitStorageLength = HitStorageLength;
        setSideHitsStorage = SetSideHitsStorage;
        setRaycastOriginPoint = SetRaycastOriginPoint;
        setSideHitsStorageElementToIgnoreOneWayPlatform = SetSideHitsStorageElementToIgnoreOnewayPlatform;
        setSideHitsStorageElement = SetSideHitsStorageElement;
    }
}

private class HorizontalRaycastHitCollider
{
    
}

private async UniTaskVoid CastRaysDownAsyncInternal()
{
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysLeftAsyncInternal()
{
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysUpAsyncInternal()
{
    await SetYieldOrSwitchToThreadPoolAsync();
}

private UniTask<UniTaskVoid> ApplyGravityAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyGravityAsyncInternal());
    }
    finally
    {
        ApplyGravityAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> InitializeFrameAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(InitializeFrameAsyncInternal());
    }
    finally
    {
        InitializeFrameAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> TestMovingPlatformAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(TestMovingPlatformAsyncInternal());
    }
    finally
    {
        TestMovingPlatformAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> SetMovementDirectionAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetMovementDirectionAsyncInternal());
    }
    finally
    {
        SetMovementDirectionAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> CastRaysAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysAsyncInternal());
    }
    finally
    {
        CastRaysAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> CastRaysRightAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysRightAsyncInternal());
    }
    finally
    {
        CastRaysRightAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> CastRaysDownAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysDownAsyncInternal());
    }
    finally
    {
        CastRaysDownAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> CastRaysLeftAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysLeftAsyncInternal());
    }
    finally
    {
        CastRaysLeftAsyncInternal().Forget();
    }
}

private UniTask<UniTaskVoid> CastRaysUpAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(CastRaysUpAsyncInternal());
    }
    finally
    {
        CastRaysUpAsyncInternal().Forget();
    }
}

/* properties */
/*public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

/* properties: methods */
/*public void Initialize()
{
    InitializeData();
    InitializeModel();
}

public UniTask<UniTaskVoid> PlatformerAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(PlatformerAsyncInternal());
    }
    finally
    {
        PlatformerAsyncInternal().Forget();
    }
}*/