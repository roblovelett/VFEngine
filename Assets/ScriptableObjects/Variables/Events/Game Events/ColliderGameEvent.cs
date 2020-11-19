using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Events.Game_Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "CollisionGameEvent.asset", menuName = SOArchitecture_Utility.GAME_EVENT + "Collision",
        order = 120)]
    public sealed class CollisionGameEvent : GameEventBase<Collision>
    {
    }
}