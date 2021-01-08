using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    [InlineEditor]
    public class PlatformerData : ScriptableObject
    {
        #region events
          
        #endregion
          
        #region properties
        
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public bool DisplayWarnings
        {
            get => displayWarnings;
            set => value = displayWarnings;
        }
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public float OneWayPlatformDelay
        {
            get => oneWayPlatformDelay;
            set => value = oneWayPlatformDelay;
        }
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public float LadderClimbThreshold
        {
            get => ladderClimbThreshold;
            set => value = ladderClimbThreshold;
        }
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public float LadderDelay
        {
            get => ladderDelay;
            set => value = ladderDelay;
        }
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public float Tolerance
        {
            get => tolerance;
            set => value = tolerance;
        }
        [FoldoutGroup("Runtime Data", false)]
        [ShowInInspector]
        [ReadOnly]
        public float IgnorePlatformsTime
        {
            get => ignorePlatformsTime;
            set => value = ignorePlatformsTime;
        }
          
        #endregion
          
        #region fields

        private bool displayWarnings;
        private float oneWayPlatformDelay; 
        private float ladderClimbThreshold; 
        private float ladderDelay; 
        private float tolerance; 
        private float ignorePlatformsTime;
        
        #endregion
          
        #region initialization

        private void InitializeInternal(PlatformerSettings settings)
        {
            ApplySettings(settings);
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
            ignorePlatformsTime = 0;
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