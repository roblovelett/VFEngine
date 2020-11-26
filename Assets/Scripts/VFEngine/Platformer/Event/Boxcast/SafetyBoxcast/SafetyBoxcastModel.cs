using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel", menuName = PlatformerSafetyBoxcastModelPath, order = 0)]
    [InlineEditor]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData s;

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!s) s = CreateInstance<SafetyBoxcastData>();
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycast = raycastController.StickyRaycastModel.Data;
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

        #endregion

        #endregion
    }
}