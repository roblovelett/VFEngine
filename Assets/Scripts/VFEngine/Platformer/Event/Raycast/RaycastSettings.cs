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

        [SerializeField] public int numberOfHorizontalRays = 8;
        [SerializeField] public int numberOfVerticalRays = 8;
        [SerializeField] public float distanceToGroundRayMaximumLength = 100f;
        [SerializeField] public bool castRaysOnBothSides = true;
        [SerializeField] public float rayOffset = 0.05f;
        [SerializeField] public bool performSafetyBoxcast = true;

        [LabelText("Draw Raycast Gizmos")] [SerializeField]
        public bool drawRaycastGizmosControl = true;

        [LabelText("Display Warnings")] [SerializeField]
        public bool displayWarningsControl = true;

        #endregion
    }
}