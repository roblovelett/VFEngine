using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastModel
    {
        public Vector2 Direction { get; set; }
        public RaycastDirection RayDirection { get; set; }

        public enum RaycastDirection
        {
            None,
            Up,
            Right,
            Down,
            Left
        }

        public RaycastModel(RaycastDirection rayDirection)
        {
            RayDirection = rayDirection;
            Direction = SetDirection(rayDirection);
        }

        private static Vector2 SetDirection(RaycastDirection rayDirection)
        {
            Vector2 d;
            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (rayDirection)
            {
                case RaycastDirection.None:
                    d = new Vector2(0, 0);
                    break;
                case RaycastDirection.Up:
                    d = new Vector2(0, 1);
                    break;
                case RaycastDirection.Right:
                    d = new Vector2(1, 0);
                    break;
                case RaycastDirection.Down:
                    d = new Vector2(0, -1);
                    break;
                case RaycastDirection.Left:
                    d = new Vector2(-1, 0);
                    break;
                default:
                    d = new Vector2();
                    break;
            }

            return d;
        }
    }
}