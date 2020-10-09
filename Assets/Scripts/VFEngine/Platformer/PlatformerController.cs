using UnityEngine;
using VFEngine.Platformer.Event.Raycasts;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static PlatformerModel;
    using static ScriptableObjectExtensions;

    [RequireComponent(typeof(LayerMaskController))]
    [RequireComponent(typeof(RaycastsController))]
    [RequireComponent(typeof(PhysicsController))]
    public class PlatformerController : MonoBehaviour, IController
    {
        
    }
}

/* fields */
/*
[SerializeField] private PlatformerModel model;

/* fields: methods */
/*
private void Awake()
{
if (!model) model = LoadData(ModelPath) as PlatformerModel;
Debug.Assert(model != null, nameof(model) + " != null");
model.Initialize();
}

private async void FixedUpdate()
{
await model.PlatformerAsync();
}

/* properties */
/*
public ScriptableObject Model => model;
/* properties: methods */