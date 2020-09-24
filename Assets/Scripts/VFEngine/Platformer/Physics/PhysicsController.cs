using UnityEngine;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static PhysicsModel;
    using static ScriptableObjectExtensions;

    [RequireComponent(typeof(GravityController))]
    public class PhysicsController : MonoBehaviour, IController
    {
        /* fields */
        [SerializeField] private PhysicsModel model;

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as PhysicsModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            model.InitializeData();
        }

        /* properties */
        public ScriptableObject Model => model;
        /* properties: methods */
    }
}