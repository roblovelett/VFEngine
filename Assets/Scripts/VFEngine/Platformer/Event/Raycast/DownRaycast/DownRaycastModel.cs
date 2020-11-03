using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static MathsExtensions;
    using static Mathf;

    [CreateAssetMenu(fileName = "DownRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Down Raycast/Down Raycast Model", order = 0)]
    public class DownRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Down Raycast Data")] [SerializeField]
        private DownRaycastData d;

        #endregion

        #region private methods

        private void Initialize()
        {
            if (d.DisplayWarningsControl) GetWarningMessages();
        }

        private void GetWarningMessages()
        {
            // foo
        }

        private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentDownRaycast = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
                d.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, d.DrawRaycastGizmosControl);
        }

        private void SetCurrentDownRaycast()
        {
            d.CurrentDownRaycast = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
                d.RaysBelowLayerMaskPlatforms, blue, d.DrawRaycastGizmosControl);
        }

        private void InitializeDownRayLength()
        {
            d.DownRayLength = d.BoundsHeight / 2 + d.RayOffset;
        }

        private void DoubleDownRayLength()
        {
            d.DownRayLength *= 2;
        }

        private void SetDownRayLengthToVerticalNewPosition()
        {
            d.DownRayLength += Abs(d.NewPosition.y);
        }

        private void SetDownRaycastFromLeft()
        {
            d.DownRaycastFromLeft = OnSetVerticalRaycast(d.BoundsBottomLeftCorner, d.BoundsTopLeftCorner, d.Transform,
                d.RayOffset, d.NewPosition.x);
        }

        private void SetDownRaycastToRight()
        {
            d.DownRaycastToRight = OnSetVerticalRaycast(d.BoundsBottomRightCorner, d.BoundsTopRightCorner, d.Transform,
                d.RayOffset, d.NewPosition.x);
        }

        private void InitializeSmallestDistanceToDownHit()
        {
            d.SmallestDistanceToDownHit = MaxValue;
        }

        private void SetSmallestDistanceToDownHitDistance()
        {
            d.SmallestDistanceToDownHit = d.RaycastDownHitAt.distance;
        }

        private void SetDistanceBetweenDownRaycastsAndSmallestDistancePoint()
        {
            d.DistanceBetweenDownRaycastsAndSmallestDistancePoint =
                DistanceBetweenPointAndLine(d.StandingOnWithSmallestDistancePoint, d.DownRaycastFromLeft,
                    d.DownRaycastToRight);
        }

        private void SetCurrentDownRaycastOriginPoint()
        {
            d.CurrentDownRaycastOrigin = OnSetCurrentRaycastOrigin(d.DownRaycastFromLeft, d.DownRaycastToRight,
                d.CurrentDownHitsStorageIndex, d.NumberOfVerticalRaysPerSide);
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycast()
        {
            SetCurrentDownRaycast();
        }

        public void OnInitializeDownRayLength()
        {
            InitializeDownRayLength();
        }

        public void OnDoubleDownRayLength()
        {
            DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            SetDownRayLengthToVerticalNewPosition();
        }

        public void OnSetDownRaycastFromLeft()
        {
            SetDownRaycastFromLeft();
        }

        public void OnSetDownRaycastToRight()
        {
            SetDownRaycastToRight();
        }

        public void OnInitializeSmallestDistanceToDownHit()
        {
            InitializeSmallestDistanceToDownHit();
        }

        public void OnSetSmallestDistanceToDownHitDistance()
        {
            SetSmallestDistanceToDownHitDistance();
        }

        public void OnSetDistanceBetweenDownRaycastsAndSmallestDistancePoint()
        {
            SetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
        }

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            SetCurrentDownRaycastOriginPoint();
        }

        public void OnInitialize()
        {
            Initialize();
        }

        #endregion

        #endregion
    }
}