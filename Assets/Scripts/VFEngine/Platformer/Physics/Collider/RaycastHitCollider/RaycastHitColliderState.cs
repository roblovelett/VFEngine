using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static LayerMask;
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
        public bool WasGroundedLastFrame { get; private set; }
        public bool WasTouchingCeilingLastFrame { get; private set; }
        public bool ColliderResized { get; private set; }
        public bool GroundedEvent { get; private set; }
        public bool PassedRightSlopeAngle { get; private set; }
        public bool PassedLeftSlopeAngle { get; private set; }
        public float DistanceToGround { get; private set; }
        public float RightLateralSlopeAngle { get; private set; }
        public float LeftLateralSlopeAngle { get; private set; }
        public float BelowSlopeAngle { get; private set; }
        public float DistanceToLeftCollider { get; private set; }
        public float DistanceToRightCollider { get; private set; }
        public GameObject CurrentWallCollider { get; private set; }
        public GameObject RightCurrentWallCollider { get; private set; }
        public GameObject LeftCurrentWallCollider { get; private set; }
        public GameObject StandingOn { get; private set; }
        public Collider2D StandingOnCollider { get; private set; }
        public GameObject StandingOnLastFrame { get; private set; }
        public Vector3 CrossBelowSlopeAngle { get; private set; }
        public bool IsGrounded => IsCollidingBelow;
        public bool HasCollisions => IsCollidingRight || IsCollidingLeft || IsCollidingAbove || IsCollidingBelow;


        public void SetStandingOnLastFrameLayer(string name)
        {
            StandingOnLastFrame.layer = NameToLayer(name);
        }

        public void SetStandingOnLastFrameLayer(int number)
        {
            StandingOnLastFrame.layer = number;
        }
        public void SetDistanceToGround(float distance)
        {
            DistanceToGround = distance;
        }
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

        private void SetIsColliding(bool colliding)
        {
            SetIsCollidingAbove(colliding);
            SetIsCollidingLeft(colliding);
            SetIsCollidingRight(colliding);
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

        public void SetPassedRightSlopeAngle(bool pass)
        {
            PassedRightSlopeAngle = pass;
        }

        public void SetPassedLeftSlopeAngle(bool pass)
        {
            PassedLeftSlopeAngle = pass;
        }

        private void SetPassedSlopeAngles(bool passed)
        {
            SetPassedRightSlopeAngle(passed);
            SetPassedLeftSlopeAngle(passed);
        }

        public void SetGroundedEvent(bool groundedEvent)
        {
            GroundedEvent = groundedEvent;
        }

        private void SetSlopeAngles(float angle)
        {
            SetRightLateralSlopeAngle(angle);
            SetLeftLateralSlopeAngle(angle);
        }

        public void SetRightLateralSlopeAngle(float angle)
        {
            RightLateralSlopeAngle = angle;
        }

        public void SetLeftLateralSlopeAngle(float angle)
        {
            LeftLateralSlopeAngle = angle;
        }

        public void SetBelowSlopeAngle(float belowSlopeAngle)
        {
            BelowSlopeAngle = belowSlopeAngle;
        }

        private void SetDistanceToColliders(float distance)
        {
            DistanceToLeftCollider = distance;
            DistanceToRightCollider = distance;
        }

        public void SetDistanceToLeftCollider(float distance)
        {
            DistanceToLeftCollider = distance;
        }

        public void SetDistanceToRightCollider(float distance)
        {
            DistanceToRightCollider = distance;
        }

        public void SetCurrentWallColliderNull()
        {
            SetRightCurrentWallCollider(null);
            SetLeftCurrentWallCollider(null);
        }

        public void SetRightCurrentWallCollider(GameObject currentWallCollider)
        {
            RightCurrentWallCollider = currentWallCollider;
        }
        
        public void SetLeftCurrentWallCollider(GameObject currentWallCollider)
        {
            LeftCurrentWallCollider = currentWallCollider;
        }

        public void SetStandingOnLastFrame(GameObject standingOnLastFrame)
        {
            StandingOnLastFrame = standingOnLastFrame;
        }

        public void SetStandingOn(GameObject standingOn)
        {
            StandingOn = standingOn;
        }

        public void SetStandingOnCollider(Collider2D collider)
        {
            StandingOnCollider = collider;
        }

        public void SetCrossBelowSlopeAngle(Vector3 cross)
        {
            CrossBelowSlopeAngle = cross;
        }

        public void Reset()
        {
            SetIsColliding(false);
            SetPassedSlopeAngles(false);
            SetGroundedEvent(false);
            SetDistanceToColliders(-1);
            SetSlopeAngles(0);
        }
    }
}