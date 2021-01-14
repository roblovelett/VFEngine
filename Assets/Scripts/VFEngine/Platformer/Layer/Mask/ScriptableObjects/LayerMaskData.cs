using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Physics2D;
    using static LayerMaskData.LayerMaskState;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = PlatformerLayerMaskDataPath, order = 0)]
    public class LayerMaskData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask Collision { get; private set; }
        public LayerMask Saved { get; private set; }
        public LayerMaskState State { get; private set; } = None;

        public enum LayerMaskState
        {
            None,
            Initialized,
            PlatformerInitializedFrame
        }

        #endregion

        #region fields

        private bool displayWarnings;
        private LayerMask ground;
        private LayerMask ladder;
        private LayerMask character;
        private LayerMask characterCollision;
        private LayerMask standOnCollision;
        private LayerMask interactive;

        #endregion

        #region initialization

        private void Initialize(LayerMaskSettings settings)
        {
            ApplySettings(settings);
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

        private void InitializeDefault()
        {
            Collision = characterCollision;
            Saved = 0;
            SetState(Initialized);
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void SetState(LayerMaskState state)
        {
            State = state;
        }

        private void InitializeFrame(ref GameObject characterObject)
        {
            SetSavedLayer(characterObject.layer);
            SetLayer(ref characterObject, IgnoreRaycastLayer);
            SetState(PlatformerInitializedFrame);
        }

        private void SetSavedLayer(LayerMask layer)
        {
            Saved = layer;
        }

        private static void SetLayer(ref GameObject characterObject, LayerMask layer)
        {
            characterObject.layer = layer;
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

        #endregion
    }
}

#region hide

/*
        private void SetSavedLayerInternal(GameObject characterObject)
        {
            Saved = characterObject.layer;
        }

        private static void SetToIgnoreRaycastInternal(ref GameObject characterObject)
        {
            characterObject.layer = IgnoreRaycastLayer;
        }*/

#endregion