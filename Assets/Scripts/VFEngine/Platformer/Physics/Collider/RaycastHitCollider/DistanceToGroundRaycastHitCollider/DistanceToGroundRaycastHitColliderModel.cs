using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

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

        [SerializeField] private GameObject character;
        private DistanceToGroundRaycastHitColliderData d;

        #endregion

        #region private methods

        private void InitializeData()
        {
            d = new DistanceToGroundRaycastHitColliderData {Character = character};
            d.RuntimeData =
                DistanceToGroundRaycastHitColliderRuntimeData.CreateInstance(d.DistanceToGroundRaycastHitConnected);
        }

        private void InitializeModel()
        {
            d.RaycastRuntimeData = d.Character.GetComponentNoAllocation<RaycastController>().RaycastRuntimeData;
            d.DistanceToGroundRaycastRuntimeData = d.Character.GetComponentNoAllocation<RaycastController>()
                .DistanceToGroundRaycastRuntimeData;
            d.DistanceToGroundRayMaximumLength = d.RaycastRuntimeData.DistanceToGroundRayMaximumLength;
            d.BoundsHeight = d.RaycastRuntimeData.BoundsHeight;
            d.DistanceToGroundRaycastHit = d.DistanceToGroundRaycastRuntimeData.DistanceToGroundRaycastHit;
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

        #region properties

        public DistanceToGroundRaycastHitColliderRuntimeData RuntimeData => d.RuntimeData;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

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

        #endregion

        #endregion
    }
}