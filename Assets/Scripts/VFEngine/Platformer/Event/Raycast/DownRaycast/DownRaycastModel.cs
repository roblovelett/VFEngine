using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static Mathf;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DownRaycastModel", menuName = PlatformerDownRaycastModelPath, order = 0)]
    [InlineEditor]
    public class DownRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Down Raycast Data")] [SerializeField]
        private DownRaycastData d = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            d.RuntimeData = d.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            d.RuntimeData.SetDownRaycast(d.DownRayLength, d.CurrentDownRaycastOrigin, d.DownRaycastFromLeft,
                d.DownRaycastToRight, d.CurrentDownRaycastHit);
        }

        private void InitializeModel()
        {
            d.Transform = d.RuntimeData.platformer.Transform;
            d.DrawRaycastGizmosControl = d.RuntimeData.raycast.DrawRaycastGizmosControl;
            d.CurrentDownHitsStorageIndex = d.RuntimeData.downRaycastHitCollider.CurrentDownHitsStorageIndex;
            d.NumberOfVerticalRaysPerSide = d.RuntimeData.raycast.NumberOfVerticalRaysPerSide;
            d.RayOffset = d.RuntimeData.raycast.RayOffset;
            d.BoundsHeight = d.RuntimeData.raycast.BoundsHeight;
            d.NewPosition = d.RuntimeData.physics.NewPosition;
            d.BoundsBottomLeftCorner = d.RuntimeData.raycast.BoundsBottomLeftCorner;
            d.BoundsBottomRightCorner = d.RuntimeData.raycast.BoundsBottomRightCorner;
            d.BoundsTopLeftCorner = d.RuntimeData.raycast.BoundsTopLeftCorner;
            d.BoundsTopRightCorner = d.RuntimeData.raycast.BoundsTopRightCorner;
            d.RaysBelowLayerMaskPlatformsWithoutOneWay =
                d.RuntimeData.layerMask.RaysBelowLayerMaskPlatformsWithoutOneWay;
            d.RaysBelowLayerMaskPlatforms = d.RuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
        }

        private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
                d.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, d.DrawRaycastGizmosControl);
        }

        private void SetCurrentDownRaycast()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
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

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            SetCurrentDownRaycastOriginPoint();
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