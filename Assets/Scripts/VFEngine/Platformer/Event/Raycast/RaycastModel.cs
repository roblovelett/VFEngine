using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static Vector2;
    using static MathsExtensions;
    using static Mathf;
    using static Time;
    using static ScriptableObject;

    [Serializable]
    public class RaycastModel
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        [SerializeField] private GameObject character;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private BoxCollider2D boxCollider;
        private RaycastData r;
        private PhysicsData physics;

        #endregion

        #region private methods

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

        public RaycastData Data => r;
        public bool DisplayWarningsControl => r.DisplayWarningsControl;
        public bool DrawRaycastGizmosControl => r.DrawRaycastGizmosControl;
        public bool CastRaysOnBothSides => r.CastRaysOnBothSides;
        public bool PerformSafetyBoxcast => r.PerformSafetyBoxcast;
        public int NumberOfHorizontalRaysPerSide => r.NumberOfHorizontalRaysPerSide;
        public int NumberOfVerticalRaysPerSide => r.NumberOfVerticalRaysPerSide;
        public float DistanceToGroundRayMaximumLength => r.DistanceToGroundRayMaximumLength;
        public float BoundsWidth => r.BoundsWidth;
        public float BoundsHeight => r.BoundsHeight;
        public float RayOffset => r.RayOffset;
        public float ObstacleHeightTolerance => r.ObstacleHeightTolerance;
        public Vector2 Bounds => r.Bounds;
        public Vector2 BoundsCenter => r.BoundsCenter;
        public Vector2 BoundsBottomLeftLeftCorner => r.BoundsBottomLeftCorner;
        public Vector2 BoundsBottomRightCorner => r.BoundsBottomRightCorner;
        public Vector2 BoundsBottomCenterPosition => r.BoundsBottomCenterPosition;
        public Vector2 BoundsTopRightCorner => r.BoundsTopRightCorner;

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