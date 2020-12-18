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

        [SerializeField] public bool castRaysOnBothSides;
        [SerializeField] public bool drawRaycastGizmosControl = true;
        [SerializeField] public bool displayWarningsControl = true;
        [SerializeField] public int numberOfHorizontalRays = 8;
        [SerializeField] public int numberOfVerticalRays = 8;
        [SerializeField] public float raySpacing = 0.125f;
        [SerializeField] public float skinWidth = 0.015f;

        #endregion
    }
}