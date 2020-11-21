using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel", menuName = PlatformerSafetyBoxcastModelPath, order = 0)]
    [InlineEditor]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData s = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            s.RuntimeData = s.Character.GetComponentNoAllocation<BoxcastController>().SafetyBoxcastRuntimeData;
            s.RuntimeData.SetSafetyBoxcast(s.SafetyBoxcast);
        }

        private void InitializeModel()
        {
            s.PlatformerRuntimeData = s.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            s.LayerMaskRuntimeData = s.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            s.RaycastRuntimeData = s.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            s.StickyRaycastRuntimeData =
                s.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            s.PhysicsRuntimeData = s.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            s.Transform = s.PlatformerRuntimeData.platformer.Transform;
            s.PlatformMask = s.LayerMaskRuntimeData.layerMask.PlatformMask;
            s.RaysBelowLayerMaskPlatforms = s.LayerMaskRuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
            s.DrawBoxcastGizmosControl = s.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            s.Bounds = s.RaycastRuntimeData.raycast.Bounds;
            s.BoundsCenter = s.RaycastRuntimeData.raycast.BoundsCenter;
            s.StickyRaycastLength = s.StickyRaycastRuntimeData.stickyRaycast.StickyRaycastLength;
            s.NewPosition = s.PhysicsRuntimeData.physics.NewPosition;
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), -transformUp,
                s.StickyRaycastLength, s.RaysBelowLayerMaskPlatforms, red, s.DrawBoxcastGizmosControl);
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), s.NewPosition.normalized,
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

        public void OnInitialize()
        {
            Initialize();
        }

        #endregion

        #endregion
    }
}