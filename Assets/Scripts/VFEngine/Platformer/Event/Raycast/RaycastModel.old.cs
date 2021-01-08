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
        
        private PhysicsData Physics { get; set; }
        private LayerMaskData LayerMask { get; set; }
        private PlatformerData Platformer { get; set; }

        #endregion

        #region private methods

        #region initialization

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data { get; } = new RaycastData();


        #region public methods

        #region constructor

        public RaycastModel()
        {
            
        }
        public RaycastModel(Collider2D collider, RaycastSettings settings)
        {
            //Raycast = new RaycastData(settings, collider);
        }

        #endregion

        public void ApplySettings(RaycastSettings settings)
        {
            
        }

        public void SetCollider(Collider2D collider)
        {
            
        }
        
        public void SetDependencies(LayerMaskData layerMask, PhysicsData physics, PlatformerData platformer)
        {
            LayerMask = layerMask;
            Physics = physics;
            Platformer = platformer;
        }
        
        public void InitializeFrame()
        {
            //Data.ResetCollision();
            //Data.SetBounds();
        }
        
        #endregion
        
        #endregion
        
        /*private RaycastBounds Bounds => Raycast.Bounds;
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

        private float HorizontalMovement => Physics.DeltaMove.x;
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
        private Vector2 SideBottomOrigin => MovingLeft ? BottomLeft : BottomRight;
        private Vector2 InitialSideOrigin => SideBottomOrigin + up;
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

        private RaycastHit2D StopHorizontalSpeedHit => Raycast(StopHorizontalSpeedOrigin, StopHorizontalSpeedDirection,
            StopHorizontalSpeedDistance, StopHorizontalSpeedLayer);

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

        private Vector2 VerticalOrigin => (MovingDown ? BottomLeft : TopLeft) +
                                          right * (VerticalSpacing * Index * HorizontalMovement);

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

        private RaycastHit2D VerticalHitAtOneWayPlatform =>
            Raycast(VerticalOrigin, VerticalDirection, VerticalDistance, OneWayPlatform);

        public void SetVerticalHitAtOneWayPlatform()
        {
            Raycast.SetHit(VerticalHitAtOneWayPlatform);
        }

        private void SetCollisionOnVerticalHit()
        {
            Collision.OnVerticalHit(VerticalMovementDirection, Hit);
        }

        public void OnInitializeClimbSteepSlope()
        {
            SetLengthForClimbSteepSlope();
            SetOriginForClimbSteepSlope();
            SetHitForClimbSteepSlope();
        }

        private float ClimbSteepSlopeLength => InitialSideLength * 2;

        private void SetLengthForClimbSteepSlope()
        {
            Raycast.SetLength(ClimbSteepSlopeLength);
        }

        private Vector2 ClimbSteepSlopeOrigin => InitialSideOrigin * VerticalMovementDirection;

        private void SetOriginForClimbSteepSlope()
        {
            Raycast.SetOrigin(ClimbSteepSlopeOrigin);
        }

        private Vector2 ClimbSteepSlopeDirection => SideDirection;
        private float ClimbSteepSlopeDistance => ClimbSteepSlopeLength;
        private LayerMask ClimbSteepSlopeLayer => CollisionLayer;

        private RaycastHit2D ClimbSteepSlopeHit => Raycast(ClimbSteepSlopeOrigin, ClimbSteepSlopeDirection,
            ClimbSteepSlopeDistance, ClimbSteepSlopeLayer);

        private void SetHitForClimbSteepSlope()
        {
            Raycast.SetHit(ClimbSteepSlopeHit);
        }

        public void OnClimbSteepSlope()
        {
            Collision.OnClimbSteepSlope(Hit);
        }

        private Vector2 Movement => Physics.DeltaMove;
        private Vector2 ClimbMildSlopeOrigin => SideBottomOrigin + Movement;
        private static Vector2 ClimbMildSlopeDirection => down;
        private static float ClimbMildSlopeDistance => 1;
        private LayerMask ClimbMildSlopeLayer => CollisionLayer;

        private RaycastHit2D ClimbMildSlopeHit => Raycast(ClimbMildSlopeOrigin, ClimbMildSlopeDirection,
            ClimbMildSlopeDistance, ClimbMildSlopeLayer);

        public void OnInitializeClimbMildSlope()
        {
            Raycast.SetHit(ClimbMildSlopeHit);
        }

        /*public void OnClimbMildSlope(){}*/

        /*public void OnInitializeDescendMildSlope()
        {
            SetLengthForDescendMildSlope();
            SetOriginForDescendMildSlope();
            SetHitForDescendMildSlope();
        }

        private float VerticalMovement => Movement.y;
        private float DescendMildSlopeLength => Abs(VerticalMovement) + SkinWidth;

        private void SetLengthForDescendMildSlope()
        {
            Raycast.SetLength(DescendMildSlopeLength);
        }

        private Vector2 DescendMildSlopeOrigin => (MovingLeft ? BottomRight : BottomLeft) + right * HorizontalMovement;

        private void SetOriginForDescendMildSlope()
        {
            Raycast.SetOrigin(DescendMildSlopeOrigin);
        }

        private static Vector2 DescendMildSlopeDirection => down;
        private float DescendMildSlopeDistance => DescendMildSlopeLength;
        private LayerMask DescendMildSlopeLayer => CollisionLayer;

        private RaycastHit2D DescendMildSlopeHit => Raycast(DescendMildSlopeOrigin, DescendMildSlopeDirection,
            DescendMildSlopeDistance, DescendMildSlopeLayer);

        private void SetHitForDescendMildSlope()
        {
            Raycast.SetHit(DescendMildSlopeHit);
        }

        public void OnDescendMildSlope()
        {
            Collision.OnDescendMildSlope(Hit);
        }

        public void OnInitializeDescendSteepSlope()
        {
            SetOriginForDescendSteepSlope();
            SetHitForDescendSteepSlope();
        }

        private Vector2 DescendSteepSlopeOrigin => (MovingRight ? BottomLeft : BottomRight) + Movement;

        private void SetOriginForDescendSteepSlope()
        {
            Raycast.SetOrigin(DescendSteepSlopeOrigin);
        }

        private static Vector2 DescendSteepSlopeDirection => down;
        private static float DescendSteepSlopeDistance => 1;
        private LayerMask DescendSteepSlopeLayer => CollisionLayer;

        private RaycastHit2D DescendSteepSlopeHit => Raycast(DescendSteepSlopeOrigin, DescendSteepSlopeDirection,
            DescendSteepSlopeDistance, DescendSteepSlopeLayer);

        private void SetHitForDescendSteepSlope()
        {
            Raycast.SetHit(DescendSteepSlopeHit);
        }

        public void OnDescendSteepSlope()
        {
        }

        #endregion

        #endregion*/
    }
}