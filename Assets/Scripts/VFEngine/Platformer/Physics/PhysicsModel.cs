using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics
{
    using static PhysicsData;
    using static Debug;
    using static DebugExtensions;
    using static ScriptableObjectExtensions;
    using static Quaternion;
    using static UniTaskExtensions;
    using static Time;
    using static Mathf;

    [CreateAssetMenu(fileName = "PhysicsModel", menuName = "VFEngine/Platformer/Physics/Physics Model", order = 0)]
    public class PhysicsModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Physics Data")] [SerializeField] private PhysicsData p;

        /* fields */
        private bool HasData => p;

        /* fields: methods */
        private async UniTaskVoid InitializeInternal()
        {
            var pTask1 = Async(InitializeData());
            var pTask2 = Async(GetWarningMessages());
            var pTask = await (pTask1, pTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            p.TransformRef = p.Transform;
            p.state.Reset();
            if (p.AutomaticGravityControl && !p.HasGravityController) p.Transform.rotation = identity;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (DisplayWarnings)
            {
                const string ph = "Physics";
                var data = $"{ph} Data";
                var settings = $"{ph} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!HasData) warningMessage += FieldString($"{data}", $"{data}");
                if (!p.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                if (!p.HasGravityController) warningMessage += FieldParentString("Gravity Controller", $"{settings}");
                if (!p.HasTransform) warningMessage += FieldParentString("Transform", $"{settings}");
                DebugLogWarning(warningMessageCount, warningMessage);

                string FieldString(string field, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldMessage(field, scriptableObject);
                }

                string FieldParentString(string field, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldParentMessage(field, scriptableObject);
                }

                void AddWarningMessageCount()
                {
                    warningMessageCount++;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties: dependencies */
        private bool DisplayWarnings => p.DisplayWarnings;

        /* properties: methods */
        public void Initialize()
        {
            Async(InitializeInternal());
        }
    }
}

/* fields */
/* fields: methods */
/*
private async UniTaskVoid SetGravityAsyncInternal()
{
    p.CurrentGravity = p.Gravity;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ApplyAscentMultiplierToGravityAsyncInternal()
{
    p.CurrentGravity /= p.AscentMultiplier;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ApplyFallMultiplierToGravityAsyncInternal()
{
    p.CurrentGravity *= p.FallMultiplier;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ApplyGravityToSpeedAsyncInternal()
{
    p.Speed = new Vector2(p.Speed.x, ApplyGravityTime());
    await SetYieldOrSwitchToThreadPoolAsync();

    float ApplyGravityTime()
    {
        return p.Speed.y + (p.CurrentGravity + p.MovingPlatformCurrentGravity) * deltaTime;
    }
}

private async UniTaskVoid ApplyFallSlowFactorToSpeedAsyncInternal()
{
    p.Speed = new Vector2(p.Speed.x, ApplyFallSlowFactor());
    await SetYieldOrSwitchToThreadPoolAsync();

    float ApplyFallSlowFactor()
    {
        return p.Speed.y * p.FallSlowFactor;
    }
}

private async UniTaskVoid SetNewPositionAsyncInternal()
{
    p.NewPosition = p.Speed * deltaTime;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ResetStateAsyncInternal()
{
    p.State.Reset();
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid TranslatePlatformSpeedToTransformAsyncInternal()
{
    p.Transform.Translate(p.MovingPlatformCurrentSpeed * deltaTime);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid DisableGravityAsyncInternal()
{
    p.State.SetGravity(false);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ApplyPlatformSpeedToNewPositionAsyncInternal()
{
    p.NewPosition = new Vector2(p.NewPosition.x, p.MovingPlatformCurrentSpeed.y * deltaTime);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid StopSpeedAsyncInternal()
{
    p.Speed = p.NewPosition / deltaTime;
    p.Speed = new Vector2(-p.Speed.x, p.Speed.y);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetAppliedForcesAsyncInternal()
{
    p.ForcesApplied = p.Speed;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetMovementDirectionAsyncInternal()
{
    p.MovementDirection = p.StoredMovementDirection;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetMovementDirectionNegativeAsyncInternal()
{
    p.MovementDirection = -1;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetMovementDirectionPositiveAsyncInternal()
{
    p.MovementDirection = 1;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid ApplyPlatformSpeedToMovementDirectionAsyncInternal()
{
    p.MovementDirection = Sign(p.MovingPlatformCurrentSpeed.x);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetStoredMovementDirectionAsyncInternal()
{
    p.StoredMovementDirection = p.MovementDirection;
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastHorizontalRayAsyncInternal(float rayDirection)
{
    
    await SetYieldOrSwitchToThreadPoolAsync();
}

/* properties */
/*
public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

/* properties: methods */
/*
public UniTask<UniTaskVoid> SetGravityAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetGravityAsyncInternal());
    }
    finally
    {
        SetGravityAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyAscentMultiplierToGravityAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyAscentMultiplierToGravityAsyncInternal());
    }
    finally
    {
        ApplyAscentMultiplierToGravityAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyFallMultiplierToGravityAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyFallMultiplierToGravityAsyncInternal());
    }
    finally
    {
        ApplyFallMultiplierToGravityAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyGravityToSpeedAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyGravityToSpeedAsyncInternal());
    }
    finally
    {
        ApplyGravityToSpeedAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyFallSlowFactorToSpeedAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyFallSlowFactorToSpeedAsyncInternal());
    }
    finally
    {
        ApplyFallSlowFactorToSpeedAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetNewPositionAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetNewPositionAsyncInternal());
    }
    finally
    {
        SetNewPositionAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ResetStateAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ResetStateAsyncInternal());
    }
    finally
    {
        ResetStateAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> TranslatePlatformSpeedToTransformAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(TranslatePlatformSpeedToTransformAsyncInternal());
    }
    finally
    {
        TranslatePlatformSpeedToTransformAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> DisableGravityAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(DisableGravityAsyncInternal());
    }
    finally
    {
        DisableGravityAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyPlatformSpeedToNewPositionAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyPlatformSpeedToNewPositionAsyncInternal());
    }
    finally
    {
        ApplyPlatformSpeedToNewPositionAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> StopSpeedAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(StopSpeedAsyncInternal());
    }
    finally
    {
        StopSpeedAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetAppliedForcesAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetAppliedForcesAsyncInternal());
    }
    finally
    {
        SetAppliedForcesAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetMovementDirectionAsync()
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

public UniTask<UniTaskVoid> SetMovementDirectionNegativeAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetMovementDirectionNegativeAsyncInternal());
    }
    finally
    {
        SetMovementDirectionNegativeAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetMovementDirectionPositiveAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetMovementDirectionPositiveAsyncInternal());
    }
    finally
    {
        SetMovementDirectionPositiveAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> ApplyPlatformSpeedToMovementDirectionAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(ApplyPlatformSpeedToMovementDirectionAsyncInternal());
    }
    finally
    {
        ApplyPlatformSpeedToMovementDirectionAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetStoredMovementDirectionAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetStoredMovementDirectionAsyncInternal());
    }
    finally
    {
        SetStoredMovementDirectionAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> CastHorizontalRayAsync(float rayDirection)
{
    try
    {
        return new UniTask<UniTaskVoid>(CastHorizontalRayAsyncInternal(rayDirection));
    }
    finally
    {
        CastHorizontalRayAsyncInternal(rayDirection).Forget();
    }
}

/*
=======================================================================================================================
 */
//    private async UniTaskVoid OnRaycastHorizontalAsyncInternal(float raycastDirection, float distanceToWall,
//        float boundsWidth, float raycastOffset, bool isGrounded)
//   {
/*
if (Abs(movementDirection - raycastDirection) < Tolerance)
{
    newPosition.x = raycastDirection <= 0
        ? -distanceToWall + boundsWidth / 2 + raycastOffset * 2
        : distanceToWall - boundsWidth / 2 - raycastOffset * 2;
    if (!isGrounded && speed.y != 0) newPosition.x = 0; // prevent pushback if airborne.
    speed.x = 0;
}

await Yield();
*/
//   }

//  private async UniTaskVoid OnRaycastBelowAsyncInternal()
//  {
//state.IsFalling = newPosition.y < -SmallValue;
//await Yield();
//   }

//    private async UniTaskVoid OnRaycastBelowHitAsyncInternal(bool raycastHit, float distanceToGround,
//        float boundsHeight, float raycastOffset, bool wasGroundedLastFrame)
//    {
/*
if (raycastHit)
{
    //state.IsFalling = false;
    newPosition.y = externalForce.y > 0 && speed.y > 0
        ? speed.y * deltaTime
        : -distanceToGround + boundsHeight / 2 * raycastOffset;
    if (!wasGroundedLastFrame && speed.y > 0) newPosition.y += speed.y * deltaTime;
    if (Abs(newPosition.y) < SmallValue) newPosition.y = 0;
}

await Yield();
*/
//   }
/*    private async UniTaskVoid OnMovingPlatformTestAsyncInternal(bool raycastHit,
        PathMovementController movingPlatformTest, PathMovementController movingPlatform, bool isGrounded,
        bool onMovingPlatform)
    {
        if (raycastHit && !movingPlatformTest && !isGrounded) DetachFromMovingPlatform(movingPlatform);
        else if (onMovingPlatform) DetachFromMovingPlatform(movingPlatform);
        await Yield();
    }

    private async UniTaskVoid OnStickToSlopeAsyncInternal(float belowSlopeAngleLeft, float belowSlopeAngleRight,
        RaycastHit2D stickyRaycast, Object ignoredCollider, float raycastOriginY, float boundsHeight)
    {
        /*
        if (stickToSlopeControl && belowSlopeAngleLeft > 0 && belowSlopeAngleRight < 0f && stickyRaycast &&
            stickyRaycast.collider != ignoredCollider) SetNewPositionY(stickyRaycast, raycastOriginY, boundsHeight);
        await Yield();
        */
/*   }

   private async UniTaskVoid OnStickyRaycastAsyncInternal(RaycastHit2D stickyRaycast, Object ignoredCollider,
       float raycastOriginY, float boundsHeight)
   {
       if (stickyRaycast && stickyRaycast.collider != ignoredCollider)
           SetNewPositionY(stickyRaycast, raycastOriginY, boundsHeight);
       await Yield();
   }

   private async UniTaskVoid OnRaycastAboveHitAsyncInternal(bool raycastHit, float smallestDistance,
       float boundsHeight, bool isGrounded, bool hitCeilingLastFrame)
   {
       /*
       if (raycastHit)
       {
           newPosition.y = smallestDistance - boundsHeight / 2;
           if (isGrounded && newPosition.y < 0) newPosition.y = 0;
           if (!hitCeilingLastFrame) speed = new Vector2(speed.x, 0f);
           speed.y = 0; // apply vertical force
       }

       await Yield();
       */
/*        }

        private async UniTaskVoid OnMoveTransformAsyncInternal(RaycastHit2D safetyBoxcast)
        {
            /*
            if (safetyBoxcast && Abs(safetyBoxcast.distance - newPosition.magnitude) != 0) newPosition = Vector2.zero;
            else p.transform.Translate(newPosition, Self);
            await Yield();
            */
/*      }

      private async UniTaskVoid OnSetNewSpeedAsyncInternal(bool isGrounded, float belowSlopeAngle,
          bool onMovingPlatform)
      {
          /*
          if (deltaTime > 0) speed = newPosition / deltaTime;
          if (isGrounded) speed.x *= slopeAngleSpeedFactor.Evaluate(Abs(belowSlopeAngle) * Sign(speed.y));
          if (!onMovingPlatform)
          {
              speed.x = Clamp(speed.x, -maximumVelocity.x, maximumVelocity.x);
              speed.y = Clamp(speed.y, -maximumVelocity.y, maximumVelocity.y);
          }

          await Yield();
          */
/*    }

    private async UniTaskVoid OnRaycastColliderHitAsyncInternal(IEnumerable<RaycastHit2D> contactList)
    {
        /*
        if (physics2DInteractionControl)
            foreach (var hit in contactList)
            {
                var body = hit.collider.attachedRigidbody;
                if (!body || body.isKinematic || body.bodyType == Static) continue;
                var pushDirection = new Vector3(externalForce.x, 0, 0);
                body.velocity = pushDirection.normalized * physics2DPushForce;
            }

        await Yield();
        */
/*  }

  private async UniTaskVoid OnSetExternalForceAsyncInternal()
  {
      /*
      externalForce.x = 0;
      externalForce.y = 0;
      await Yield();
      */
/*}

private async UniTaskVoid OnSetWorldSpeedAsyncInternal()
{
    /*
    worldSpeed = speed;
    await Yield();
    */
/*}

private void DetachFromMovingPlatform(PathMovementController movingPlatform)
{
    if (!movingPlatform) return;
    //state.GravityActive = true;
}

private void SetNewPositionY(RaycastHit2D stickyRaycast, float raycastOriginY, float boundsHeight)
{
    /*
    newPosition.y = -Abs(stickyRaycast.point.y - raycastOriginY) + boundsHeight / 2;
    */
//}
//}
//}