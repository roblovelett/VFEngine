using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
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

        [LabelText("Boxcast Data")] [SerializeField]
        private BoxcastData b = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            b.RuntimeData = b.Character.GetComponentNoAllocation<BoxcastController>().RuntimeData;
        }

        private void InitializeModel()
        {
            // foobar
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