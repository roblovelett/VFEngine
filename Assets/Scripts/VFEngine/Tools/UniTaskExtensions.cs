using Cysharp.Threading.Tasks;

namespace VFEngine.Tools
{
    using static UniTask;

    public static class UniTaskExtensions
    {
        public static async UniTask SetYieldOrSwitchToThreadPoolAsync()
        {
#if UNITY_WEBGL
            await Yield();
#else
            await SwitchToThreadPool();
#endif
        }
    }
}