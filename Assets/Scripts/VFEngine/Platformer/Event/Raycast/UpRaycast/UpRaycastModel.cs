using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

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

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            u.RuntimeData = u.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            u.RuntimeData.SetUpRaycast(u.UpRaycastSmallestDistance, u.CurrentUpRaycastOrigin, u.CurrentUpRaycastHit);
        }

        private void InitializeModel()
        {
            u.Transform = u.RuntimeData.platformer.Transform;
            u.DrawRaycastGizmosControl = u.RuntimeData.raycast.DrawRaycastGizmosControl;
            u.GroundedEvent = u.RuntimeData.downRaycastHitCollider.GroundedEvent;
            u.CurrentUpHitsStorageIndex = u.RuntimeData.upRaycastHitCollider.CurrentUpHitsStorageIndex;
            u.NumberOfVerticalRaysPerSide = u.RuntimeData.raycast.NumberOfVerticalRaysPerSide;
            u.RayOffset = u.RuntimeData.raycast.RayOffset;
            u.NewPosition = u.RuntimeData.physics.NewPosition;
            u.BoundsBottomLeftCorner = u.RuntimeData.raycast.BoundsBottomLeftCorner;
            u.BoundsBottomRightCorner = u.RuntimeData.raycast.BoundsBottomRightCorner;
            u.BoundsTopLeftCorner = u.RuntimeData.raycast.BoundsTopLeftCorner;
            u.BoundsTopRightCorner = u.RuntimeData.raycast.BoundsTopRightCorner;
            u.PlatformMask = u.RuntimeData.layerMask.PlatformMask;
            u.OneWayPlatformMask = u.RuntimeData.layerMask.OneWayPlatformMask;
            u.MovingOneWayPlatformMask = u.RuntimeData.layerMask.MovingOneWayPlatformMask;
            u.RaycastUpHitAt = u.RuntimeData.upRaycastHitCollider.RaycastUpHitAt;
        }

        private void InitializeUpRaycastLength()
        {
            u.UpRayLength = u.GroundedEvent ? u.RayOffset : u.NewPosition.y;
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
            u.CurrentUpRaycastHit = Raycast(u.CurrentUpRaycastOrigin, u.Transform.up, u.UpRayLength,
                u.PlatformMask & ~ u.OneWayPlatformMask & ~ u.MovingOneWayPlatformMask, cyan,
                u.DrawRaycastGizmosControl);
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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}