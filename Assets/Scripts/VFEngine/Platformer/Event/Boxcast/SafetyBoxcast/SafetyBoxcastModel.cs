using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel",
        menuName = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast Model", order = 0)]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData sbc;

        private const string AssetPath = "Event/Boxcast/Safety Boxcast/DefaultSafetyBoxcastModel.asset";

        /* fields: methods */
        private void InitializeData()
        {
            sbc.Initialize();
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