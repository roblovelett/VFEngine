using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics
{
    using static Time;
    using static Vector2;
    using static ScriptableObject;

    public class PhysicsController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings;
        private GameObject character;
        private RaycastController raycastController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private PhysicsData p;
        private DownRaycastHitColliderData downRaycastHitCollider;

        #endregion

        #region internal

        private bool IgnoreFriction => p.IgnoreFriction;
        private bool OnGround => downRaycastHitCollider.Collision.onGround;
        private bool OnSlope => downRaycastHitCollider.Collision.OnSlope;
        private bool VerticalSpeed => p.Speed.y > 0;
        private bool ExceededMaximumSlopeAngle => GroundAngle > MaximumSlopeAngle;
        private bool MetMinimumWallAngle => GroundAngle < MinimumWallAngle;
        private bool NoHorizontalSpeed => HorizontalSpeed == 0;
        private bool AddToHorizontalExternalForce =>
            OnSlope && ExceededMaximumSlopeAngle && (MetMinimumWallAngle || NoHorizontalSpeed);
        private int GroundDirection => downRaycastHitCollider.Collision.groundDirection;
        private float HorizontalSpeed => p.Speed.x;
        private float GroundFriction => p.GroundFriction;
        private float AirFriction => p.AirFriction;
        private float Friction => OnGround ? GroundFriction : AirFriction;
        private float Gravity => p.Gravity;
        private float GravityScale => p.GravityScale;
        private float GravitationalForce => Gravity * GravityScale * deltaTime;
        private float GroundAngle => downRaycastHitCollider.Collision.groundAngle;
        private float MaximumSlopeAngle => p.MaximumSlopeAngle;
        private float MinimumWallAngle => p.MinimumWallAngle;
        private Vector2 ExternalForce => p.ExternalForce;

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
            raycastController = GetComponent<RaycastController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
        }

        private void InitializeData()
        {
            p = new PhysicsData();
            p.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            downRaycastHitCollider = downRaycastHitColliderController.Data;
        }

        private void Initialize()
        {
            p.Initialize(settings);
        }

        #endregion

        private void PlatformerSetExternalForce()
        {
            if (IgnoreFriction) return;
            p.ExternalForce = MoveTowards(ExternalForce, zero, ExternalForce.magnitude * Friction * deltaTime);
        }

        private void PlatformerSetGravity()
        {
            if (VerticalSpeed) p.SpeedY += GravitationalForce;
            else p.ExternalForceY += GravitationalForce;
        }

        private void PlatformerSetHorizontalExternalForce()
        {
            if (!AddToHorizontalExternalForce) return;
            p.ExternalForceX += -Gravity * GroundFriction * GroundDirection * fixedDeltaTime / 4;
        }

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => p;

        #region public methods

        public void OnPlatformerSetExternalForce()
        {
            PlatformerSetExternalForce();
        }

        public void OnPlatformerSetGravity()
        {
            PlatformerSetGravity();
        }

        public void OnPlatformerSetHorizontalExternalForce()
        {
            PlatformerSetHorizontalExternalForce();
        }

        #endregion

        #endregion
    }
}