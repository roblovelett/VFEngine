using UnityEngine;

namespace VFEngine.Tools
{
    using static Time;

    public static class TimeExtensions
    {
        public static bool TimeLteZero()
        {
            return timeScale == 0 || deltaTime <= 0;
        }
    }
}