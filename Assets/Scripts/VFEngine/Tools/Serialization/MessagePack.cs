using UnityEditor;
using UnityEngine;
/*
using MessagePack;

namespace VFEngine.Tools.Serialization
{
    public class Startup
    {
        private static bool serializerRegistered;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (!serializerRegistered)
            {
                Instance.Register(GeneratedResolver.Instance, StandardResolver.Instance);
                var option = Standard.WithResolver(Instance);
                DefaultOptions = option;
                serializerRegistered = true;
            }
        }

#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        private static void EditorInitialize()
        {
            Initialize();
        }

#endif
    }
}*/