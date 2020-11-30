using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;

    public class SafetyBoxcastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private StickyRaycastController stickyRaycastController;
        private LayerMaskController layerMaskController;
        private SafetyBoxcastData s;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
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
            s = new SafetyBoxcastData();
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            stickyRaycastController = character.GetComponentNoAllocation<StickyRaycastController>();
            layerMaskController = character.GetComponentNoAllocation<LayerMaskController>();
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            stickyRaycast = stickyRaycastController.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = physics.Transform.up;
            s.SafetyBoxcastHit = Boxcast(raycast.BoundsCenter, raycast.Bounds, Angle(transformUp, up), -transformUp,
                stickyRaycast.StickyRaycastLength, layerMask.RaysBelowLayerMaskPlatforms, red,
                raycast.DrawRaycastGizmosControl);
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = physics.Transform.up;
            s.SafetyBoxcastHit = Boxcast(raycast.BoundsCenter, raycast.Bounds, Angle(transformUp, up),
                physics.NewPosition.normalized, physics.NewPosition.magnitude, layerMask.PlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public SafetyBoxcastData Data => s;

        #region public methods

        public void OnSetSafetyBoxcastForImpassableAngle()
        {
            SetSafetyBoxcastForImpassableAngle();
        }

        public void OnSetSafetyBoxcast()
        {
            SetSafetyBoxcast();
        }

        #endregion

        #endregion
    }
}