using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;

namespace VFEngine.Platformer.Event.Raycasts
{
    [RequireComponent(typeof(StickyRaycastController))]
    [RequireComponent(typeof(SafetyBoxcastController))]
    public class RaycastsController : MonoBehaviour
    {
        
    }
}

/*
[SerializeField] private RaycastsModel model;

private async void Awake()
{
    await model.InitializeAsync(model);
}
*/