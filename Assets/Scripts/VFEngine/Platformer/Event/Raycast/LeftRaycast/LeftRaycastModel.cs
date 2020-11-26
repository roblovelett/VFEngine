using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastModel", menuName = PlatformerLeftRaycastModelPath, order = 0)]
    [InlineEditor]
    public class LeftRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Left Raycast Data")] [SerializeField] private LeftRaycastData l;
        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!l) l = CreateInstance<LeftRaycastData>();
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!raycastHitColliderController) raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            leftRaycastHitCollider = raycastHitColliderController.LeftRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void SetLeftRaycastFromBottomOrigin()
        {
            l.LeftRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetLeftRaycastToTopOrigin()
        {
            l.LeftRaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetCurrentLeftRaycastOrigin()
        {
            l.CurrentLeftRaycastOrigin = OnSetCurrentRaycastOrigin(l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, leftRaycastHitCollider.CurrentLeftHitsStorageIndex,
                raycast.NumberOfHorizontalRaysPerSide);
        }

        private void InitializeLeftRaycastLength()
        {
            l.LeftRayLength = OnSetHorizontalRayLength(physics.Speed.x, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -physics.Transform.right, l.LeftRayLength,
                layerMask.PlatformMask, red, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentLeftRaycast()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -physics.Transform.right, l.LeftRayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastData Data => l;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetLeftRaycastFromBottomOrigin()
        {
            SetLeftRaycastFromBottomOrigin();
        }

        public void OnSetLeftRaycastToTopOrigin()
        {
            SetLeftRaycastToTopOrigin();
        }

        public void OnInitializeLeftRaycastLength()
        {
            InitializeLeftRaycastLength();
        }

        public void OnSetCurrentLeftRaycastOrigin()
        {
            SetCurrentLeftRaycastOrigin();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
        }

        #endregion

        #endregion
    }
}