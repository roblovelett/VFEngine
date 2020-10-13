using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static RaycastDirection;
    using static Vector2;
    using static MathsExtensions;

    [CreateAssetMenu(fileName = "RaycastModel", menuName = "VFEngine/Platformer/Event/Raycast/Raycast Model",
        order = 0)]
    public class RaycastModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Raycast Data")] [SerializeField]
        private RaycastData r;

        /* fields */
        private const string Rc = "Raycast";
        private RaycastDirection raycastDirection = None;
        private Vector2 direction = zero;

        /* fields: methods */
        private async UniTaskVoid InitializeInternal(RaycastDirection rayDirection)
        {
            var rTask1 = Async(InitializeData(rayDirection));
            var rTask2 = Async(GetWarningMessages());
            var rTask3 = Async(InitializeModel());
            var rTask = await (rTask1, rTask2, rTask3);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData(RaycastDirection rayDirection)
        {
            r.NumberOfHorizontalRaysRef = r.NumberOfHorizontalRays;
            r.NumberOfVerticalRaysRef = r.NumberOfVerticalRays;
            r.CastRaysOnBothSidesRef = r.CastRaysOnBothSides;
            switch (rayDirection)
            {
                case None:
                    SetRaycastDirection(rayDirection, zero);
                    break;
                case Up:
                    SetRaycastDirection(rayDirection, new Vector2(0, 1));
                    break;
                case Right:
                    SetRaycastDirection(rayDirection, new Vector2(1, 0));
                    break;
                case Down:
                    SetRaycastDirection(rayDirection, new Vector2(0, -1));
                    break;
                case Left:
                    SetRaycastDirection(rayDirection, new Vector2(-1, 0));
                    break;
                default:
                    if (r.DisplayWarnings)
                        DebugLogWarning(1,
                            $"{Rc} initialized with incorrect value. Please use RaycastDirection " +
                            "of value Up, Right, Down, Left, or None.");
                    break;
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetRaycastDirection(RaycastDirection rd, Vector2 d)
        {
            raycastDirection = rd;
            direction = d;
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (r.DisplayWarnings)
            {
                const string ra = "Rays";
                const string rc = "Raycasts";
                const string nuOf = "Number of";
                const string diGrRa = "Distance To Ground Ray Maximum Length";
                var settings = $"{rc} Settings";
                var rcOf = $"{rc} Offset";
                var nuOfHoRa = $"{nuOf} Horizontal {ra}";
                var nuOfVeRa = $"{nuOf} Vertical {ra}";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!r.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
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

                void WarningMessageCountAdd()
                {
                    warningMessageCount++;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModel()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetRaysParameters()
        {
            var top = SetPositiveBounds(r.BoxColliderOffset.y, r.BoxColliderSize.y);
            var bottom = SetNegativeBounds(r.BoxColliderOffset.y, r.BoxColliderSize.y);
            var left = SetNegativeBounds(r.BoxColliderOffset.x, r.BoxColliderSize.x);
            var right = SetPositiveBounds(r.BoxColliderOffset.x, r.BoxColliderSize.x);
            r.BoundsTopLeftCorner = SetBoundsCorner(left, top);
            r.BoundsTopRightCorner = SetBoundsCorner(right, top);
            r.BoundsBottomLeftCorner = SetBoundsCorner(left, bottom);
            r.BoundsBottomRightCorner = SetBoundsCorner(right, bottom);
            r.BoundsCenter = r.BoxColliderBoundsCenter;
            r.BoundsWidth = Distance(r.BoundsBottomLeftCorner, r.BoundsBottomRightCorner);
            r.BoundsHeight = Distance(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner);
        }

        private static float SetPositiveBounds(float offset, float size)
        {
            return Half(offset + size);
        }

        private static float SetNegativeBounds(float offset, float size)
        {
            return Half(offset - size);
        }

        private Vector2 SetBoundsCorner(float x, float y)
        {
            return r.Transform.TransformPoint(new Vector2(x, y));
        }

        public async UniTaskVoid Initialize(RaycastDirection rayDirection)
        {
            await Async(InitializeInternal(rayDirection));
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetRaysParameters()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }
    }
}