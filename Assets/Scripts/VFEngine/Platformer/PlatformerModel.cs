using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = "VFEngine/Platformer/Platformer Model", order = 0)]
    public class PlatformerModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Platformer Data")] [SerializeField]
        private PlatformerData p;

        private const string AssetPath = "DefaultPlatformerModel.asset";
        /* fields: methods */

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
        public void InitializeData()
        {
            p.Initialize();
        }
    }
}

/*
public UniTask<UniTaskVoid> InitializeAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(InitializeAsyncInternal());
    }
    finally
    {
        InitializeAsyncInternal().Forget();
    }
}
private async UniTaskVoid InitializeAsyncInternal()
{*/
/*
var phInitialize = physics.model.InitializeAsync();
var grInitialize = gravity.model.InitializeAsync();
var rcInitialize = raycast.model.InitializeAsync();
var rhcInitialize = raycastHitCollider.model.InitializeAsync();
var srcInitialize = stickyRaycast.model.InitializeAsync();
var sbcInitialize = safetyBoxcast.model.InitializeAsync();
var lmInitialize = layerMask.model.InitializeAsync();
var (ph, gr, rc, rhc, src, sbc, lm) = await WhenAll(phInitialize, grInitialize, rcInitialize, rhcInitialize,
    srcInitialize, sbcInitialize, lmInitialize);
await SetYieldOrSwitchToThreadPoolAsync();
*/
//}