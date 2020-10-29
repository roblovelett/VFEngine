using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static Vector2;
    using static Color;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel",
        menuName = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast Model", order = 0)]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        [SerializeField] private SafetyBoxcastData sb;

        private async UniTaskVoid Initialize()
        {
            var sbTask1 = Async(InitializeData());
            var sbTask2 = Async(GetWarningMessages());
            var task1 = await (sbTask1, sbTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            sb.HasSafetyBoxcastRef = sb.HasSafetyBoxcast;
            sb.SafetyBoxcastColliderRef = sb.SafetyBoxcastCollider;
            sb.SafetyBoxcastRef = sb.SafetyBoxcast;
            sb.SafetyBoxcastDistanceRef = sb.SafetyBoxcastDistance;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = sb.Transform.up;
            sb.SafetyBoxcast = Boxcast(sb.BoundsCenter, sb.Bounds, Angle(transformUp, up),
                -transformUp, sb.StickyRaycastLength, sb.RaysBelowLayerMaskPlatforms, red, true);
        }

        private void SetHasSafetyBoxcast()
        {
            sb.HasSafetyBoxcast = true;
        }
        
        private void SetSafetyBoxcast()
        {
            var transformUp = sb.Transform.up;
            sb.SafetyBoxcast = Boxcast(sb.BoundsCenter, sb.Bounds, Angle(transformUp, up),
                sb.NewPosition.normalized, sb.NewPosition.magnitude, sb.PlatformMask, red, true);
        }
        public void OnInitialize()
        {
            Async(Initialize());
        }

        public void OnSetSafetyBoxcastForImpassableAngle()
        {
            SetSafetyBoxcastForImpassableAngle();
        }

        public void OnSetHasSafetyBoxcast()
        {
            SetHasSafetyBoxcast();
        }

        public void OnSetSafetyBoxcast()
        {
            SetSafetyBoxcast();
        }
    }
}