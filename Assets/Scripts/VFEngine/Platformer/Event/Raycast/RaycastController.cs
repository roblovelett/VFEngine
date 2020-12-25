using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        private new Collider2D collider;
        private Raycast raycast;
        private LayerMaskData layerMask;
        private PhysicsData physics;
        private PlatformerData platformer;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            collider = GetComponent<BoxCollider2D>();
            raycast = new Raycast(settings, collider);
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            layerMask = GetComponent<LayerMaskController>().Data;
            physics = GetComponent<PhysicsController>().Data;
            platformer = GetComponent<PlatformerController>().Data;
        }

        #endregion

        
        #endregion

        /*private void PlatformerInitializeFrame()
        {
            r.SetRayOrigins();
        }

        private void PlatformerCastRaysDown()
        {
            CastRaysDown();
        }

        private void CastRaysDown()
        {
            r.InitializeDownIndex();*/
            //for (var i = 0; i < r.VerticalCount; i++)
            //{
                //downRaycastHitColliderController.OnSetHit();
                //if (DownHitConnected)
                //{
                //    downRaycastHitColliderController.OnHitConnected();
                //    downRaycastController.OnHitConnected();
                //    break;
                //}
                /*r.AddToDownIndex();
            }
        }*/

        //private int HorizontalDirection => physics.HorizontalMovementDirection;
        //private bool CastRight => HorizontalDirection == 1;
        //private bool CastLeft => HorizontalDirection == 1;
        //private bool DoNotCast => !CastRight || !CastLeft;

        //private void PlatformerCastRaysToSides()
        //{
            //if (DoNotCast) return;
            //if (CastRight) CastRaysRight();
            //if (CastLeft) CastRaysLeft();
        //}

        //private RaycastHit2D RightHit => rightRaycastHitCollider.Hit;
        //private bool RightHitConnected => DownHit.distance <= 0;
        
        /*private void CastRaysRight()
        {
            r.InitializeRightIndex();
            for (var i = 0; i < r.HorizontalCount; i++)
            {*/
                /*rightRaycastController.OnCastRays();
                if (RightHit)
                {
                    rightRaycastHitColliderController.OnHit();
                }*/
                /*r.AddToRightIndex();
            }
        }

        private void CastRaysLeft()
        {
            r.InitializeLeftIndex();
        }*/

        #endregion

        #region properties
        public RaycastData Data => raycast.Data;

        #region public methods

        /*
        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        public void OnPlatformerCastRaysToSides()
        {
            PlatformerCastRaysToSides();
        }*/

        #endregion

        #endregion
    }
}