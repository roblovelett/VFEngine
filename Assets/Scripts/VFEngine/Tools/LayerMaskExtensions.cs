using UnityEngine;

namespace VFEngine.Tools
{
    public static class LayerMaskExtensions
    {
        public static bool LayerMaskContains(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) > 0;
        }

        public static bool LayerMaskContains(this LayerMask mask, GameObject gameObject)
        {
            return (mask.value & (1 << gameObject.layer)) > 0;
        }
    }
}