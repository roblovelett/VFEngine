using System;
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools
{
    public static class GameObjectExtensions
    {
        private static readonly List<Component> ComponentCache = new List<Component>();

        public static Component GetComponentNoAllocation(this GameObject @this, Type componentType)
        {
            @this.GetComponents(componentType, ComponentCache);
            var component = ComponentCache.Count > 0 ? ComponentCache[0] : null;
            ComponentCache.Clear();
            return component;
        }

        public static T GetComponentNoAllocation<T>(this GameObject @this) where T : class
        {
            @this.GetComponents(typeof(T), ComponentCache);
            var component = ComponentCache.Count > 0 ? ComponentCache[0] : null;
            ComponentCache.Clear();
            return component as T;
        }
    }
}