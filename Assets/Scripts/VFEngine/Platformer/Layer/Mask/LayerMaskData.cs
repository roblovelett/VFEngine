using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    using static Physics2D;
    using static LayerMask;

    public struct LayerMaskData
    {
        #region fields

        #region private methods

        private void InitializeDependencies(LayerMaskSettings settings)
        {
            InitializeSettings(settings);
        }

        private void InitializeDependencies()
        {
            InitializeSettings();
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

        private void InitializeSettings()
        {
            DisplayWarningsControl = false;
            Ground = GetMask("Ground");
            OneWayPlatform = GetMask("OneWayPlatform");
            Ladder = GetMask("Ladder");
            Character = GetMask("Character");
            CharacterCollision = GetMask("CharacterCollision");
            StandOnCollision = GetMask("StandOnCollision");
            Interactive = GetMask("Interactive");
        }

        private void Initialize(GameObject character)
        {
            InitializeCharacter(character);
            InitializeInternal();
        }

        private void Initialize()
        {
            InitializeCharacter();
            InitializeInternal();
        }

        private void InitializeCharacter(GameObject character)
        {
            CharacterGameObject = character;
        }

        private void InitializeCharacter()
        {
            CharacterGameObject = new GameObject();
        }

        private void InitializeInternal()
        {
            Collision = CharacterCollision;
            SavedLayer = CharacterGameObject.layer;
            CharacterGameObject.layer = IgnoreRaycastLayer;
        }

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
        public LayerMask Collision { get; set; }
        public GameObject CharacterGameObject { get; set; }

        #region public methods

        #region constructors

        public LayerMaskData(LayerMaskSettings settings, GameObject character) : this()
        {
            InitializeDependencies(settings);
            Initialize(character);
        }

        public LayerMaskData(LayerMaskSettings settings) : this()
        {
            InitializeDependencies(settings);
            Initialize();
        }

        public LayerMaskData(GameObject character) : this()
        {
            InitializeDependencies();
            Initialize(character);
        }

        #endregion

        #endregion

        #endregion
    }
}