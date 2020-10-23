using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    [CreateAssetMenu(fileName = "StickyRaycastSettings",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Settings", order = 0)]
    [InlineEditor]
    public class StickyRaycastSettings : ScriptableObject
    {
        [SerializeField] public float stickyRaycastLength = 0.5f;
        [SerializeField] public float stickToSlopesOffsetY = 0.2f;
        [LabelText("Draw Raycast Gizmos")] [SerializeField] public bool drawRaycastGizmosControl = true;
        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;
    }
}