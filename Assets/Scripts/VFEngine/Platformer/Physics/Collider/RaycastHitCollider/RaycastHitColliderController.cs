using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static RaycastHitColliderModel;
    using static ScriptableObjectExtensions;

    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastHitColliderController : MonoBehaviour, IController
    {
        /* fields */
        [SerializeField] private RaycastHitColliderModel model;

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as RaycastHitColliderModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            model.InitializeData();
        }

        /* properties */
        public ScriptableObject Model => model;
        /* properties: methods */
    }
}