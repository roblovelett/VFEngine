using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastSettings", menuName = PlatformerRaycastSettingsPath, order = 0)]
    [InlineEditor]
    public class RaycastSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public bool displayWarnings = true;
        [SerializeField] public bool drawGizmos = true;
        [SerializeField] public int totalHorizontalRays = 8;
        [SerializeField] public int totalVerticalRays = 8;
        [SerializeField] public float spacing = 0.125f;
        [SerializeField] public float skinWidth = 0.015f;

        #endregion
    }
}