using UnityEngine;
using VFEngine.Platformer.Physics;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static Mathf;
    using static Vector2;
    public class RightRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private RaycastController raycastController;
        private PhysicsController physicsController;
        private RightRaycastData r;
        private RaycastData raycast;
        private PhysicsData physics;

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
            raycastController = GetComponent<RaycastController>();
            physicsController = GetComponent<PhysicsController>();
        }

        private void InitializeData()
        {
            r = new RightRaycastData();
            r.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            physics = physicsController.Data;
        }

        private float HorizontalDeltaMovement => physics.DeltaMovementX;
        private float SkinWidth => raycast.SkinWidth;
        private float RayLength => Abs(HorizontalDeltaMovement) + SkinWidth;
        private Vector2 BottomRight => raycast.Bounds.BottomRight;
        private float RaySpacing => raycast.HorizontalRaySpacing;
        private int Index => raycast.RightIndex;
        private Vector2 Origin => BottomRight + up * RaySpacing * Index;
        private void Initialize()
        {
            r.RayLength = RayLength;
            r.Origin = Origin;
        }

        #endregion
        
        #endregion

        #endregion

        #region properties

        public RightRaycastData Data => r;

        #region public methods

        #endregion

        #endregion
    }
}