using UnityEngine;

namespace VFEngine.Tools
{
    public static class MathsExtensions
    {
        public static bool IsZero(float number)
        {
            return number == 0f;
        }
        public static float Half(float number)
        {
            return number / 2f;
        }
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
        public static bool IsTime(float timeScale, float deltaTime)
        {
            return timeScale != 0 || deltaTime > 0;
        }
        public static bool IsNan(Vector3 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
        }
        
        public static float DistanceBetweenPointAndLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Magnitude(ProjectPointOnLine(point, lineStart, lineEnd) - point);
        }

        private static Vector3 ProjectPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            var rhs = point - lineStart;
            var vector2 = lineEnd - lineStart;
            var magnitude = vector2.magnitude;
            var lhs = vector2;
            if (magnitude > 1E-06f) lhs /= magnitude;
            var num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
            return lineStart + lhs * num2;
        }
    }
}