using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;
    public class SafetyBoxcastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private SafetyBoxcastSettings settings;
        [SerializeField] private Vector2Reference bounds;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;

        /* fields */
        [SerializeField] private BoolReference hasSafetyBoxcast;
        [SerializeField] private RaycastHit2DReference safetyBoxcast;
        [SerializeField] private Collider2DReference safetyBoxcastCollider;
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}DefaultSafetyBoxcastModel.asset";
        
        /* properties: dependencies */
        public Vector2 Bounds => bounds.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public Transform Transform => transform.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        
        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public RaycastHit2D SafetyBoxcast { get; set; }

        public RaycastHit2D SafetyBoxcastRef
        {
            set => value = safetyBoxcast.Value;
        }
        public bool HasSafetyBoxcast { get; set; }

        public bool HasSafetyBoxcastRef
        {
            set => value = hasSafetyBoxcast.Value;
        }

        public Collider2D SafetyBoxcastCollider => SafetyBoxcast.collider;

        public Collider2D SafetyBoxcastColliderRef
        {
            set => value = safetyBoxcastCollider.Value;
        }
    }
}

/* fields: methods */
/*
private void GetWarningMessage()
{
if (!DisplayWarnings) return;
var warningMessage = "";
var warningMessageCount = 0;
    if (!settings) warningMessage += FieldMessage("Settings", "Layer Mask Settings");
    if (!safetyBoxcastControl) warningMessage += FieldMessage("Safety Boxcast Control", "Bool Reference");
DebugLogWarning(warningMessageCount, warningMessage);

string FieldMessage(string field, string scriptableObject)
{
    warningMessageCount++;
    return $"{field} field not set to {scriptableObject} ScriptableObject.";
}
}
*/