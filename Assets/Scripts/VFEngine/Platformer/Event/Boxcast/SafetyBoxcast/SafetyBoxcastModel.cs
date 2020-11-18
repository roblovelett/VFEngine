using Sirenix.OdinInspector;
using UnityEngine;
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
            s.RuntimeData = s.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            s.RuntimeData.SetSafetyBoxcast(s.SafetyBoxcast);
        }

        private void InitializeModel()
        {
            s.Transform = s.RuntimeData.platformer.Transform;
            s.DrawBoxcastGizmosControl = s.RuntimeData.raycast.DrawRaycastGizmosControl;
            s.StickyRaycastLength = s.RuntimeData.stickyRaycast.StickyRaycastLength;
            s.NewPosition = s.RuntimeData.physics.NewPosition;
            s.Bounds = s.RuntimeData.raycast.Bounds;
            s.BoundsCenter = s.RuntimeData.raycast.BoundsCenter;
            s.PlatformMask = s.RuntimeData.layerMask.PlatformMask;
            s.RaysBelowLayerMaskPlatforms = s.RuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
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