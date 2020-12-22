using UnityEngine;
using VFEngine.Platformer.Physics;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Vector2;
    using static Color;
    using static Debug;

    public class DownRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private RaycastController raycastController;
        private PhysicsController physicsController;
        private DownRaycastData d;
        private RaycastData raycast;
        private PhysicsData physics;

        #endregion

        #region internal

        private bool MovingRight => physics.HorizontalMovementDirection == 1;
        private Vector2 InitialOrigin => MovingRight ? raycast.Bounds.BottomLeft : raycast.Bounds.BottomRight;
        private Vector2 HorizontalDirection => MovingRight ? right : left;
        private float Spacing => raycast.VerticalRaySpacing;
        private int Index => raycast.DownIndex;
        private float SkinWidth => raycast.SkinWidth;

        private Vector2 Origin
        {
            get
            {
                var origin = InitialOrigin + HorizontalDirection * (Spacing * Index);
                origin.y += SkinWidth * 2;
                return origin;
            }
        }

        #endregion

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            raycastController = GetComponent<RaycastController>();
            physicsController = GetComponent<PhysicsController>();
        }

        private void InitializeData()
        {
            d = new DownRaycastData();
            d.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            physics = physicsController.Data;
        }

        private void Initialize()
        {
            d.Origin = Origin;
        }

        #endregion

        private void HitConnected()
        {
            DrawRay(Origin, down * (SkinWidth * 2), blue);
        }

        #endregion

        #region properties

        public DownRaycastData Data => d;

        #region public methods

        public void OnHitConnected()
        {
            HitConnected();
        }

        #endregion

        #endregion
    }
}