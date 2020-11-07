using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;
    using static ScriptableObjectExtensions;

    //$"{PlatformerPath}"

    [CreateAssetMenu(fileName = "SafetyBoxcastModel", menuName = PlatformerSafetyBoxcastModelPath, order = 0)]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData s;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            s.SafetyBoxcastDistance = s.SafetyBoxcast.distance;
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), -transformUp,
                s.StickyRaycastLength, s.RaysBelowLayerMaskPlatforms, red, s.DrawBoxcastGizmosControl);
        }

        private void SetHasSafetyBoxcast()
        {
            s.HasSafetyBoxcast = true;
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), s.NewPosition.normalized,
                s.NewPosition.magnitude, s.PlatformMask, red, s.DrawBoxcastGizmosControl);
        }

        private void ResetState()
        {
            s.HasSafetyBoxcast = false;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetSafetyBoxcastForImpassableAngle()
        {
            SetSafetyBoxcastForImpassableAngle();
        }

        public void OnSetHasSafetyBoxcast()
        {
            SetHasSafetyBoxcast();
        }

        public void OnSetSafetyBoxcast()
        {
            SetSafetyBoxcast();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public void OnInitialize()
        {
            if (s.PerformSafetyBoxcast) Initialize();
        }

        #endregion

        #endregion
    }
}