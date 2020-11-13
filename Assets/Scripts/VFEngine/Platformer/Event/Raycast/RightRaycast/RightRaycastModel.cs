﻿using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
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
        private RightRaycastData r = null;

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
            r.currentRightRaycastOrigin = OnSetCurrentRaycastOrigin(r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, r.CurrentRightHitsStorageIndex, r.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            var hit = Raycast(r.currentRightRaycastOrigin, r.Transform.right, r.RightRayLength, r.PlatformMask, red,
                r.DrawRaycastGizmosControl);
            r.CurrentRightRaycast = OnSetRaycast(hit);
        }

        private void SetCurrentRightRaycast()
        {
            var hit = Raycast(r.currentRightRaycastOrigin, r.Transform.right, r.RightRayLength,
                r.PlatformMask & ~r.OneWayPlatformMask & ~r.MovingOneWayPlatformMask, red, r.DrawRaycastGizmosControl);
            r.CurrentRightRaycast = OnSetRaycast(hit);
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