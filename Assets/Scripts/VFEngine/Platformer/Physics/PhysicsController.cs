using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics
{
    using static Quaternion;
    using static Time;
    using static Mathf;
    using static Vector2;
    using static ScriptableObject;
    using static UniTaskExtensions;
    using static GameObject;
    using static RaycastDirection;

    public class PhysicsController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings;
        private GameObject character;
        private PhysicsData p;

        #endregion

        #region internal

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
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            p = new PhysicsData();
            p.ApplySettings(settings);
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
            p.Initialize();
        }

        #endregion

        private void PlatformerInitializeFrame()
        {
            p.ApplyDeltaMoveToHorizontalMovementDirection();
        }
        
        #endregion

        #endregion

        #region properties

        public PhysicsData Data => p;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        #endregion

        #endregion
    }
}