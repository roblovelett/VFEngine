using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast
{
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "BoxcastModel", menuName = PlatformerBoxcastModelPath, order = 0)]
    [InlineEditor]
    public class BoxcastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private BoxcastData b;

        #endregion

        #region private methods

        private void InitializeData()
        {
            b = new BoxcastData {Character = character};
            b.Controller = b.Character.GetComponentNoAllocation<BoxcastController>();
            b.RuntimeData = BoxcastRuntimeData.CreateInstance(b.Controller);
        }

        private void InitializeModel()
        {
            //
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}