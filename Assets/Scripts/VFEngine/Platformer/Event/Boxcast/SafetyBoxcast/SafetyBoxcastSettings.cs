using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    [CreateAssetMenu(fileName = "SafetyBoxcastSettings", menuName = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast/Safety Boxcast Settings", order = 0)]
    [InlineEditor]
    public class SafetyBoxcastSettings : ScriptableObject
    {
        [LabelText("Draw Boxcast Gizmos")] [SerializeField] public bool drawBoxcastGizmosControl = true;
        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;
    }
}