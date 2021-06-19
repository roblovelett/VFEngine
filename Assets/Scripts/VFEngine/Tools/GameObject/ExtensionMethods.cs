using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject
{
    public static class ExtensionMethods
    {
        private static readonly List<Component> ComponentCache = new List<Component>();
        
        public static Component GetComponentNoAlloc(this UnityGameObject @this, Type componentType)
        {
            @this.GetComponents(componentType, ComponentCache);
            var component = ComponentCache.Count > 0 ? ComponentCache[0] : null;
            ComponentCache.Clear();
            return component;
        }
        
        public static T GetComponentNoAlloc<T>(this UnityGameObject @this) where T : Component
        {
            @this.GetComponents(typeof(T), ComponentCache);
            var component = ComponentCache.Count > 0 ? ComponentCache[0] : null;
            ComponentCache.Clear();
            return component as T;
        }
        
    }
}