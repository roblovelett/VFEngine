using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    [CreateAssetMenu(fileName = "StickyRaycastSettings",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Settings", order = 0)]
    [InlineEditor]
    public class StickyRaycastSettings : ScriptableObject
    {
        [SerializeField] public float stickToSlopesOffsetY = 0.2f;
    }
}