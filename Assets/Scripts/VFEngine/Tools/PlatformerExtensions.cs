namespace VFEngine.Tools
{
    public static class PlatformerExtensions
    {
        public static bool MovingPlatformTest(bool collidingWithMovingPlatform, bool timeLteZero, bool axisSpeedNan,
            bool wasTouchingCeilingLastFrame)
        {
            return collidingWithMovingPlatform && !timeLteZero && !axisSpeedNan && !wasTouchingCeilingLastFrame;
        }

        public static bool MovingPlatformTest(bool timeLteZero, bool axisSpeedNan, bool wasTouchingCeilingLastFrame)
        {
            return !timeLteZero && !axisSpeedNan && !wasTouchingCeilingLastFrame;
        }
    }
}