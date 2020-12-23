using UnityEngine;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        private BoxCollider2D boxCollider;
        private RightRaycastController rightRaycastController;
        private DownRaycastController downRaycastController;
        private LeftRaycastController leftRaycastController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private PhysicsController physicsController;
        private RaycastData r;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private PhysicsData physics;
        private PlatformerData platformer;

        #endregion

        #region internal

        private RaycastHit2D Hit => downRaycastHitCollider.Collision.hit;
        private bool HitConnected => Hit.distance <= 0;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            downRaycastController = GetComponent<DownRaycastController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            physicsController = GetComponent<PhysicsController>();
        }

        private void InitializeData()
        {
            r = new RaycastData();
            r.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            physics = physicsController.Data;
        }

        private void Initialize()
        {
            r.Initialize(settings, boxCollider);
        }

        #endregion

        private void PlatformerInitializeFrame()
        {
            r.SetRayOrigins();
        }

        private void PlatformerCastRaysDown()
        {
            CastRaysDown();
        }

        private void CastRaysDown()
        {
            r.InitializeDownIndex();
            for (var i = 0; i < r.VerticalRayCount; i++)
            {
                downRaycastHitColliderController.OnSetHit();
                if (HitConnected)
                {
                    downRaycastHitColliderController.OnHitConnected();
                    downRaycastController.OnHitConnected();
                    break;
                }

                r.AddToDownIndex();
            }
        }

        private int HorizontalDirection => physics.HorizontalMovementDirection;
        private bool CastRight => HorizontalDirection == 1;
        private bool CastLeft => HorizontalDirection == 1;
        private bool DoNotCast => !CastRight || !CastLeft;

        private void PlatformerCastRaysToSides()
        {
            if (DoNotCast) return;
            if (CastRight) CastRaysRight();
            if (CastLeft) CastRaysLeft();
        }

        private void CastRaysRight()
        {
            r.InitializeRightIndex();
            for (var i = 0; i < r.HorizontalRayCount; i++)
            {
                rightRaycastHitColliderController.OnSetHit();
            }
        }

        private void CastRaysLeft()
        {
            r.InitializeLeftIndex();
        }

        #endregion

        #endregion

        #region properties

        public RaycastData Data => r;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        public void OnPlatformerCastRaysToSides()
        {
            PlatformerCastRaysToSides();
        }

        #endregion

        #endregion
    }
}