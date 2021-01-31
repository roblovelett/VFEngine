using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Character.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static CharacterSettings.CharacterType;
    using static CharacterSettings.SpawnFacingDirection;
    
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = PlatformerCharacterSettingsPath, order = 0)]
    [InlineEditor]

    public class CharacterSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public CharacterType characterType = AI;
        [SerializeField] public SpawnFacingDirection spawnFacingDirection = Default;
        [SerializeField] public FacingDirection initialFacingDirection = FacingDirection.Right;
        [SerializeField] public string playerID = "";
        
        
        public enum FacingDirection { Left, Right }
        public enum SpawnFacingDirection { Default, Left, Right }
        public enum CharacterType { Player, AI }
        
        #endregion
    }
}