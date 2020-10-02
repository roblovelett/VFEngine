using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
namespace VFEngine.Platformer.Event.Raycast
{
    [CreateAssetMenu(fileName = "RaycastsSettings", menuName = "VFEngine/Platformer/Event/Raycast/Raycasts Settings",
        order = 0)]
    [InlineEditor]
    public class RaycastsSettings : ScriptableObject
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