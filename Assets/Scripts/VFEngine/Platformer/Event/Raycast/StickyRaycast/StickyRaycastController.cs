using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static Debug;
    using static StickyRaycastData;

    public class StickyRaycastController : MonoBehaviour, IController
    {
        [SerializeField] private StickyRaycastModel model;

        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as StickyRaycastModel;
            Assert(model != null, nameof(model) + " !=  null");
            model.OnInitialize();
        }
    }
}