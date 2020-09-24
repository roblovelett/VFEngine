using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel",
        menuName = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast Model", order = 0)]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData sbc;

        private const string AssetPath = "Event/Boxcast/Safety Boxcast/DefaultSafetyBoxcastModel.asset";

        /* fields: methods */
        private async UniTaskVoid InitializeModelAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
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

        public void InitializeData()
        {
            sbc.Initialize();
        }
    }
}