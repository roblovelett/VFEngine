using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    public class DownRaycastHitColliderRuntimeData
    {
        #region properties

        public bool HasPhysicsMaterialClosestToDownHit { get; private set; }
        public bool HasPathMovementClosestToDownHit { get; private set; }
        public bool DownHitConnected { get; private set; }
        public bool IsCollidingBelow { get; private set; }
        public bool OnMovingPlatform { get; private set; }
        public bool HasMovingPlatform { get; private set; }
        public bool HasStandingOnLastFrame { get; private set; }
        public bool HasGroundedLastFrame { get; private set; }
        public bool GroundedEvent { get; private set; }
        public int DownHitsStorageLength { get; private set; }
        public int CurrentDownHitsStorageIndex { get; private set; }
        public float CurrentDownHitSmallestDistance { get; private set; }
        public float SmallestDistanceToDownHit { get; private set; }
        public float MovingPlatformCurrentGravity { get; private set; }
        public Vector2 MovingPlatformCurrentSpeed { get; private set; }
        public Vector3 CrossBelowSlopeAngle { get; private set; }
        public GameObject StandingOnLastFrame { get; private set; }
        public Collider2D StandingOnCollider { get; private set; }
        public LayerMask StandingOnWithSmallestDistanceLayer { get; private set; }
        public RaycastHit2D RaycastDownHitAt { get; private set; }

        #region public methods

        public static DownRaycastHitColliderRuntimeData CreateInstance(bool hasPhysicsMaterialClosestToDownHit,
            bool hasPathMovementClosestToDownHit, bool downHitConnected, bool isCollidingBelow, bool onMovingPlatform,
            bool hasMovingPlatform, bool hasStandingOnLastFrame, bool hasGroundedLastFrame, bool groundedEvent,
            int downHitsStorageLength, int currentDownHitsStorageIndex, float currentDownHitSmallestDistance,
            float smallestDistanceToDownHit, float movingPlatformCurrentGravity, Vector2 movingPlatformCurrentSpeed,
            Vector2 crossBelowSlopeAngle, GameObject standingOnLastFrame, Collider2D standingOnCollider,
            LayerMask standingOnWithSmallestDistanceLayer, RaycastHit2D raycastDownHitAt)
        {
            return new DownRaycastHitColliderRuntimeData
            {
                HasPhysicsMaterialClosestToDownHit = hasPhysicsMaterialClosestToDownHit,
                HasPathMovementClosestToDownHit = hasPathMovementClosestToDownHit,
                DownHitConnected = downHitConnected,
                IsCollidingBelow = isCollidingBelow,
                OnMovingPlatform = onMovingPlatform,
                HasMovingPlatform = hasMovingPlatform,
                HasStandingOnLastFrame = hasStandingOnLastFrame,
                HasGroundedLastFrame = hasGroundedLastFrame,
                GroundedEvent = groundedEvent,
                DownHitsStorageLength = downHitsStorageLength,
                CurrentDownHitsStorageIndex = currentDownHitsStorageIndex,
                CurrentDownHitSmallestDistance = currentDownHitSmallestDistance,
                SmallestDistanceToDownHit = smallestDistanceToDownHit,
                MovingPlatformCurrentGravity = movingPlatformCurrentGravity,
                MovingPlatformCurrentSpeed = movingPlatformCurrentSpeed,
                CrossBelowSlopeAngle = crossBelowSlopeAngle,
                StandingOnLastFrame = standingOnLastFrame,
                StandingOnCollider = standingOnCollider,
                StandingOnWithSmallestDistanceLayer = standingOnWithSmallestDistanceLayer,
                RaycastDownHitAt = raycastDownHitAt
            };
        }

        #endregion

        #endregion
    }
}