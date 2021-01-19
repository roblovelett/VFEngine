using UnityEngine;
using VFEngine.Tools;

// ReSharper disable NotAccessedField.Local

namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Physics2D;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = PlatformerLayerMaskDataPath, order = 0)]
    public class LayerMaskData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public LayerMask Collision => characterCollision;
        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask Ground { get; private set; }
        private LayerMask Saved { get; set; }

        #endregion

        #region fields

        private LayerMask ladder;
        private LayerMask character;
        private LayerMask standOnCollision;
        private LayerMask interactive;
        private LayerMask characterCollision;

        #endregion

        #region initialization

        private void Initialize(LayerMaskSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
        }

        private void ApplySettings(LayerMaskSettings settings)
        {
            Ground = settings.ground;
            OneWayPlatform = settings.oneWayPlatform;
            characterCollision = settings.characterCollision;
            ladder = settings.ladder;
            character = settings.character;
            standOnCollision = settings.standOnCollision;
            interactive = settings.interactive;
        }

        private void InitializeDefault()
        {
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void InitializeFrame(ref GameObject characterObject)
        {
            Saved = characterObject.layer;
            characterObject.layer = IgnoreRaycastLayer;
            Debug.Log("Init frame... Character's layer is: " + LayerMask.LayerToName(characterObject.layer));
        }

        private void ResetLayerMask(ref GameObject characterObject)
        {
            characterObject.layer = Saved;
            Debug.Log("Exit frame... Character's layer is: " + LayerMask.LayerToName(characterObject.layer));
        }
        
        #endregion

        #region event handlers

        public void OnInitialize(LayerMaskSettings settings)
        {
            Initialize(settings);
        }

        public void OnInitializeFrame(ref GameObject characterObject)
        {
            InitializeFrame(ref characterObject);
        }

        public void OnResetLayerMask(ref GameObject characterObject)
        {
            ResetLayerMask(ref characterObject);
        }

        #endregion
    }
}