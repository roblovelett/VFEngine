using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask
{
    using static LayerMaskModel;
    using static ScriptableObjectExtensions;

    public class LayerMaskController : MonoBehaviour, IController
    {
        
    }
}

/* fields */
/*
[SerializeField] private LayerMaskModel model;

/* fields: methods */
/*
private void Awake()
{
if (!model) model = LoadData(ModelPath) as LayerMaskModel;
Debug.Assert(model != null, nameof(model) + " != null");
model.Initialize();
}

/* properties */
/*
public ScriptableObject Model => model;
/* properties: methods */