using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static Vector2;
    using static MathsExtensions;
    using static Mathf;
    using static Time;
    using static ScriptableObject;
    
    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastModel raycastModel;
        [SerializeField] private UpRaycastModel upRaycastModel;
        [SerializeField] private RightRaycastModel rightRaycastModel;
        [SerializeField] private DownRaycastModel downRaycastModel;
        [SerializeField] private LeftRaycastModel leftRaycastModel;
        [SerializeField] private DistanceToGroundRaycastModel distanceToGroundRaycastModel;
        [SerializeField] private StickyRaycastModel stickyRaycastModel;
        [SerializeField] private LeftStickyRaycastModel leftStickyRaycastModel;
        [SerializeField] private RightStickyRaycastModel rightStickyRaycastModel;
        // ===========================================================================================//
        [SerializeField] private RaycastSettings settings;
        [SerializeField] private GameObject character;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private BoxCollider2D boxCollider;
        private RaycastData r;
        private PhysicsData physics;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            //if (p.DisplayWarningsControl) GetWarningMessages();
        }private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }
        
        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadRaycastModel();
            LoadUpRaycastModel();
            LoadRightRaycastModel();
            LoadDownRaycastModel();
            LoadLeftRaycastModel();
            LoadDistanceToGroundRaycastModel();
            LoadStickyRaycastModel();
            LoadRightStickyRaycastModel();
            LoadLeftStickyRaycastModel();
            InitializeRaycastData();
            InitializeUpRaycastData();
            InitializeRightRaycastData();
            InitializeDownRaycastData();
            InitializeLeftRaycastData();
            InitializeDistanceToGroundRaycastData();
            InitializeStickyRaycastData();
            InitializeRightStickyRaycastData();
            InitializeLeftStickyRaycastData();
        }

        

        private void LoadRaycastModel()
        {
            raycastModel = new RaycastModel();
        }

        private void LoadUpRaycastModel()
        {
            raycastModel = new RaycastModel();
        }

        private void LoadRightRaycastModel()
        {
            rightRaycastModel = new RightRaycastModel();
        }

        private void LoadDownRaycastModel()
        {
            downRaycastModel = new DownRaycastModel();
        }

        private void LoadLeftRaycastModel()
        {
            leftRaycastModel = new LeftRaycastModel();
        }

        private void LoadDistanceToGroundRaycastModel()
        {
            distanceToGroundRaycastModel = new DistanceToGroundRaycastModel();
        }

        private void LoadStickyRaycastModel()
        {
            stickyRaycastModel = new StickyRaycastModel();
        }

        private void LoadRightStickyRaycastModel()
        {
            rightStickyRaycastModel = new RightStickyRaycastModel();
        }

        private void LoadLeftStickyRaycastModel()
        {
            leftStickyRaycastModel = new LeftStickyRaycastModel();
        }

        private void InitializeRaycastData()
        {
            raycastModel.OnInitializeData();
        }

        private void InitializeUpRaycastData()
        {
            upRaycastModel.OnInitializeData();
        }

        private void InitializeRightRaycastData()
        {
            rightRaycastModel.OnInitializeData();
        }

        private void InitializeDownRaycastData()
        {
            downRaycastModel.OnInitializeData();
        }

        private void InitializeLeftRaycastData()
        {
            leftRaycastModel.OnInitializeData();
        }

        private void InitializeDistanceToGroundRaycastData()
        {
            distanceToGroundRaycastModel.OnInitializeData();
        }

        private void InitializeStickyRaycastData()
        {
            stickyRaycastModel.OnInitializeData();
        }

        private void InitializeRightStickyRaycastData()
        {
            rightStickyRaycastModel.OnInitializeData();
        }

        private void InitializeLeftStickyRaycastData()
        {
            leftStickyRaycastModel.OnInitializeData();
        }
        
        // =============================================================================================== //
        
        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            r = new RaycastData();
            r.ApplySettings(settings);
            if (r.CastRaysOnBothSides) r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays / 2;
            else r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays;
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
            if (!boxCollider) boxCollider = character.GetComponent<BoxCollider2D>();
            if (r.DisplayWarningsControl) GetWarningMessages();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            SetRaysParameters();
        }

        private void GetWarningMessages()
        {
            const string ra = "Rays";
            const string rc = "Raycasts";
            const string nuOf = "Number of";
            const string bc = "Box Collider 2D";
            const string ch = "Character";
            const string diGrRa = "Distance To Ground Ray Maximum Length";
            const string colliderWarning = "This may cause issues near walls on direction change.";
            var raycastSettings = $"{rc} Settings";
            var rcOf = $"{rc} Offset";
            var nuOfHoRa = $"{nuOf} Horizontal {ra}";
            var nuOfVeRa = $"{nuOf} Vertical {ra}";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += FieldString($"{raycastSettings}", $"{raycastSettings}");
            if (!boxCollider) warningMessage += FieldParentGameObjectString($"{bc}", $"{ch}");
            if (boxCollider.offset.x != 0)
                warningMessage +=
                    PropertyNtZeroParentString($"{bc}", "x offset", $"{raycastSettings}", $"{colliderWarning}");
            if (r.NumberOfHorizontalRays < 0) warningMessage += LtZeroString($"{nuOfHoRa}", $"{raycastSettings}");
            if (r.NumberOfVerticalRays < 0) warningMessage += LtZeroString($"{nuOfVeRa}", $"{raycastSettings}");
            if (r.CastRaysOnBothSides)
            {
                if (!IsEven(r.NumberOfHorizontalRays))
                    warningMessage += IsOddString($"{nuOfHoRa}", $"{raycastSettings}");
                else if (!IsEven(r.NumberOfVerticalRays))
                    warningMessage += IsOddString($"{nuOfVeRa}", $"{raycastSettings}");
            }

            if (r.DistanceToGroundRayMaximumLength <= 0)
                warningMessage += LtEqZeroString($"{diGrRa}", $"{raycastSettings}");
            if (r.RayOffset <= 0) warningMessage += LtEqZeroString($"{rcOf}", $"{raycastSettings}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldString(string field, string scriptableObject)
            {
                WarningMessageCountAdd();
                return FieldMessage(field, scriptableObject);
            }

            string FieldParentGameObjectString(string field, string gameObject)
            {
                WarningMessageCountAdd();
                return FieldParentGameObjectMessage(field, gameObject);
            }

            string LtZeroString(string field, string scriptableObject)
            {
                WarningMessageCountAdd();
                return LtZeroMessage(field, scriptableObject);
            }

            string LtEqZeroString(string field, string scriptableObject)
            {
                WarningMessageCountAdd();
                return LtEqZeroMessage(field, scriptableObject);
            }

            string IsOddString(string field, string scriptableObject)
            {
                WarningMessageCountAdd();
                return IsOddMessage(field, scriptableObject);
            }

            string PropertyNtZeroParentString(string field, string property, string scriptableObject, string message)
            {
                WarningMessageCountAdd();
                return PropertyNtZeroParentMessage(field, property, scriptableObject, message);
            }

            void WarningMessageCountAdd()
            {
                warningMessageCount++;
            }
        }

        private void SetRaysParameters()
        {
            var bounds = boxCollider.bounds;
            var top = SetPositiveBounds(boxCollider.offset.y, boxCollider.size.y);
            var bottom = SetNegativeBounds(boxCollider.offset.y, boxCollider.size.y);
            var left = SetNegativeBounds(boxCollider.offset.x, boxCollider.size.x);
            var right = SetPositiveBounds(boxCollider.offset.x, boxCollider.size.x);
            r.BoundsTopLeftCorner = SetBoundsCorner(left, top);
            r.BoundsTopRightCorner = SetBoundsCorner(right, top);
            r.BoundsBottomLeftCorner = SetBoundsCorner(left, bottom);
            r.BoundsBottomRightCorner = SetBoundsCorner(right, bottom);
            r.BoundsCenter = bounds.center;
            r.BoundsBottomCenterPosition = new Vector2(bounds.center.x, bounds.min.y);
            r.BoundsWidth = Distance(r.BoundsBottomLeftCorner, r.BoundsBottomRightCorner);
            r.BoundsHeight = Distance(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner);
            r.Bounds = new Vector2 {x = r.BoundsWidth, y = r.BoundsHeight};
        }

        private static Vector2 SetVerticalRaycast(Vector2 bounds1, Vector2 bounds2, Transform t, float offset, float x)
        {
            var ray = SetBounds(bounds1, bounds2);
            ray += (Vector2) t.up * offset;
            ray += (Vector2) t.right * x;
            return ray;
        }

        private static float SetPositiveBounds(float offset, float size)
        {
            return (offset + size) / 2;
        }

        private static float SetNegativeBounds(float offset, float size)
        {
            return (offset - size) / 2;
        }

        private Vector2 SetBoundsCorner(float x, float y)
        {
            return physics.Transform.TransformPoint(new Vector2(x, y));
        }

        private static Vector2 SetCurrentRaycastOrigin(Vector2 origin1, Vector2 origin2, int index, int rays)
        {
            return Lerp(origin1, origin2, index / (float) (rays - 1));
        }

        private static Vector2 SetRaycastToTopOrigin(Vector2 bounds1, Vector2 bounds2, Transform t,
            float obstacleTolerance)
        {
            var b = SetBounds(bounds1, bounds2);
            var tol = SetTolerance(t, obstacleTolerance);
            b -= tol;
            return b;
        }

        private static Vector2 SetRaycastFromBottomOrigin(Vector2 bounds1, Vector2 bounds2, Transform t,
            float obstacleTolerance)
        {
            var b = SetBounds(bounds1, bounds2);
            var tol = SetTolerance(t, obstacleTolerance);
            b += tol;
            return b;
        }

        private static Vector2 SetBounds(Vector2 bounds1, Vector2 bounds2)
        {
            return (bounds1 + bounds2) / 2;
        }

        private static Vector2 SetTolerance(Transform t, float obstacleTolerance)
        {
            return (Vector2) t.up * obstacleTolerance;
        }

        private static float SetHorizontalRayLength(float x, float width, float offset)
        {
            return Abs(x * deltaTime) + width / 2 + offset * 2;
        }
        
        

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public RaycastModel RaycastModel => raycastModel;
        public DistanceToGroundRaycastModel DistanceToGroundRaycastModel => distanceToGroundRaycastModel;
        public DownRaycastModel DownRaycastModel => downRaycastModel;
        public LeftRaycastModel LeftRaycastModel => leftRaycastModel;
        public RightRaycastModel RightRaycastModel => rightRaycastModel;
        public StickyRaycastModel StickyRaycastModel => stickyRaycastModel;
        public RightStickyRaycastModel RightStickyRaycastModel => rightStickyRaycastModel;
        public LeftStickyRaycastModel LeftStickyRaycastModel => leftStickyRaycastModel;
        public UpRaycastModel UpRaycastModel => upRaycastModel;
        public RaycastData Data => r;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeData()
        {
            PlatformerInitializeData();
        }

        #endregion

        #region raycast model

        public async UniTaskVoid SetRaysParameters()
        {
            raycastModel.OnSetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region up raycast model

        public async UniTaskVoid InitializeUpRaycastLength()
        {
            upRaycastModel.OnInitializeUpRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastStart()
        {
            upRaycastModel.OnInitializeUpRaycastStart();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastEnd()
        {
            upRaycastModel.OnInitializeUpRaycastEnd();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastSmallestDistance()
        {
            upRaycastModel.OnInitializeUpRaycastSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCurrentUpRaycastOrigin()
        {
            upRaycastModel.OnSetCurrentUpRaycastOrigin();
        }

        public void SetCurrentUpRaycast()
        {
            upRaycastModel.OnSetCurrentUpRaycast();
        }

        public void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            upRaycastModel.OnSetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }

        #endregion

        #region right raycast model

        public void SetRightRaycastFromBottomOrigin()
        {
            rightRaycastModel.OnSetRightRaycastFromBottomOrigin();
        }

        public void SetRightRaycastToTopOrigin()
        {
            rightRaycastModel.OnSetRightRaycastToTopOrigin();
        }

        public void InitializeRightRaycastLength()
        {
            rightRaycastModel.OnInitializeRightRaycastLength();
        }

        public void SetCurrentRightRaycastOrigin()
        {
            rightRaycastModel.OnSetCurrentRightRaycastOrigin();
        }

        public void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            rightRaycastModel.OnSetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentRightRaycast()
        {
            rightRaycastModel.OnSetCurrentRightRaycast();
        }

        #endregion

        #region down raycast model

        public void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            downRaycastModel.OnSetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentDownRaycast()
        {
            downRaycastModel.OnSetCurrentDownRaycast();
        }

        public async UniTaskVoid InitializeDownRayLength()
        {
            downRaycastModel.OnInitializeDownRayLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void DoubleDownRayLength()
        {
            downRaycastModel.OnDoubleDownRayLength();
        }

        public void SetDownRayLengthToVerticalNewPosition()
        {
            downRaycastModel.OnSetDownRayLengthToVerticalNewPosition();
        }

        public async UniTaskVoid SetDownRaycastFromLeft()
        {
            downRaycastModel.OnSetDownRaycastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDownRaycastToRight()
        {
            downRaycastModel.OnSetDownRaycastToRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /*public async UniTaskVoid InitializeSmallestDistanceToDownHit()
        {
            downRaycastModel.OnInitializeSmallestDistanceToDownHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceToDownHitDistance()
        {
            downRaycastModel.OnSetSmallestDistanceToDownHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/
        /*public void SetDistanceBetweenDownRaycastsAndSmallestDistancePoint()
        {
            downRaycastModel.OnSetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
        }*/

        public void SetCurrentDownRaycastOriginPoint()
        {
            downRaycastModel.OnSetCurrentDownRaycastOriginPoint();
        }

        #endregion

        #region left raycast model

        public void SetLeftRaycastFromBottomOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastFromBottomOrigin();
        }

        public void SetLeftRaycastToTopOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastToTopOrigin();
        }

        public void InitializeLeftRaycastLength()
        {
            leftRaycastModel.OnInitializeLeftRaycastLength();
        }

        public void SetCurrentLeftRaycastOrigin()
        {
            leftRaycastModel.OnSetCurrentLeftRaycastOrigin();
        }

        public void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            leftRaycastModel.OnSetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentLeftRaycast()
        {
            leftRaycastModel.OnSetCurrentLeftRaycast();
        }

        #endregion

        #region distance to ground raycast model

        public void SetDistanceToGroundRaycastOrigin()
        {
            distanceToGroundRaycastModel.OnSetDistanceToGroundRaycastOrigin();
        }

        public async UniTaskVoid SetDistanceToGroundRaycast()
        {
            distanceToGroundRaycastModel.OnSetDistanceToGroundRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region sticky raycast model

        public async UniTaskVoid SetStickyRaycastLength()
        {
            stickyRaycastModel.OnSetStickyRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStickyRaycastLengthToSelf()
        {
            stickyRaycastModel.OnSetStickyRaycastLengthToSelf();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDoNotCastFromLeft()
        {
            stickyRaycastModel.OnSetDoNotCastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            stickyRaycastModel.OnSetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public async UniTaskVoid ResetStickyRaycastState()
        {
            stickyRaycastModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region left stickyraycast model

        public void SetLeftStickyRaycastLength()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastLength();
        }

        public void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastLengthToStickyRaycastLength();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginX()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginY()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycast()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region right stickyraycast model

        public void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void SetRightStickyRaycastLength()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastLength();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginY()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginX()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycast()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        // ====================================================================================================== //
        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public static Vector2 OnSetCurrentRaycastOrigin(Vector2 origin1, Vector2 origin2, int index, int rays)
        {
            return SetCurrentRaycastOrigin(origin1, origin2, index, rays);
        }

        public static Vector2 OnSetBounds(Vector2 bounds1, Vector2 bounds2)
        {
            return SetBounds(bounds1, bounds2);
        }

        public static float OnSetHorizontalRayLength(float x, float width, float offset)
        {
            return SetHorizontalRayLength(x, width, offset);
        }

        public static Vector2 OnSetRaycastToTopOrigin(Vector2 bounds1, Vector2 bounds2, Transform t,
            float obstacleTolerance)
        {
            return SetRaycastToTopOrigin(bounds1, bounds2, t, obstacleTolerance);
        }

        public static Vector2 OnSetRaycastFromBottomOrigin(Vector2 bounds1, Vector2 bounds2, Transform t,
            float obstacleTolerance)
        {
            return SetRaycastFromBottomOrigin(bounds1, bounds2, t, obstacleTolerance);
        }

        public static Vector2 OnSetVerticalRaycast(Vector2 bounds1, Vector2 bounds2, Transform t, float offset, float x)
        {
            return SetVerticalRaycast(bounds1, bounds2, t, offset, x);
        }

        public void OnSetRaysParameters()
        {
            SetRaysParameters();
        }
        
        #endregion

        #endregion
    }
}