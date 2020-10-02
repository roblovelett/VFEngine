using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;

    [RequireComponent(typeof(StickyRaycastController))]
    [RequireComponent(typeof(SafetyBoxcastController))]
    public class RaycastsController : MonoBehaviour
    {
        [SerializeField] private RaycastsModel model;

        private async void Awake()
        {
            await OnInitializeAsync();
        }

        private async UniTaskVoid OnInitializeAsyncInternal()
        {
            model.Initialize(model);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private UniTask<UniTaskVoid> OnInitializeAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(OnInitializeAsyncInternal());
            }
            finally
            {
                OnInitializeAsyncInternal().Forget();
            }
        }
    }
}