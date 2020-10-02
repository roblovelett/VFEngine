using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static DebugExtensions;

    public class RaycastsModel : ScriptableObject
    {
        /* fields */
        [SerializeField] private RaycastsData r;

        /* fields: methods */
        private void InitializeData()
        {
            r.numberOfHorizontalRays.Value = r.settings.numberOfHorizontalRays;
            r.numberOfVerticalRays.Value = r.settings.numberOfVerticalRays;
            r.castRaysOnBothSides.Value = r.settings.castRaysOnBothSides;
        }

        private void InitializeModel(RaycastsModel model)
        {
            if (!model) model = LoadData(r.modelPath) as RaycastsModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            if (r.DisplayWarnings) GetWarningMessages();
            SetRaysParameters();
        }

        private void GetWarningMessages()
        {
            const string ra = "Rays";
            const string rc = "Raycast";
            const string nuOf = "Number of";
            const string diGrRa = "Distance To Ground Ray Maximum Length";
            var settings = $"{rc} Settings";
            var rcOf = $"{rc} Offset";
            var nuOfHoRa = $"{nuOf} Horizontal {ra}";
            var nuOfVeRa = $"{nuOf} Vertical {ra}";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!r.settings)
            {
                warningMessage += FieldMessage($"{settings}", $"{settings}");
                warningMessageCount++;
            }

            if (r.NumberOfHorizontalRays < 0)
            {
                warningMessage += LtZeroMessage($"{nuOfHoRa}", $"{settings}");
                warningMessageCount++;
            }

            if (r.NumberOfVerticalRays < 0)
            {
                warningMessage += LtZeroMessage($"{nuOfVeRa}", $"{settings}");
                warningMessageCount++;
            }

            if (r.DistanceToGroundRayMaximumLength <= 0)
            {
                warningMessage += LtEqZeroMessage($"{diGrRa}", $"{settings}");
                warningMessageCount++;
            }

            if (r.RayOffset <= 0)
            {
                warningMessage += LtEqZeroMessage($"{rcOf}", $"{settings}");
                warningMessageCount++;
            }

            DebugLogWarning(warningMessageCount, warningMessage);
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

        /*
        private async UniTaskVoid SetRaysParametersAsyncInternal()
        {
            SetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysRightAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysLeftAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysDownAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysUpAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        */

        /* properties: methods */
        public void Initialize(RaycastsModel model)
        {
            InitializeData();
            InitializeModel(model);
        }

        /*
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
        public UniTask<UniTaskVoid> CastRaysRightAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysRightAsyncInternal());
            }
            finally
            {
                CastRaysRightAsyncInternal().Forget();
            }
        }
        public UniTask<UniTaskVoid> CastRaysLeftAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysLeftAsyncInternal());
            }
            finally
            {
                CastRaysLeftAsyncInternal().Forget();
            }
        }
        public UniTask<UniTaskVoid> CastRaysDownAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysDownAsyncInternal());
            }
            finally
            {
                CastRaysDownAsyncInternal().Forget();
            }
        }
        public UniTask<UniTaskVoid> CastRaysUpAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysUpAsyncInternal());
            }
            finally
            {
                CastRaysUpAsyncInternal().Forget();
            }
        }
        */
    }
}