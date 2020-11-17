using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;
    using static ScriptableObjectExtensions;
    using static RaycastModel;

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

        private void SetSafetyBoxcastForImpassableAngle()
        {
            /*
            var transformUp = s.Transform.up;
            var hit = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), -transformUp, s.StickyRaycastLength,
                s.RaysBelowLayerMaskPlatforms, red, s.DrawBoxcastGizmosControl);
            s.SafetyBoxcast = OnSetRaycast(hit);
            */
        }

        private void SetSafetyBoxcast()
        {
            /*
            var transformUp = s.Transform.up;
            var hit = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), s.NewPosition.normalized,
                s.NewPosition.magnitude, s.PlatformMask, red, s.DrawBoxcastGizmosControl);
            s.SafetyBoxcast = OnSetRaycast(hit);
            */
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

        #endregion

        #endregion
    }
}