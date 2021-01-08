using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    public class PlatformerData : ScriptableObject
    {
        #region events
          
        #endregion
          
        #region properties

        public float IgnorePlatformsTime { get; private set; }

        #endregion
          
        #region fields

        private bool displayWarnings;
        private float oneWayPlatformDelay; 
        private float ladderClimbThreshold; 
        private float ladderDelay; 
        private float tolerance;

        #endregion
          
        #region initialization

        private void InitializeInternal(PlatformerSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
        }

        private void ApplySettings(PlatformerSettings settings)
        {
            displayWarnings = settings.displayWarnings;
            oneWayPlatformDelay = settings.oneWayPlatformDelay;
            ladderClimbThreshold = settings.ladderClimbThreshold;
            ladderDelay = settings.ladderDelay;
        }

        private void InitializeDefault()
        {
            tolerance = 0;
            IgnorePlatformsTime = 0;
        }
          
        #endregion
          
        #region public methods

        public void Initialize(PlatformerSettings settings)
        {
            InitializeInternal(settings);
        }
        
        #endregion
          
        #region private methods
          
        #endregion
          
        #region event handlers
          
        #endregion
    }
}