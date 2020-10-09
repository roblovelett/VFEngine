using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    [CreateAssetMenu(fileName = "RaycastHitColliderSettings",
        menuName = "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Settings",
        order = 0)]
    [InlineEditor]
    public class RaycastHitColliderSettings : ScriptableObject
    {
        [LabelText("Draw Collider Gizmos")] [SerializeField]
        public bool drawColliderGizmosControl = true;

        [LabelText("Display Warnings")] [SerializeField]
        public bool displayWarningsControl = true;
    }
}