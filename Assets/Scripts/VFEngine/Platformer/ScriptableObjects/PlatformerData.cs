using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static PlatformerData.PlatformerState;
    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    public class PlatformerData : ScriptableObject
    {
        #region events
          
        #endregion
          
        #region properties

        public int Index { get; private set; }
        public PlatformerState State { get; private set; } = None;

        public enum PlatformerState
        {
            None,
            Initialized,
            InitializedFrame
        }
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
            Index = 0;
            SetState(Initialized);
        }
        
        #endregion
          
        #region public methods
        
        #endregion
          
        #region private methods

        private void SetState(PlatformerState state)
        {
            State = state;
        }

        private void InitializeFrame()
        {
            SetState(InitializedFrame);
        }
        
        #endregion
          
        #region event handlers

        public void OnInitialize()
        {
            Initialize();
        }

        public void OnInitializedFrame()
        {
            InitializeFrame();
        }
          
        #endregion
    }
}