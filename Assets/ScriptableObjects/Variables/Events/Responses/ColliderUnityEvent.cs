using System;
using UnityEngine.Events;

namespace ScriptableObjects.Variables.Events.Responses
{
    [Serializable]
    public sealed class CollisionUnityEvent : UnityEvent<Collision>
    {
    }
}