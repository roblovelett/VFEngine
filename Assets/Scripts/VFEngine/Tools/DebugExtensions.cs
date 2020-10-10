using UnityEngine;

namespace VFEngine.Tools
{
    using static Debug;
    using static Physics2D;
    using static Quaternion;
    using static Vector2;

    public static class DebugExtensions
    {
        private const string Ze = "zero";
        private const string SObj = "ScriptableObject";
        private const string GObj = "GameObject";
        private const string Fld = "field";
        private const string OrEq = "or equal to";
        private const string SetValLt = "cannot be set to value less than";
        private const string NtSe = "not set";
        private static readonly string NtSeTo = $"{NtSe} to";
        private static readonly string FldIn = $"{Fld} in";
        private static readonly string FldNtSe = $"{Fld} {NtSeTo}";

        public static string PropertyNtZeroParentMessage(string field, string property, string scriptableObject,
            string message)
        {
            return $"{field}'s {property} {NtSeTo} {Ze} in {scriptableObject} {SObj}. {message}.@";
        }

        public static string PropertyNtZeroParentMessage(string field, string property, string scriptableObject)
        {
            return $"{field}'s {property} {NtSeTo} {Ze} in {scriptableObject} {SObj}.@";
        }

        public static string LtEqZeroMessage(string field, string scriptableObject)
        {
            return $"{field} {FldIn} {scriptableObject} {SObj} {SetValLt} {OrEq} {Ze}.@";
        }

        public static string LtZeroMessage(string field, string scriptableObject)
        {
            return $"{field} {FldIn} {scriptableObject} {SObj} {SetValLt} {Ze}.@";
        }

        public static string FieldParentMessage(string field, string scriptableObject)
        {
            return $"{field} {FldNtSe} {field} of parent GameObject in {scriptableObject} {SObj}.@";
        }

        public static string FieldMessage(string field, string scriptableObject)
        {
            return $"{field} {FldNtSe} {scriptableObject} {SObj}.@";
        }

        public static string FieldGameObjectMessage(string field, string gameObject)
        {
            return $"{field} {FldNtSe} {gameObject} {GObj}.@";
        }

        public static string IsOddMessage(string field, string scriptableObject)
        {
            return $"{field} {FldNtSe} even value divisible by 2 in {scriptableObject} {SObj}.@";
        }

        public static void DebugLogWarning(int warningMessageCount, string warningMessage)
        {
            if (warningMessageCount <= 0) return;
            warningMessage = warningMessage.Replace("@", warningMessageCount == 1 ? "" : "\n");
            LogWarning($"{warningMessage}");
        }

        public static RaycastHit2D Raycast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance,
            LayerMask mask, Color color, bool drawGizmos = false)
        {
            if (drawGizmos) DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            return Physics2D.Raycast(rayOriginPoint, rayDirection, rayDistance, mask);
        }

        public static RaycastHit2D Boxcast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float length,
            LayerMask mask, Color color, bool drawGizmos = false)
        {
            if (!drawGizmos) return BoxCast(origin, size, angle, direction, length, mask);
            var rotation = Euler(0f, 0f, angle);
            var points = new Vector3[8];
            var halfSizeX = size.x / 2f;
            var halfSizeY = size.y / 2f;
            CalculatePoints();
            DebugDrawLines();
            return BoxCast(origin, size, angle, direction, length, mask);

            void CalculatePoints()
            {
                for (var i = 0; i < points.Length; i++)
                {
                    var vector = origin + left * halfSizeX + up * halfSizeY;
                    if (i >= 4) vector += length * direction;
                    points[i] = rotation * vector;
                }
            }

            void DebugDrawLines()
            {
                DrawLine(points[0], points[1], color);
                DrawLine(points[1], points[2], color);
                DrawLine(points[2], points[3], color);
                DrawLine(points[3], points[0], color);
                DrawLine(points[4], points[5], color);
                DrawLine(points[5], points[6], color);
                DrawLine(points[6], points[7], color);
                DrawLine(points[7], points[4], color);
                DrawLine(points[0], points[4], color);
                DrawLine(points[1], points[5], color);
                DrawLine(points[2], points[6], color);
                DrawLine(points[3], points[7], color);
            }
        }
    }
}