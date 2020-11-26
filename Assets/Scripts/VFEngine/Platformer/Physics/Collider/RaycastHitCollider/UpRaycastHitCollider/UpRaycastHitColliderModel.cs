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

        [SerializeField] private UpRaycastHitColliderData u;

        #endregion

        #region private methods

        private void InitializeData()
        {
            u = new UpRaycastHitColliderData {Character = character};
            u.UpHitsStorageLength = u.UpHitsStorage.Length;
            u.RuntimeData = UpRaycastHitColliderRuntimeData.CreateInstance(u.UpHitConnected, u.IsCollidingAbove,
                u.WasTouchingCeilingLastFrame, u.UpHitsStorageLength, u.UpHitsStorageCollidingIndex,
                u.CurrentUpHitsStorageIndex, u.RaycastUpHitAt);
        }

        private void InitializeModel()
        {
            u.PhysicsRuntimeData = u.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            u.RaycastRuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().RaycastRuntimeData;
            u.UpRaycastRuntimeData = u.Character.GetComponentNoAllocation<RaycastController>().UpRaycastRuntimeData;
            u.Transform = u.PhysicsRuntimeData.Transform;
            u.NumberOfVerticalRaysPerSide = u.RaycastRuntimeData.NumberOfVerticalRaysPerSide;
            u.CurrentUpRaycastHit = u.UpRaycastRuntimeData.CurrentUpRaycastHit;
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