using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = PlatformerLayerMaskDataPath, order = 0)]
    public class LayerMaskData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask Collision { get; private set; }
        public LayerMask Saved { get; private set; }

        #endregion

        #region fields

        private bool displayWarnings;
        private LayerMask ground;
        private LayerMask ladder;
        private LayerMask character;
        private LayerMask characterCollision;
        private LayerMask standOnCollision;
        private LayerMask interactive;
        private GameObject characterObject;

        #endregion

        #region initialization

        private void InitializeInternal(GameObject characterGameObject, LayerMaskSettings settings)
        {
            ApplySettings(settings);
            InitializeCharacter(characterGameObject);
            InitializeDefault();
        }

        private void ApplySettings(LayerMaskSettings settings)
        {
            displayWarnings = settings.displayWarnings;
            ground = settings.ground;
            OneWayPlatform = settings.oneWayPlatform;
            ladder = settings.ladder;
            character = settings.character;
            characterCollision = settings.characterCollision;
            standOnCollision = settings.standOnCollision;
            interactive = settings.interactive;
        }

        private void InitializeCharacter(GameObject characterGameObject)
        {
            characterObject = characterGameObject;
        }

        private void InitializeDefault()
        {
            Collision = characterCollision;
            Saved = 0;
        }

        #endregion

        #region public methods

        public void Initialize(GameObject characterGameObject, LayerMaskSettings settings)
        {
            InitializeInternal(characterGameObject, settings);
        }

        public void SetSaved(LayerMask layer)
        {
            Saved = layer;
        }

        public void OnInitializeFrame(LayerMask layer)
        {
            Saved = layer;
        }

        #endregion

        #region private methods

        #endregion

        #region event handlers

        #endregion
    }
}