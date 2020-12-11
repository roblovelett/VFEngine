using JetBrains.Annotations;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    public class DownRaycastHitColliderData
    {
        #region properties

        public bool HasPhysicsMaterialClosestToHit { get; set; }
        public bool HasPathMovementClosestToHit { get; set; }
        public bool HitConnected { get; set; }
        public bool IsCollidingBelow { get; set; }
        public bool OnMovingPlatform { get; set; }
        public bool HasMovingPlatform { get; set; }
        public bool HasStandingOnLastFrame { get; set; }
        public bool WasGroundedLastFrame { get; set; }
        public bool GroundedEvent { get; set; }
        public bool MovingPlatformHasSpeed { get; set; }
        public bool MovingPlatformHasSpeedOnAxis { get; set; }
        public int CurrentHitsStorageIndex { get; set; }
        public int SmallestDistanceIndex { get; set; }
        
        public float DistanceBetweenSmallestPointAndVerticalRaycast { get; set; }
        public float SmallValue { get; set; }
        public float SmallestDistance { get; set; }
        public float MovingPlatformCurrentGravity { get; set; }
        public float CurrentSmallestDistanceHit { get; set; }
        public float Friction { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float MovingPlatformGravity { get; set; } //= -500f;
        public Vector2 MovingPlatformCurrentSpeed { get; set; }
        public Vector3 CrossBelowSlopeAngle { get; set; }
        public Collider2D StandingOnCollider { get; set; }
        public RaycastHit2D CurrentRaycast { get; set; }
        //public RaycastHit2D RaycastHitAt { get; set; }
        //public RaycastHit2D SmallestDistanceToHitStorage{ get; set; }
        public GameObject StandingOnLastFrame { get; set; }
        public GameObject StandingOn { get; set; }
        public GameObject StandingOnWithSmallestDistance { get; set; }
        public RaycastHit2D[] HitsStorage { get; set; }
        [CanBeNull] public PhysicsMaterialController PhysicsMaterialClosestToHit { get; set; }
        [CanBeNull] public PathMovementController PathMovementClosestToHit { get; set; }
        [CanBeNull] public PathMovementController MovingPlatform { get; set; }

        #endregion
    }
}