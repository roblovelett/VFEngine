using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;
    using static Debug;
    using static Color;
    using static Vector2;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private new Collider2D collider;
        [SerializeField] private RaycastSettings settings;
        private RaycastModel _model;
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
            _model = new RaycastModel(collider, settings, _layerMaskController, _physicsController,
                _platformerController);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => _model.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            _model.OnInitializeFrame();
        }

        public void OnPlatformerCastRaysDown()
        {
            _model.OnCastRaysDown();
        }

        public void OnPlatformerSetDownHitAtOneWayPlatform()
        {
            _model.SetDownHitAtOneWayPlatform();
        }

        private RaycastData Raycast => _model.Data;
        private Vector2 Origin => Raycast.Origin;
        private Vector2 DownRayOrigin => Origin;
        private float SkinWidth => Raycast.SkinWidth;
        private Vector2 DownRayDirection => down * SkinWidth * 2;
        private static Color DownRayColor => blue;

        public void OnPlatformerDownHit()
        {
            _model.OnDownHit();
            DrawRay(DownRayOrigin, DownRayDirection, DownRayColor);
        }

        public void OnPlatformerSlopeBehavior()
        {
            _model.OnSlopeBehavior();
        }

        public void OnPlatformerInitializeLengthForSideRay()
        {
            _model.InitializeLengthForSideRay();
        }

        private PhysicsData Physics => _physicsController.Data;
        private Vector2 SideRayOrigin => Origin;
        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private float Length => Raycast.Length;
        private float SideRayLength => Length;
        private Vector2 SideRayDirection => right * HorizontalMovementDirection * SideRayLength;
        private static Color SideRayColor => red;

        public void OnPlatformerCastRaysToSides()
        {
            _model.OnCastRaysToSides();
            DrawRay(SideRayOrigin, SideRayDirection, SideRayColor);
        }

        public void OnPlatformerOnFirstSideHit()
        {
            _model.OnFirstSideHit();
        }

        public void OnPlatformerSetLengthForSideRay()
        {
            _model.SetLengthForSideRay();
        }

        public void OnPlatformerHitWall()
        {
            _model.OnHitWall();
        }

        public void OnPlatformerOnStopHorizontalSpeedHit()
        {
            _model.OnStopHorizontalSpeedHit();
        }

        public void OnPlatformerInitializeLengthForVerticalRay()
        {
            _model.InitializeLengthForVerticalRay();
        }

        private Vector2 VerticalRayOrigin => Origin;
        private int VerticalMovementDirection => Physics.VerticalMovementDirection;
        private float VerticalRayLength => Length;
        private Vector2 VerticalRayDirection => up * VerticalMovementDirection * VerticalRayLength;
        private static Color VerticalRayColor => red;

        public void OnPlatformerCastRaysVertically()
        {
            _model.OnCastRaysVertically();
            DrawRay(VerticalRayOrigin, VerticalRayDirection, VerticalRayColor);
        }

        public void OnPlatformerSetVerticalHitAtOneWayPlatform()
        {
            _model.SetVerticalHitAtOneWayPlatform();
        }
        public void OnPlatformerVerticalHit()
        {
            _model.OnVerticalHit();
        }

        public void OnPlatformerClimbSteepSlope()
        {
            _model.SetLengthForSteepSlope();
            //_model.
        }

        #endregion

        #endregion
    }
}