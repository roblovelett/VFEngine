using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    public class DownRaycastHitColliderRuntimeData : ScriptableObject
    {
        public DownRaycastHitCollider downRaycastHitCollider;
        
        public struct DownRaycastHitCollider
        {
            public bool HasPhysicsMaterialClosestToDownHit { get; set; }
            public bool HasPathMovementClosestToDownHit { get; set; }
            public bool DownHitConnected { get; set; }
            public bool IsCollidingBelow { get; set; }
            public bool OnMovingPlatform { get; set; }
            public bool HasMovingPlatform { get; set; }
            public bool HasStandingOnLastFrame { get; set; }
            public bool HasGroundedLastFrame { get; set; }
            public bool GroundedEvent { get; set; }
            public int DownHitsStorageLength { get; set; }
            public int CurrentDownHitsStorageIndex { get; set; }
            public float CurrentDownHitSmallestDistance { get; set; }
            public float SmallestDistanceToDownHit { get; set; }
            public float MovingPlatformCurrentGravity { get; set; }
            public Vector2 MovingPlatformCurrentSpeed { get; set; }
            public Vector3 CrossBelowSlopeAngle { get; set; }
            public GameObject StandingOnLastFrame { get; set; }
            public Collider2D StandingOnCollider { get; set; }
            public LayerMask StandingOnWithSmallestDistanceLayer { get; set; }
            public RaycastHit2D RaycastDownHitAt { get; set; }
        }
        
        public void SetDownRaycastHitCollider(bool hasPhysicsMaterialClosestToDownHit,
            bool hasPathMovementClosestToDownHit, bool downHitConnected, bool isCollidingBelow, bool onMovingPlatform,
            bool hasMovingPlatform, bool hasStandingOnLastFrame, bool hasGroundedLastFrame, bool groundedEvent,
            int downHitsStorageLength, int currentDownHitsStorageIndex, float currentDownHitSmallestDistance,
            float smallestDistanceToDownHit, float movingPlatformCurrentGravity, Vector2 movingPlatformCurrentSpeed,
            Vector2 crossBelowSlopeAngle, GameObject standingOnLastFrame, Collider2D standingOnCollider,
            LayerMask standingOnWithSmallestDistanceLayer, RaycastHit2D raycastDownHitAt)
        {
            downRaycastHitCollider.HasPhysicsMaterialClosestToDownHit = hasPhysicsMaterialClosestToDownHit;
            downRaycastHitCollider.HasPathMovementClosestToDownHit = hasPathMovementClosestToDownHit;
            downRaycastHitCollider.DownHitConnected = downHitConnected;
            downRaycastHitCollider.IsCollidingBelow = isCollidingBelow;
            downRaycastHitCollider.OnMovingPlatform = onMovingPlatform;
            downRaycastHitCollider.HasMovingPlatform = hasMovingPlatform;
            downRaycastHitCollider.HasStandingOnLastFrame = hasStandingOnLastFrame;
            downRaycastHitCollider.HasGroundedLastFrame = hasGroundedLastFrame;
            downRaycastHitCollider.GroundedEvent = groundedEvent;
            downRaycastHitCollider.DownHitsStorageLength = downHitsStorageLength;
            downRaycastHitCollider.CurrentDownHitsStorageIndex = currentDownHitsStorageIndex;
            downRaycastHitCollider.CurrentDownHitSmallestDistance = currentDownHitSmallestDistance;
            downRaycastHitCollider.SmallestDistanceToDownHit = smallestDistanceToDownHit;
            downRaycastHitCollider.MovingPlatformCurrentGravity = movingPlatformCurrentGravity;
            downRaycastHitCollider.MovingPlatformCurrentSpeed = movingPlatformCurrentSpeed;
            downRaycastHitCollider.CrossBelowSlopeAngle = crossBelowSlopeAngle;
            downRaycastHitCollider.StandingOnLastFrame = standingOnLastFrame;
            downRaycastHitCollider.StandingOnCollider = standingOnCollider;
            downRaycastHitCollider.StandingOnWithSmallestDistanceLayer = standingOnWithSmallestDistanceLayer;
            downRaycastHitCollider.RaycastDownHitAt = raycastDownHitAt;
        }
    }
}