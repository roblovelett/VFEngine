using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static ColliderDirection;
    using static MathsExtensions;
    using static Color;

    [CreateAssetMenu(fileName = "RaycastHitColliderModel",
        menuName = "VFEngine/Platformer/Physics/Raycast Hit Collider/Raycast Hit Collider Model", order = 0)]
    public class RaycastHitColliderModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [FormerlySerializedAs("r")] [LabelText("Raycast Hit Collider Data")] [SerializeField]
        private RaycastHitColliderData rhc;

        /* fields */
        private const string Rh = "Raycast Hit Collider";

        /* fields: methods */
        private async UniTaskVoid InitializeInternal(ColliderDirection direction)
        {
            var rTask1 = Async(InitializeData(direction));
            var rTask2 = Async(GetWarningMessages());
            var rTask3 = Async(InitializeModel());
            var rTask = await (rTask1, rTask2, rTask3);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData(ColliderDirection direction)
        {
            var xRaysNumber = rhc.NumberOfHorizontalRays;
            var yRaysNumber = rhc.NumberOfVerticalRays;
            var xRaysNumberHalved = (int) Half(xRaysNumber);
            var yRaysNumberHalved = (int) Half(yRaysNumber);
            switch (direction)
            {
                case Up:
                    rhc.UpHitsStorage = new RaycastHit2D[yRaysNumberHalved];
                    break;
                case Right:
                    rhc.RightHitsStorage = rhc.CastRaysBothSides
                        ? new RaycastHit2D[xRaysNumberHalved]
                        : new RaycastHit2D[xRaysNumber];
                    break;
                case Down:
                    rhc.DownHitsStorage = new RaycastHit2D[yRaysNumberHalved];
                    break;
                case Left:
                    rhc.LeftHitsStorage = rhc.CastRaysBothSides
                        ? new RaycastHit2D[xRaysNumberHalved]
                        : new RaycastHit2D[xRaysNumber];
                    break;
                case None: break;
                default:
                    if (rhc.DisplayWarnings)
                        DebugLogWarning(1,
                            $"{Rh} initialized with incorrect value. Please use ColliderDirection of " +
                            "value Up, Right, Down, Left, or None.");
                    break;
            }

            rhc.BoxColliderSizeRef = rhc.OriginalColliderSize;
            rhc.BoxColliderOffsetRef = rhc.OriginalColliderOffset;
            rhc.BoxColliderBoundsCenterRef = rhc.OriginalColliderBoundsCenter;
            rhc.HorizontalHitsStorageIndexesAmountRef = rhc.CastRaysBothSides ? xRaysNumberHalved : xRaysNumber;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (rhc.DisplayWarnings)
            {
                const string bc = "Box Collider 2D";
                const string colliderWarning = "This may cause issues upon direction change near walls";
                var settings = $"{Rh} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!rhc.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                if (!rhc.HasBoxCollider) warningMessage += FieldParentString($"{bc}", $"{settings}");
                if (rhc.OriginalColliderOffset.x != 0)
                    warningMessage +=
                        PropertyNtZeroParentMessage($"{bc}", "x offset", $"{settings}", $"{colliderWarning}");
                DebugLogWarning(warningMessageCount, warningMessage);

                string FieldString(string field, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldMessage(field, scriptableObject);
                }

                string FieldParentString(string field, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldParentMessage(field, scriptableObject);
                }

                void AddWarningMessageCount()
                {
                    warningMessageCount++;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModel()
        {
            rhc.contactList.Clear();
            rhc.state.SetCurrentWallCollider(null);
            rhc.state.Reset();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void ClearContactList()
        {
            rhc.contactList.Clear();
        }

        private void SetWasGroundedLastFrame()
        {
            rhc.state.SetWasGroundedLastFrame(rhc.state.IsCollidingBelow);
        }

        private void SetStandingOnLastFrame()
        {
            rhc.state.SetStandingOnLastFrame(rhc.state.StandingOn);
        }

        private void SetWasTouchingCeilingLastFrame()
        {
            rhc.state.SetWasTouchingCeilingLastFrame(rhc.state.IsCollidingAbove);
        }

        private void SetCurrentWallCollider()
        {
            rhc.state.SetCurrentWallCollider(null);
        }

        private void ResetState()
        {
            rhc.state.Reset();
        }

        private void SetOnMovingPlatform()
        {
            rhc.state.SetOnMovingPlatform(true);
        }

        private void SetMovingPlatformCurrentGravity()
        {
            rhc.MovingPlatformCurrentGravity = rhc.MovingPlatformGravity;
        }

        private void InitializeRightHitsStorage()
        {
            rhc.RightHitsStorage = new RaycastHit2D[rhc.NumberOfHorizontalRays];
        }

        private void InitializeRightHitsStorageHalf()
        {
            rhc.RightHitsStorage = new RaycastHit2D[(int) Half(rhc.NumberOfHorizontalRays)];
        }

        private void InitializeLeftHitsStorage()
        {
            rhc.LeftHitsStorage = new RaycastHit2D[rhc.NumberOfHorizontalRays];
        }

        private void InitializeLeftHitsStorageHalf()
        {
            rhc.LeftHitsStorage = new RaycastHit2D[(int) Half(rhc.NumberOfHorizontalRays)];
        }

        private void SetRightHitsStorageToIgnoreOneWayPlatform()
        {
            rhc.RightHitsStorage[0] = Raycast(rhc.RightRaycastOriginPoint, rhc.Transform.right, rhc.HorizontalRayLength,
                rhc.PlatformMask, red, rhc.DrawRaycastGizmos);
        }

        private void SetLeftHitsStorageToIgnoreOneWayPlatform()
        {
            rhc.LeftHitsStorage[0] = Raycast(rhc.LeftRaycastOriginPoint, -rhc.Transform.right, rhc.HorizontalRayLength,
                rhc.PlatformMask, red, rhc.DrawRaycastGizmos);
        }

        private void SetRightHitsStorage()
        {
            rhc.RightHitsStorage[rhc.RightHitsStorageIndex] = Raycast(rhc.RightRaycastOriginPoint, rhc.Transform.right,
                rhc.HorizontalRayLength, rhc.PlatformMask & ~rhc.OneWayPlatformMask & ~rhc.MovingOneWayPlatformMask,
                red, rhc.DrawRaycastGizmos);
        }

        private void SetLeftHitsStorage()
        {
            rhc.LeftHitsStorage[rhc.LeftHitsStorageIndex] = Raycast(rhc.LeftRaycastOriginPoint, -rhc.Transform.right,
                rhc.HorizontalRayLength, rhc.PlatformMask & ~rhc.OneWayPlatformMask & ~rhc.MovingOneWayPlatformMask,
                red, rhc.DrawRaycastGizmos);
        }

        /* properties: dependencies */

        /* properties: methods */
        public async UniTaskVoid Initialize(ColliderDirection direction)
        {
            Async(InitializeInternal(direction));
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnClearContactList()
        {
            ClearContactList();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetWasGroundedLastFrame()
        {
            SetWasGroundedLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetStandingOnLastFrame()
        {
            SetStandingOnLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetWasTouchingCeilingLastFrame()
        {
            SetWasTouchingCeilingLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCurrentWallCollider()
        {
            SetCurrentWallCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetOnMovingPlatform()
        {
            SetOnMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetMovingPlatformCurrentGravity()
        {
            SetMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnInitializeRightHitsStorage()
        {
            InitializeRightHitsStorage();
        }

        public void OnInitializeRightHitsStorageHalf()
        {
            InitializeRightHitsStorageHalf();
        }

        public void OnInitializeLeftHitsStorage()
        {
            InitializeLeftHitsStorage();
        }

        public void OnInitializeLeftHitsStorageHalf()
        {
            InitializeLeftHitsStorageHalf();
        }

        public void OnSetRightHitsStorageToIgnoreOneWayPlatform()
        {
            SetRightHitsStorageToIgnoreOneWayPlatform();
        }

        public void OnSetLeftHitsStorageToIgnoreOneWayPlatform()
        {
            SetLeftHitsStorageToIgnoreOneWayPlatform();
        }

        public void OnSetRightHitsStorage()
        {
            SetRightHitsStorage();
        }

        public void OnSetLeftHitsStorage()
        {
            SetLeftHitsStorage();
        }
    }
}