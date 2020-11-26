using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastModel", menuName = PlatformerDistanceToGroundRaycastModelPath,
        order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Data")] [SerializeField] private DistanceToGroundRaycastData d;
        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!d) d = CreateInstance<DistanceToGroundRaycastData>();
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!raycastHitColliderController) raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycastHitCollider = raycastHitColliderController.StickyRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
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

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public void OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
        }

        #endregion

        #endregion
    }
}