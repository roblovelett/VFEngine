using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

// ReSharper disable UnusedMember.Local
namespace VFEngine.Platformer
{
    using static Mathf;
    using static Vector2;
    public class PlatformerModel
    {
        #region fields

        private readonly RaycastController raycastController;
        private readonly LayerMaskController layerMaskController;
        private readonly PhysicsController physicsController;

        #region internal

        private PlatformerData Platformer { get; }
        private RaycastData Raycast => raycastController.Data;
        private RaycastCollision Collision => Raycast.Collision;
        private RaycastBounds Bounds => Raycast.Bounds;
        private LayerMaskData LayerMask => layerMaskController.Data;
        private PhysicsData Physics => physicsController.Data;
        private int VerticalRays => Raycast.VerticalRays;

        #endregion

        #region private methods

        private void RunInternal()
        {
            InitializeFrame();
            CastRaysDown();
            SetForces();
            SetSlopeBehavior();
        }

        private void InitializeFrame()
        {
            raycastController.OnPlatformerInitializeFrame();
            layerMaskController.OnPlatformerInitializeFrame();
            physicsController.OnPlatformerInitializeFrame();
        }

        private RaycastHit2D Hit => Raycast.Hit;
        private float IgnorePlatformsTime => Platformer.IgnorePlatformsTime;
        private bool SetHitAtOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private bool HitMissed => Hit.distance <= 0;

        private void CastRaysDown()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                Platformer.SetIndex(i);
                raycastController.OnPlatformerCastRaysDown();
                if (SetHitAtOneWayPlatform)
                {
                    raycastController.OnPlatformerSetDownHitAtOneWayPlatform();
                    if (HitMissed) continue;
                }

                if (!Hit) continue;
                raycastController.OnPlatformerDownHit();
                break;
            }
        }

        private bool IgnoreFriction => Physics.IgnoreFriction;
        private bool OnSlope => Raycast.Collision.OnSlope;
        private float GroundAngle => Raycast.Collision.GroundAngle;
        private float MaximumSlopeAngle => Physics.MaximumSlopeAngle;
        private bool ExceededMaximumSlopeAngle => OnSlope && GroundAngle > MaximumSlopeAngle;
        private float MinimumWallAngle => Physics.MinimumWallAngle;
        private bool OnAngle => GroundAngle < MinimumWallAngle;
        private Vector2 Speed => Physics.Speed;
        private bool NoHorizontalSpeed => Speed.x == 0;
        private bool ApplyForcesToExternal => ExceededMaximumSlopeAngle && (OnAngle || NoHorizontalSpeed);
        private void SetForces()
        {
            if (IgnoreFriction) return;
            physicsController.OnPlatformerSetExternalForce();
            physicsController.OnPlatformerApplyGravity();
            if (ApplyForcesToExternal) physicsController.OnPlatformerApplyForcesToExternal();
        }

        private int HorizontalMovementDirection => Physics.MovementDirection;
        private Vector2 DeltaMovement => Physics.Movement;
        private float MovementX => DeltaMovement.x;
        private bool HasHorizontalMovement => MovementX != 0;
        private float MovementY => DeltaMovement.y;
        private bool NegativeVerticalMovement => MovementY <= 0;
        private int GroundDirection => Collision.GroundDirection;
        private bool MovementIsGroundDirection => GroundDirection == HorizontalMovementDirection;
        private bool DescendSlope => NegativeVerticalMovement && OnSlope && MovementIsGroundDirection;
        private float DistanceX => Abs(DeltaMovement.x);
        private bool ClimbSlope => OnAngle;
        
        private void SetSlopeBehavior()
        {
            if (HasHorizontalMovement)
            {
                if (DescendSlope)
                {
                    physicsController.OnPlatformerDescendSlope();
                    raycastController.OnPlatformerSlopeBehavior();
                }
                else if (ClimbSlope)
                {
                    physicsController.OnPlatformerClimbSlope();
                    raycastController.OnPlatformerSlopeBehavior();
                }
                SetHorizontalCollisions();
            }

            SetVerticalCollisions();
        }
        
        private int HorizontalRays => Raycast.HorizontalRays;
        private int Index => Platformer.Index;
        private float SideAngle => Angle(Hit.normal, up);
        private bool OnSideAngle => SideAngle < MinimumWallAngle;
        private bool FirstHit => Index == 0;
        private bool FirstHitOnAngle => FirstHit && !OnSlope && OnSideAngle;
        private void SetHorizontalCollisions()
        {
            raycastController.OnPlatformerInitializeRayLength();
            for (var i = 0; i < HorizontalRays; i++)
            {
                Platformer.SetIndex(i);
                raycastController.OnPlatformerCastRaysToSides();
                if (Hit)
                {
                    if (FirstHitOnAngle)
                    {
                        raycastController.OnPlatformerOnFirstHitOnAngle();
                        //physicsController.OnPlatformerOnFirstHitOnAngle();
                        raycastController.OnPlatformerSetRayLength();
                    }
                }
            }
        }
        private void SetVerticalCollisions(){}

        #endregion

        #endregion

        #region properties

        public PlatformerData Data => Platformer;

        #region public methods

        public PlatformerModel(PlatformerSettings settings, RaycastController raycast,
            LayerMaskController layerMask, PhysicsController physics)
        {
            Platformer = new PlatformerData(settings);
            raycastController = raycast;
            layerMaskController = layerMask;
            physicsController = physics;
        }

        public void Run()
        {
            RunInternal();
        }

        #endregion

        #endregion
    }
}