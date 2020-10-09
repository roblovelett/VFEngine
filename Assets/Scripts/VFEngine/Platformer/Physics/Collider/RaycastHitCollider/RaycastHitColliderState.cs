﻿using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderState
    {
        public bool IsCollidingWithMovingPlatform { get; private set; }
        public bool IsCollidingWithStairs { get; private set; }
        public bool IsCollidingWithFrictionSurface { get; private set; }
        public bool IsCollidingRight { get; private set; }
        public bool IsCollidingLeft { get; private set; }
        public bool IsCollidingAbove { get; private set; }
        public bool IsCollidingBelow { get; private set; }
        public bool IsCollidingWithLevelBounds { get; private set; }
        public bool OnMovingPlatform { get; private set; }
        public bool IsPassingSlopeAngle { get; private set; }
        public bool WasGroundedLastFrame { get; private set; }
        public bool WasTouchingCeilingLastFrame { get; private set; }
        public bool ColliderResized { get; private set; }
        public bool GroundedEvent { get; private set; }
        public float LateralSlopeAngle { get; private set; }
        public float BelowSlopeAngle { get; private set; }
        public float DistanceToLeftRaycastHit { get; private set; }
        public float DistanceToRightRaycastHit { get; private set; }
        public GameObject CurrentWallCollider { get; private set; }
        public bool IsGrounded => IsCollidingBelow;
        public bool HasCollisions => IsCollidingRight || IsCollidingLeft || IsCollidingAbove || IsCollidingBelow;

        public void SetIsCollidingWithMovingPlatform(bool isCollidingWithMovingPlatform)
        {
            IsCollidingWithMovingPlatform = isCollidingWithMovingPlatform;
        }

        public void SetIsCollidingWithStairs(bool isCollidingWithStairs)
        {
            IsCollidingWithStairs = isCollidingWithStairs;
        }

        public void SetIsCollidingWithFrictionSurface(bool isCollidingWithFrictionSurface)
        {
            IsCollidingWithFrictionSurface = isCollidingWithFrictionSurface;
        }

        public void SetIsCollidingRight(bool isCollidingRight)
        {
            IsCollidingRight = isCollidingRight;
        }

        public void SetIsCollidingLeft(bool isCollidingLeft)
        {
            IsCollidingLeft = isCollidingLeft;
        }

        public void SetIsCollidingAbove(bool isCollidingAbove)
        {
            IsCollidingAbove = isCollidingAbove;
        }

        public void SetIsCollidingBelow(bool isCollidingBelow)
        {
            IsCollidingBelow = isCollidingBelow;
        }

        public void SetIsCollidingWithLevelBounds(bool isCollidingWithLevelBounds)
        {
            IsCollidingWithLevelBounds = isCollidingWithLevelBounds;
        }

        public void SetOnMovingPlatform(bool onMovingPlatform)
        {
            OnMovingPlatform = onMovingPlatform;
        }

        public void SetWasGroundedLastFrame(bool wasGroundedLastFrame)
        {
            WasGroundedLastFrame = wasGroundedLastFrame;
        }

        public void SetWasTouchingCeilingLastFrame(bool wasTouchingCeilingLastFrame)
        {
            WasTouchingCeilingLastFrame = wasTouchingCeilingLastFrame;
        }

        public void SetColliderResized(bool colliderResized)
        {
            ColliderResized = colliderResized;
        }

        public void SetLateralSlopeAngle(float lateralSlopeAngle)
        {
            LateralSlopeAngle = lateralSlopeAngle;
        }

        public void SetBelowSlopeAngle(float belowSlopeAngle)
        {
            BelowSlopeAngle = belowSlopeAngle;
        }

        public void SetDistanceToLeftRaycastHit(float distanceToLeftRaycastHit)
        {
            DistanceToLeftRaycastHit = distanceToLeftRaycastHit;
        }

        public void SetDistanceToRightRaycastHit(float distanceToRightRaycastHit)
        {
            DistanceToRightRaycastHit = distanceToRightRaycastHit;
        }

        public void SetCurrentWallCollider(GameObject currentWallCollider)
        {
            CurrentWallCollider = currentWallCollider;
        }

        public void Reset()
        {
            IsCollidingLeft = false;
            IsCollidingRight = false;
            IsCollidingAbove = false;
            IsPassingSlopeAngle = false;
            GroundedEvent = false;
            DistanceToLeftRaycastHit = -1;
            DistanceToRightRaycastHit = -1;
            LateralSlopeAngle = 0;
        }
    }
}