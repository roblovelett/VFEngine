using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static StickyRaycastModel;
    using static ScriptableObjectExtensions;

    public class StickyRaycastController : MonoBehaviour, IController
    {
        /* fields */
        [SerializeField] private StickyRaycastModel model;

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as StickyRaycastModel;
            Debug.Assert(model != null, nameof(model) + " != null");
            model.InitializeData();
        }

        /* properties */
        public ScriptableObject Model => model;
        /* properties: methods */
    }
}