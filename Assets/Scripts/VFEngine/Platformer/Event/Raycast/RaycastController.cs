using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static ScriptableObject;
    using static RaycastDirection;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        private PlatformerController platformerController;
        private PhysicsController physicsController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private BoxCollider2D boxCollider;
        private RaycastData r;
        private PlatformerData platformer;
        private PhysicsData physics;
        private DownRaycastHitColliderData downRaycastHitCollider;

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
            platformerController = GetComponent<PlatformerController>();
            physicsController = GetComponent<PhysicsController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            r = new RaycastData
            {
                OriginalColliderSize = boxCollider.size,
                OriginalColliderOffset = boxCollider.offset,
                CurrentRaycastDirection = None
            };
            r.ApplySettings(settings);
            if (r.CastRaysOnBothSides) r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays / 2;
            else r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays;
        }

        /*private void GetWarningMessages()
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

            string FieldParentGameObjectString(string field, string c)
            {
                WarningMessageCountAdd();
                return FieldParentGameObjectMessage(field, c);
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
        }*/

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            platformer = platformerController.Data;
            physics = physicsController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
        }

        private void Initialize()
        {
            SetRaysParameters();
        }

        #endregion

        #region platformer

        private void PlatformerInitializeFrame()
        {
            SetRaysParameters();
        }

        private bool TestMovingPlatform => downRaycastHitCollider.HasMovingPlatform && platformer.TestPlatform;
        private void PlatformerTestMovingPlatform()
        {
            if (TestMovingPlatform) SetRaysParameters();
        }

        private void PlatformerSetRaycastDirectionToLeft()
        {
            r.CurrentRaycastDirection = Left;
        }

        private void PlatformerSetRaycastDirectionToRight()
        {
            r.CurrentRaycastDirection = Right;
        }

        #endregion

        private void SetRaysParameters()
        {
            var bounds = boxCollider.bounds;
            var offset = boxCollider.offset;
            var size = boxCollider.size;
            var top = offset.y + size.y / 2f;
            var bottom = offset.y - size.y / 2f;
            var left = offset.x - size.x / 2f;
            var right = offset.x + size.x / 2f;
            r.BoundsCenter = new Vector2 {x = bounds.center.x, y = bounds.center.y};
            r.BoundsBottomCenterPosition = new Vector2 {x = bounds.center.x, y = bounds.min.y};
            r.BoundsTopLeftCorner = physics.Transform.TransformPoint(new Vector2 {x = left, y = top});
            r.BoundsTopRightCorner = physics.Transform.TransformPoint(new Vector2 {x = right, y = top});
            r.BoundsBottomLeftCorner = physics.Transform.TransformPoint(new Vector2 {x = left, y = bottom});
            r.BoundsBottomRightCorner = physics.Transform.TransformPoint(new Vector2 {x = right, y = bottom});
            r.BoundsWidth = Distance(r.BoundsBottomLeftCorner, r.BoundsBottomRightCorner);
            r.BoundsHeight = Distance(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner);
            r.Bounds = new Vector2 {x = r.BoundsWidth, y = r.BoundsHeight};
        }

        #endregion

        #endregion

        #region properties

        public RaycastData Data => r;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnPlatformerTestMovingPlatform()
        {
            PlatformerTestMovingPlatform();
        }

        public void OnPlatformerSetRaycastDirectionToLeft()
        {
            PlatformerSetRaycastDirectionToLeft();
        }

        public void OnPlatformerSetRaycastDirectionToRight()
        {
            PlatformerSetRaycastDirectionToRight();
        }

        #endregion

        #endregion

        #endregion
    }
}