using UnityEngine;
using VFEngine.Tools;
// ReSharper disable InvertIf

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
        public float DeltaMoveDistanceY => Abs(DeltaMove.y);

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

        private void VerticalCollision(float hitDistance, float skinWidth)
        {
            SetDeltaMoveY(VerticalCollisionDeltaMoveY(hitDistance, skinWidth));
        }

        private float VerticalCollisionDeltaMoveY(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveYDirectionAxis;
        }

        private void VerticalCollisionHitClimbingSlope(float groundAngle)
        {
            SetDeltaMoveX(VerticalCollisionHitClimbingSlopeDeltaMoveX(groundAngle));
            StopForcesX();
        }

        private float VerticalCollisionHitClimbingSlopeDeltaMoveX(float groundAngle)
        {
            return DeltaMove.y / Tan(groundAngle * Deg2Rad) * DeltaMoveXDirectionAxis;
        }

        private float ClimbSteepSlopeDeltaMoveX(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void ClimbSteepSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveX(ClimbSteepSlopeDeltaMoveX(hitDistance, skinWidth));
        }

        private void ClimbMildSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ApplyToDeltaMove(EdgeCaseDeltaMove(false, groundAngle, hitDistance, hitAngle, skinWidth));
        }

        private void ApplyToDeltaMove(Vector2 force)
        {
            deltaMove = DeltaMove;
            deltaMove += force;
            SetDeltaMove(deltaMove);
        }

        private void DescendMildSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveY(DescendMildSlopeDeltaMoveY(hitDistance, skinWidth));
        }

        private static float DescendMildSlopeDeltaMoveY(float hitDistance, float skinWidth)
        {
            return -(hitDistance - skinWidth);
        }
        private void DescendSteepSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ApplyToDeltaMove(EdgeCaseDeltaMove(true, groundAngle, hitDistance, hitAngle, skinWidth));
        }
        private Vector2 EdgeCaseDeltaMove(bool descending, float groundAngle, float hitDistance, float hitAngle, float skinWidth)
        {
            float overshoot;
            
            var groundAngleSin = Sin(groundAngle * Deg2Rad);
            var hitAngleTan = Tan(hitAngle * Deg2Rad);
            
            if (descending)
            {
                overshoot = groundAngle > 0
                    ? OvershootDescendingSteepSlope(groundAngle, hitAngleTan, hitDistance, groundAngleSin)
                    : OvershootDefault(hitDistance, hitAngleTan);
            }
            else
            {
                overshoot = hitAngle > 0 ? OvershootClimbingMildSlope(groundAngle, hitAngleTan, hitDistance, groundAngleSin) 
                    : OvershootDefault(hitDistance, groundAngleSin);
            }

            var removeX = RemoveX(groundAngle, overshoot);
            var removeY = RemoveY(groundAngleSin, overshoot);
            var addX = AddX(hitAngle, overshoot);
            var addY = AddY(hitAngle, overshoot);

            if (descending)
            {
                removeY = -removeY;
                addY = -addY;
                skinWidth = -skinWidth;
            }
            
            return new Vector2(addX - removeX, addY - removeY + skinWidth);

            static float OvershootDescendingSteepSlope(float angle, float angleTan, float distance,
                float angleSin)
            {
                var cos = Cos(angle * Deg2Rad);
                return (distance * cos) / (angleTan / cos - angleSin);
            }

            static float OvershootClimbingMildSlope(float angle, float angleTan, float distance,
                float angleSin)
            {
                var tan = Tan(angle * Deg2Rad);
                return (2 * angleTan * distance - tan * distance) /
                       (angleTan * angleSin - tan * angleSin);
            }

            static float OvershootDefault(float distance, float angle)
            {
                return distance / angle;
            }
            
            float RemoveX(float angle, float err)
            {
                return Cos(angle * Deg2Rad) * err * DeltaMoveXDirectionAxis;
            }

            static float RemoveY(float angle, float err)
            {
                return angle * err;
            }

            float AddX(float angle, float err)
            {
                return Cos(angle * Deg2Rad) * err * DeltaMoveXDirectionAxis;
            }

            static float AddY(float angle, float err)
            {
                return Sin(angle * Deg2Rad) * err;
            }
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

        public void OnVerticalCollision(float hitDistance, float skinWidth)
        {
            VerticalCollision(hitDistance, skinWidth);
        }

        public void OnVerticalCollisionHitClimbingSlope(float groundAngle)
        {
            VerticalCollisionHitClimbingSlope(groundAngle);
        }

        public void OnClimbSteepSlope(float hitDistance, float skinWidth)
        {
            ClimbSteepSlope(hitDistance, skinWidth);
        }

        public void OnClimbMildSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ClimbMildSlope(hitAngle, groundAngle, hitDistance, skinWidth);
        }

        public void OnDescendMildSlope(float hitDistance, float skinWidth)
        {
            DescendMildSlope(hitDistance, skinWidth);
        }

        public void OnDescendSteepSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            DescendSteepSlope(hitAngle, groundAngle, hitDistance, skinWidth);
        }

        #endregion
    }
}