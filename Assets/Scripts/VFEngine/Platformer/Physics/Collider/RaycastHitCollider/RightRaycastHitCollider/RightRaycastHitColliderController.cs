using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable MemberCanBeMadeStatic.Local
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    public class RightRaycastHitColliderController : MonoBehaviour
    {
        #region fields

        private RightRaycastHitColliderData r;

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
            r = new RightRaycastHitColliderData();
            r.Initialize();
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
            r.Reset();
        }
        #endregion

        #endregion

        #region properties

        public RightRaycastHitColliderData Data => r;

        #region public methods
        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        #endregion

        #endregion
    }
}