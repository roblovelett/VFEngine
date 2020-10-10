using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;
    using static LayerMaskData;
    using static Debug;
    
    public class LayerMaskController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private LayerMaskModel model;

        /* fields */
        
        /* fields: methods */
        private void Awake()
        {
            //if (!model) model = LoadData(ModelPath) as LayerMaskModel;
            Assert(model != null, nameof(model) + " != null");
            //model.Initialize();
        }
    }
}