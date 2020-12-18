using UnityEngine;

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
        private RaycastData r;

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
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            r = new RaycastData();
            r.ApplySettings(settings);
            r.Initialize(boxCollider);
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
            r.SetRayOrigins();
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

        #endregion

        #endregion
    }
}