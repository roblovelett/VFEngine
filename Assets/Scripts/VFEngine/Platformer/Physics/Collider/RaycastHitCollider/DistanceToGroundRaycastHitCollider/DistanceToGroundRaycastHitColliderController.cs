using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
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