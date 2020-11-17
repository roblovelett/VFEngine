using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.Events.Game_Events;
using ScriptableObjects.Variables.Events.Responses;
using UnityEngine;

namespace ScriptableObjects.Variables.Events.Listeners
{
    using static SOArchitecture_Utility;

    [AddComponentMenu(EVENT_LISTENER_SUBMENU + "Raycast")]
    public sealed class RaycastGameEventListener : BaseGameEventListener<Raycast, RaycastGameEvent, RaycastUnityEvent>
    {
    }
}