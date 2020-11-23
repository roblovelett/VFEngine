using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
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

        [SerializeField] private GameObject character;
        private SafetyBoxcastData s;

        #endregion

        #region private methods

        private void InitializeData()
        {
            s = new SafetyBoxcastData {Character = character};
            s.RuntimeData = SafetyBoxcastRuntimeData.CreateInstance(s.SafetyBoxcastHit);
        }

        private void InitializeModel()
        {
            s.PhysicsRuntimeData = s.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            s.RaycastRuntimeData = s.Character.GetComponentNoAllocation<RaycastController>().RaycastRuntimeData;
            s.StickyRaycastRuntimeData =
                s.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            s.LayerMaskRuntimeData = s.Character.GetComponentNoAllocation<LayerMaskController>().LayerMaskRuntimeData;
            s.Transform = s.PhysicsRuntimeData.Transform;
            s.NewPosition = s.PhysicsRuntimeData.NewPosition;
            s.DrawBoxcastGizmosControl = s.RaycastRuntimeData.DrawRaycastGizmosControl;
            s.Bounds = s.RaycastRuntimeData.Bounds;
            s.BoundsCenter = s.RaycastRuntimeData.BoundsCenter;
            s.StickyRaycastLength = s.StickyRaycastRuntimeData.StickyRaycastLength;
            s.PlatformMask = s.LayerMaskRuntimeData.PlatformMask;
            s.RaysBelowLayerMaskPlatforms = s.LayerMaskRuntimeData.RaysBelowLayerMaskPlatforms;
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcastHit = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), -transformUp,
                s.StickyRaycastLength, s.RaysBelowLayerMaskPlatforms, red, s.DrawBoxcastGizmosControl);
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcastHit = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), s.NewPosition.normalized,
                s.NewPosition.magnitude, s.PlatformMask, red, s.DrawBoxcastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

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