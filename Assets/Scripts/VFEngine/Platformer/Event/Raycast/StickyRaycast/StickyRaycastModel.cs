using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Model", order = 0)]
    public class StickyRaycastModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Sticky Raycast Data")] [SerializeField]
        private StickyRaycastData sr;

        private const string AssetPath = "Event/Raycast/Sticky Raycast/DefaultStickyRaycastModel.asset";

        /* fields: methods */
        private void InitializeData()
        {
            sr.Initialize();
        }

        private static void InitializeModel()
        {
        }

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
        public void Initialize()
        {
            InitializeData();
            InitializeModel();
        }
    }
}