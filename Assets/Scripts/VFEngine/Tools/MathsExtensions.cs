﻿using UnityEngine;

namespace VFEngine.Tools
{
    public static class MathsExtensions
    {
        public static float DistanceBetweenPointAndLine(UnityEngine.Vector3 point, Vector3 lineStart, Vector3 lineEnd)
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