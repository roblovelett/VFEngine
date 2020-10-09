using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Gravity.GravityPoint;

namespace VFEngine.Platformer.Physics.Gravity
{
    using static UniTask;

    [CreateAssetMenu(fileName = "GravityModel", menuName = "VFEngine/Platformer/Physics/Gravity/Gravity Model",
        order = 0)]
    public class GravityModel : ScriptableObject
    {
        
    }
}

/*
[SerializeField] private float initialGravityAngle;
[SerializeField] private Transform characterTransform;
private Vector3 newRotationAngle = Vector3.zero;
//private float defaultGravityAngle;
//private float currentGravityAngle;
//private float previousGravityAngle;
private List<GravityPointController> gravityPoints;

public UniTask<UniTaskVoid> InitializeAsync()
{
try
{
    return new UniTask<UniTaskVoid>(InitializeAsyncInternal());
}
finally
{
    InitializeAsyncInternal().Forget();
}
}

private async UniTaskVoid InitializeAsyncInternal()
{
newRotationAngle.z = initialGravityAngle;
characterTransform.localEulerAngles = newRotationAngle;
//defaultGravityAngle = initialGravityAngle;
//currentGravityAngle = defaultGravityAngle;
//previousGravityAngle = defaultGravityAngle;
gravityPoints = new List<GravityPointController>();
UpdateGravityPointsList();
await Yield();
}

private void UpdateGravityPointsList()
{
if (gravityPoints.Count != 0) gravityPoints.Clear();
gravityPoints.AddRange(FindObjectsOfType(typeof(GravityPointController)) as GravityPointController[] ??
Array.Empty<GravityPointController>());
}
*/