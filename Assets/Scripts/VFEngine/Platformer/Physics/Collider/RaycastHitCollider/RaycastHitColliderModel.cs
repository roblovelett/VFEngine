using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static ColliderDirection;

    [CreateAssetMenu(fileName = "RaycastHitColliderModel",
        menuName = "VFEngine/Platformer/Physics/Raycast Hit Collider/Raycast Hit Collider Model", order = 0)]
    public class RaycastHitColliderModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Raycast Hit Collider Data")] [SerializeField]
        private RaycastHitColliderData r;

        /* fields */
        private const string Rh = "Raycast Hit Collider";
        private readonly string data = $"{Rh} Data";

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
            var xRaysNumber = r.NumberOfHorizontalRays;
            var yRaysNumber = r.NumberOfVerticalRays;
            switch (direction)
            {
                case Up:
                    r.UpHitStorage = new RaycastHit2D[SetHalf(yRaysNumber)];
                    break;
                case Right:
                    r.RightHitStorage = r.CastRaysBothSides
                        ? new RaycastHit2D[SetHalf(xRaysNumber)]
                        : new RaycastHit2D[xRaysNumber];
                    break;
                case Down:
                    r.DownHitStorage = new RaycastHit2D[SetHalf(yRaysNumber)];
                    break;
                case Left:
                    r.LeftHitStorage = r.CastRaysBothSides
                        ? new RaycastHit2D[SetHalf(xRaysNumber)]
                        : new RaycastHit2D[xRaysNumber];
                    break;
                case None: break;
                default:
                    if (r.DisplayWarnings)
                        DebugLogWarning(1,
                            $"{Rh} initialized with incorrect value. Please use ColliderDirection of " +
                            "value Up, Right, Down, Left, or None.");
                    break;
            }

            r.BoxColliderSizeRef = r.OriginalColliderSize;
            r.BoxColliderSizeRef = r.OriginalColliderOffset;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static int SetHalf(int number)
        {
            return number / 2;
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (r.DisplayWarnings)
            {
                const string bc = "Box Collider 2D";
                const string colliderWarning = "This may cause issues upon direction change near walls";
                var settings = $"{Rh} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!r.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                if (!r.HasBoxCollider) warningMessage += FieldParentString($"{bc}", $"{settings}");
                if (r.OriginalColliderOffset.x != 0)
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
            r.contactList.Clear();
            r.state.SetCurrentWallCollider(null);
            r.state.Reset();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties: dependencies */

        /* properties: methods */
        public UniTask<UniTaskVoid> Initialize(ColliderDirection direction)
        {
            return Async(InitializeInternal(direction));
        }
    }
}