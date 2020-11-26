using System;
using UnityEngine;

namespace VFEngine.Tools
{
    using static Single;

    public static class PhysicsExtensions
    {
        public static bool SpeedNan(Vector2 speed)
        {
            return IsNaN(speed.x) && IsNaN(speed.y);
        }

        public static bool AxisSpeedNan(Vector2 speed)
        {
            return IsNaN(speed.x) || IsNaN(speed.y);
        }
    }
}