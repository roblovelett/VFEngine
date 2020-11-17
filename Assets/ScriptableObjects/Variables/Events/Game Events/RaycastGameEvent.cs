using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Events.Game_Events
{
    using static SOArchitecture_Utility;

    [Serializable]
    [CreateAssetMenu(fileName = "RaycastGameEvent.asset", menuName = GAME_EVENT + "Raycast", order = 120)]
    public sealed class RaycastGameEvent : GameEventBase<Raycast>
    {
    }
}