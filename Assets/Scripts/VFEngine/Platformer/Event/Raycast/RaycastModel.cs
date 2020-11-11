using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Event.Raycast
{
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static Vector2;
    using static MathsExtensions;
    using static Mathf;
    using static Time;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastModel", menuName = PlatformerRaycastModelPath, order = 0)]
    public class RaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Raycast Data")] [SerializeField]
        private RaycastData r;

        #endregion

        #region private methods

        private async UniTaskVoid Initialize()
        {
            var rTask1 = Async(InitializeData());
            var rTask2 = Async(GetWarningMessages());
            var rTask3 = Async(InitializeModel());
            var rTask = await (rTask1, rTask2, rTask3);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            r.DisplayWarningsControl = r.DisplayWarningsControlSetting;
            r.DrawRaycastGizmosControl = r.DrawRaycastGizmosControlSetting;
            r.CastRaysOnBothSides = r.CastRaysOnBothSidesSetting;
            r.PerformSafetyBoxcast = r.PerformSafetyBoxcastSetting;
            r.RayOffset = r.RayOffsetSetting;
            r.DistanceToGroundRayMaximumLength = r.DistanceToGroundRayMaximumLengthSetting;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (r.DisplayWarningsControl)
            {
                const string ra = "Rays";
                const string rc = "Raycasts";
                const string nuOf = "Number of";
                const string bc = "Box Collider 2D";
                const string ch = "Character";
                const string diGrRa = "Distance To Ground Ray Maximum Length";
                const string colliderWarning = "This may cause issues near walls on direction change.";
                var settings = $"{rc} Settings";
                var rcOf = $"{rc} Offset";
                var nuOfHoRa = $"{nuOf} Horizontal {ra}";
                var nuOfVeRa = $"{nuOf} Vertical {ra}";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!r.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                if (!r.HasBoxCollider) warningMessage += FieldParentGameObjectString($"{bc}", $"{ch}");
                if (r.BoxCollider.offset.x != 0)
                    warningMessage +=
                        PropertyNtZeroParentString($"{bc}", "x offset", $"{settings}", $"{colliderWarning}");
                if (r.NumberOfHorizontalRays < 0) warningMessage += LtZeroString($"{nuOfHoRa}", $"{settings}");
                if (r.NumberOfVerticalRays < 0) warningMessage += LtZeroString($"{nuOfVeRa}", $"{settings}");
                if (r.CastRaysOnBothSides)
                {
                    if (!IsEven(r.NumberOfHorizontalRays)) warningMessage += IsOddString($"{nuOfHoRa}", $"{settings}");
                    else if (!IsEven(r.NumberOfVerticalRays))
                        warningMessage += IsOddString($"{nuOfVeRa}", $"{settings}");
                }

                if (r.DistanceToGroundRayMaximumLength <= 0)
                    warningMessage += LtEqZeroString($"{diGrRa}", $"{settings}");
                if (r.RayOffset <= 0) warningMessage += LtEqZeroString($"{rcOf}", $"{settings}");
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

                string PropertyNtZeroParentString(string field, string property, string scriptableObject,
                    string message)
                {
                    WarningMessageCountAdd();
                    return PropertyNtZeroParentMessage(field, property, scriptableObject, message);
                }

                void WarningMessageCountAdd()
                {
                    warningMessageCount++;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModel()
        {
            r.ObstacleHeightTolerance = r.RayOffset;
            r.NumberOfVerticalRaysPerSide = r.NumberOfVerticalRays / 2;
            r.BoundsBottomCenterPosition = SetBoundsBottomCenterPosition(r.BoxCollider.bounds);
            if (r.CastRaysOnBothSides) r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays / 2;
            else r.NumberOfHorizontalRaysPerSide = r.NumberOfHorizontalRays;
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetRaysParameters()
        {
            var top = SetPositiveBounds(r.BoxCollider.offset.y, r.BoxCollider.size.y);
            var bottom = SetNegativeBounds(r.BoxCollider.offset.y, r.BoxCollider.size.y);
            var left = SetNegativeBounds(r.BoxCollider.offset.x, r.BoxCollider.size.x);
            var right = SetPositiveBounds(r.BoxCollider.offset.x, r.BoxCollider.size.x);
            r.BoundsTopLeftCorner = SetBoundsCorner(left, top);
            r.BoundsTopRightCorner = SetBoundsCorner(right, top);
            r.BoundsBottomLeftCorner = SetBoundsCorner(left, bottom);
            r.BoundsBottomRightCorner = SetBoundsCorner(right, bottom);
            r.BoundsCenter = r.BoxCollider.bounds.center;
            r.BoundsWidth = Distance(r.BoundsBottomLeftCorner, r.BoundsBottomRightCorner);
            r.BoundsHeight = Distance(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner);
            r.bounds = new Vector2 {x = r.BoundsWidth, y = r.BoundsHeight};
        }

        private static Vector2 SetBoundsBottomCenterPosition(Bounds b)
        {
            return new Vector2(b.center.x, b.min.y);
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
            return r.Transform.TransformPoint(new Vector2(x, y));
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

        private static ScriptableObjects.Atoms.Raycast.Raycast SetRaycast(RaycastHit2D hit)
        {
            return new ScriptableObjects.Atoms.Raycast.Raycast(hit);
        }

        #endregion

        #endregion

        #region properties

        #region public methods

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

        public static ScriptableObjects.Atoms.Raycast.Raycast OnSetRaycast(RaycastHit2D hit)
        {
            return SetRaycast(hit);
        }

        public async UniTaskVoid OnInitialize()
        {
            await Async(Initialize());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetRaysParameters()
        {
            SetRaysParameters();
        }

        #endregion

        #endregion
    }
}