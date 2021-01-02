using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    public struct LayerMaskData
    {
        #region fields

        #region private methods

        private void InitializeDependencies(GameObject character, LayerMaskSettings settings)
        {
            InitializeCharacter(character);
            InitializeSettings(settings);
        }

        private void InitializeCharacter(GameObject character)
        {
            CharacterGameObject = character;
        }

        private void InitializeSettings(LayerMaskSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            Ground = settings.ground;
            OneWayPlatform = settings.oneWayPlatform;
            Ladder = settings.ladder;
            Character = settings.character;
            CharacterCollision = settings.characterCollision;
            StandOnCollision = settings.standOnCollision;
            Interactive = settings.interactive;
        }

        private void Initialize()
        {
            Collision = CharacterCollision;
            SavedLayer = 0;
        }

        //SavedLayer = CharacterGameObject.layer;
        //CharacterGameObject.layer = Physics2D.IgnoreRaycastLayer;

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public LayerMask Ground { get; private set; }
        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask Ladder { get; private set; }
        public LayerMask Character { get; private set; }
        public LayerMask CharacterCollision { get; private set; }
        public LayerMask StandOnCollision { get; private set; }
        public LayerMask Interactive { get; private set; }

        #endregion

        public int SavedLayer { get; set; }
        public LayerMask Collision { get; private set; }
        public GameObject CharacterGameObject { get; set; }

        #region public methods

        #region constructor

        public LayerMaskData(GameObject character, LayerMaskSettings settings) : this()
        {
            InitializeDependencies(character, settings);
            Initialize();
        }

        #endregion

        public void SetSavedLayer(LayerMask layer)
        {
            SavedLayer = layer;
        }

        public void SetCharacterLayer(LayerMask layer)
        {
            CharacterGameObject.layer = layer;
        }

        #endregion

        #endregion
    }
}