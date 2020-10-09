using UnityEngine;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static Debug;
    using static PhysicsData;
    using static ScriptableObjectExtensions;

    [RequireComponent(typeof(GravityController))]
    public class PhysicsController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private PhysicsModel model;

        /* fields: methods */
        private void Awake()
        {
            GetModel();
            model.Initialize();
        }

        private void GetModel()
        {
            if (!model) model = LoadData(ModelPath) as PhysicsModel;
            Assert(model != null, nameof(model) + " != null");
        }
    }
}