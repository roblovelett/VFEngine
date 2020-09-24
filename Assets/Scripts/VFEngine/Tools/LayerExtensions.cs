using UnityEngine;

namespace VFEngine.Tools
{
    public static class LayerExtensions
    {
        public static bool ContainsLayer(LayerMask layerMask, GameObject gameObject)
        {
            var containsLayer = layerMask == (layerMask | (1 << gameObject.layer));
            return containsLayer;
        }
    }
}