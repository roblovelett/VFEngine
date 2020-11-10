using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RightRaycastModel", menuName = PlatformerRightRaycastModelPath, order = 0)]
    public class RightRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Right Raycast Data")] [SerializeField]
        private RightRaycastData r;

        #endregion

        #region private methods

        private void SetRightRaycastFromBottomOrigin()
        {
            r.RightRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(r.BoundsBottomRightCorner,
                r.BoundsBottomLeftCorner, r.Transform, r.ObstacleHeightTolerance);
        }

        private void SetRightRaycastToTopOrigin()
        {
            r.RightRaycastToTopOrigin = OnSetRaycastToTopOrigin(r.BoundsTopLeftCorner, r.BoundsTopRightCorner,
                r.Transform, r.ObstacleHeightTolerance);
        }

        private void InitializeRightRaycastLength()
        {
            r.RightRayLength = OnSetHorizontalRayLength(r.Speed.x, r.BoundsWidth, r.RayOffset);
        }

        private void SetCurrentRightRaycastOrigin()
        {
            r.CurrentRightRaycastOrigin = OnSetCurrentRaycastOrigin(r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, r.CurrentRightHitsStorageIndex, r.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            /*r.CurrentRightRaycast = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.RightRayLength,
                r.PlatformMask, red, r.DrawRaycastGizmosControl);*/
        }

        private void SetCurrentRightRaycast()
        {
            /*r.CurrentRightRaycast = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.RightRayLength,
                r.PlatformMask & ~r.OneWayPlatformMask & ~r.MovingOneWayPlatformMask, red, r.DrawRaycastGizmosControl);*/
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetRightRaycastFromBottomOrigin()
        {
            SetRightRaycastFromBottomOrigin();
        }

        public void OnSetRightRaycastToTopOrigin()
        {
            SetRightRaycastToTopOrigin();
        }

        public void OnInitializeRightRaycastLength()
        {
            InitializeRightRaycastLength();
        }

        public void OnSetCurrentRightRaycastOrigin()
        {
            SetCurrentRightRaycastOrigin();
        }

        public void OnSetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentRightRaycast()
        {
            SetCurrentRightRaycast();
        }

        #endregion

        #endregion
    }
}