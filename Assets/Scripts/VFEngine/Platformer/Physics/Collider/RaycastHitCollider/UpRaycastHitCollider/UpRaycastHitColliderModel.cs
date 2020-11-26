using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

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

        [SerializeField] private UpRaycastHitColliderData u;
        [SerializeField] private RaycastController raycastController;
        private RaycastData raycast;
        private UpRaycastData upRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!u) u = CreateInstance<UpRaycastHitColliderData>();
            u.UpHitsStorageLength = u.UpHitsStorage.Length;
        }

        private void InitializeModel()
        {
            raycast = raycastController.RaycastModel.Data;
            upRaycast = raycastController.UpRaycastModel.Data;
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
            u.UpHitsStorage = new RaycastHit2D[raycast.NumberOfVerticalRaysPerSide];
        }

        private void AddToUpHitsStorageCurrentIndex()
        {
            u.CurrentUpHitsStorageIndex++;
        }

        private void SetCurrentUpHitsStorage()
        {
            u.UpHitsStorage[u.CurrentUpHitsStorageIndex] = upRaycast.CurrentUpRaycastHit;
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

        public UpRaycastHitColliderData Data => u;

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

        #endregion

        #endregion
    }
}