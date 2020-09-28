using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static RaycastModel;
    using static ScriptableObjectExtensions;

    [RequireComponent(typeof(StickyRaycastController))]
    [RequireComponent(typeof(SafetyBoxcastController))]
    [RequireComponent(typeof(RaycastHitColliderController))]
    public class RaycastController : MonoBehaviour, IController
    {
        /* fields */
        [SerializeField] private RaycastModel model;

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as RaycastModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            model.Initialize();
        }

        /* properties */
        public ScriptableObject Model => model;
        /* properties: methods */
    }
}