using UnityEngine;
using VFEngine.Tools;
using VFEngine.Tools.LayerMask;

// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = LayerMaskDataPath, order = 0)]
    public class LayerMaskData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public LayerMask Platform { get; private set; }
        public LayerMask BelowPlatforms { get; private set; }
        public LayerMask SavedBelow { get; private set; }
        private LayerMask MidHeightOneWayPlatform { get; set; }
        private LayerMask Stairs { get; set; }
        public LayerMask OneWayPlatform { get; private set; }
        public LayerMask MovingOneWayPlatform { get; private set; }
        public LayerMask BelowPlatformsWithoutOneWay { get; private set; }
        public bool MidHeightOneWayPlatformContainsStandingOnLastFrame { get; private set; }
        public bool StairsContainsStandingOnLastFrame { get; private set; }
        public bool OneWayPlatformContainsStandingOn { get; private set; }
        public bool MovingOneWayPlatformContainsStandingOn { get; private set; }

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

        private void RaysBelowPlatforms()
        {
            BelowPlatformsInternal(Platform);
            SetBelowPlatformsWithoutOneWay(
                Platform & ~MidHeightOneWayPlatform & ~OneWayPlatform & ~MovingOneWayPlatform);
            BelowPlatformsWithoutMidHeight(BelowPlatforms & ~MidHeightOneWayPlatform);
        }

        private void BelowPlatformsInternal(LayerMask layer)
        {
            BelowPlatforms = layer;
        }

        private void SetBelowPlatformsWithoutOneWay(LayerMask layer)
        {
            BelowPlatformsWithoutOneWay = layer;
        }

        private void BelowPlatformsWithoutMidHeight(LayerMask layer)
        {
            belowPlatformsWithoutMidHeight = layer;
        }

        private void SetSavedBelowLayerToStandingOnLastFrame(LayerMask layer)
        {
            SavedBelowInternal(layer);
        }

        private void SavedBelowInternal(LayerMask layer)
        {
            SavedBelow = layer;
        }

        private void SetMidHeightOneWayPlatformContainsStandingOnLastFrame(GameObject standingOnLastFrame)
        {
            if (standingOnLastFrame == null) MidHeightOneWayPlatformContainsStandingOnLastFrame = false;
            else if (MidHeightOneWayPlatform.Contains(standingOnLastFrame))
                MidHeightOneWayPlatformContainsStandingOnLastFrame = true;
            else MidHeightOneWayPlatformContainsStandingOnLastFrame = false;
        }

        private void RaysBelowToPlatformsWithoutMidHeight()
        {
            RaysBelowPlatforms(belowPlatformsWithoutMidHeight);
        }

        private void RaysBelowPlatforms(LayerMask layer)
        {
            BelowPlatformsInternal(layer);
        }

        private void SetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            BelowPlatformsInternal((BelowPlatforms & ~OneWayPlatform) | Stairs);
        }

        private void SetRaysBelowToOneWayPlatform()
        {
            BelowPlatformsInternal(BelowPlatforms & ~OneWayPlatform);
        }

        private void SetStairsContainsStandingOnLastFrame(GameObject standingOnLastFrame)
        {
            if (standingOnLastFrame == null) StairsContainsStandingOnLastFrame = false;
            else if (Stairs.Contains(standingOnLastFrame)) StairsContainsStandingOnLastFrame = true;
            else StairsContainsStandingOnLastFrame = false;
        }

        private void SetPlatformsContainsStandingOn(GameObject standingOn)
        {
            if (standingOn == null)
            {
                SetPlatformsContainsStandingOn(false);
            }
            else
            {
                SetOneWayPlatformsContainsStandingOn(standingOn);
                SetMovingOneWayPlatformContainsStandingOn(standingOn);
            }
        }

        private void SetOneWayPlatformsContainsStandingOn(GameObject standingOn)
        {
            SetOneWayPlatformsContainsStandingOn(OneWayPlatform.Contains(standingOn.layer));
        }

        private void SetMovingOneWayPlatformContainsStandingOn(GameObject standingOn)
        {
            SetMovingOneWayPlatformContainsStandingOn(MovingOneWayPlatform.Contains(standingOn.layer));
        }

        private void SetPlatformsContainsStandingOn(bool contains)
        {
            SetOneWayPlatformsContainsStandingOn(contains);
            SetMovingOneWayPlatformContainsStandingOn(contains);
        }

        private void SetOneWayPlatformsContainsStandingOn(bool contains)
        {
            OneWayPlatformContainsStandingOn = contains;
        }

        private void SetMovingOneWayPlatformContainsStandingOn(bool contains)
        {
            MovingOneWayPlatformContainsStandingOn = contains;
        }

        #endregion

        #region event handlers

        public void OnInitialize(LayerMaskSettings settings)
        {
            Initialize(settings);
        }

        public void OnSetRaysBelowPlatforms()
        {
            RaysBelowPlatforms();
        }

        public void OnSetSavedBelowLayerToStandingOnLastFrame(LayerMask layer)
        {
            SetSavedBelowLayerToStandingOnLastFrame(layer);
        }

        public void OnSetMidHeightOneWayPlatformContainsStandingOnLastFrame(GameObject standingOnLastFrame)
        {
            SetMidHeightOneWayPlatformContainsStandingOnLastFrame(standingOnLastFrame);
        }

        public void OnSetRaysBelowToPlatformsWithoutMidHeight()
        {
            RaysBelowToPlatformsWithoutMidHeight();
        }

        public void OnSetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            SetRaysBelowToPlatformsAndOneWayOrStairs();
        }

        public void OnSetRaysBelowToOneWayPlatform()
        {
            SetRaysBelowToOneWayPlatform();
        }

        public void OnSetStairsContainsStandingOnLastFrame(GameObject standingOnLastFrame)
        {
            SetStairsContainsStandingOnLastFrame(standingOnLastFrame);
        }

        public void OnSetPlatformsContainStandingOn(GameObject standingOn)
        {
            SetPlatformsContainsStandingOn(standingOn);
        }

        #endregion
    }
}