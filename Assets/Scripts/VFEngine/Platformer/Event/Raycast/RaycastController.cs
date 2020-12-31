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
        private RaycastModel _raycast;
        private LayerMaskController _layerMaskController;
        private PhysicsController _physicsController;
        private PlatformerController _platformerController;

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
            _layerMaskController = GetComponent<LayerMaskController>();
            _physicsController = GetComponent<PhysicsController>();
            _platformerController = GetComponent<PlatformerController>();
            if (!collider) collider = GetComponent<BoxCollider2D>();
            if (!settings) settings = CreateInstance<RaycastSettings>();
            _raycast = new RaycastModel(collider, settings, _layerMaskController, _physicsController,
                _platformerController);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => _raycast.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            _raycast.ResetCollision();
            _raycast.SetBounds();
        }

        public void OnPlatformerCastRaysDown()
        {
            _raycast.SetDownOrigin();
            _raycast.SetDownHit();
        }

        public void OnPlatformerSetDownHitAtOneWayPlatform()
        {
            _raycast.SetDownHitAtOneWayPlatform();
        }

        private Vector2 Origin => _raycast.Data.Origin;
        private float SkinWidth => _raycast.Data.SkinWidth;
        private Vector2 DownRayDirection => down * SkinWidth * 2;
        private static Color DownRayColor => blue;

        public void OnPlatformerDownHit()
        {
            _raycast.SetCollisionOnDownHit();
            DrawRay(Origin, DownRayDirection, DownRayColor);
        }

        public void OnPlatformerSlopeBehavior()
        {
            _raycast.SetCollisionBelow();
        }

        public void OnPlatformerInitializeLengthForSideRay()
        {
            _raycast.InitializeLengthForSideRay();
        }

        private PhysicsData Physics => _physicsController.Data;
        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private float MoveX => Physics.Movement.x;
        private float SideRayLength => Abs(MoveX) + SkinWidth;
        private Vector2 SideRayDirection => right * HorizontalMovementDirection * SideRayLength;
        private static Color SideRayColor => red;

        public void OnPlatformerCastRaysToSides()
        {
            _raycast.SetSideOrigin();
            _raycast.SetSideHit();
            DrawRay(Origin, SideRayDirection, SideRayColor);
        }

        public void OnPlatformerOnFirstSideHit()
        {
            _raycast.SetCollisionOnSideHit();
        }

        public void OnPlatformerSetRayLengthForSideRay()
        {
            _raycast.SetLengthForSideRay();
        }

        public void OnPlatformerHitWall()
        {
            _raycast.OnHitWall();
        }

        public void OnPlatformerStopHorizontalSpeedAndSetHit()
        {
            _raycast.OnStopHorizontalSpeedAndSetHit();
        }

        public void OnPlatformerInitializeLengthForVerticalRay()
        {
            _raycast.InitializeLengthForVerticalRay();
        }

        public void OnPlatformerCastRaysVertically()
        {
            //_raycast.SetVerticalOrigin();
            //_raycast.SetVerticalHit();
        }

        #endregion

        #endregion
    }
}