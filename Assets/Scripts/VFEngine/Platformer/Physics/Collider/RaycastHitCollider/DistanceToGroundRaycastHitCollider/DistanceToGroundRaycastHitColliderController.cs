using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static UniTaskExtensions;

    public class DistanceToGroundRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private RaycastController raycastController;
        private DistanceToGroundRaycastController distanceToGroundRaycastController;
        private DistanceToGroundRaycastHitColliderData d;
        private RaycastData raycast;
        private DistanceToGroundRaycastData distanceToGroundRaycast;

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
            d = new DistanceToGroundRaycastHitColliderData();
        }

        private void SetControllers()
        {
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            distanceToGroundRaycastController = character.GetComponentNoAllocation<DistanceToGroundRaycastController>();
        }

        private void Start()
        {
            SetDependencies();
            ResetState();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            distanceToGroundRaycast = distanceToGroundRaycastController.Data;
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
            d.DistanceToGround = raycast.DistanceToGroundRayMaximumLength;
        }

        private void DecreaseDistanceToGround()
        {
            d.DistanceToGround -= 1f;
        }

        private void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            d.DistanceToGround = distanceToGroundRaycast.DistanceToGroundRaycastHit.distance - raycast.BoundsHeight / 2;
        }

        #endregion

        #endregion

        #region properties

        public DistanceToGroundRaycastHitColliderData Data => d;

        #region public methods

        public async UniTaskVoid OnSetDistanceToGroundRaycastHit()
        {
            SetDistanceToGroundRaycastHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeDistanceToGround()
        {
            InitializeDistanceToGround();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnDecreaseDistanceToGround()
        {
            DecreaseDistanceToGround();
        }

        public void OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
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