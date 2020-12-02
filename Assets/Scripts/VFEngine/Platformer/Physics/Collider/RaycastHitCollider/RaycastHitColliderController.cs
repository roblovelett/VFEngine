using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObject;
    using static UniTaskExtensions;

    public class RaycastHitColliderController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private RightRaycastController rightRaycastController;
        private LeftRaycastController leftRaycastController;
        private RaycastHitColliderData r;
        private RightRaycastData rightRaycast;
        private LeftRaycastData leftRaycast;

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
            r = new RaycastHitColliderData {ContactList = CreateInstance<RaycastHitColliderContactList>()};
        }

        private void SetControllers()
        {
            rightRaycastController = character.GetComponentNoAllocation<RightRaycastController>();
            leftRaycastController = character.GetComponentNoAllocation<LeftRaycastController>();
        }

        private void Start()
        {
            SetDependencies();
            InitializeFrame();
        }

        private void SetDependencies()
        {
            rightRaycast = rightRaycastController.Data;
            leftRaycast = leftRaycastController.Data;
        }

        private void InitializeFrame()
        {
            ClearContactList();
        }

        private void AddRightHitToContactList()
        {
            r.ContactList.Add(rightRaycast.CurrentRightRaycastHit);
        }

        private void AddLeftHitToContactList()
        {
            r.ContactList.Add(leftRaycast.CurrentLeftRaycastHit);
        }

        private void ClearContactList()
        {
            r.ContactList.Clear();
        }

        private void ResetState()
        {
            ClearContactList();
        }

        #endregion

        #endregion

        #region properties

        public RaycastHitColliderData Data => r;

        #region public methods

        public void OnAddRightHitToContactList()
        {
            AddRightHitToContactList();
        }

        public void OnAddLeftHitToContactList()
        {
            AddLeftHitToContactList();
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