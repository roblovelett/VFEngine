namespace VFEngine.Tools.LayerMask
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this UnityEngine.LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) > 0;
        }

        public static bool Contains(this UnityEngine.LayerMask mask, UnityEngine.GameObject gameObject)
        {
            return (mask.value & (1 << gameObject.layer)) > 0;
        }
    }
}