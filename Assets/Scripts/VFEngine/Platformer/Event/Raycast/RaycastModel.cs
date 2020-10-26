using System;
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
    using static Color;
    using static Mathf;
    using static Single;

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
            r.HorizontalRaycastFromBottomRef = r.HorizontalRaycastFromBottom;
            r.HorizontalRaycastToTopRef = r.HorizontalRaycastToTop;
            r.VerticalRaycastFromLeftRef = r.VerticalRaycastFromLeft;
            r.VerticalRaycastToRightRef = r.VerticalRaycastToRight;
            r.HorizontalRayLengthRef = r.HorizontalRayLength;
            r.CurrentRightRaycastOriginRef = r.CurrentRightRaycastOrigin;
            r.CurrentLeftRaycastOriginRef = r.CurrentLeftRaycastOrigin;
            r.CurrentUpRaycastOriginRef = r.CurrentUpRaycastOrigin;
            r.CurrentDownRaycastOriginRef = r.CurrentDownRaycastOrigin;
            r.NumberOfHorizontalRaysPerSideRef = r.NumberOfHorizontalRaysPerSide;
            r.NumberOfVerticalRaysPerSideRef = r.NumberOfVerticalRaysPerSide;
            r.DrawRaycastGizmosRef = r.DrawRaycastGizmos;
            r.CurrentRightRaycastRef = r.CurrentRightRaycast;
            r.CurrentLeftRaycastRef = r.CurrentLeftRaycast;
            r.CurrentUpRaycastRef = r.CurrentUpRaycast;
            r.CurrentDownRaycastRef = r.CurrentDownRaycast;
            r.DownRayLengthRef = r.DownRayLength;
            r.SmallestDistanceRef = r.SmallestDistance;
            r.SmallValueRef = r.SmallValue;
            r.BoundsRef = r.Bounds;
            r.BoundsHeightRef = r.BoundsHeight;
            r.BoundsWidthRef = r.BoundsWidth;
            r.RayOffsetRef = r.RayOffset;
            r.BoundsBottomLeftCornerRef = r.BoundsBottomLeftCorner;
            r.BoundsBottomRightCornerRef = r.BoundsBottomRightCorner;
            r.BoundsCenterRef = r.BoundsCenter;
            r.DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPointRef =
                r.DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint;
            r.UpRaycastSmallestDistanceRef = r.UpRaycastSmallestDistance;
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

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentRightRaycast = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.HorizontalRayLength,
                r.PlatformMask, red, r.DrawRaycastGizmos);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentLeftRaycast = Raycast(r.CurrentLeftRaycastOrigin, -r.Transform.right, r.HorizontalRayLength,
                r.PlatformMask, red, r.DrawRaycastGizmos);
        }

        private void SetCurrentRightRaycast()
        {
            r.CurrentRightRaycast = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.HorizontalRayLength,
                r.PlatformMask & ~r.OneWayPlatformMask & ~r.MovingOneWayPlatformMask, red, r.DrawRaycastGizmos);
        }

        private void SetCurrentLeftRaycast()
        {
            r.CurrentLeftRaycast = Raycast(r.CurrentLeftRaycastOrigin, -r.Transform.right, r.HorizontalRayLength,
                r.PlatformMask & ~r.OneWayPlatformMask & ~r.MovingOneWayPlatformMask, red, r.DrawRaycastGizmos);
        }

        private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentDownRaycast = Raycast(r.CurrentDownRaycastOrigin, -r.Transform.up, r.DownRayLength,
                r.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, r.DrawRaycastGizmos);
        }

        private void SetCurrentDownRaycast()
        {
            r.CurrentDownRaycast = Raycast(r.CurrentDownRaycastOrigin, -r.Transform.up, r.DownRayLength,
                r.RaysBelowLayerMaskPlatforms, blue, r.DrawRaycastGizmos);
        }
        

        private void InitializeDownRayLength()
        {
            r.DownRayLength = r.BoundsHeight / 2 + r.RayOffset;
        }

        private void DoubleDownRayLength()
        {
            r.DownRayLength *= 2;
        }

        private void SetDownRayLengthToVerticalNewPosition()
        {
            r.DownRayLength += Abs(r.NewPosition.y);
        }

        private void SetVerticalRaycastFromLeft()
        {
            r.VerticalRaycastFromLeft = SetVerticalRaycast(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner, r.Transform,
                r.RayOffset, r.NewPosition.x);
        }

        private void SetVerticalRaycastToRight()
        {
            r.VerticalRaycastToRight = SetVerticalRaycast(r.BoundsBottomRightCorner, r.BoundsTopRightCorner,
                r.Transform, r.RayOffset, r.NewPosition.x);
        }

        private void InitializeSmallestDistance()
        {
            r.SmallestDistance = MaxValue;
        }

        private void SetSmallestDistanceToDownHitDistance()
        {
            r.SmallestDistance = r.RaycastDownHitAt.distance;
        }

        private void SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint()
        {
            r.DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint = SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint(r.StandingOnWithSmallestDistancePoint, r.VerticalRaycastFromLeft, r.VerticalRaycastToRight); //
        }

        private static float SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint(Vector2 point, Vector2 verticalRayFromLeft, Vector2 verticalRayToRight)
        {
            return DistanceBetweenPointAndLine(point, verticalRayFromLeft, verticalRayToRight);;
        }

        private static Vector2 SetVerticalRaycast(Vector2 boundsBottomCorner, Vector2 boundsTopCorner,
            Transform transform, float offset, float positionX)
        {
            var verticalRay = (boundsBottomCorner + boundsTopCorner) / 2;
            verticalRay += (Vector2) transform.up * offset;
            verticalRay += (Vector2) transform.right * positionX;
            return verticalRay;
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
        
        private void InitializeUpRaycastLength()
        {
            r.UpRayLength = r.IsGrounded ? r.RayOffset : r.NewPosition.y;
        }

        private void InitializeUpRaycastStart()
        {
            r.UpRaycastStart = (r.BoundsBottomLeftCorner + r.BoundsTopLeftCorner) / 2 * 2 +
                               (Vector2) r.Transform.right * r.NewPosition.x;
        }

        private void InitializeUpRaycastEnd()
        {
            r.UpRaycastEnd = (r.BoundsBottomRightCorner + r.BoundsTopRightCorner) / 2 * 2 +
                             (Vector2) r.Transform.right * r.NewPosition.y;
        }

        private void InitializeUpRaycastSmallestDistance()
        {
            r.UpRaycastSmallestDistance = MaxValue;
        }

        private void SetCurrentUpRaycastOriginPoint()
        {
            r.CurrentUpRaycastOrigin = Lerp(r.UpRaycastStart, r.UpRaycastEnd,
                r.CurrentUpHitsStorageIndex / (float) (r.NumberOfVerticalRaysPerSide - 1));
        }

        private void SetCurrentUpRaycast()
        {
            r.CurrentUpRaycast = Raycast(r.CurrentUpRaycastOrigin, r.Transform.up, r.UpRayLength,
                r.PlatformMask & ~ r.OneWayPlatformMask & ~ r.MovingOneWayPlatformMask, cyan, r.DrawRaycastGizmos);
        }

        private void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            r.UpRaycastSmallestDistance = r.RaycastUpHitAt.distance;
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

        public void OnSetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentRightRaycast()
        {
            SetCurrentRightRaycast();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
        }
        
        public void OnSetCurrentDownRaycast()
        {
            SetCurrentDownRaycast();
        }

        public void OnInitializeDownRayLength()
        {
            InitializeDownRayLength();
        }

        public void OnDoubleDownRayLength()
        {
            DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            SetDownRayLengthToVerticalNewPosition();
        }

        public void OnSetVerticalRaycastFromLeft()
        {
            SetVerticalRaycastFromLeft();
        }

        public void OnSetVerticalRaycastToRight()
        {
            SetVerticalRaycastToRight();
        }

        public void OnInitializeSmallestDistance()
        {
            InitializeSmallestDistance();
        }

        public void OnSetSmallestDistanceToDownHitDistance()
        {
            SetSmallestDistanceToDownHitDistance();
        }

        public void OnSetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint()
        {
            SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint();
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

        public void OnSetCurrentUpRaycastOriginPoint()
        {
            SetCurrentUpRaycastOriginPoint();
        }

        public void OnSetCurrentUpRaycast()
        {
            SetCurrentUpRaycast();
        }

        public void OnSetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            SetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }
    }
}