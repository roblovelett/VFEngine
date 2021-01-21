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
        public float Tolerance { get; } = 0f;

        #endregion

        #region fields

        #endregion

        #region initialization

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void Initialize()
        {
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