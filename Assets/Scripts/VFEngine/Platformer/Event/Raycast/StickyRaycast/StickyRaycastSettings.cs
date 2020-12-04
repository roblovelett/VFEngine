using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastSettings", menuName = PlatformerStickyRaycastSettingsPath, order = 0)]
    [InlineEditor]
    public class StickyRaycastSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public bool stickToSlopeControl;
        [SerializeField] public float stickyRaycastLength;
        [SerializeField] public float stickToSlopesOffsetY = 0.2f;
        [SerializeField] public bool displayWarningsControl;

        #endregion
    }
}