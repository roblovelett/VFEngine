using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;
    using static Debug;
    using static Color;
    using static Vector2;
    using static Mathf;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private new Collider2D collider;
        [SerializeField] private RaycastSettings settings;
        private RaycastModel raycast;
        private LayerMaskController layerMaskController;
        private PhysicsController physicsController;
        private PlatformerController platformerController;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            layerMaskController = GetComponent<LayerMaskController>();
            physicsController = GetComponent<PhysicsController>();
            platformerController = GetComponent<PlatformerController>();
            if (!collider) collider = GetComponent<BoxCollider2D>();
            if (!settings) settings = CreateInstance<RaycastSettings>();
            raycast = new RaycastModel(collider, settings, layerMaskController, physicsController,
                platformerController);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => raycast.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            raycast.ResetCollision();
            raycast.SetBounds();
        }

        public void OnPlatformerCastRaysDown()
        {
            raycast.SetDownOrigin();
            raycast.SetDownHit();
        }

        public void OnPlatformerSetDownHitAtOneWayPlatform()
        {
            raycast.SetDownHitAtOneWayPlatform();
        }

        private Vector2 Origin => raycast.Data.Origin;
        private float SkinWidth => raycast.Data.SkinWidth;
        private Vector2 DownRayDirection => down * SkinWidth * 2;
        private static Color DownRayColor => blue;

        public void OnPlatformerDownHit()
        {
            raycast.SetCollisionOnDownHit();
            DrawRay(Origin, DownRayDirection, DownRayColor);
        }

        public void OnPlatformerSlopeBehavior()
        {
            raycast.SetCollisionBelow();
        }

        public void OnPlatformerInitializeRayLength()
        {
            raycast.InitializeLength();
        }

        private PhysicsData Physics => physicsController.Data;
        private int MovementDirection => Physics.MovementDirection;
        private float MoveX => Physics.Movement.x;
        private float SideRayLength => Abs(MoveX) + SkinWidth;
        private Vector2 SideRayDirection => right * MovementDirection * SideRayLength;
        private static Color SideRayColor => red;

        public void OnPlatformerCastRaysToSides()
        {
            raycast.SetSideOrigin();
            raycast.SetSideHit();
            DrawRay(Origin, SideRayDirection, SideRayColor);
        }

        public void OnPlatformerOnFirstHitOnAngle()
        {
            raycast.SetCollisionOnSideHit();
            //raycast.SetAngle();
            //raycast.SetCollisionOnSideHit();
        }

        public void OnPlatformerSetRayLength()
        {
            raycast.SetLength();
        }

        #endregion

        #endregion
    }
}