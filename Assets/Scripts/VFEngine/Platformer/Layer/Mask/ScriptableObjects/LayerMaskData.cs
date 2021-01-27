using UnityEngine;
using VFEngine.Tools;

// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = PlatformerLayerMaskDataPath, order = 0)]
    public class LayerMaskData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public LayerMask Platform { get; private set; }
        public LayerMask BelowPlatforms { get; private set; }
        public LayerMask SavedBelow { get; private set; }
        public LayerMask MidHeightOneWayPlatform { get; private set; }
        public LayerMask Stairs { get; private set; }
        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask MovingOneWayPlatform { get; private set; }
        public LayerMask BelowPlatformsWithoutOneWay { get; private set; }

        #endregion

        #region fields

        private bool displayWarnings;
        private LayerMask movingPlatform;
        private LayerMask platformSaved;
        private LayerMask belowPlatformsWithoutMidHeight;

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
            Platform = settings.platform;
            movingPlatform = settings.movingPlatform;
            OneWayPlatform = settings.oneWayPlatform;
            MovingOneWayPlatform = settings.movingOneWayPlatform;
            MidHeightOneWayPlatform = settings.midHeightOneWayPlatform;
            Stairs = settings.stairs;
        }

        private void InitializeDefault()
        {
            platformSaved = Platform;
            Platform |= OneWayPlatform;
            Platform |= movingPlatform;
            Platform |= MovingOneWayPlatform;
            Platform |= MidHeightOneWayPlatform;
        }

        #endregion

        #region public methods
        
        #endregion

        #region private methods

        private void SetRaysBelowPlatforms()
        {
            SetBelowPlatforms(Platform);
            SetBelowPlatformsWithoutOneWay(Platform & ~MidHeightOneWayPlatform & ~OneWayPlatform & ~MovingOneWayPlatform);
            SetBelowPlatformsWithoutMidHeight(BelowPlatforms & ~MidHeightOneWayPlatform);
        }

        private void SetBelowPlatforms(LayerMask layer)
        {
            BelowPlatforms = layer;
        }

        private void SetBelowPlatformsWithoutOneWay(LayerMask layer)
        {
            BelowPlatformsWithoutOneWay = layer;
        }

        private void SetBelowPlatformsWithoutMidHeight(LayerMask layer)
        {
            belowPlatformsWithoutMidHeight = layer;
        }

        private void SetSavedBelowLayerToStandingOnLastFrame(LayerMask layer)
        {
            SetSavedBelow(layer);
        }

        private void SetSavedBelow(LayerMask layer)
        {
            SavedBelow = layer;
        }

        private void SetRaysBelowToPlatformsWithoutMidHeight()
        {
            SetRaysBelowPlatforms(belowPlatformsWithoutMidHeight);
        }

        private void SetRaysBelowPlatforms(LayerMask layer)
        {
            SetBelowPlatforms(layer);
        }

        private void SetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            SetBelowPlatforms(BelowPlatforms & ~OneWayPlatform | Stairs);
        }

        private void SetRaysBelowToOneWayPlatform()
        {
            SetBelowPlatforms(BelowPlatforms & ~OneWayPlatform);
        }

        #endregion

        #region event handlers

        public void OnInitialize(LayerMaskSettings settings)
        {
            Initialize(settings);
        }

        public void OnSetRaysBelowPlatforms()
        {
            SetRaysBelowPlatforms();
        }

        public void OnSetSavedBelowLayerToStandingOnLastFrame(LayerMask layer)
        {
            SetSavedBelowLayerToStandingOnLastFrame(layer);
        }

        public void OnSetRaysBelowToPlatformsWithoutMidHeight()
        {
            SetRaysBelowToPlatformsWithoutMidHeight();
        }

        public void OnSetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            SetRaysBelowToPlatformsAndOneWayOrStairs();
        }

        public void OnSetRaysBelowToOneWayPlatform()
        {
            SetRaysBelowToOneWayPlatform();
        }
        
        #endregion
    }
}

#region hide

/*
public LayerMask Collision => characterCollision;
public LayerMask OneWayPlatform { get; private set; }
public LayerMask Ground { get; private set; }
private LayerMask Saved { get; set; }private LayerMask ladder;
private LayerMask character;
private LayerMask standOnCollision;
private LayerMask interactive;
private LayerMask characterCollision;
Ground = settings.ground;
OneWayPlatform = settings.oneWayPlatform;
characterCollision = settings.characterCollision;
ladder = settings.ladder;
character = settings.character;
standOnCollision = settings.standOnCollision;
interactive = settings.interactive;/*private void InitializeFrame(ref GameObject characterObject)
        {
            Saved = characterObject.layer;
            characterObject.layer = IgnoreRaycastLayer;
            Debug.Log("Init frame... Character's layer is: " + LayerMask.LayerToName(characterObject.layer));
        }*/
/*
private void SetSavedLayer(ref GameObject characterObject)
{
    Saved = characterObject.layer;
    characterObject.layer = IgnoreRaycastLayer;
}

private void ResetLayerMask(ref GameObject characterObject)
{
    characterObject.layer = Saved;
    Debug.Log("Exit frame... Character's layer is: " + LayerMask.LayerToName(characterObject.layer));
}public void OnInitialize(LayerMaskSettings settings)
{
    Initialize(settings);
}

/*public void OnInitializeFrame(ref GameObject characterObject)
{
    InitializeFrame(ref characterObject);
}*/
/*
public void OnSetSavedLayer(ref GameObject characterObject)
{
    SetSavedLayer(ref characterObject);
}

public void OnResetLayerMask(ref GameObject characterObject)
{
    ResetLayerMask(ref characterObject);
}
*/

#endregion