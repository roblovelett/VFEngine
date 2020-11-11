using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastHitColliderSettings", menuName = PlatformerRaycastHitColliderSettingsPath,
        order = 0)]
    [InlineEditor]
    public class RaycastHitColliderSettings : ScriptableObject
    {
        #region properties

        [LabelText("Draw Collider Gizmos")] [SerializeField]
        public bool drawColliderGizmosControl = true;

        [LabelText("Display Warnings")] [SerializeField]
        public bool displayWarningsControl = true;

        #endregion
    }
}