using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskSettings", menuName = PlatformerLayerMaskSettingsPath, order = 0)]
    [InlineEditor]
    public class LayerMaskSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public bool displayWarnings = true;
        [SerializeField] public LayerMask platform;
        [SerializeField] public LayerMask movingPlatform;
        [SerializeField] public LayerMask oneWayPlatform;
        [SerializeField] public LayerMask movingOneWayPlatform;
        [SerializeField] public LayerMask midHeightOneWayPlatform;
        [SerializeField] public LayerMask stairs;

        #endregion
    }
}