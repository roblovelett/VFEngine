using UnityEngine;
using VFEngine.Platformer.Physics;


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
        private PhysicsController physicsController;
        //private RaycastData r;
        //private PhysicsData physics;
        private PlatformerData platformer;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            //SetControllers();
            //InitializeData();
        }

        /*private void SetControllers()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            physicsController = GetComponent<PhysicsController>();
        }*/

        //private void InitializeData()
        //{
            /*r = new RaycastData();
            r.InitializeData();*/
        //}

        private void Start()
        {
        //    SetDependencies();
        //    Initialize();
        }

        //private void SetDependencies()
        //{
            //if (!settings) settings = CreateInstance<RaycastSettings>();
            //physics = physicsController.Data;
        //}

        //private void Initialize()
        //{
            //r.Initialize(settings, boxCollider);
        //}

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

        #endregion

        #region properties

        //public RaycastData Data => r;

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