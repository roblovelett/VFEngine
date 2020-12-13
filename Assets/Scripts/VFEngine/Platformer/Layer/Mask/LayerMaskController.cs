using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObject;
    using static LayerMaskExtensions;
    using static RaycastDirection;

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskSettings settings;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LayerMaskData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastHitColliderData downRaycastHitCollider;

        #endregion

        #region internal

        
        
        #endregion
        
        #region private methods

        #region initialization

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            l = new LayerMaskData
            {
                Platform = 0,
                MovingPlatform = 0,
                OneWayPlatform = 0,
                MovingOneWayPlatform = 0,
                MidHeightOneWayPlatform = 0,
                Stairs = 0
            };
            l.ApplySettings(settings);
            l.SavedPlatform = l.Platform;
            l.Platform |= l.OneWayPlatform;
            l.Platform |= l.MovingPlatform;
            l.Platform |= l.MovingOneWayPlatform;
            l.Platform |= l.MidHeightOneWayPlatform;
        }

        /*private void GetWarningMessages()
        {
            const string lM = "Layer Mask";
            const string player = "Player";
            const string platform = "Platform";
            const string movingPlatform = "MovingPlatform";
            const string oneWayPlatform = "OneWayPlatform";
            const string movingOneWayPlatform = "MovingOneWayPlatform";
            const string midHeightOneWayPlatform = "MidHeightOneWayPlatform";
            const string stairs = "Stairs";
            var platformLayers = GetMask($"{player}", $"{platform}");
            var movingPlatformLayer = GetMask($"{movingPlatform}");
            var oneWayPlatformLayer = GetMask($"{oneWayPlatform}");
            var movingOneWayPlatformLayer = GetMask($"{movingOneWayPlatform}");
            var midHeightOneWayPlatformLayer = GetMask($"{midHeightOneWayPlatform}");
            var stairsLayer = GetMask($"{stairs}");
            LayerMask[] layers =
            {
                platformLayers, movingPlatformLayer, oneWayPlatformLayer, movingOneWayPlatformLayer,
                midHeightOneWayPlatformLayer, stairsLayer
            };
            LayerMask[] layerMasks =
            {
                l.PlatformMask, l.MovingPlatformMask, l.OneWayPlatformMask, l.MovingOneWayPlatformMask,
                l.MidHeightOneWayPlatformMask, l.StairsMask
            };
            var layerMaskSettings = $"{lM} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += FieldString($"{layerMaskSettings}", $"{layerMaskSettings}");
            for (var i = 0; i < layers.Length; i++)
            {
                if (layers[i].value == layerMasks[i].value) continue;
                var mask = LayerToName(layerMasks[i]);
                var layer = LayerToName(layers[i]);
                warningMessage += FieldPropertyString($"{mask}", $"{layer}", $"{layerMaskSettings}");
            }

            string FieldString(string field, string scriptableObject)
            {
                AddWarningMessageCount();
                return FieldMessage(field, scriptableObject);
            }

            string FieldPropertyString(string field, string property, string scriptableObject)
            {
                AddWarningMessageCount();
                return FieldPropertyMessage(field, property, scriptableObject);
            }

            void AddWarningMessageCount()
            {
                warningMessageCount++;
            }

            DebugLogWarning(warningMessageCount, warningMessage);
        }*/

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
        }

        #endregion

        #region platformer

        private bool CastingDown => raycast.CurrentRaycastDirection == Down;
        private bool NotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool HasStandingOnLastFrame => downRaycastHitCollider.HasStandingOnLastFrame;
        private bool MidHeightOneWayPlatformHasStandingOnLastFrame =>
            l.MidHeightOneWayPlatform.Contains(downRaycastHitCollider.StandingOnLastFrame.layer);

        private bool SetRaysBelowPlatformsMaskToPlatformsWithoutHeight => downRaycastHitCollider.WasGroundedLastFrame &&
                                                                          HasStandingOnLastFrame &&
                                                                          !MidHeightOneWayPlatformHasStandingOnLastFrame;

        private bool StairsMaskHasStandingOnLastFrame =>
            l.Stairs.Contains(downRaycastHitCollider.StandingOnLastFrame.layer);

        private bool StandingOnColliderHasBottomCenterPosition =>
            downRaycastHitCollider.StandingOnCollider.bounds.Contains(raycast.BoundsBottomCenterPosition);

        private bool SetRaysBelowPlatformsMaskToOneWayOrStairs => downRaycastHitCollider.WasGroundedLastFrame &&
                                                                  HasStandingOnLastFrame &&
                                                                  StairsMaskHasStandingOnLastFrame &&
                                                                  StandingOnColliderHasBottomCenterPosition;

        private bool SetRaysBelowPlatformsMaskToOneWay =>
            downRaycastHitCollider.OnMovingPlatform && physics.NewPosition.y > 0;
        
        
        
        
        private void PlatformerCastRaysDown()
        {
            if (!CastingDown || NotCollidingBelow) return;
            SetRaysBelowPlatforms();
            SetRaysBelowPlatformsWithoutOneWay();
            SetRaysBelowPlatformsWithoutMidHeight();
            if (HasStandingOnLastFrame)
            {
                SetSavedBelowToStandingOnLastFrame();
                SetMidHeightOneWayPlatformHasStandingOnLastFrame();
            }

            if (SetRaysBelowPlatformsMaskToPlatformsWithoutHeight) SetRaysBelowPlatformsToPlatformsWithoutHeight();
            
            if (SetRaysBelowPlatformsMaskToOneWayOrStairs) SetRaysBelowPlatformsToOneWayOrStairs();
            if (SetRaysBelowPlatformsMaskToOneWay) SetRaysBelowPlatformsToOneWay();
        }

        #endregion

        private void SetRaysBelowPlatforms()
        {
            l.RaysBelowPlatforms = l.Platform;
        }

        private void SetRaysBelowPlatformsWithoutOneWay()
        {
            l.RaysBelowPlatformsWithoutOneWay = l.Platform & ~l.MidHeightOneWayPlatform &
                                                ~l.OneWayPlatform & ~l.MovingOneWayPlatform;
        }

        private void SetRaysBelowPlatformsWithoutMidHeight()
        {
            l.RaysBelowPlatformsWithoutMidHeight = l.RaysBelowPlatforms & ~l.MidHeightOneWayPlatform;
        }

        private void SetSavedBelowToStandingOnLastFrame()
        {
            l.SavedBelowLayer = downRaycastHitCollider.StandingOnLastFrame.layer;
        }

        private void SetMidHeightOneWayPlatformHasStandingOnLastFrame()
        {
            l.MidHeightOneWayPlatformHasStandingOnLastFrame =
                l.MidHeightOneWayPlatform.Contains(downRaycastHitCollider.StandingOnLastFrame);
        }

        private void SetRaysBelowPlatformsToPlatformsWithoutHeight()
        {
            l.RaysBelowPlatforms = l.RaysBelowPlatformsWithoutMidHeight;
        }

        private void SetRaysBelowPlatformsToOneWayOrStairs()
        {
            l.RaysBelowPlatforms = (l.RaysBelowPlatforms & ~l.OneWayPlatform) | l.Stairs;
        }

        private void SetRaysBelowPlatformsToOneWay()
        {
            l.RaysBelowPlatforms &= ~l.OneWayPlatform;
        }

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => l;

        #region public methods

        #region platformer

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        #endregion

        #endregion

        #endregion
    }
}