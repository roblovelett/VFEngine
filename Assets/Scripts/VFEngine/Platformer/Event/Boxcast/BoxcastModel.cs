using System;
using Cysharp.Threading.Tasks;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Boxcast
{
    using static UniTaskExtensions;

    [Serializable]
    public class BoxcastModel
    {
        #region fields

        #region dependencies

        private BoxcastData b;

        #endregion

        #region private methods

        private void InitializeData()
        {
            b = new BoxcastData();
        }

        private void InitializeModel()
        {
            //
        }

        #endregion

        #endregion

        #region properties

        public BoxcastData Data => b;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
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