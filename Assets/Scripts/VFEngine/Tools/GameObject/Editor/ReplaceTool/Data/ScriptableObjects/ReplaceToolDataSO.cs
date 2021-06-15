using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects
{
    internal class ReplaceToolDataSO : ScriptableObject
    {
        internal UnityGameObject Prefab { get; private set; }
        protected internal UnityGameObject[] GameObjects { get; internal set; }

        internal ReplaceToolDataSO(Object dataObject)
        {
            if (dataObject != null)
            {
                var data = this;
                Prefab = data.Prefab;
                GameObjects = data.GameObjects;
            }

            Initialize();
        }

        internal ReplaceToolDataSO()
        {
            Initialize();
        }

        private void Initialize()
        {
            Prefab = null;
            GameObjects = null;
        }
    }
}