using UnityEngine;

// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    public class LeftRaycastHitColliderController : MonoBehaviour
    {
        #region fields

        private LeftRaycastHitColliderData l;

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
            l = new LeftRaycastHitColliderData();
            l.Initialize();
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
            l.Reset();
        }
        #endregion

        #endregion

        #region properties

        public LeftRaycastHitColliderData Data => l;

        #region public methods
        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        #endregion

        #endregion
    }
}