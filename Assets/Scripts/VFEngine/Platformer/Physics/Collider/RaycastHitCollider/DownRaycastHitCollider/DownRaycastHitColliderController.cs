using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable MemberCanBeMadeStatic.Local
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        private DownRaycastHitColliderData d;

        #region dependencies

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
        }

        private void InitializeData()
        {
            d = new DownRaycastHitColliderData();
            d.Initialize();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
        }

        private void Initialize()
        {
        }

        #endregion
        private void PlatformerInitializeFrame()
        {
            d.Reset();
        }
        #endregion

        #endregion

        #region properties

        public DownRaycastHitColliderData Data => d;

        #region public methods
        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        #endregion

        #endregion
    }
}