using System;
using UnityEngine;

namespace VFEngine.Tools
{
    using static Single;
    public static class PhysicsExtensions
    {
        public static bool SpeedNan(float xSpeed, float ySpeed, float zSpeed)
        {
            return IsNaN(xSpeed) && IsNaN(ySpeed) && IsNaN(zSpeed);
        }

        public static bool SpeedNan(Vector2 speed)
        {
            return IsNaN(speed.x) && IsNaN(speed.y);
        }

        public static bool SpeedNan(Vector3 speed)
        {
            return IsNaN(speed.x) && IsNaN(speed.y) && IsNaN(speed.z);
        }
        
        public static bool AxisSpeedNan(float xSpeed, float ySpeed, float zSpeed)
        {
            return IsNaN(xSpeed) || IsNaN(ySpeed) || IsNaN(zSpeed);
        }

        public static bool AxisSpeedNan(Vector2 speed)
        {
            return IsNaN(speed.x) || IsNaN(speed.y);
        }
        
        public static bool AxisSpeedNan(Vector3 speed)
        {
            return IsNaN(speed.x) || IsNaN(speed.y) || IsNaN(speed.z);
        }
    }
}