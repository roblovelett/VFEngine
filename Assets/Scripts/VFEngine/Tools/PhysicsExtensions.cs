using UnityEngine;

namespace VFEngine.Tools
{
    public static class PhysicsExtensions
    {
        public static bool SpeedNan(float xSpeed, float ySpeed, float zSpeed)
        {
            return float.IsNaN(xSpeed) && float.IsNaN(ySpeed) && float.IsNaN(zSpeed);
        }

        public static bool SpeedNan(Vector3 speedVector)
        {
            return float.IsNaN(speedVector.x) && float.IsNaN(speedVector.y) && float.IsNaN(speedVector.z);
        }
        
        public static bool AxisSpeedNan(float xSpeed, float ySpeed, float zSpeed)
        {
            return float.IsNaN(xSpeed) || float.IsNaN(ySpeed) || float.IsNaN(zSpeed);
        }

        public static bool AxisSpeedNan(Vector3 speedVector)
        {
            return float.IsNaN(speedVector.x) || float.IsNaN(speedVector.y) || float.IsNaN(speedVector.z);
        }
    }
}