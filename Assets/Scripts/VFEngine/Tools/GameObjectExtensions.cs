using System;
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools
{
    public static class GameObjectExtensions
    {
        private static readonly List<Component> MComponentCache = new List<Component>();

        public static Component GetComponentNoAlloc(this GameObject @this, Type componentType)
        {
            @this.GetComponents(componentType, MComponentCache);
            var component = MComponentCache.Count > 0 ? MComponentCache[0] : null;
            MComponentCache.Clear();
            return component;
        }

        public static T GetComponentNoAlloc<T>(this GameObject @this) where T : Component
        {
            @this.GetComponents(typeof(T), MComponentCache);
            var component = MComponentCache.Count > 0 ? MComponentCache[0] : null;
            MComponentCache.Clear();
            return component as T;
        }
    }
}