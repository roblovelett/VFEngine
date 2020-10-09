using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Gravity
{
    using static ScriptableObjectExtensions;

    public class GravityController : MonoBehaviour, IController
    {
       
    }
}

/*
/* fields */
/*
[SerializeField] private GravityModel model;

/* fields: methods */
/*
private void Awake()
{
GetModel();

void GetModel()
{
    if (!model)
        model = LoadData(
                "Assets/Scripts/VFEngine/Platformer/ScriptableObjects/Physics/Gravity/DefaultGravityModel.asset")
            as GravityModel;
}
}
        
/* properties */
/*
public ScriptableObject Model => model;
        
/* properties: methods */