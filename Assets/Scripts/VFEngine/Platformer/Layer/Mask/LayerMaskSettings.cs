using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Layer.Mask
{
    [CreateAssetMenu(fileName = "LayerMaskSettings", menuName = "VFEngine/Platformer/Layer/Mask/Layer Mask Settings", order = 0)]
    [InlineEditor]
    public class LayerMaskSettings : ScriptableObject
    {
        [SerializeField] public LayerMask platformMask;
        [SerializeField] public LayerMask movingPlatformMask;
        [SerializeField] public LayerMask oneWayPlatformMask;
        [SerializeField] public LayerMask movingOneWayPlatformMask;
        [SerializeField] public LayerMask midHeightOneWayPlatformMask;
        [SerializeField] public LayerMask stairsMask;
        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;
    }
}