using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static Single;
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;

   
    public class UpRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private UpRaycastData u;
        private PhysicsData physics;
        private RaycastData raycast;
        private UpRaycastHitColliderData upRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods
        
        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            //if (p.DisplayWarningsControl) GetWarningMessages();
        }
        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            u = new UpRaycastData();
            if (!raycastController && character)
            {
                raycastController = character.GetComponent<RaycastController>();
            }
            else if (raycastController && !character)
            {
                character = raycastController.Character;
                raycastController = character.GetComponent<RaycastController>();
            }

            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastHitColliderController)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            upRaycastHitCollider = raycastHitColliderController.UpRaycastHitColliderModel.Data;
            downRaycastHitCollider = raycastHitColliderController.DownRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void InitializeUpRaycastLength()
        {
            u.UpRayLength = downRaycastHitCollider.GroundedEvent ? raycast.RayOffset : physics.NewPosition.y;
        }

        private void InitializeUpRaycastStart()
        {
            u.UpRaycastStart = SetPoint(raycast.BoundsBottomLeftCorner, raycast.BoundsTopLeftCorner, physics.Transform,
                physics.NewPosition.x);
        }

        private void InitializeUpRaycastEnd()
        {
            u.UpRaycastEnd = SetPoint(raycast.BoundsBottomRightCorner, raycast.BoundsTopRightCorner, physics.Transform,
                physics.NewPosition.y);
        }

        private static Vector2 SetPoint(Vector2 bounds1, Vector2 bounds2, Transform t, float axis)
        {
            return OnSetBounds(bounds1, bounds2) * 2 + (Vector2) t.right * axis;
        }

        private void InitializeUpRaycastSmallestDistance()
        {
            u.UpRaycastSmallestDistance = MaxValue;
        }

        private void SetCurrentUpRaycastOrigin()
        {
            u.CurrentUpRaycastOrigin = OnSetCurrentRaycastOrigin(u.UpRaycastStart, u.UpRaycastEnd,
                upRaycastHitCollider.CurrentUpHitsStorageIndex, raycast.NumberOfVerticalRaysPerSide);
        }

        private void SetCurrentUpRaycast()
        {
            u.CurrentUpRaycastHit = Raycast(u.CurrentUpRaycastOrigin, physics.Transform.up, u.UpRayLength,
                layerMask.PlatformMask & ~ layerMask.OneWayPlatformMask & ~ layerMask.MovingOneWayPlatformMask, cyan,
                raycast.DrawRaycastGizmosControl);
        }

        private void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            u.UpRaycastSmallestDistance = upRaycastHitCollider.RaycastUpHitAt.distance;
        }

        #endregion

        #endregion

        #region properties

        public UpRaycastData Data => u;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnInitializeUpRaycastLength()
        {
            InitializeUpRaycastLength();
        }

        public void OnInitializeUpRaycastStart()
        {
            InitializeUpRaycastStart();
        }

        public void OnInitializeUpRaycastEnd()
        {
            InitializeUpRaycastEnd();
        }

        public void OnInitializeUpRaycastSmallestDistance()
        {
            InitializeUpRaycastSmallestDistance();
        }

        public void OnSetCurrentUpRaycastOrigin()
        {
            SetCurrentUpRaycastOrigin();
        }

        public void OnSetCurrentUpRaycast()
        {
            SetCurrentUpRaycast();
        }

        public void OnSetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            SetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }

        #endregion

        #endregion
    }
}