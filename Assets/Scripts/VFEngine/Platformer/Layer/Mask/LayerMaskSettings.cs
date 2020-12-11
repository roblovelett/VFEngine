using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace VFEngine.Platformer.Layer.Mask
{
    [CreateAssetMenu(fileName = "LayerMaskSettings", menuName = "VFEngine/Platformer/Layer/Mask/Layer Mask Settings",
        order = 0)]
    [InlineEditor]
    public class LayerMaskSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public LayerMask platform;
        [SerializeField] public LayerMask movingPlatform;
        [SerializeField] public LayerMask oneWayPlatform;
        [SerializeField] public LayerMask movingOneWayPlatform;
        [SerializeField] public LayerMask midHeightOneWayPlatform;
        [SerializeField] public LayerMask stairs;

        [LabelText("Display Warnings")] [SerializeField]
        public bool displayWarningsControl;

        #endregion
    }
}