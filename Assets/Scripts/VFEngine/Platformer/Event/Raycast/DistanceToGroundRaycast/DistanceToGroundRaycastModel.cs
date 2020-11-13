﻿using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static RaycastModel;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastModel", menuName = PlatformerDistanceToGroundRaycastModelPath,
        order = 0)]
    public class DistanceToGroundRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Data")] [SerializeField]
        private DistanceToGroundRaycastData d = null;

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
            var hit = Raycast(d.DistanceToGroundRaycastOrigin, -d.Transform.up, d.DistanceToGroundRayMaximumLength,
                d.RaysBelowLayerMaskPlatforms, blue, d.DrawRaycastGizmosControl);
            d.DistanceToGroundRaycast = OnSetRaycast(hit);
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

        #endregion

        #endregion
    }
}