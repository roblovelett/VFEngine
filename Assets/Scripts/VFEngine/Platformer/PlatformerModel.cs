using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer
{
    public class PlatformerModel
    {
        #region fields

        private readonly PlatformerData platformer;
        private readonly RaycastController raycast;
        private readonly LayerMaskController layerMask;
        private readonly PhysicsController physics;

        #region internal

        private RaycastData Raycast => raycast.Data;
        private RaycastCollision Collision => Raycast.Collision;
        private RaycastBounds Bounds => Raycast.Bounds;
        private LayerMaskData LayerMask => layerMask.Data;

        private PhysicsData Physics => physics.Data;
        /*private bool HorizontalMovement => Physics.DeltaMovement.x != 0;
        private bool NegativeVerticalMovement => Physics.DeltaMovement.y <= 0;
        private bool OnSlope => Collision.OnSlope;
        private bool OnSlopes => NegativeVerticalMovement && OnSlope;
        private bool DescendingSlope => GroundDirection == HorizontalMovementDirection;
        private int GroundDirection => Collision.GroundDirection;
        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private int VerticalRays => Raycast.VerticalRays;
        private RaycastHit2D Hit => Raycast.Hit;
        private float IgnorePlatformsTime => Data.IgnorePlatformsTime;
        private bool IgnorePlatformsTimeStopped => IgnorePlatformsTime <= 0;
        private bool CastDownAtOneWayPlatform => !Hit && IgnorePlatformsTimeStopped;
        private bool HitMissed => Hit.distance <= 0;*/

        #endregion

        #region private methods

        private void RunInternal()
        {
            InitializeFrame();
        }

        private void InitializeFrame()
        {
            raycast.PlatformerInitializeFrame();
            //layerMask.PlatformerInitializeFrame();
            //physics.PlatformerInitializeFrame();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerData Data => platformer;

        #region public methods

        public PlatformerModel(PlatformerSettings settings, RaycastController raycastController,
            LayerMaskController layerMaskController, PhysicsController physicsController)
        {
            platformer = new PlatformerData(settings);
            raycast = raycastController;
            layerMask = layerMaskController;
            physics = physicsController;
        }

        public void Run()
        {
            RunInternal();
        }

        #endregion

        #endregion
    }
}