using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastSettings", menuName = PlatformerRaycastSettingsPath, order = 0)]
    [InlineEditor]
    public class RaycastSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public float spacing = 0.125f;
        [SerializeField] public float skinWidth = 0.015f;
        [SerializeField] public float oneWayPlatformDelay = 0.1f;
        [SerializeField] public float ladderClimbThreshold = 0.3f;
        [SerializeField] public float ladderDelay = 0.3f;

        #endregion
    }
}