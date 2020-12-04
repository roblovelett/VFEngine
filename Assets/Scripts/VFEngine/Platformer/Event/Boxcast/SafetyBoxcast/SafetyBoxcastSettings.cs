using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "SafetyBoxcastSettings", menuName = PlatformerSafetyBoxcastSettingsPath, order = 0)]
    [InlineEditor]
    public class SafetyBoxcastSettings : ScriptableObject
    {
        #region properties
        
        [SerializeField] public bool performSafetyBoxcast;
        
        #endregion
    }
}