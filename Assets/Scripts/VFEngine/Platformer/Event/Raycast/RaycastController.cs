using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static Raycast;
    using static Vector2;
    using static MathsExtensions;
    using static ScriptableObject;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastSettings settings;
        private PhysicsController physicsController;
        private BoxCollider2D boxCollider;
        private RaycastData r;
        private PhysicsData physics;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            if (r.DisplayWarningsControl) GetWarningMessages();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<RaycastSettings>();
            r = new RaycastData();
            r.ApplySettings(settings);
            if (r.CastRaysOnBothSides) r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays / 2;
            else r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays;
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            boxCollider = character.GetComponentNoAllocation<BoxCollider2D>();
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
        }

        private void Start()
        {
            SetDependencies();
            SetRaysParameters();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
        }

        private void SetRaysParameters()
        {
            var bounds = boxCollider.bounds;
            var top = OnSetPositiveBounds(boxCollider.offset.y, boxCollider.size.y);
            var bottom = OnSetNegativeBounds(boxCollider.offset.y, boxCollider.size.y);
            var left = OnSetNegativeBounds(boxCollider.offset.x, boxCollider.size.x);
            var right = OnSetPositiveBounds(boxCollider.offset.x, boxCollider.size.x);
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

        private Vector2 SetBoundsCorner(float x, float y)
        {
            return physics.Transform.TransformPoint(new Vector2(x, y));
        }

        #endregion

        #endregion

        #region properties

        public RaycastData Data => r;

        #region public methods

        #region raycast model

        public async UniTaskVoid OnSetRaysParameters()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #endregion
    }
}