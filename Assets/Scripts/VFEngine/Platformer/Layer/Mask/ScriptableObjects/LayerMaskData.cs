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

        #endregion

        #region fields

        private bool displayWarnings;
        private LayerMask platform;
        private LayerMask movingPlatform;
        private LayerMask oneWayPlatform;
        private LayerMask movingOneWayPlatform;
        private LayerMask midHeightOneWayPlatform;
        private LayerMask stairs;
        private LayerMask saved;
        private LayerMask belowPlatforms;
        private LayerMask belowPlatformsWithoutOneWay;
        private LayerMask belowPlatformsWithoutMidHeight;
        private LayerMask savedBelow;

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
            platform = settings.platform;
            movingPlatform = settings.movingPlatform;
            oneWayPlatform = settings.oneWayPlatform;
            movingOneWayPlatform = settings.movingOneWayPlatform;
            midHeightOneWayPlatform = settings.midHeightOneWayPlatform;
            stairs = settings.stairs;
        }

        private void InitializeDefault()
        {
            saved = platform;
            platform |= oneWayPlatform;
            platform |= movingPlatform;
            platform |= movingOneWayPlatform;
            platform |= midHeightOneWayPlatform;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        #endregion

        #region event handlers

        public void OnInitialize(LayerMaskSettings settings)
        {
            Initialize(settings);
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