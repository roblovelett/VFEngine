using UnityEngine;
using VFEngine.Tools;
using Debug = System.Diagnostics.Debug;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static SafetyBoxcastModel;
    using static ScriptableObjectExtensions;

    public class SafetyBoxcastController : MonoBehaviour, IController
    {
        
    }
}

/* fields */
/*
[SerializeField] private SafetyBoxcastModel model;

/* fields: methods */
/*
private void Awake()
{
if (!model) model = LoadData(ModelPath) as SafetyBoxcastModel;
Debug.Assert(model != null, nameof(model) + " != null");
model.Initialize();
}

/* properties */
/*
public ScriptableObject Model => model;
/* properties: methods */