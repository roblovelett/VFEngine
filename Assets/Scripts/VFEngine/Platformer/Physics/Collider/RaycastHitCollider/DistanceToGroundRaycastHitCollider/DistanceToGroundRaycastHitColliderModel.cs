using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastHitColliderModel",
        menuName = PlatformerDistanceToGroundRaycastHitColliderModelPath, order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Hit Collider Data")] [SerializeField]
        private DistanceToGroundRaycastHitColliderData d = null;

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
            d.RuntimeData.SetDistanceToGroundRaycastHitCollider(d.DistanceToGroundRaycastHitConnected);
        }

        private void InitializeModel()
        {
            d.DistanceToGroundRayMaximumLength = d.RuntimeData.raycast.DistanceToGroundRayMaximumLength;
            d.DistanceToGroundRaycastHit = d.RuntimeData.distanceToGroundRaycast.DistanceToGroundRaycastHit;
            d.BoundsHeight = d.RuntimeData.raycast.BoundsHeight;
            ResetState();
        }

        private void SetDistanceToGroundRaycastHit()
        {
            d.DistanceToGroundRaycastHitConnected = true;
        }

        private void ResetState()
        {
            d.DistanceToGroundRaycastHitConnected = false;
            InitializeDistanceToGround();
        }

        private void InitializeDistanceToGround()
        {
            d.DistanceToGround = d.DistanceToGroundRayMaximumLength;
        }

        private void DecreaseDistanceToGround()
        {
            d.DistanceToGround -= 1f;
        }

        private void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            d.DistanceToGround = d.DistanceToGroundRaycastHit.distance - d.BoundsHeight / 2;
        }

        #endregion

        #endregion

        #region public methods

        public void OnSetDistanceToGroundRaycastHit()
        {
            SetDistanceToGroundRaycastHit();
        }

        public void OnInitializeDistanceToGround()
        {
            InitializeDistanceToGround();
        }

        public void OnDecreaseDistanceToGround()
        {
            DecreaseDistanceToGround();
        }

        public void OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion
    }
}