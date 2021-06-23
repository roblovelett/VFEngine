﻿using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.ReplaceTool.Editor.Data.ScriptableObjects
{
    internal class ReplaceToolDataSO : ScriptableObject
    {
        [SerializeField] internal UnityGameObject replacementPrefab;
        [SerializeField] private UnityGameObject[] objectsToReplace;
    }
}