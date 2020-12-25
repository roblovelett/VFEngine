using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnassignedField.Global
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastSettings", menuName = PlatformerLayerMaskSettingsPath, order = 0)]
    [InlineEditor]
    public class LayerMaskSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public bool displayWarningsControl;
        public LayerMask ground;
        public LayerMask oneWayPlatform;
        public LayerMask ladder;
        public LayerMask character;
        public LayerMask characterCollision;
        public LayerMask standOnCollision;
        public LayerMask interactive;

        #endregion
    }
}