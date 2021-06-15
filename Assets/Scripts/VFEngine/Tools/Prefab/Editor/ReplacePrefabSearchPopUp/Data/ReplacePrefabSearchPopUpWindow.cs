using UnityEngine;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data
{
    internal struct ReplacePrefabSearchPopUpWindow
    {
        internal Vector2 StartPosition { get; private set; }
        internal Vector2 StartSize { get; private set; }
        internal Vector2 NewSize { get; private set; }
        internal Vector2 LastSize { get; private set; }
        internal Rect Position { get; private set; }

        internal ReplacePrefabSearchPopUpWindow(Vector2 startPosition, Vector2 startSize, Rect position) : this()
        {
            StartPosition = startPosition;
            StartSize = startSize;
            Position = position;
            NewSize = new Vector2();
            LastSize = new Vector2();
        }

        internal void InitializeStartPosition(Vector2 startPosition)
        {
            StartPosition = startPosition;
        }

        internal void InitializeStartSize(Vector2 startSize)
        {
            StartSize = startSize;
        }

        internal void InitializeNewSize(Vector2 newSize)
        {
            NewSize = newSize;
        }

        internal void InitializeLastSize(Vector2 lastSize)
        {
            LastSize = lastSize;
        }

        internal void InitializePosition(Rect position)
        {
            Position = position;
        }
    }
}