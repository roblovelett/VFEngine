using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastModel", menuName = PlatformerDistanceToGroundRaycastModelPath,
        order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Data")] [SerializeField]
        private DistanceToGroundRaycastData d = null;

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
            d.RuntimeData.SetDistanceToGroundRaycast(d.DistanceToGroundRaycast);
        }

        private void InitializeModel()
        {
            d.Transform = d.RuntimeData.platformer.Transform;
            d.DrawRaycastGizmosControl = d.RuntimeData.raycast.DrawRaycastGizmosControl;
            d.BelowSlopeAngle = d.RuntimeData.stickyRaycastHitCollider.BelowSlopeAngle;
            d.DistanceToGroundRayMaximumLength = d.RuntimeData.raycast.DistanceToGroundRayMaximumLength;
            d.BoundsBottomLeftCorner = d.RuntimeData.raycast.BoundsBottomLeftCorner;
            d.BoundsBottomRightCorner = d.RuntimeData.raycast.BoundsBottomRightCorner;
            d.BoundsCenter = d.RuntimeData.raycast.BoundsCenter;
            d.RaysBelowLayerMaskPlatforms = d.RuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
        }

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
                d.DistanceToGroundRayMaximumLength, d.RaysBelowLayerMaskPlatforms, blue, d.DrawRaycastGizmosControl);
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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}