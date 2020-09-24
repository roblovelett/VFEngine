using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer
{
    [CreateAssetMenu(fileName = "PlatformerSettings", menuName = "VFEngine/Platformer/Platformer Settings", order = 0)]
    [InlineEditor]
    public class PlatformerSettings : ScriptableObject
    {
        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;
    }
}