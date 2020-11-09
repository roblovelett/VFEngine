using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static PlatformerData;
    using static ScriptableObjectExtensions;
    using static Debug;

    [RequireComponent(typeof(LayerMaskController))]
    [RequireComponent(typeof(RaycastController))]
    [RequireComponent(typeof(PhysicsController))]
    public class PlatformerController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private PlatformerModel model;

        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as PlatformerModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        private void FixedUpdate()
        {
            model.OnRunPlatformer();
        }
    }
}