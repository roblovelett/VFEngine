using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;

    public class DistanceToGroundRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private StickyRaycastHitColliderController stickyRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private DistanceToGroundRaycastData d;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            d = new DistanceToGroundRaycastData();
        }

        private void SetControllers()
        {
            physicsController = character.GetComponent<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            stickyRaycastHitColliderController = character.GetComponent<StickyRaycastHitColliderController>();
            layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            stickyRaycastHitCollider = stickyRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void SetDistanceToGroundRaycastOrigin()
        {
            d.DistanceToGroundRaycastOrigin = new Vector2
            {
                x = stickyRaycastHitCollider.BelowSlopeAngle < 0
                    ? raycast.BoundsBottomLeftCorner.x
                    : raycast.BoundsBottomRightCorner.x,
                y = raycast.BoundsCenter.y
            };
        }

        private void SetDistanceToGroundRaycast()
        {
            d.DistanceToGroundRaycastHit = Raycast(d.DistanceToGroundRaycastOrigin, -physics.Transform.up,
                raycast.DistanceToGroundRayMaximumLength, layerMask.RaysBelowLayerMaskPlatforms, blue,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public DistanceToGroundRaycastData Data => d;

        #region public methods

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public async UniTaskVoid OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}