using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "UpRaycastModel", menuName = PlatformerUpRaycastModelPath, order = 0)]
    [InlineEditor]
    public class UpRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Up Raycast Data")] [SerializeField]
        private UpRaycastData u = null;

        #endregion

        #region private methods

        private void InitializeUpRaycastLength()
        {
            u.upRayLength = u.GroundedEvent ? u.RayOffset : u.NewPosition.y;
        }

        private void InitializeUpRaycastStart()
        {
            u.UpRaycastStart = SetPoint(u.BoundsBottomLeftCorner, u.BoundsTopLeftCorner, u.Transform, u.NewPosition.x);
        }

        private void InitializeUpRaycastEnd()
        {
            u.UpRaycastEnd = SetPoint(u.BoundsBottomRightCorner, u.BoundsTopRightCorner, u.Transform, u.NewPosition.y);
        }

        private static Vector2 SetPoint(Vector2 bounds1, Vector2 bounds2, Transform t, float axis)
        {
            return OnSetBounds(bounds1, bounds2) * 2 + (Vector2) t.right * axis;
        }

        private void InitializeUpRaycastSmallestDistance()
        {
            u.UpRaycastSmallestDistance = MaxValue;
        }

        private void SetCurrentUpRaycastOrigin()
        {
            u.CurrentUpRaycastOrigin = OnSetCurrentRaycastOrigin(u.UpRaycastStart, u.UpRaycastEnd,
                u.CurrentUpHitsStorageIndex, u.NumberOfVerticalRaysPerSide);
        }

        private void SetCurrentUpRaycast()
        {
            var hit = Raycast(u.CurrentUpRaycastOrigin, u.Transform.up, u.upRayLength,
                u.PlatformMask & ~ u.OneWayPlatformMask & ~ u.MovingOneWayPlatformMask, cyan, u.DrawRaycastGizmos);
            u.CurrentUpRaycast = OnSetRaycast(hit);
        }

        private void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            u.UpRaycastSmallestDistance = u.RaycastUpHitAt.distance;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnInitializeUpRaycastLength()
        {
            InitializeUpRaycastLength();
        }

        public void OnInitializeUpRaycastStart()
        {
            InitializeUpRaycastStart();
        }

        public void OnInitializeUpRaycastEnd()
        {
            InitializeUpRaycastEnd();
        }

        public void OnInitializeUpRaycastSmallestDistance()
        {
            InitializeUpRaycastSmallestDistance();
        }

        public void OnSetCurrentUpRaycastOrigin()
        {
            SetCurrentUpRaycastOrigin();
        }

        public void OnSetCurrentUpRaycast()
        {
            SetCurrentUpRaycast();
        }

        public void OnSetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            SetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }

        #endregion

        #endregion
    }
}