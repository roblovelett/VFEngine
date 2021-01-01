using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer
{
    //using static Mathf;
    using static Vector2;

    public class PlatformerModel
    {
        #region fields

        private readonly RaycastController _raycastController;
        private readonly LayerMaskController _layerMaskController;
        private readonly PhysicsController _physicsController;

        #region internal

        private PlatformerData Platformer { get; }
        private RaycastData Raycast => _raycastController.Data;
        private RaycastCollision Collision => Raycast.Collision;

        //private RaycastBounds Bounds => Raycast.Bounds;
        //private LayerMaskData LayerMask => _layerMaskController.Data;
        private PhysicsData Physics => _physicsController.Data;
        private int VerticalRays => Raycast.VerticalRays;

        #endregion

        #region private methods

        private void RunInternal()
        {
            InitializeFrame();
            SetGroundCollision();
            SetForces();
            SetSlopeBehavior();
            SetHorizontalCollision();
            SetVerticalCollision();
            OnSlopeChange();
        }

        private void InitializeFrame()
        {
            _raycastController.OnPlatformerInitializeFrame();
            _layerMaskController.OnPlatformerInitializeFrame();
            _physicsController.OnPlatformerInitializeFrame();
        }

        private RaycastHit2D Hit => Raycast.Hit;
        private float IgnorePlatformsTime => Platformer.IgnorePlatformsTime;
        private bool SetDownHitAtOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private bool DownHitMissed => Hit.distance <= 0;
        private bool CastNextRayDown => !Hit;

        private void SetGroundCollision()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                Platformer.SetIndex(i);
                _raycastController.OnPlatformerCastRaysDown();
                if (SetDownHitAtOneWayPlatform)
                {
                    _raycastController.OnPlatformerSetDownHitAtOneWayPlatform();
                    if (DownHitMissed) continue;
                }

                if (CastNextRayDown) continue;
                _raycastController.OnPlatformerDownHit();
                break;
            }
        }

        private bool IgnoreFriction => Physics.IgnoreFriction;
        private bool OnSlope => Raycast.Collision.OnSlope;
        private float GroundAngle => Raycast.Collision.GroundAngle;
        private float MaximumSlopeAngle => Physics.MaximumSlopeAngle;
        private bool FailedMaximumSlopeAngle => GroundAngle > MaximumSlopeAngle;
        private bool ExceededMaximumSlopeAngle => OnSlope && FailedMaximumSlopeAngle;
        private float MinimumWallAngle => Physics.MinimumWallAngle;
        private bool OnAngle => GroundAngle < MinimumWallAngle;
        private Vector2 Speed => Physics.Speed;
        private float HorizontalSpeed => Speed.x;
        private bool NoHorizontalSpeed => HorizontalSpeed == 0;
        private bool ApplyForcesToExternal => ExceededMaximumSlopeAngle && (OnAngle || NoHorizontalSpeed);
        private bool CannotSetForces => IgnoreFriction;

        private void SetForces()
        {
            if (CannotSetForces) return;
            _physicsController.OnPlatformerSetExternalForce();
            _physicsController.OnPlatformerApplyGravity();
            if (ApplyForcesToExternal) _physicsController.OnPlatformerApplyForcesToExternal();
        }

        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private Vector2 Movement => Physics.Movement;
        private float MovementX => Movement.x;
        private bool HorizontalMovement => MovementX != 0;
        private float VerticalMovement => Movement.y;
        private bool NoVerticalMovement => VerticalMovement == 0;
        private bool NegativeVerticalMovement => VerticalMovement < 0;
        private bool NegativeOrNoVerticalMovement => NoVerticalMovement || NegativeVerticalMovement;
        private int GroundDirection => Collision.GroundDirection;
        private bool MovementIsGroundDirection => GroundDirection == HorizontalMovementDirection;
        private bool DescendSlope => NegativeOrNoVerticalMovement && OnSlope && MovementIsGroundDirection;

        //private float DistanceX => Abs(Movement.x);
        private bool ClimbSlope => OnAngle;
        private bool CannotSetSlopeBehavior => !HorizontalMovement;

        private void SetSlopeBehavior()
        {
            if (CannotSetSlopeBehavior) return;
            if (DescendSlope) OnDescendSlope();
            else if (ClimbSlope) OnClimbSlope();
        }

        private void OnDescendSlope()
        {
            _physicsController.OnPlatformerDescendSlope();
            _raycastController.OnPlatformerSlopeBehavior();
        }

        private void OnClimbSlope()
        {
            _physicsController.OnPlatformerClimbSlope();
            _raycastController.OnPlatformerSlopeBehavior();
        }

        private bool CastToSides => HorizontalMovement;
        private int HorizontalRays => Raycast.HorizontalRays;
        private int Index => Platformer.Index;
        private float SideAngle => Angle(Hit.normal, up);
        private bool OnSideAngle => SideAngle < MinimumWallAngle;
        private bool FirstSideHit => Index == 0;
        private bool FirstSideHitOnAngle => FirstSideHit && !OnSlope && OnSideAngle;
        private bool SideHitMissed => !Hit;
        private bool CastNextRayToSides => FirstSideHit && OnSlope || !FailedMaximumSlopeAngle || OnSideAngle;
        private bool SetVerticalMovement => OnSlope && OnAngle;
        private bool MetMinimumWallAngle => GroundAngle >= MinimumWallAngle;
        private bool GroundNotHorizontalMovementDirection => GroundDirection != HorizontalMovementDirection;
        private float VerticalSpeed => Speed.y;
        private bool NegativeVerticalSpeed => VerticalSpeed < 0;

        private bool StopHorizontalSpeedAndSetHit => OnSlope && MetMinimumWallAngle &&
                                                     GroundNotHorizontalMovementDirection && NegativeVerticalSpeed;

        private void SetHorizontalCollision()
        {
            if (CastToSides)
            {
                _raycastController.OnPlatformerInitializeLengthForSideRay();
                for (var i = 0; i < HorizontalRays; i++)
                {
                    Platformer.SetIndex(i);
                    _raycastController.OnPlatformerCastRaysToSides();
                    if (SideHitMissed) continue;
                    if (FirstSideHitOnAngle)
                    {
                        _raycastController.OnPlatformerOnFirstSideHit();
                        _physicsController.OnPlatformerOnFirstSideHit();
                        _raycastController.OnPlatformerSetLengthForSideRay();
                    }

                    if (CastNextRayToSides) continue;
                    _physicsController.OnPlatformerOnSideHit();
                    _raycastController.OnPlatformerSetLengthForSideRay();
                    if (SetVerticalMovement)
                    {
                        if (NegativeVerticalMovement) _physicsController.OnPlatformerStopVerticalMovement();
                        else _physicsController.OnPlatformerAdjustVerticalMovementToSlope();
                    }

                    _raycastController.OnPlatformerHitWall();
                    _physicsController.OnPlatformerHitWall();
                }
            }

            if (!StopHorizontalSpeedAndSetHit) return;
            _raycastController.OnPlatformerOnStopHorizontalSpeedHit();
            _physicsController.OnPlatformerStopHorizontalSpeed();
        }

        private bool PositiveVerticalMovement => VerticalMovement > 0;
        private bool NoHorizontalMovement => MovementX == 0;
        private bool InAir => !OnSlope || NoHorizontalMovement;
        private bool CastVertically => PositiveVerticalMovement || NegativeVerticalMovement && InAir;
        private bool VerticalHitMissed => !Hit;
        private bool SetVerticalHitAtOneWayPlatform => NegativeVerticalMovement && VerticalHitMissed;
        private bool ApplySlopeBehaviorToPhysics => OnSlope && PositiveVerticalMovement;

        private void SetVerticalCollision()
        {
            if (!CastVertically) return;
            _raycastController.OnPlatformerInitializeLengthForVerticalRay();
            for (var i = 0; i < VerticalRays; i++)
            {
                Platformer.SetIndex(i);
                _raycastController.OnPlatformerCastRaysVertically();
                if (SetVerticalHitAtOneWayPlatform) _raycastController.OnPlatformerSetVerticalHitAtOneWayPlatform();
                if (VerticalHitMissed) continue;
                _physicsController.OnPlatformerVerticalHit();
                _raycastController.OnPlatformerVerticalHit();
                if (ApplySlopeBehaviorToPhysics) _physicsController.OnPlatformerApplyGroundAngle();
            }
        }

        private bool OnGround => Collision.OnGround;
        private bool NoVerticalSpeed => VerticalSpeed == 0; 
        private bool NegativeOrNoVerticalSpeed => NoVerticalSpeed || NegativeVerticalSpeed;
        private bool SlopeChange => OnGround && HorizontalMovement && NegativeOrNoVerticalSpeed;
        private bool ClimbingSlope => PositiveVerticalMovement;
        private void OnSlopeChange()
        {
            if (!SlopeChange) return;
            if (ClimbingSlope)
            {
                ClimbSteepSlope();
            }
        }

        private void ClimbSteepSlope()
        {
            _raycastController.OnPlatformerClimbSteepSlope();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerData Data => Platformer;

        #region public methods

        public PlatformerModel(PlatformerSettings settings, RaycastController raycast, LayerMaskController layerMask,
            PhysicsController physics)
        {
            Platformer = new PlatformerData(settings);
            _raycastController = raycast;
            _layerMaskController = layerMask;
            _physicsController = physics;
        }

        public void Run()
        {
            RunInternal();
        }

        #endregion

        #endregion
    }
}