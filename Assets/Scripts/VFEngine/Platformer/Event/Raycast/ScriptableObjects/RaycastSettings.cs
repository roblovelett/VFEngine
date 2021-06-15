using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;
    using static RaycastSettings.DetachmentMethods;

    [CreateAssetMenu(fileName = "RaycastSettings", menuName = RaycastSettingsPath, order = 0)]
    
    public class RaycastSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public bool drawRaycastGizmosControl = true;
        [SerializeField] public bool displayWarnings = true;
        [SerializeField] public int numberOfHorizontalRays = 8;
        [SerializeField] public int numberOfVerticalRays = 8;
        [SerializeField] public float rayOffset = 0.05f;
        [SerializeField] public float crouchedRaycastLengthMultiplier = 1f;
        [SerializeField] public bool castRaysOnBothSides;
        [SerializeField] public float distanceToGroundRaycastMaximumLength = 100f;
        [SerializeField] public bool performSafetyBoxcast;
        [SerializeField] public float stickToSlopeRaycastLength;
        [SerializeField] public float stickToSlopeOffsetY = 0.2f;
        [SerializeField] public DetachmentMethods detachmentMethod = Layer;
        [SerializeField] public float obstacleHeightTolerance = 0.05f;
        public enum DetachmentMethods
        {
            Layer,
            Object
        }

        #endregion
    }
}