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
    using static Vector2;
    using static Vector3;

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
            var xRays = rhc.NumberOfHorizontalRays;
            var yRays = rhc.NumberOfVerticalRays / 2;
            if (rhc.CastRaysBothSides) xRays /= 2;
            switch (direction)
            {
                case Up:
                    rhc.UpHitsStorage = new RaycastHit2D[yRays];
                    break;
                case Right:
                    rhc.RightHitsStorage = new RaycastHit2D[xRays];
                    break;
                case Down:
                    rhc.DownHitsStorage = new RaycastHit2D[yRays];
                    break;
                case Left:
                    rhc.LeftHitsStorage = new RaycastHit2D[xRays];
                    break;
                case None: break;
                default:
                    if (rhc.DisplayWarnings)
                        DebugLogWarning(1,
                            $"{Rh} initialized with incorrect value. Please use ColliderDirection of " +
                            "value Up, Right, Down, Left, or None.");
                    break;
            }

            rhc.HorizontalHitsStorageLength = xRays;
            rhc.VerticalHitsStorageLength = yRays;
            rhc.UpHitsStorageLengthRef = rhc.UpHitsStorageLength;
            rhc.RightHitsStorageLengthRef = rhc.RightHitsStorageLength;
            rhc.DownHitsStorageLengthRef = rhc.DownHitsStorageLength;
            rhc.LeftHitsStorageLengthRef = rhc.LeftHitsStorageLength;
            rhc.BoxColliderSizeRef = rhc.OriginalColliderSize;
            rhc.BoxColliderOffsetRef = rhc.OriginalColliderOffset;
            rhc.BoxColliderBoundsCenterRef = rhc.OriginalColliderBoundsCenter;
            rhc.CurrentRightHitsStorageIndexRef = rhc.CurrentRightHitsStorageIndex;
            rhc.CurrentLeftHitsStorageIndexRef = rhc.CurrentLeftHitsStorageIndex;
            rhc.CurrentUpHitsStorageIndexRef = rhc.CurrentUpHitsStorageIndex;
            rhc.CurrentDownHitsStorageIndexRef = rhc.CurrentDownHitsStorageIndex;
            rhc.HorizontalHitsStorageLengthRef = rhc.HorizontalHitsStorageLength;
            rhc.CurrentRightHitDistanceRef = rhc.CurrentRightHitDistance;
            rhc.CurrentLeftHitDistanceRef = rhc.CurrentLeftHitDistance;
            rhc.CurrentDownHitSmallestDistanceRef = rhc.CurrentDownHitSmallestDistance;
            rhc.CurrentRightHitColliderRef = rhc.CurrentRightHitCollider;
            rhc.CurrentLeftHitColliderRef = rhc.CurrentLeftHitCollider;
            rhc.IgnoredColliderRef = rhc.IgnoredCollider;
            rhc.CurrentRightHitAngleRef = rhc.CurrentRightHitAngle;
            rhc.CurrentLeftHitAngleRef = rhc.CurrentLeftHitAngle;
            rhc.CurrentRightHitPointRef = rhc.CurrentRightHitPoint;
            rhc.CurrentLeftHitPointRef = rhc.CurrentLeftHitPoint;
            rhc.IsGroundedRef = rhc.state.IsGrounded;
            rhc.FrictionRef = rhc.Friction;
            rhc.RaycastDownHitAtRef = rhc.RaycastDownHitAt;
            rhc.OnMovingPlatformRef = rhc.state.OnMovingPlatform;
            rhc.VerticalHitsStorageLengthRef = rhc.VerticalHitsStorageLength;
            rhc.StandingOnLastFrameRef = rhc.state.StandingOnLastFrame;
            rhc.IsStandingOnLastFrameNotNullRef = rhc.IsStandingOnLastFrameNotNull;
            rhc.StandingOnColliderRef = rhc.state.StandingOnCollider;
            rhc.ColliderBottomCenterPositionRef = rhc.ColliderBottomCenterPosition;
            rhc.DownHitsStorageSmallestDistanceIndexRef = rhc.DownHitsStorageSmallestDistanceIndex;
            rhc.DownHitConnectedRef = rhc.DownHitConnected;
            rhc.CurrentDownHitsStorageIndexRef = rhc.CurrentDownHitsStorageIndex;
            rhc.CrossBelowSlopeAngleRef = rhc.state.CrossBelowSlopeAngle;
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
            rhc.state.SetCurrentWallColliderNull();
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

        private void SetCurrentWallColliderNull()
        {
            rhc.state.SetCurrentWallColliderNull();
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
            rhc.RightHitsStorage = new RaycastHit2D[rhc.HorizontalHitsStorageLength];
        }

        private void InitializeLeftHitsStorage()
        {
            rhc.LeftHitsStorage = new RaycastHit2D[rhc.HorizontalHitsStorageLength];
        }

        private void InitializeRightHitsStorageIndex()
        {
            rhc.CurrentRightHitsStorageIndex = 0;
        }

        private void InitializeLeftHitsStorageIndex()
        {
            rhc.CurrentLeftHitsStorageIndex = 0;
        }

        private void SetCurrentRightHitsStorage()
        {
            rhc.RightHitsStorage[rhc.CurrentRightHitsStorageIndex] = rhc.CurrentRightRaycast;
        }
        
        private void SetCurrentLeftHitsStorage()
        {
            rhc.LeftHitsStorage[rhc.CurrentLeftHitsStorageIndex] = rhc.CurrentLeftRaycast;
        }

        private void SetCurrentDownHitsStorage()
        {
            rhc.DownHitsStorage[rhc.CurrentDownHitsStorageIndex] = rhc.CurrentDownRaycast;
        }

        private void SetRightHitAngle()
        {
            rhc.state.SetRightHitAngle(rhc.CurrentRightHitAngle);
        }

        private void SetLeftHitAngle()
        {
            rhc.state.SetLeftHitAngle(rhc.CurrentLeftHitAngle);
        }

        private void SetLeftIsCollidingLeft()
        {
            rhc.state.SetIsCollidingLeft(true);
        }

        private void SetLeftDistanceToLeftCollider()
        {
            rhc.state.SetDistanceToLeftCollider(rhc.CurrentLeftHitAngle);
        }

        private void SetRightIsCollidingRight()
        {
            rhc.state.SetIsCollidingRight(true);
        }

        private void SetRightDistanceToRightCollider()
        {
            rhc.state.SetDistanceToRightCollider(rhc.CurrentRightHitAngle);
        }

        private void SetRightCurrentWallCollider()
        {
            rhc.state.SetRightCurrentWallCollider(rhc.CurrentRightHitCollider.gameObject);
        }

        private void SetRightFailedSlopeAngle()
        {
            rhc.state.SetPassedRightSlopeAngle(false);
        }

        private void SetLeftCurrentWallCollider()
        {
            rhc.state.SetLeftCurrentWallCollider(rhc.CurrentLeftHitCollider.gameObject);
        }

        private void SetLeftFailedSlopeAngle()
        {
            rhc.state.SetPassedLeftSlopeAngle(false);
        }

        private void AddRightHitToContactList()
        {
            rhc.contactList.Add(rhc.RightHitsStorage[rhc.CurrentRightHitsStorageIndex]);
        }

        private void AddLeftHitToContactList()
        {
            rhc.contactList.Add(rhc.LeftHitsStorage[rhc.CurrentLeftHitsStorageIndex]);
        }

        private void AddToRightHitsStorageIndex()
        {
            rhc.CurrentRightHitsStorageIndex++;
        }

        private void AddToLeftHitsStorageIndex()
        {
            rhc.CurrentLeftHitsStorageIndex++;
        }

        private void InitializeFriction()
        {
            rhc.Friction = 0;
        }

        private void SetIsNotCollidingBelow()
        {
            rhc.state.SetIsCollidingAbove(false);
        }

        private void InitializeDownHitsStorage()
        {
            rhc.DownHitsStorage = new RaycastHit2D[rhc.VerticalHitsStorageLength];
        }

        private void InitializeUpHitsStorage()
        {
            rhc.UpHitsStorage = new RaycastHit2D[rhc.VerticalHitsStorageLength];
        }

        private void InitializeDownHitsStorageSmallestDistanceIndex()
        {
            rhc.DownHitsStorageSmallestDistanceIndex = 0;
        }

        private void InitializeDownHitConnected()
        {
            rhc.DownHitConnected = false;
        }

        private void InitializeDownHitsStorageIndex()
        {
            rhc.CurrentDownHitsStorageIndex = 0;
        }

        private void AddDownHitsStorageIndex()
        {
            rhc.CurrentDownHitsStorageIndex++;
        }

        private void SetRaycastDownHitAt(int index)
        {
            rhc.RaycastDownHitAt = rhc.DownHitsStorage[index];
        }

        private void SetDownHitConnected()
        {
            rhc.DownHitConnected = true;
        }

        private void SetBelowSlopeAngleAt(int index)
        {
            rhc.state.SetBelowSlopeAngle(Vector2.Angle(rhc.DownHitsStorage[index].normal, rhc.Transform.up));
        }

        private void SetCrossBelowSlopeAngleAt(int index)
        {
            rhc.state.SetCrossBelowSlopeAngle(Cross(rhc.Transform.up, rhc.DownHitsStorage[index].normal));
        }

        private void SetSmallestDistanceIndexAt(int index)
        {
            rhc.DownHitsStorageSmallestDistanceIndex = index;
        }

        private void SetNegativeBelowSlopeAngle()
        {
            rhc.state.SetCrossBelowSlopeAngle(-rhc.state.CrossBelowSlopeAngle);
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

        public async UniTaskVoid OnSetCurrentWallColliderNull()
        {
            SetCurrentWallColliderNull();
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

        public void OnInitializeLeftHitsStorage()
        {
            InitializeLeftHitsStorage();
        }

        public void OnInitializeRightHitsStorageIndex()
        {
            InitializeRightHitsStorageIndex();
        }

        public void OnInitializeLeftHitsStorageIndex()
        {
            InitializeLeftHitsStorageIndex();
        }

        public void OnSetCurrentRightHitsStorage()
        {
            SetCurrentRightHitsStorage();
        }

        public void OnSetCurrentLeftHitsStorage()
        {
            SetCurrentLeftHitsStorage();
        }

        public void OnSetCurrentDownHitsStorage()
        {
            SetCurrentDownHitsStorage();
        }

        public void OnSetRightHitAngle()
        {
            SetRightHitAngle();
        }

        public void OnSetLeftHitAngle()
        {
            SetLeftHitAngle();
        }

        public void OnSetLeftIsCollidingLeft()
        {
            SetLeftIsCollidingLeft();
        }

        public void OnSetLeftDistanceToLeftCollider()
        {
            SetLeftDistanceToLeftCollider();
        }

        public void OnSetRightIsCollidingRight()
        {
            SetRightIsCollidingRight();
        }

        public void OnSetRightDistanceToRightCollider()
        {
            SetRightDistanceToRightCollider();
        }

        public void OnSetRightCurrentWallCollider()
        {
            SetRightCurrentWallCollider();
        }

        public void OnSetRightFailedSlopeAngle()
        {
            SetRightFailedSlopeAngle();
        }

        public void OnSetLeftCurrentWallCollider()
        {
            SetLeftCurrentWallCollider();
        }

        public void OnSetLeftFailedSlopeAngle()
        {
            SetLeftFailedSlopeAngle();
        }

        public void OnAddRightHitToContactList()
        {
            AddRightHitToContactList();
        }

        public void OnAddLeftHitToContactList()
        {
            AddLeftHitToContactList();
        }
        public void OnAddToRightHitsStorageIndex()
        {
            AddToRightHitsStorageIndex();
        }

        public void OnAddToLeftHitsStorageIndex()
        {
            AddToLeftHitsStorageIndex();
        }

        public void OnInitializeFriction()
        {
            InitializeFriction();
        }

        public void OnSetIsNotCollidingBelow()
        {
            SetIsNotCollidingBelow();
        }

        public void OnInitializeDownHitsStorage()
        {
            InitializeDownHitsStorage();
        }

        public void OnInitializeDownHitsStorageSmallestDistanceIndex()
        {
            InitializeDownHitsStorageSmallestDistanceIndex();
        }

        public void OnInitializeDownHitConnected()
        {
            InitializeDownHitConnected();
        }

        public void OnInitializeDownHitsStorageIndex()
        {
            InitializeDownHitsStorageIndex();
        }

        public void OnAddDownHitsStorageIndex()
        {
            AddDownHitsStorageIndex();
        }

        public void OnSetRaycastDownHitAt(int index)
        {
            SetRaycastDownHitAt(index);
        }

        public void OnSetDownHitConnected()
        {
            SetDownHitConnected();
        }

        public void OnSetBelowSlopeAngleAt(int index)
        {
            SetBelowSlopeAngleAt(index);
        }

        public void OnSetCrossBelowSlopeAngleAt(int index)
        {
            SetCrossBelowSlopeAngleAt(index);
        }

        public void OnSetSmallestDistanceIndexAt(int index)
        {
            SetSmallestDistanceIndexAt(index);
        }

        public void OnSetNegativeBelowSlopeAngle()
        {
            SetNegativeBelowSlopeAngle();
        }
    }
}