using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    using static LayerMask;
    using static Physics2D;

    public class LayerMaskData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public LayerMask Ground { get; private set; }
        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask Ladder { get; private set; }
        public LayerMask Character { get; private set; }
        public LayerMask CharacterCollision { get; private set; }
        public LayerMask StandOnCollision { get; private set; }
        public LayerMask Interactable { get; private set; }

        #endregion

        public int Layer { get; set; }
        public GameObject CharacterObject { get; set; }
        public LayerMask Collision { get; set; }
        

        #region public methods

        public void ApplySettings(LayerMaskSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            Ground = settings.ground;
            OneWayPlatform = settings.oneWayPlatform;
            Ladder = settings.ladder;
            Character = settings.character;
            CharacterCollision = settings.characterCollision;
            StandOnCollision = settings.standOnCollision;
            Interactable = settings.interactable;
        }

        public void Initialize(GameObject character)
        {
            LayerMask[] masks =
            {
                Ground, OneWayPlatform, Ladder, Character, CharacterCollision, StandOnCollision, Interactable
            };
            foreach (var currentMask in masks)
            {
                if (currentMask != 0) continue;
                if (currentMask == Ground) Ground = GetMask("Ground");
                if (currentMask == OneWayPlatform) OneWayPlatform = GetMask("OneWayPlatform");
                if (currentMask == Ladder) Ladder = GetMask("Ladder");
                if (currentMask == Character) Character = GetMask("Character");
                if (currentMask == CharacterCollision) CharacterCollision = GetMask("CharacterCollision");
                if (currentMask == StandOnCollision) StandOnCollision = GetMask("StandOnCollision");
                if (currentMask == Interactable) Interactable = GetMask("Interactable");
            }

            Collision = CharacterCollision;
            CharacterObject = character;
        }

        public void SetLayerToCharacter()
        {
            Layer = CharacterObject.layer;
        }

        public void SetCharacterLayerToIgnoreRaycast()
        {
            CharacterObject.layer = IgnoreRaycastLayer;
        }

        #endregion

        #endregion
    }
}