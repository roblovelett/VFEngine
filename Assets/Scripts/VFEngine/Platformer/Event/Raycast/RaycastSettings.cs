using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    [CreateAssetMenu(fileName = "RaycastSettings", menuName = "VFEngine/Platformer/Event/Raycast/Raycast Settings",
        order = 0)]
    [InlineEditor]
    public class RaycastSettings : ScriptableObject
    {
        [SerializeField] public int numberOfHorizontalRays = 8;
        [SerializeField] public int numberOfVerticalRays = 8;
        [SerializeField] public float distanceToGroundRayMaximumLength = 100f;
        [SerializeField] public bool castRaysOnBothSides = true;
        [SerializeField] public float rayOffset = 0.05f;
        [LabelText("Draw Raycast Gizmos")] [SerializeField] public bool drawRaycastGizmosControl = true;
        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;
    }
}