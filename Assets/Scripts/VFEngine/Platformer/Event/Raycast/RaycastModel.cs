using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastModel", menuName = "VFEngine/Platformer/Event/Raycast/Raycast Model",
        order = 0)]
    public class RaycastModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Raycast Data")] [SerializeField]
        private RaycastData r;

        private const string AssetPath = "Event/Raycast/DefaultRaycastModel.asset";

        /* fields: methods */
        private async UniTaskVoid InitializeModelAsyncInternal()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
        public void InitializeData()
        {
            r.Initialize();
        }

        public UniTask<UniTaskVoid> InitializeModelAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(InitializeModelAsyncInternal());
            }
            finally
            {
                InitializeModelAsyncInternal().Forget();
            }
        }
    }
}