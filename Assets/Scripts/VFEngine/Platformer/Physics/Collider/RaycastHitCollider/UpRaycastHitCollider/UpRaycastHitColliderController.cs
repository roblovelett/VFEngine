using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable MemberCanBeMadeStatic.Local
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    public class UpRaycastHitColliderController : MonoBehaviour
    {
        #region fields

        private UpRaycastHitColliderData u;

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
            u = new UpRaycastHitColliderData();
            u.Initialize();
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
            u.Reset();
        }

        #endregion

        #endregion

        #region properties

        public UpRaycastHitColliderData Data => u;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        
        #endregion

        #endregion
    }
}