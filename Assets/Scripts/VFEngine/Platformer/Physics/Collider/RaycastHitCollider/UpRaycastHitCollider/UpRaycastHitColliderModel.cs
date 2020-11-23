using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "UpRaycastHitColliderModel", menuName = PlatformerUpRaycastHitColliderModelPath,
        order = 0)]
    [InlineEditor]
    public class UpRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Up Raycast Hit Collider Data")] [SerializeField]
        private UpRaycastHitColliderData u = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            u.RuntimeData = u.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .UpRaycastHitColliderRuntimeData;
            u.WasTouchingCeilingLastFrame = false;
            u.UpHitsStorageLength = u.UpHitsStorage.Length;
            u.RuntimeData.SetUpRaycastHitCollider(u.UpHitConnected, u.IsCollidingAbove, u.WasTouchingCeilingLastFrame,
                u.UpHitsStorageLength, u.UpHitsStorageCollidingIndex, u.CurrentUpHitsStorageIndex, u.RaycastUpHitAt);
        }

        private void InitializeModel()
        {
            u.PlatformerRuntimeData = u.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            u.RaycastRuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            u.UpRaycastRuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().UpRaycastRuntimeData;
            u.Transform = u.PlatformerRuntimeData.platformer.Transform;
            u.NumberOfVerticalRaysPerSide = u.RaycastRuntimeData.raycast.NumberOfVerticalRaysPerSide;
            u.CurrentUpRaycastHit = u.UpRaycastRuntimeData.upRaycast.CurrentUpRaycastHit;
            InitializeUpHitsStorageCollidingIndex();
            InitializeUpHitsStorageCurrentIndex();
            InitializeUpHitsStorage();
            ResetState();
        }

        private void InitializeUpHitConnected()
        {
            u.UpHitConnected = false;
        }

        private void InitializeUpHitsStorageCollidingIndex()
        {
            u.UpHitsStorageCollidingIndex = 0;
        }

        private void InitializeUpHitsStorageCurrentIndex()
        {
            u.CurrentUpHitsStorageIndex = 0;
        }

        private void InitializeUpHitsStorage()
        {
            u.UpHitsStorage = new RaycastHit2D[u.NumberOfVerticalRaysPerSide];
        }

        private void AddToUpHitsStorageCurrentIndex()
        {
            u.CurrentUpHitsStorageIndex++;
        }

        private void SetCurrentUpHitsStorage()
        {
            u.UpHitsStorage[u.CurrentUpHitsStorageIndex] = u.CurrentUpRaycastHit;
        }

        private void SetRaycastUpHitAt()
        {
            u.RaycastUpHitAt = u.UpHitsStorage[u.CurrentUpHitsStorageIndex];
        }

        private void SetUpHitConnected()
        {
            u.UpHitConnected = true;
        }

        private void SetUpHitsStorageCollidingIndexAt()
        {
            u.UpHitsStorageCollidingIndex = u.CurrentUpHitsStorageIndex;
        }

        private void SetIsCollidingAbove()
        {
            u.IsCollidingAbove = true;
        }

        private void SetWasTouchingCeilingLastFrame()
        {
            u.WasTouchingCeilingLastFrame = u.IsCollidingAbove;
        }

        private void ResetState()
        {
            InitializeUpHitConnected();
            u.IsCollidingAbove = false;
        }

        #endregion

        #endregion

        #region properties

        public UpRaycastHitColliderRuntimeData RuntimeData => u.RuntimeData;

        #region public methods

        public void OnInitializeUpHitConnected()
        {
            InitializeUpHitConnected();
        }

        public void OnInitializeUpHitsStorageCollidingIndex()
        {
            InitializeUpHitsStorageCollidingIndex();
        }

        public void OnInitializeUpHitsStorageCurrentIndex()
        {
            InitializeUpHitsStorageCurrentIndex();
        }

        public void OnInitializeUpHitsStorage()
        {
            InitializeUpHitsStorage();
        }

        public void OnAddToUpHitsStorageCurrentIndex()
        {
            AddToUpHitsStorageCurrentIndex();
        }

        public void OnSetCurrentUpHitsStorage()
        {
            SetCurrentUpHitsStorage();
        }

        public void OnSetRaycastUpHitAt()
        {
            SetRaycastUpHitAt();
        }

        public void OnSetUpHitConnected()
        {
            SetUpHitConnected();
        }

        public void OnSetUpHitsStorageCollidingIndexAt()
        {
            SetUpHitsStorageCollidingIndexAt();
        }

        public void OnSetIsCollidingAbove()
        {
            SetIsCollidingAbove();
        }

        public void OnSetWasTouchingCeilingLastFrame()
        {
            SetWasTouchingCeilingLastFrame();
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

        #endregion
    }
}