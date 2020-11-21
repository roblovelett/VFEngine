using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    public class LeftRaycastRuntimeData : ScriptableObject
    {
        public LeftRaycast leftRaycast;
        
        public struct LeftRaycast
        {
            public float LeftRayLength { get; set; }
            public Vector2 LeftRaycastFromBottomOrigin { get; set; }
            public Vector2 LeftRaycastToTopOrigin { get; set; }
            public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        }
        
        public void SetLeftRaycast(float leftRayLength, Vector2 leftRaycastFromBottomOrigin,
            Vector2 leftRaycastToTopOrigin, RaycastHit2D currentLeftRaycastHit)
        {
            leftRaycast.LeftRayLength = leftRayLength;
            leftRaycast.LeftRaycastFromBottomOrigin = leftRaycastFromBottomOrigin;
            leftRaycast.LeftRaycastToTopOrigin = leftRaycastToTopOrigin;
            leftRaycast.CurrentLeftRaycastHit = currentLeftRaycastHit;
        }
    }
}