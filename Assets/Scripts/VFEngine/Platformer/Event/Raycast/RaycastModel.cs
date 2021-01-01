using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static Physics2D;
    using static Mathf;

    public class RaycastModel
    {
        #region fields

        #region internal

        #endregion

        #region private methods

        #region initialization

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => Raycast;
        private PhysicsData Physics { get; }
        private LayerMaskData LayerMask { get; }
        private PlatformerData Platformer { get; }

        #region public methods

        #region constructors

        public RaycastModel(ref Collider2D collider, ref RaycastSettings settings, ref LayerMaskData layerMask,
            ref PhysicsData physics, ref PlatformerData platformer)
        {
            Raycast = new RaycastData(settings, collider);
            LayerMask = layerMask;
            Physics = physics;
            Platformer = platformer;
        }

        #endregion

        private RaycastData Raycast { get; }

        public void OnInitializeFrame()
        {
            ResetCollision();
            SetBounds();
        }
        public void ResetCollision()
        {
            Raycast.ResetCollision();
        }

        private void SetBounds()
        {
            Raycast.SetBounds();
        }

        private RaycastBounds Bounds => Raycast.Bounds;
        private RaycastCollision Collision => Raycast.Collision;
        
        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private bool MovingRight => HorizontalMovementDirection == 1;
        private Vector2 BottomLeft => Bounds.BottomLeft;
        private Vector2 BottomRight => Bounds.BottomRight;
        private float VerticalSpacing => Raycast.VerticalSpacing;
        private int Index => Platformer.Index;
        private float SkinWidth => Raycast.SkinWidth;

        private Vector2 DownOrigin
        {
            get
            {
                var origin = (MovingRight ? BottomLeft : BottomRight) +
                             (MovingRight ? right : left) * (VerticalSpacing * Index);
                origin.y += SkinWidth * 2;
                return origin;
            }
        }

        private LayerMask CollisionLayer => LayerMask.Collision;
        private static Vector2 DownDirection => down;
        private float DownDistance => SkinWidth * 4;
        private LayerMask DownLayer => CollisionLayer;
        private RaycastHit2D DownHit => Raycast(DownOrigin, DownDirection, DownDistance, DownLayer);

        public void OnCastRaysDown()
        {
            SetDownOrigin();
            SetDownHit();
        }
        private void SetDownOrigin()
        {
            Raycast.SetOrigin(DownOrigin);
        }

        private void SetDownHit()
        {
            Raycast.SetHit(DownHit);
        }

        private LayerMask OneWayPlatform => LayerMask.OneWayPlatform;
        private LayerMask DownLayerOneWayPlatform => OneWayPlatform;

        private RaycastHit2D DownHitAtOneWayPlatform =>
            Raycast(DownOrigin, DownDirection, DownDistance, DownLayerOneWayPlatform);

        public void SetDownHitAtOneWayPlatform()
        {
            Raycast.SetHit(DownHitAtOneWayPlatform);
        }

        public void OnDownHit()
        {
            SetCollisionOnDownHit();
        }

        private void SetCollisionOnDownHit()
        {
            Collision.OnDownHit(Hit);
        }

        public void OnSlopeBehavior()
        {
            SetCollisionBelow();
        }

        private void SetCollisionBelow()
        {
            Collision.SetCollisionBelow(true);
        }

        private float HorizontalMovement => Physics.Movement.x;
        private float InitialSideLength => Abs(HorizontalMovement) + SkinWidth;
        private float Length => Raycast.Length;

        public void InitializeLengthForSideRay()
        {
            Raycast.SetLength(InitialSideLength);
        }

        private bool MovingLeft => HorizontalMovementDirection == -1;
        private float HorizontalSpacing => Raycast.HorizontalSpacing;
        private Vector2 SideDirection => right * HorizontalMovementDirection;
        private LayerMask SideLayer => CollisionLayer;
        private float SideDistance => Length;
        private Vector2 InitialSideOrigin => (MovingLeft ? BottomLeft : BottomRight) + up;
        private Vector2 SideOrigin => InitialSideOrigin * HorizontalSpacing * Index;
        private RaycastHit2D SideHit => Raycast(SideOrigin, SideDirection, SideDistance, SideLayer);
        private RaycastHit2D Hit => Raycast.Hit;
        private float HitDistance => Hit.distance;

        public void OnCastRaysToSides()
        {
            SetSideOrigin();
            SetSideHit();
        }
        private void SetSideOrigin()
        {
            Raycast.SetOrigin(SideOrigin);
        }

        private void SetSideHit()
        {
            Raycast.SetHit(SideHit);
        }

        private float MinimumSideLength => Min(InitialSideLength, HitDistance);

        public void OnFirstSideHit()
        {
            SetCollisionOnSideHit();
        }

        private void SetCollisionOnSideHit()
        {
            Collision.OnSideHit(Hit);
        }

        public void SetLengthForSideRay()
        {
            Raycast.SetLength(MinimumSideLength);
        }

        public void OnHitWall()
        {
            Collision.OnHitWall(HorizontalMovementDirection, Hit);
        }

        private int GroundDirection => Collision.GroundDirection;
        private Vector2 StopHorizontalSpeedOrigin => BottomRight;
        private Vector2 StopHorizontalSpeedDirection => left * GroundDirection;
        private static float StopHorizontalSpeedDistance => 1;
        private LayerMask StopHorizontalSpeedLayer => CollisionLayer;
        private RaycastHit2D StopHorizontalSpeedHit => Raycast(StopHorizontalSpeedOrigin, StopHorizontalSpeedDirection, StopHorizontalSpeedDistance, StopHorizontalSpeedLayer);
        
        public void OnStopHorizontalSpeedHit()
        {
            Collision.SetHorizontalHit(StopHorizontalSpeedHit);
        }

        private int VerticalMovementDirection => Physics.VerticalMovementDirection;
        private float InitialVerticalLength => VerticalMovementDirection + SkinWidth;
        public void InitializeLengthForVerticalRay()
        {
            Raycast.SetLength(InitialVerticalLength);
        }

        private bool MovingDown => VerticalMovementDirection == -1;
        private Vector2 TopLeft => Bounds.TopLeft;

        private Vector2 VerticalOrigin =>
            (MovingDown ? BottomLeft : TopLeft) + right * (VerticalSpacing * Index * HorizontalMovement);

        public void OnCastRaysVertically()
        {
            SetVerticalOrigin();
            SetVerticalHit();
        }
        private void SetVerticalOrigin()
        {
            Raycast.SetOrigin(VerticalOrigin);
        }

        private Vector2 VerticalDirection => up * VerticalMovementDirection;
        private float VerticalDistance => Length;
        private LayerMask VerticalLayer => CollisionLayer;
        private RaycastHit2D VerticalHit => Raycast(VerticalOrigin, VerticalDirection, VerticalDistance, VerticalLayer);
        
        private void SetVerticalHit()
        {
            Raycast.SetHit(VerticalHit);
        }

        private float VerticalRayLength => HitDistance;

        public void OnVerticalHit()
        {
            SetLengthForVerticalRay();
            SetCollisionOnVerticalHit();
        }
        private void SetLengthForVerticalRay()
        {
            Raycast.SetLength(VerticalRayLength);
        }

        private RaycastHit2D VerticalHitAtOneWayPlatform => Raycast(VerticalOrigin, VerticalDirection, VerticalDistance, OneWayPlatform);
        public void SetVerticalHitAtOneWayPlatform()
        {
            Raycast.SetHit(VerticalHitAtOneWayPlatform);
        }

        private void SetCollisionOnVerticalHit()
        {
            Collision.OnVerticalHit(VerticalMovementDirection, Hit);
        }

        public void OnClimbSteepSlope()
        {
            SetLengthForSteepSlope();
            SetOriginForSteepSlope();
            SetHitForSteepSlope();
        }
        private float SteepSlopeLength => InitialSideLength * 2;
        private void SetLengthForSteepSlope()
        {
            Raycast.SetLength(SteepSlopeLength);
        }

        private Vector2 SteepSlopeOrigin => InitialSideOrigin * VerticalMovementDirection;
        private void SetOriginForSteepSlope()
        {
            Raycast.SetOrigin(SteepSlopeOrigin);
        }

        private Vector2 SteepSlopeDirection => SideDirection;
        private LayerMask SteepSlopeLayer => CollisionLayer;
        private RaycastHit2D SteepSlopeHit => Raycast(SteepSlopeOrigin, SteepSlopeDirection, SteepSlopeLength, SteepSlopeLayer);
        private void SetHitForSteepSlope()
        {
            Raycast.SetHit(SteepSlopeHit);
        }
        
        #endregion

        #endregion
    }
}