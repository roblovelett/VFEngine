using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    public class RightRaycastRuntimeData : ScriptableObject
    {
        public RightRaycast rightRaycast;
        
        public struct RightRaycast
        {
            public float RightRayLength { get; set; }
            public Vector2 RightRaycastFromBottomOrigin { get; set; }
            public Vector2 RightRaycastToTopOrigin { get; set; }
            public RaycastHit2D CurrentRightRaycastHit { get; set; }
        }
        
        public void SetRightRaycast(float rightRayLength, Vector2 rightRaycastFromBottomOrigin,
            Vector2 rightRaycastToTopOrigin, RaycastHit2D currentRightRaycastHit)
        {
            rightRaycast.RightRayLength = rightRayLength;
            rightRaycast.RightRaycastFromBottomOrigin = rightRaycastFromBottomOrigin;
            rightRaycast.RightRaycastToTopOrigin = rightRaycastToTopOrigin;
            rightRaycast.CurrentRightRaycastHit = currentRightRaycastHit;
        }
    }
}