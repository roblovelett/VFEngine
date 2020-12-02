using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static UniTaskExtensions;

    public class UpRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private RaycastController raycastController;
        private UpRaycastController upRaycastController;
        private UpRaycastHitColliderData u;
        private RaycastData raycast;
        private UpRaycastData upRaycast;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            u = new UpRaycastHitColliderData();
        }

        private void SetControllers()
        {
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            upRaycastController = character.GetComponentNoAllocation<UpRaycastController>();
        }

        private void Start()
        {
            SetDependencies();
            InitializeFrame();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            upRaycast = upRaycastController.Data;
        }

        private void InitializeFrame()
        {
            InitializeUpHitsStorage();
            u.UpHitsStorageLength = u.UpHitsStorage.Length;
            InitializeUpHitsStorageCollidingIndex();
            InitializeUpHitsStorageCurrentIndex();
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

        public async UniTaskVoid OnInitializeUpHitConnected()
        {
            InitializeUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeUpHitsStorageCollidingIndex()
        {
            InitializeUpHitsStorageCollidingIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeUpHitsStorageCurrentIndex()
        {
            InitializeUpHitsStorageCurrentIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        public async UniTaskVoid OnSetUpHitConnected()
        {
            SetUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetUpHitsStorageCollidingIndexAt()
        {
            SetUpHitsStorageCollidingIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetIsCollidingAbove()
        {
            SetIsCollidingAbove();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetWasTouchingCeilingLastFrame()
        {
            SetWasTouchingCeilingLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}