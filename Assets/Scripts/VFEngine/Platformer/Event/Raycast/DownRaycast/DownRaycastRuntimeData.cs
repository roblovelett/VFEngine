using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    public class DownRaycastRuntimeData : ScriptableObject
    {
        public DownRaycast downRaycast;
        
        public struct DownRaycast
        {
            public float DownRayLength { get; set; }
            public Vector2 CurrentDownRaycastOrigin { get; set; }
            public Vector2 DownRaycastFromLeft { get; set; }
            public Vector2 DownRaycastToRight { get; set; }
            public RaycastHit2D CurrentDownRaycastHit { get; set; }
        }
        
        public void SetDownRaycast(float downRayLength, Vector2 currentDownRaycastOrigin, Vector2 downRaycastFromLeft,
            Vector2 downRaycastToRight, RaycastHit2D currentDownRaycastHit)
        {
            downRaycast.DownRayLength = downRayLength;
            downRaycast.CurrentDownRaycastOrigin = currentDownRaycastOrigin;
            downRaycast.DownRaycastFromLeft = downRaycastFromLeft;
            downRaycast.DownRaycastToRight = downRaycastToRight;
            downRaycast.CurrentDownRaycastHit = currentDownRaycastHit;
        }
    }
}