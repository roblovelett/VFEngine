using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;
    using static PlatformerExtensions;
    using static PhysicsExtensions;
    using static TimeExtensions;

    [CreateAssetMenu(fileName = "RaycastModel", menuName = "VFEngine/Platformer/Event/Raycast/Raycast Model",
        order = 0)]
    public class RaycastModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Raycast Data")] [SerializeField]
        private RaycastData r;

        private const string AssetPath = "Event/Raycast/DefaultRaycastModel.asset";

        /* fields: methods */
        private void InitializeData()
        {
            r.Initialize();
        }
        private void InitializeModel()
        {
            SetRaysParameters();
        }
        private void SetRaysParameters()
        {
            var top = SetPositiveAxis(r.BoxColliderOffset.y, r.BoxColliderSize.y);
            var right = SetPositiveAxis(r.BoxColliderOffset.x, r.BoxColliderSize.x);
            var bottom = SetNegativeAxis(r.BoxColliderOffset.y, r.BoxColliderSize.y);
            var left = SetNegativeAxis(r.BoxColliderOffset.x, r.BoxColliderSize.x);
            r.BoundsTopLeftCorner = new Vector3 {x = left, y = top};
            r.BoundsTopRightCorner = new Vector3 {x = right, y = top};
            r.BoundsBottomLeftCorner = new Vector3 {x = left, y = bottom};
            r.BoundsBottomRightCorner = new Vector3 {x = right, y = bottom};
            r.BoundsTopLeftCorner = r.Transform.TransformPoint(r.BoundsTopLeftCorner);
            r.BoundsTopRightCorner = r.Transform.TransformPoint(r.BoundsTopRightCorner);
            r.BoundsBottomLeftCorner = r.Transform.TransformPoint(r.BoundsBottomLeftCorner);
            r.BoundsBottomRightCorner = r.Transform.TransformPoint(r.BoundsBottomRightCorner);
            r.BoundsCenter = r.BoxColliderBoundsCenter;
            r.BoundsWidth = Vector2.Distance(r.BoundsBottomLeftCorner, r.BoundsBottomRightCorner);
            r.BoundsHeight = Vector2.Distance(r.BoundsBottomLeftCorner, r.BoundsTopLeftCorner);
        }

        private static float SetPositiveAxis(float offset, float size)
        {
            return offset + size / 2f;
        }

        private static float SetNegativeAxis(float offset, float size)
        {
            return offset - size / 2f;
        }
        
        private async UniTaskVoid SetRaysParametersAsyncInternal()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        
        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
        public void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        public UniTask<UniTaskVoid> SetRaysParametersAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(SetRaysParametersAsyncInternal());
            }
            finally
            {
                SetRaysParametersAsyncInternal().Forget();
            }
        }
    }
}

/*
private async UniTaskVoid OnPlatformerSetRaysParametersAsyncInternal()
{
SetRaysParameters();
await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid OnPlatformerTestMovingPlatformAsyncInternal()
{
var test = MovingPlatformTest(r.IsCollidingWithMovingPlatform, TimeLteZero(),
        AxisSpeedNan(r.MovingPlatformCurrentSpeed), r.WasTouchingCeilingLastFrame);
    if (test) SetRaysParameters();
await SetYieldOrSwitchToThreadPoolAsync();
}
public UniTask<UniTaskVoid> OnPlatformerTestMovingPlatformAsync()
{
try
{
    return new UniTask<UniTaskVoid>(OnPlatformerTestMovingPlatformAsyncInternal());
}
finally
{
    OnPlatformerTestMovingPlatformAsyncInternal().Forget();
}
}*/