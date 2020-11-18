using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastModel", menuName = PlatformerLeftRaycastModelPath, order = 0)]
    [InlineEditor]
    public class LeftRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Left Raycast Data")] [SerializeField]
        private LeftRaycastData l = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            l.RuntimeData = l.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            l.RuntimeData.SetLeftRaycast(l.LeftRayLength, l.LeftRaycastFromBottomOrigin, l.LeftRaycastToTopOrigin, l.CurrentLeftRaycastHit);
        }

        private void InitializeModel()
        {
            l.Transform = l.RuntimeData.platformer.Transform;
            l.DrawRaycastGizmosControl = l.RuntimeData.raycast.DrawRaycastGizmosControl;
            l.NumberOfHorizontalRaysPerSide = l.RuntimeData.raycast.NumberOfHorizontalRaysPerSide;
            l.CurrentLeftHitsStorageIndex = l.RuntimeData.leftRaycastHitCollider.CurrentLeftHitsStorageIndex;
            l.RayOffset = l.RuntimeData.raycast.RayOffset;
            l.ObstacleHeightTolerance = l.RuntimeData.raycast.ObstacleHeightTolerance;
            l.BoundsWidth = l.RuntimeData.raycast.BoundsWidth;
            l.BoundsBottomLeftCorner = l.RuntimeData.raycast.BoundsBottomLeftCorner;
            l.BoundsBottomRightCorner = l.RuntimeData.raycast.BoundsBottomRightCorner;
            l.BoundsTopLeftCorner = l.RuntimeData.raycast.BoundsTopLeftCorner;
            l.BoundsTopRightCorner = l.RuntimeData.raycast.BoundsTopRightCorner;
            l.Speed = l.RuntimeData.physics.Speed;
            l.PlatformMask = l.RuntimeData.layerMask.PlatformMask;
            l.OneWayPlatformMask = l.RuntimeData.layerMask.OneWayPlatformMask;
            l.MovingOneWayPlatformMask = l.RuntimeData.layerMask.MovingOneWayPlatformMask;
        }
        private void SetLeftRaycastFromBottomOrigin()
        {
            l.LeftRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(l.BoundsBottomRightCorner,
                l.BoundsBottomLeftCorner, l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetLeftRaycastToTopOrigin()
        {
            l.LeftRaycastToTopOrigin = OnSetRaycastToTopOrigin(l.BoundsTopLeftCorner, l.BoundsTopRightCorner,
                l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetCurrentLeftRaycastOrigin()
        {
            l.CurrentLeftRaycastOrigin = OnSetCurrentRaycastOrigin(l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, l.CurrentLeftHitsStorageIndex, l.NumberOfHorizontalRaysPerSide);
        }

        private void InitializeLeftRaycastLength()
        {
            l.LeftRayLength = OnSetHorizontalRayLength(l.Speed.x, l.BoundsWidth, l.RayOffset);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength, l.PlatformMask, red,
                l.DrawRaycastGizmosControl);
        }

        private void SetCurrentLeftRaycast()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength,
                l.PlatformMask & ~l.OneWayPlatformMask & ~l.MovingOneWayPlatformMask, red, l.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetLeftRaycastFromBottomOrigin()
        {
            SetLeftRaycastFromBottomOrigin();
        }

        public void OnSetLeftRaycastToTopOrigin()
        {
            SetLeftRaycastToTopOrigin();
        }

        public void OnInitializeLeftRaycastLength()
        {
            InitializeLeftRaycastLength();
        }

        public void OnSetCurrentLeftRaycastOrigin()
        {
            SetCurrentLeftRaycastOrigin();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
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