using Cysharp.Threading.Tasks;

namespace VFEngine.Tools
{
    using static UniTask;

    public static class UniTaskExtensions
    {
        public static UniTask<UniTaskVoid> Async(UniTaskVoid method)
        {
            try
            {
                return new UniTask<UniTaskVoid>(method);
            }
            finally
            {
                method.Forget();
            }
        }

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