using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Distance To Ground Raycast/Distance To Ground Raycast Model",
        order = 0)]
    public class DistanceToGroundRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Data")] [SerializeField]
        private DistanceToGroundRaycastData d;

        #endregion

        #region private methods

        private void SetDistanceToGroundRaycastOrigin()
        {
            d.DistanceToGroundRaycastOrigin = new Vector2
            {
                x = d.BelowSlopeAngle < 0 ? d.BoundsBottomLeftCorner.x : d.BoundsBottomRightCorner.x,
                y = d.BoundsCenter.y
            };
        }

        private void SetDistanceToGroundRaycast()
        {
            d.DistanceToGroundRaycast = Raycast(d.DistanceToGroundRaycastOrigin, -d.Transform.up,
                d.DistanceToGroundRayMaximumLength, d.RaysBelowLayerMaskPlatforms, blue);
        }

        private void SetHasDistanceToGroundRaycast()
        {
            d.HasDistanceToGroundRaycast = true;
        }

        private void Reset()
        {
            d.HasDistanceToGroundRaycast = false;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public void OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
        }

        public void OnSetHasDistanceToGroundRaycast()
        {
            SetHasDistanceToGroundRaycast();
        }

        #endregion

        #endregion
    }
}