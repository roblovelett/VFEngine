using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderController : MonoBehaviour
    {
        
    }
}

/*
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.Manager
{
    using static ScriptableObjectExtensions;
    using static RaycastHitCollidersManagerModel;

    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastHitCollidersManager : MonoBehaviour, IController
    {
        /* fields */
  //      [SerializeField] private RaycastHitCollidersManagerModel model;

        /* fields: methods */
    /*    private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as RaycastHitCollidersManagerModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            model.Initialize();
        }

        /* properties */
      //  public ScriptableObject Model => model;
        /* properties: methods */
   /* }
}*/