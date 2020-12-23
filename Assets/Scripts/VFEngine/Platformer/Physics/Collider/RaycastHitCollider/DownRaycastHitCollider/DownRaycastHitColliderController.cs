using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable MemberCanBeMadeStatic.Local
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static Vector2;
    using static Physics2D;

    public class DownRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        private RaycastController raycastController;
        private DownRaycastController downRaycastController;
        private LayerMaskController layerMaskController;
        private PlatformerController platformerController;
        private DownRaycastHitColliderData d;
        private RaycastData raycast;
        private DownRaycastData downRaycast;
        private LayerMaskData layerMask;
        private PlatformerData platformer;

        #region dependencies

        #endregion

        #region internal

        private bool SetHitOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private float SkinWidth => raycast.SkinWidth;
        private float IgnorePlatformsTime => platformer.IgnorePlatformsTime;
        private Vector2 Origin => downRaycast.Origin;
        private LayerMask Collision => layerMask.Collision;
        private LayerMask OneWayPlatform => layerMask.OneWayPlatform;
        private RaycastHit2D Hit => Raycast(Origin, down, SkinWidth, Collision);
        private RaycastHit2D HitOneWayPlatform => Raycast(Origin, down, SkinWidth * 4f, OneWayPlatform);

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
            downRaycastController = GetComponent<DownRaycastController>();
            layerMaskController = GetComponent<LayerMaskController>();
            platformerController = GetComponent<PlatformerController>();
        }

        private void InitializeData()
        {
            d = new DownRaycastHitColliderData();
            d.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            downRaycast = downRaycastController.Data;
            layerMask = layerMaskController.Data;
            platformer = platformerController.Data;
        }

        private void Initialize()
        {
        }

        #endregion

        private void PlatformerInitializeFrame()
        {
            d.Collision.Reset();
        }

        private void SetHit()
        {
            d.Hit = Hit;
            if (SetHitOneWayPlatform) d.Hit = HitOneWayPlatform;
        }

        private void HitConnected()
        {
            d.OnHitConnected();
        }

        private void PlatformerDescendSlope()
        {
            d.OnCollision();
        }

        private void PlatformerClimbSlope()
        {
            d.OnCollision();
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

        public void OnSetHit()
        {
            SetHit();
        }

        public void OnHitConnected()
        {
            HitConnected();
        }

        public void OnPlatformerDescendSlope()
        {
            PlatformerDescendSlope();
        }
        
        public void OnPlatformerClimbSlope()
        {
            PlatformerClimbSlope();
        }

        #endregion

        #endregion
    }
}