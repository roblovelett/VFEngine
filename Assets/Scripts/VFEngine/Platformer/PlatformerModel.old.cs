/*
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools.BetterEvent;

namespace VFEngine.Platformer
{
    using static Mathf;
    using static Vector2;

    public class PlatformerModel
    {
        #region fields

        #region internal

        private PlatformerData Platformer { get; set; }
        private RaycastController Raycast { get; set; }
        private PhysicsController Physics { get; set; }
        private LayerMaskController LayerMask { get; set; }
        //private RaycastData RaycastData => Raycast.Data;
        private PhysicsData PhysicsData => Physics.Data;

        #endregion
        
        #endregion
        */

        //#region private methods

        /*private void RunInternal()
        {
            InitializeFrame();
            SetGroundCollision();
            SetForces();
            SetSlopeBehavior();
            SetHorizontalCollision();
            SetVerticalCollision();
            OnSlopeChange();
            CastRayFromInitialPosition();
            TranslateMovement();
            OnCeilingOrGroundCollision();
            SetLayerMaskToSaved();
            ResetFriction();
        }*/

        /*private void InitializeFrame()
        {
            //platformerInitializeFrame.Invoke();
            Raycast.OnPlatformerInitializeFrame();
            LayerMask.OnPlatformerInitializeFrame();
            Physics.OnPlatformerInitializeFrame();
            //Platformer.OnInitializeFrame();
        }*/

        /*private int VerticalRays => RaycastData.VerticalRays;
        private RaycastHit2D Hit => RaycastData.Hit;
        private float IgnorePlatformsTime => Platformer.IgnorePlatformsTime;
        private bool SetDownHitAtOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private bool DownHitMissed => Hit.distance <= 0;
        private bool CastNextRayDown => !Hit;

        private void SetGroundCollision()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                Platformer.SetIndex(i);
                Raycast.OnPlatformerCastRaysDown();
                if (SetDownHitAtOneWayPlatform)
                {
                    Raycast.OnPlatformerSetDownHitAtOneWayPlatform();
                    if (DownHitMissed) continue;
                }

                if (CastNextRayDown) continue;
                Raycast.OnPlatformerDownHit();
                break;
            }
        }

        private bool IgnoreFriction => PhysicsData.IgnoreFriction;
        private bool OnSlope => RaycastData.Collision.OnSlope;
        private float GroundAngle => RaycastData.Collision.GroundAngle;
        private float MaximumSlopeAngle => PhysicsData.MaximumSlopeAngle;
        private bool FailedMaximumSlopeAngle => GroundAngle > MaximumSlopeAngle;
        private bool ExceededMaximumSlopeAngle => OnSlope && FailedMaximumSlopeAngle;
        private float MinimumWallAngle => PhysicsData.MinimumWallAngle;
        private bool OnAngle => GroundAngle < MinimumWallAngle;
        private Vector2 Speed => PhysicsData.Speed;
        private float HorizontalSpeed => Speed.x;
        private bool NoHorizontalSpeed => HorizontalSpeed == 0;
        private bool ApplyForcesToExternal => ExceededMaximumSlopeAngle && (OnAngle || NoHorizontalSpeed);
        private bool CannotSetForces => IgnoreFriction;

        private void SetForces()
        {
            if (CannotSetForces) return;
            Physics.OnPlatformerMoveExternalForce();
            Physics.OnPlatformerApplyGravity();
            if (ApplyForcesToExternal) Physics.OnPlatformerApplyForcesToExternal();
        }

        private int HorizontalMovementDirection => PhysicsData.HorizontalMovementDirection;
        private Vector2 Movement => PhysicsData.Movement;
        private float HorizontalMovement => Movement.x;
        private bool HasHorizontalMovement => HorizontalMovement != 0;
        private float VerticalMovement => Movement.y;
        private bool NoVerticalMovement => VerticalMovement == 0;
        private bool NegativeVerticalMovement => VerticalMovement < 0;
        private bool NegativeOrNoVerticalMovement => NoVerticalMovement || NegativeVerticalMovement;
        private int GroundDirection => Collision.GroundDirection;
        private bool MovementIsGroundDirection => GroundDirection == HorizontalMovementDirection;
        private bool DescendSlope => NegativeOrNoVerticalMovement && OnSlope && MovementIsGroundDirection;
        private bool ClimbSlope => OnAngle;
        private bool CannotSetSlopeBehavior => !HasHorizontalMovement;

        private void SetSlopeBehavior()
        {
            if (CannotSetSlopeBehavior) return;
            if (DescendSlope) OnDescendSlope();
            else if (ClimbSlope) OnClimbSlope();
        }

        private void OnDescendSlope()
        {
            Physics.OnPlatformerDescendSlope();
            Raycast.OnPlatformerSlopeBehavior();
        }

        private void OnClimbSlope()
        {
            Physics.OnPlatformerClimbSlope();
            Raycast.OnPlatformerSlopeBehavior();
        }

        private bool CastToSides => HasHorizontalMovement;
        private int HorizontalRays => RaycastData.HorizontalRays;
        private int Index => Platformer.Index;
        private float SideAngle => Angle(Hit.normal, up);
        private bool OnSideAngle => SideAngle < MinimumWallAngle;
        private bool FirstSideRay => Index == 0;
        private bool FirstSideHitOnAngle => FirstSideRay && !OnSlope && OnSideAngle;
        private bool SideHitMissed => !Hit;
        private bool CastNextRayToSides => FirstSideRay && OnSlope || !FailedMaximumSlopeAngle || OnSideAngle;
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
                Raycast.OnPlatformerInitializeLengthForSideRay();
                for (var i = 0; i < HorizontalRays; i++)
                {
                    Platformer.SetIndex(i);
                    Raycast.OnPlatformerCastRaysToSides();
                    if (SideHitMissed) continue;
                    if (FirstSideHitOnAngle)
                    {
                        Raycast.OnPlatformerOnFirstSideHit();
                        Physics.OnPlatformerOnFirstSideHit();
                        Raycast.OnPlatformerSetLengthForSideRay();
                    }

                    if (CastNextRayToSides) continue;
                    Physics.OnPlatformerOnSideHit();
                    Raycast.OnPlatformerSetLengthForSideRay();
                    if (SetVerticalMovement)
                    {
                        if (NegativeVerticalMovement) Physics.OnPlatformerStopVerticalMovement();
                        else Physics.OnPlatformerAdjustVerticalMovementToSlope();
                    }

                    Raycast.OnPlatformerHitWall();
                    Physics.OnPlatformerHitWall();
                }
            }

            if (!StopHorizontalSpeedAndSetHit) return;
            Raycast.OnPlatformerOnStopHorizontalSpeedHit();
            Physics.OnPlatformerStopHorizontalSpeed();
        }

        private bool PositiveVerticalMovement => VerticalMovement > 0;
        private bool NoHorizontalMovement => HorizontalMovement == 0;
        private bool InAir => !OnSlope || NoHorizontalMovement;
        private bool CastVertically => PositiveVerticalMovement || NegativeVerticalMovement && InAir;
        private bool VerticalHitMissed => !Hit;
        private bool SetVerticalHitAtOneWayPlatform => NegativeVerticalMovement && VerticalHitMissed;
        private bool ApplySlopeBehaviorToPhysics => OnSlope && PositiveVerticalMovement;

        private void SetVerticalCollision()
        {
            if (!CastVertically) return;
            Raycast.OnPlatformerInitializeLengthForVerticalRay();
            for (var i = 0; i < VerticalRays; i++)
            {
                Platformer.SetIndex(i);
                Raycast.OnPlatformerCastRaysVertically();
                if (SetVerticalHitAtOneWayPlatform) Raycast.OnPlatformerSetVerticalHitAtOneWayPlatform();
                if (VerticalHitMissed) continue;
                Physics.OnPlatformerVerticalHit();
                Raycast.OnPlatformerVerticalHit();
                if (ApplySlopeBehaviorToPhysics) Physics.OnPlatformerApplyGroundAngle();
            }
        }

        private bool OnGround => Collision.OnGround;
        private bool NoVerticalSpeed => VerticalSpeed == 0;
        private bool NegativeOrNoVerticalSpeed => NoVerticalSpeed || NegativeVerticalSpeed;
        private bool SlopeChange => OnGround && HasHorizontalMovement && NegativeOrNoVerticalSpeed;
        private bool PositiveSlopeChange => PositiveVerticalMovement;

        private void OnSlopeChange()
        {
            if (!SlopeChange) return;
            if (PositiveSlopeChange) OnPositiveSlopeChange();
            else OnNegativeSlopeChange();
        }

        private bool SteepSlopeHit => Hit;
        private float SteepSlopeAngle => SideAngle;
        private float Tolerance => Platformer.Tolerance;
        private bool SteepSlopeNotGroundAngle => Abs(SteepSlopeAngle - GroundAngle) > Tolerance;
        private bool ClimbSteepSlope => SteepSlopeHit && SteepSlopeNotGroundAngle;
        private bool MildSlopeHit => Hit;
        private LayerMask GroundLayer => Collision.GroundLayer;
        private LayerMask HitLayer => Hit.collider.gameObject.layer;
        private LayerMask MildSlopeHitLayer => HitLayer;
        private bool MildSlopeHitLayerIsGround => MildSlopeHitLayer == GroundLayer;
        private bool ClimbMildSlope => MildSlopeHit && MildSlopeHitLayerIsGround;

        private void OnPositiveSlopeChange()
        {
            Raycast.OnPlatformerInitializeClimbSteepSlope();
            if (ClimbSteepSlope)
            {
                OnClimbSteepSlope();
            }
            else
            {
                Raycast.OnPlatformerInitializeClimbMildSlope();
                if (ClimbMildSlope) Physics.OnPlatformerClimbMildSlope();
            }
        }

        private void OnClimbSteepSlope()
        {
            Physics.OnPlatformerClimbSteepSlope();
            Raycast.OnPlatformerClimbSteepSlope();
        }

        private RaycastHit2D DescendMildSlopeHit => Hit;
        private float DescendMildSlopeAngle => SideAngle;
        private bool DescendMildSlope => DescendMildSlopeHit && DescendMildSlopeAngle < GroundAngle;
        private RaycastHit2D DescendSteepSlopeHit => Hit;

        private bool SteepSlopeHitIsMovementDirection =>
            (int) Sign(DescendSteepSlopeHit.normal.x) == HorizontalMovementDirection;

        private bool SteepSlopeHitLayerIsGround => DescendSteepSlopeHit.collider.gameObject.layer == GroundLayer;

        private bool DescendSteepSlope =>
            DescendSteepSlopeHit && SteepSlopeHitIsMovementDirection && SteepSlopeHitLayerIsGround;

        private void OnNegativeSlopeChange()
        {
            Raycast.OnPlatformerInitializeDescendMildSlope();
            if (DescendMildSlope)
            {
                OnDescendMildSlope();
            }
            else
            {
                Raycast.OnPlatformerInitializeDescendSteepSlope();
                if (DescendSteepSlope) Physics.OnPlatformerDescendSteepSlope();
            }
        }

        private void OnDescendMildSlope()
        {
            Physics.OnPlatformerDescendMildSlope();
            Raycast.OnPlatformerDescendMildSlope();
        }

        private void CastRayFromInitialPosition()
        {
            Raycast.OnPlatformerCastRayFromInitialPosition();
        }

        private void TranslateMovement()
        {
            Physics.OnPlatformerTranslateMovement();
        }

        private RaycastHit2D VerticalHit => Collision.VerticalHit;
        private bool CollidingBelow => Collision.Below;
        private Vector2 TotalSpeed => PhysicsData.TotalSpeed;
        private bool NegativeVerticalTotalSpeed => TotalSpeed.y < 0;
        private bool CollidingAbove => Collision.Above;
        private bool PositiveVerticalTotalSpeed => TotalSpeed.y > 0;
        private bool HitGround => CollidingBelow && NegativeVerticalTotalSpeed;
        private bool HitCeiling => CollidingAbove && PositiveVerticalTotalSpeed;
        private bool AboveOrBelowCollision => HitGround || HitCeiling;
        private bool OnAngleOrNotOnSlope => !OnSlope || OnAngle;
        private bool StopVerticalForces => VerticalHit && AboveOrBelowCollision && OnAngleOrNotOnSlope;

        private void OnCeilingOrGroundCollision()
        {
            if (!StopVerticalForces) return;
            Physics.OnPlatformerCeilingOrGroundCollision();
        }

        private void SetLayerMaskToSaved()
        {
            LayerMask.OnPlatformerSetLayerToSaved();
        }

        private void ResetFriction()
        {
            Physics.OnPlatformerResetFriction();
        }

        #endregion

        #endregion
        *//*

        #region properties

        public PlatformerData Data => Platformer;

        #region public methods

        #region constructor

        public PlatformerModel(){}
        public void ApplySettings(PlatformerSettings settings)
        {
            Platformer = new PlatformerData(settings);
        }

        public void SetDependencies(RaycastController raycast, LayerMaskController layerMask, PhysicsController physics)
        {
            Raycast = raycast;
            LayerMask = layerMask;
            Physics = physics;
        }

        #endregion

        public void InitializeFrame()
        {
            Platformer.Initialize();
        }

        #endregion

        #endregion*//*
    }
}*/