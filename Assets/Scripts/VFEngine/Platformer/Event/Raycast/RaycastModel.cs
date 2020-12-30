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

        private readonly LayerMaskController layerMaskController;
        private readonly PhysicsController physicsController;
        private readonly PlatformerController platformerController;

        #region internal

        #endregion

        #region private methods

        #region initialization

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => Raycast;

        #region public methods

        #region constructors

        public RaycastModel(Collider2D collider, RaycastSettings settings, LayerMaskController layerMask,
            PhysicsController physics, PlatformerController platformer)
        {
            Raycast = new RaycastData(settings, collider);
            layerMaskController = layerMask;
            physicsController = physics;
            platformerController = platformer;
        }

        #endregion

        private RaycastData Raycast { get; }

        public void ResetCollision()
        {
            Raycast.ResetCollision();
        }

        public void SetBounds()
        {
            Raycast.SetBounds();
        }

        private RaycastBounds Bounds => Raycast.Bounds;
        private RaycastCollision Collision => Raycast.Collision;
        private PhysicsData Physics => physicsController.Data;
        private LayerMaskData LayerMask => layerMaskController.Data;
        private PlatformerData Platformer => platformerController.Data;
        private int MovementDirection => Physics.MovementDirection;
        private bool MovingRight => MovementDirection == 1;
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

        public void SetDownOrigin()
        {
            Raycast.SetOrigin(DownOrigin);
        }

        public void SetDownHit()
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

        public void SetCollisionOnDownHit()
        {
            Collision.OnDownHit(DownHit);
        }

        public void SetCollisionBelow()
        {
            Collision.SetCollisionBelow(true);
        }

        private float MoveX => Physics.Movement.x;
        private float InitialLength => Abs(MoveX) + SkinWidth;
        private float Length => Raycast.Length;

        public void InitializeLength()
        {
            Raycast.SetLength(InitialLength);
        }

        private bool MovingLeft => MovementDirection == -1;
        private float HorizontalSpacing => Raycast.HorizontalSpacing;
        private Vector2 SideDirection => right * MovementDirection;
        private LayerMask SideLayer => CollisionLayer;
        private float SideDistance => Length;
        private Vector2 SideOrigin => (MovingLeft ? BottomLeft : BottomRight) + up * HorizontalSpacing * Index;
        private RaycastHit2D SideHit => Raycast(SideOrigin, SideDirection, SideDistance, SideLayer);

        public void SetSideOrigin()
        {
            Raycast.SetOrigin(SideOrigin);
        }

        public void SetSideHit()
        {
            Raycast.SetHit(SideHit);
        }

        private float MinimumLength => Min(InitialLength + SkinWidth, SideHit.distance);

        public void SetCollisionOnSideHit()
        {
            Collision.OnSideHit(SideHit);
        }

        public void SetLength()
        {
            Raycast.SetLength(MinimumLength);
        }

        #endregion

        #endregion
    }
}