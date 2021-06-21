﻿using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.ReplaceTool.Editor.Data.ScriptableObjects
{
    public class ReplaceToolDataSO : ScriptableObject
    {
        [SerializeField] public UnityGameObject replacementPrefab;
        [SerializeField] public UnityGameObject[] objectsToReplace;
    }
}