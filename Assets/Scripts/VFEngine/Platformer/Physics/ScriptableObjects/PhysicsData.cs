using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.ScriptableObjects
{
    using static Vector2;
    using static Mathf;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PlatformerPhysicsDataPath, order = 0)]
    public class PhysicsData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public Vector2 DeltaMove { get; private set; }
        public bool IgnoreFriction { get; private set; }
        public float GroundFriction { get; private set; }
        public float AirFriction { get; private set; }
        public Vector2 ExternalForce { get; private set; }
        public float MinimumMoveThreshold { get; private set; }
        public float Gravity { get; private set; }
        public float GravityScale { get; private set; }
        public Vector2 Speed { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float MinimumWallAngle { get; private set; }
        public bool FacingRight { get; private set; }
        public Vector2 TotalSpeed => Speed + ExternalForce;
        public int DeltaMoveXDirectionAxis => AxisDirection(DeltaMove.x);
        public int DeltaMoveYDirectionAxis => AxisDirection(DeltaMove.y);
        public float DeltaMoveDistanceX => Abs(DeltaMove.x);

        #endregion

        #region fields

        private Vector2 speed;
        private Vector2 externalForce;
        private Vector2 deltaMove;

        #endregion

        #region initialization

        private void Initialize(PhysicsSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
        }

        private void ApplySettings(PhysicsSettings settings)
        {
            MaximumSlopeAngle = settings.maximumSlopeAngle;
            MinimumWallAngle = settings.minimumWallAngle;
            MinimumMoveThreshold = settings.minimumMovementThreshold;
            Gravity = settings.gravity;
            AirFriction = settings.airFriction;
            GroundFriction = settings.groundFriction;
        }

        private void InitializeDefault()
        {
            FacingRight = true;
            IgnoreFriction = false;
            GravityScale = 1;
            Speed = zero;
            ExternalForce = zero;
            DeltaMove = zero;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private static int AxisDirection(float axis)
        {
            return (int) Sign(axis);
        }

        private void InitializeFrame(float deltaTime)
        {
            SetDeltaMove(DeltaMoveInitialized(deltaTime));
        }

        private Vector2 DeltaMoveInitialized(float deltaTime)
        {
            return TotalSpeed * deltaTime;
        }

        private void SetDeltaMove(Vector2 force)
        {
            DeltaMove = force;
        }

        private void UpdateForces(bool onGround, bool onSlope, int groundDirection, float groundAngle, float deltaTime)
        {
            UpdateExternalForce(onGround, deltaTime);
            UpdateGravity(deltaTime);
            UpdateExternalForceX(onSlope, groundDirection, groundAngle, deltaTime);
        }

        private bool StopExternalForce => ExternalForce.magnitude <= MinimumMoveThreshold;

        private void UpdateExternalForce(bool onGround, float deltaTime)
        {
            if (IgnoreFriction) return;
            var friction = OnGroundFriction(onGround);
            SetExternalForce(GroundFrictionExternalForce(friction, deltaTime));
            if (StopExternalForce) SetExternalForce(zero);
        }

        private float OnGroundFriction(bool onGround)
        {
            return onGround ? GroundFriction : AirFriction;
        }

        private Vector2 GroundFrictionExternalForce(float friction, float deltaTime)
        {
            return MoveTowards(ExternalForce, zero, ExternalForce.magnitude * friction * deltaTime);
        }

        private void SetExternalForce(Vector2 force)
        {
            ExternalForce = force;
        }

        private float GravityScaled => Gravity * GravityScale;
        private bool ApplyGravityToSpeed => Speed.y > 0;

        private void UpdateGravity(float deltaTime)
        {
            var gravityScaledToTime = GravityScaledToTime(deltaTime);
            if (ApplyGravityToSpeed) ApplyToSpeedY(gravityScaledToTime);
            else ApplyToExternalForceY(gravityScaledToTime);
        }

        private float GravityScaledToTime(float deltaTime)
        {
            return GravityScaled * deltaTime;
        }

        private void ApplyToSpeedY(float force)
        {
            speed = Speed;
            speed.y += force;
            SetSpeed(speed);
        }

        private void SetSpeed(Vector2 force)
        {
            Speed = force;
        }

        private void ApplyToExternalForceY(float force)
        {
            externalForce = ExternalForce;
            externalForce.y += force;
            SetExternalForce(externalForce);
        }

        private bool NoHorizontalSpeed => Speed.x == 0;

        private void UpdateExternalForceX(bool onSlope, int groundDirection, float groundAngle, float deltaTime)
        {
            var applyCombinedForcesToExternalForceX = onSlope && groundAngle > MaximumSlopeAngle &&
                                                      (groundAngle < MinimumWallAngle || NoHorizontalSpeed);
            if (applyCombinedForcesToExternalForceX) ApplyToExternalForceX(CombinedForcesX(groundDirection, deltaTime));
        }

        private float CombinedForcesX(int groundDirection, float deltaTime)
        {
            return -Gravity * GroundFriction * groundDirection * deltaTime / 4;
        }

        private void ApplyToExternalForceX(float force)
        {
            externalForce = ExternalForce;
            externalForce.x += force;
            SetExternalForce(externalForce);
        }

        private void DescendSlope(float groundAngle)
        {
            SlopeBehavior(false, groundAngle);
        }

        private void SlopeBehavior(bool climbing, float groundAngle)
        {
            SetDeltaMove(SlopeDeltaMove(climbing, groundAngle));
            StopForcesY();
        }

        private Vector2 SlopeDeltaMove(bool climbing, float groundAngle)
        {
            return climbing
                ? new Vector2(SlopeDeltaMoveX(groundAngle), SlopeDeltaMoveY(groundAngle))
                : new Vector2(SlopeDeltaMoveX(groundAngle), -SlopeDeltaMoveY(groundAngle));
        }

        private float SlopeDeltaMoveX(float groundAngle)
        {
            return Cos(groundAngle * Deg2Rad) * DeltaMoveDistanceX * DeltaMoveXDirectionAxis;
        }

        private float SlopeDeltaMoveY(float groundAngle)
        {
            return Sin(groundAngle * Deg2Rad) * DeltaMoveDistanceX;
        }

        private void StopForcesY()
        {
            SetSpeedY(0);
            SetExternalForceY(0);
        }

        private void SetSpeedY(float y)
        {
            speed = Speed;
            speed.y = y;
            SetSpeed(speed);
        }

        private void SetExternalForceY(float y)
        {
            externalForce = ExternalForce;
            externalForce.y = y;
            SetExternalForce(externalForce);
        }

        private void ClimbSlope(float groundAngle)
        {
            SlopeBehavior(true, groundAngle);
        }

        private void HitClimbingSlope(float groundAngle, float hitDistance, float skinWidth)
        {
            var deltaMoveXHitClimbingSlope = DeltaMoveXHitClimbingSlope(hitDistance, skinWidth);
            ApplyToDeltaMoveX(-deltaMoveXHitClimbingSlope);
            ClimbSlope(groundAngle);
            ApplyToDeltaMoveX(deltaMoveXHitClimbingSlope);
        }

        private float DeltaMoveXHitClimbingSlope(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void ApplyToDeltaMoveX(float x)
        {
            deltaMove = DeltaMove;
            deltaMove.x += x;
            SetDeltaMove(deltaMove);
        }

        private void HitMaximumSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveX(HitMaximumSlopeDeltaMoveX(hitDistance, skinWidth));
        }

        private float HitMaximumSlopeDeltaMoveX(float hitDistance, float skinWidth)
        {
            return Min(DeltaMoveDistanceX, hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void SetDeltaMoveX(float x)
        {
            deltaMove = DeltaMove;
            deltaMove.x = x;
            SetDeltaMove(deltaMove);
        }

        private bool NegativeDeltaMoveY => DeltaMove.y < 0;
        private void HitSlopedGroundAngle(float groundAngle)
        {
            if (NegativeDeltaMoveY) SetDeltaMoveY(0);
            else SetDeltaMoveY(HitSlopedGroundAngleDeltaMoveY(groundAngle));
        }

        private void SetDeltaMoveY(float y)
        {
            deltaMove = DeltaMove;
            deltaMove.y = y;
            SetDeltaMove(DeltaMove);
        }

        private float HitSlopedGroundAngleDeltaMoveY(float groundAngle)
        {
            return Tan(groundAngle * Deg2Rad) * DeltaMoveDistanceX * DeltaMoveYDirectionAxis;
        }

        private void StopForcesX()
        {
            SetSpeedX(0);
            SetExternalForceX(0);
        }

        private void SetSpeedX(float x)
        {
            speed = Speed;
            speed.x = x;
            SetSpeed(speed);
        }

        private void SetExternalForceX(float x)
        {
            externalForce = ExternalForce;
            externalForce.x = x;
            SetExternalForce(externalForce);
        }

        #endregion

        #region event handlers

        public void OnInitialize(PhysicsSettings settings)
        {
            Initialize(settings);
        }

        public void OnInitializeFrame(float deltaTime)
        {
            InitializeFrame(deltaTime);
        }

        public void OnUpdateForces(bool onGround, bool onSlope, int groundDirection, float groundAngle, float deltaTime)
        {
            UpdateForces(onGround, onSlope, groundDirection, groundAngle, deltaTime);
        }

        public void OnDescendSlope(float groundAngle)
        {
            DescendSlope(groundAngle);
        }

        public void OnClimbSlope(float groundAngle)
        {
            ClimbSlope(groundAngle);
        }

        public void OnHitClimbingSlope(float groundAngle, float hitDistance, float skinWidth)
        {
            HitClimbingSlope(groundAngle, hitDistance, skinWidth);
        }

        public void OnHitMaximumSlope(float hitDistance, float skinWidth)
        {
            HitMaximumSlope(hitDistance, skinWidth);
        }
        
        public void OnHitSlopedGroundAngle(float groundAngle)
        {
            HitSlopedGroundAngle(groundAngle);
        }

        public void OnHitMaximumSlope()
        {
            StopForcesX();
        }

        public void OnStopHorizontalSpeed()
        {
            SetSpeedX(0);
        }

        #endregion
    }
}

#region hide

/*
public void SetExternalForce(Vector2 force)
{
    ExternalForce = force;
}

public void ApplyToExternalForceX(int groundDirectionAxis, float deltaTime)
{
    var force = -Gravity * GroundFriction * groundDirectionAxis * deltaTime / 4;
    externalForce = ExternalForce;
    externalForce.x += force;
    ExternalForce = externalForce;
}

public void SetDeltaMove(DeltaMoveBehavior behavior, float value)
{
    var deltaTime = value;
    var groundAngle = value * Deg2Rad;
    var deltaMoveInitialized = TotalSpeed * deltaTime;
    var slopeDistance = Abs(DeltaMove.x);
    var deltaMoveSlopeX = Cos(groundAngle) * slopeDistance * DeltaMoveXDirectionAxis;
    var deltaMoveDescendSlopeY = -Sin(groundAngle) * slopeDistance;
    var deltaMoveDescendSlope = new Vector2(deltaMoveSlopeX, deltaMoveDescendSlopeY);
    var deltaMoveClimbSlopeY = Sin(groundAngle) * slopeDistance;
    var deltaMoveClimbSlope = new Vector2(deltaMoveSlopeX, deltaMoveClimbSlopeY);
    
    switch (behavior)
    {
        case Initialization:
            DeltaMove = deltaMoveInitialized;
            break;
        case DescendSlope:
            DeltaMove = deltaMoveDescendSlope;
            break;
        case ClimbSlope:
            DeltaMove = deltaMoveClimbSlope;
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
    }
}

public void SetSpeedY(float y)
{
    speed = Speed;
    speed.y = y;
    Speed = speed;
}

public void SetExternalForceY(float y)
{
    externalForce = ExternalForce;
    externalForce.y = y;
    ExternalForce = externalForce;
}

public void ApplyToDeltaMoveX(float force)
{
    deltaMove = DeltaMove;
    deltaMove.x += force;
    DeltaMove = deltaMove;
}

public void SetDeltaMoveX(float x)
{
    deltaMove = DeltaMove;
    deltaMove.x = x;
    DeltaMove = deltaMove;
}

public void SetDeltaMoveY(float y)
{
    deltaMove = DeltaMove;
    deltaMove.y = y;
    DeltaMove = deltaMove;
}

public void SetSpeedX(float x)
{
    speed = Speed;
    speed.x = x;
    Speed = speed;
}

public void SetExternalForceX(float x)
{
    externalForce = ExternalForce;
    externalForce.x = x;
    ExternalForce = externalForce;
}

public void ApplyToDeltaMove(Vector2 force)
{
    deltaMove = DeltaMove;
    deltaMove += force;
    DeltaMove = deltaMove;
}

public void SetIgnoreFriction(bool ignore)
{
    IgnoreFriction = ignore;
}

public void ApplyGravity(float deltaTime)
{
    var gravityScaledToTime = Gravity * GravityScale * deltaTime;
    if (Speed.y > 0) ApplyToSpeedY(gravityScaledToTime);
    else ApplyToExternalForceY(gravityScaledToTime);
}
private Vector2 DescendSlopeDeltaMove()
*/ /*
        public void SetExternalForce(Vector2 force)
        {
            ExternalForce = force;
        }

        public void ApplyToExternalForceX(int groundDirectionAxis, float deltaTime)
        {
            var force = -Gravity * GroundFriction * groundDirectionAxis * deltaTime / 4;
            externalForce = ExternalForce;
            externalForce.x += force;
            ExternalForce = externalForce;
        }
        
        public void SetDeltaMove(DeltaMoveBehavior behavior, float value)
        {
            var deltaTime = value;
            var groundAngle = value * Deg2Rad;
            var deltaMoveInitialized = TotalSpeed * deltaTime;
            var slopeDistance = Abs(DeltaMove.x);
            var deltaMoveSlopeX = Cos(groundAngle) * slopeDistance * DeltaMoveXDirectionAxis;
            var deltaMoveDescendSlopeY = -Sin(groundAngle) * slopeDistance;
            var deltaMoveDescendSlope = new Vector2(deltaMoveSlopeX, deltaMoveDescendSlopeY);
            var deltaMoveClimbSlopeY = Sin(groundAngle) * slopeDistance;
            var deltaMoveClimbSlope = new Vector2(deltaMoveSlopeX, deltaMoveClimbSlopeY);
            
            switch (behavior)
            {
                case Initialization:
                    DeltaMove = deltaMoveInitialized;
                    break;
                case DescendSlope:
                    DeltaMove = deltaMoveDescendSlope;
                    break;
                case ClimbSlope:
                    DeltaMove = deltaMoveClimbSlope;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
            }
        }

        public void SetSpeedY(float y)
        {
            speed = Speed;
            speed.y = y;
            Speed = speed;
        }

        public void SetExternalForceY(float y)
        {
            externalForce = ExternalForce;
            externalForce.y = y;
            ExternalForce = externalForce;
        }

        public void ApplyToDeltaMoveX(float force)
        {
            deltaMove = DeltaMove;
            deltaMove.x += force;
            DeltaMove = deltaMove;
        }

        public void SetDeltaMoveX(float x)
        {
            deltaMove = DeltaMove;
            deltaMove.x = x;
            DeltaMove = deltaMove;
        }

        public void SetDeltaMoveY(float y)
        {
            deltaMove = DeltaMove;
            deltaMove.y = y;
            DeltaMove = deltaMove;
        }

        public void SetSpeedX(float x)
        {
            speed = Speed;
            speed.x = x;
            Speed = speed;
        }

        public void SetExternalForceX(float x)
        {
            externalForce = ExternalForce;
            externalForce.x = x;
            ExternalForce = externalForce;
        }

        public void ApplyToDeltaMove(Vector2 force)
        {
            deltaMove = DeltaMove;
            deltaMove += force;
            DeltaMove = deltaMove;
        }

        public void SetIgnoreFriction(bool ignore)
        {
            IgnoreFriction = ignore;
        }

        public void ApplyGravity(float deltaTime)
        {
            var gravityScaledToTime = Gravity * GravityScale * deltaTime;
            if (Speed.y > 0) ApplyToSpeedY(gravityScaledToTime);
            else ApplyToExternalForceY(gravityScaledToTime);
        }
        private Vector2 DescendSlopeDeltaMove()
        */
/*
public void OnSetForces(bool onGround, bool onSlope, int groundDirection, float groundAngle, float deltaTime)
{
    if (IgnoreFriction) return;
    ApplyFriction(onGround, deltaTime);
    ApplyGravity(deltaTime);
    ApplyForcesToExternalForceX(onSlope, groundDirection, groundAngle, deltaTime);
}

public void OnSetSlopeBehavior(int groundDirectionAxis, float groundAngle)
{
    var descendingSlope = groundDirectionAxis == DeltaMoveXDirectionAxis;
    DeltaMove = descendingSlope ? DeltaMoveOnDescendSlope(groundAngle) : DeltaMoveOnClimbSlope(groundAngle);
    StopForcesY();
}

public void OnRaycastHorizontalCollisionHit(RaycastHit2D hit, float skinWidth, float groundAngle)
{
    var horizontalCollisionHitDistance = (hit.distance - skinWidth) * DeltaMoveXDirectionAxis;
    var moveY = Sin(groundAngle * Deg2Rad) * SlopeDistance;
    var climbSlope = (groundAngle < MinimumWallAngle) && (DeltaMove.y <= moveY);
    ApplyForceToDeltaMoveX(-horizontalCollisionHitDistance);
    if (climbSlope)
    {
        DeltaMove = DeltaMoveOnClimbSlope(groundAngle, moveY);
        StopForcesY();
    }
    ApplyForceToDeltaMoveX(-horizontalCollisionHitDistance);
}

private void ApplyForceToDeltaMoveX(float force)
{
    deltaMove = DeltaMove;
    deltaMove.x += force;
    DeltaMove = deltaMove;
}

private Vector2 DeltaMoveOnClimbSlope(float groundAngle, float moveY)
{
    return new Vector2(DeltaMoveXOnSlope(groundAngle), moveY);
}
*/
/*private bool StopExternalForce => ExternalForce.magnitude <= MinimumMoveThreshold;
        
        private void ApplyFriction(bool onGround, float deltaTime)
        {
            var friction = onGround ? GroundFriction : AirFriction;
            ExternalForce = ExternalForceFrictionApplied(onGround, friction, deltaTime);
            if (StopExternalForce) ExternalForce = zero;
        }
        
        private Vector2 ExternalForceFrictionApplied(bool onGround, float friction, float deltaTime)
        {
            return MoveTowards(ExternalForce, zero, ExternalForce.magnitude * friction * deltaTime);
        }
        
        private bool ApplyGravityToVerticalSpeed => Speed.y > 0;
        private void ApplyGravity(float deltaTime)
        {
            var gravityScaled = GravityScaled(deltaTime);
            if (ApplyGravityToVerticalSpeed) ApplyToSpeedY(gravityScaled);
            else ApplyToExternalForceY(gravityScaled);
        }
        private float GravityScaled(float deltaTime)
        {
            return (Gravity * GravityScale * deltaTime);
        }
        private void ApplyToSpeedY(float force)
        {
            speed = Speed;
            speed.y += force;
            Speed = speed;
        }
        private void ApplyToExternalForceY(float force)
        {
            externalForce = ExternalForce;
            externalForce.y += force;
            ExternalForce = externalForce;
        }

        private void ApplyForcesToExternalForceX(bool onSlope, int groundDirection, float groundAngle, float deltaTime)
        {
            var applyForcesToExternalForceX = onSlope && groundAngle > MaximumSlopeAngle &&
                                              (groundAngle < MinimumWallAngle || Speed.x == 0);
            var externalForceXForcesApplied = ExternalForceXForcesApplied(groundDirection, deltaTime);
            if (applyForcesToExternalForceX) ApplyToExternalForceX(externalForceXForcesApplied);
        }

        private float ExternalForceXForcesApplied(int groundDirection, float deltaTime)
        {
            return -Gravity * GroundFriction * groundDirection * deltaTime / 4;
        }
        
        private void ApplyToExternalForceX(float force)
        {
            externalForce = ExternalForce;
            externalForce.x += force;
            ExternalForce = externalForce;
        }
        
        private float SlopeDistance => Abs(DeltaMove.x);
        
        private float DeltaMoveXOnSlope(float groundAngle)
        {
            return Cos(groundAngle * Deg2Rad) * SlopeDistance * DeltaMoveXDirectionAxis;
        }

        private float DeltaMoveYOnSlope(float groundAngle)
        {
            return Sin(groundAngle * Deg2Rad) * SlopeDistance;
        }
        
        private Vector2 DeltaMoveOnClimbSlope(float groundAngle)
        {
            return new Vector2(DeltaMoveXOnSlope(groundAngle), DeltaMoveYOnSlope(groundAngle));
        }

        private Vector2 DeltaMoveOnDescendSlope(float groundAngle)
        {
            return new Vector2(DeltaMoveXOnSlope(groundAngle), -DeltaMoveYOnSlope(groundAngle));
        }
        private void StopForcesY()
        {
            SetSpeedY(0);
            SetExternalForceY(0);
        }

        private void SetSpeedY(float y)
        {
            speed = Speed;
            speed.y = y;
            Speed = speed;
        }

        private void SetExternalForceY(float y)
        {
            externalForce = ExternalForce;
            externalForce.y = y;
            ExternalForce = externalForce;
        }*/

#endregion