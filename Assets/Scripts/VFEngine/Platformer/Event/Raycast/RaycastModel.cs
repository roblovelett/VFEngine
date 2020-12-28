using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static Physics2D;
    using static Mathf;
    using static Color;
    using static Debug;
    public class RaycastModel
    {
        #region fields

        private RaycastData raycast;
        private readonly LayerMaskController layerMask;
        private readonly PhysicsController physics;
        private readonly PlatformerController platformer;
        
        #region internal

        /*private LayerMaskData LayerMask => layerMask.Data;
        private PhysicsData Physics => physics.Data;
        private PlatformerData Platformer => platformer.Data;
        private RaycastBounds Bounds => raycast.Bounds;
        private RaycastCollision Collision => raycast.Collision;
        private Collider2D Collider => raycast.Collider;
        private float SkinWidth => raycast.SkinWidth;
        private bool FacingRight => Physics.HorizontalMovementDirection == 1;
        private Vector2 BottomLeft => raycast.Bounds.BottomLeft;
        private Vector2 BottomRight => raycast.Bounds.BottomRight;
        private float VerticalSpacing => raycast.VerticalSpacing;
        private int Index => Platformer.Index;
        private Vector2 DownOrigin
        {
            get
            {
                var origin = (FacingRight ? BottomLeft : BottomRight) +
                             (FacingRight ? right : left) * (VerticalSpacing * Index);
                origin.y += SkinWidth * 2;
                return origin;
            }
        }
        
        private LayerMask CollisionMask => LayerMask.Collision;
        private float DownRayDistance => SkinWidth * 4;
        private RaycastHit2D DownHit => Raycast(DownOrigin, down, DownRayDistance, CollisionMask);
        private LayerMask OneWayPlatform => LayerMask.OneWayPlatform;
        private RaycastHit2D DownHitAtOneWayPlatform => Raycast(DownOrigin, down, DownRayDistance, OneWayPlatform);
        private RaycastHit2D CurrentDownHit => raycast.Hit;
        private int GroundDirection => (int) Sign(CurrentDownHit.normal.x);
        private int GroundLayer => CurrentDownHit.collider.gameObject.layer;
        private float GroundAngle => Angle(CurrentDownHit.normal, up);*/
        #endregion

        #region private methods

        #region initialization

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => raycast;

        #region public methods
        
        #region constructors

        public RaycastModel(Collider2D collider, RaycastSettings settings, LayerMaskController layerMaskController,
            PhysicsController physicsController,
            PlatformerController platformerController)
        {
            raycast = new RaycastData(settings, collider);
            layerMask = layerMaskController;
            physics = physicsController;
            platformer = platformerController;
        }
        
        #endregion

        public void ResetCollision()
        {
            raycast.ResetCollision();
        }

        public void SetBounds()
        {
            raycast.SetBounds();
        }

        /*
        
        public void SetOrigin(Vector2 origin)
        {
            raycast.Origin = origin;
        }

        public void SetHit(RaycastHit2D hit)
        {
            raycast.Hit = hit;
        }

        */

        #endregion

        #endregion
    }
}