using UnityEngine;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        private BoxCollider2D boxCollider;
        private DownRaycastController downRaycastController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private RaycastData r;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private PlatformerData platformer;

        #endregion

        #region internal

        private RaycastHit2D Hit => downRaycastHitCollider.Collision.hit;
        private bool HitConnected => Hit.distance <= 0;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            downRaycastController = GetComponent<DownRaycastController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
        }

        private void InitializeData()
        {
            r = new RaycastData();
            r.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            downRaycastHitCollider = downRaycastHitColliderController.Data;
        }

        private void Initialize()
        {
            r.Initialize(settings, boxCollider);
        }

        #endregion

        private void PlatformerInitializeFrame()
        {
            r.SetRayOrigins();
        }

        private void PlatformerCastRaysDown()
        {
            CastRaysDown();
        }

        private void CastRaysDown()
        {
            r.InitializeDownIndex();
            for (var i = 0; i < r.VerticalRayCount; i++)
            {
                downRaycastHitColliderController.OnSetHit();
                if (HitConnected)
                {
                    downRaycastHitColliderController.OnHitConnected();
                    downRaycastController.OnHitConnected();
                    break;
                }

                r.AddToDownIndex();
            }
        }

        #endregion

        #endregion

        #region properties

        public RaycastData Data => r;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        #endregion

        #endregion
    }
}