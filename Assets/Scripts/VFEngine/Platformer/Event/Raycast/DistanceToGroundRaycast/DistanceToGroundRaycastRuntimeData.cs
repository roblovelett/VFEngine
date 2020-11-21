using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    public class DistanceToGroundRaycastRuntimeData : ScriptableObject
    {
        public DistanceToGroundRaycast distanceToGroundRaycast;

        public struct DistanceToGroundRaycast
        {
            public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        }

        public void SetDistanceToGroundRaycast(RaycastHit2D hit)
        {
            distanceToGroundRaycast.DistanceToGroundRaycastHit = hit;
        }
    }
}