using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    public class PlatformerData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public int Index { get; private set; }

        #endregion

        #region fields

        #endregion

        #region initialization

        private void Initialize()
        {
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            SetIndex(0);
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void SetIndex(int index)
        {
            Index = index;
        }

        #endregion

        #region event handlers

        public void OnInitialize()
        {
            Initialize();
        }

        #endregion
    }
}