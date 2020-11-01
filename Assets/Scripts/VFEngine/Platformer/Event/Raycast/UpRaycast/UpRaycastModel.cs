using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;

    [CreateAssetMenu(fileName = "UpRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/UpRaycast/Up Raycast Model", order = 0)]
    public class UpRaycastModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Up Raycast Data")] [SerializeField]
        private UpRaycastData upRc;

        /* fields: methods */
        private void InitializeUpRaycastLength()
        {
            upRc.UpRayLength = upRc.IsGrounded ? upRc.RayOffset : upRc.NewPosition.y;
        }

        private void InitializeUpRaycastStart()
        {
            upRc.UpRaycastStart = SetPoint(upRc.BoundsBottomLeftCorner, upRc.BoundsTopLeftCorner, upRc.Transform,
                upRc.NewPosition.x);
        }

        private void InitializeUpRaycastEnd()
        {
            upRc.UpRaycastEnd = SetPoint(upRc.BoundsBottomRightCorner, upRc.BoundsTopRightCorner, upRc.Transform,
                upRc.NewPosition.y);
        }

        private static Vector2 SetPoint(Vector2 bounds1, Vector2 bounds2, Transform t, float axis)
        {
            return OnSetBounds(bounds1, bounds2) * 2 + (Vector2) t.right * axis;
        }

        private void InitializeUpRaycastSmallestDistance()
        {
            upRc.UpRaycastSmallestDistance = MaxValue;
        }

        private void SetCurrentUpRaycastOrigin()
        {
            upRc.CurrentUpRaycastOrigin = OnSetCurrentRaycastOrigin(upRc.UpRaycastStart, upRc.UpRaycastEnd,
                upRc.CurrentUpHitsStorageIndex, upRc.NumberOfVerticalRaysPerSide);
        }

        private void SetCurrentUpRaycast()
        {
            upRc.CurrentUpRaycast = Raycast(upRc.CurrentUpRaycastOrigin, upRc.Transform.up, upRc.UpRayLength,
                upRc.PlatformMask & ~ upRc.OneWayPlatformMask & ~ upRc.MovingOneWayPlatformMask, cyan,
                upRc.DrawRaycastGizmos);
        }

        private void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            upRc.UpRaycastSmallestDistance = upRc.RaycastUpHitAt.distance;
        }

        /* properties: methods */
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
    }
}