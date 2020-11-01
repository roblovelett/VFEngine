namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    public class DistanceToGroundRaycastModel
    {
        
    }
}

public void SetDistanceToGroundRaycastOrigin()
{
distanceToGroundRaycastModel.OnSetDistanceToGroundRaycastOrigin();
}

public async UniTaskVoid SetDistanceToGroundRaycast()
{
distanceToGroundRaycastModel.OnSetDistanceToGroundRaycast();
await SetYieldOrSwitchToThreadPoolAsync();
}

public async UniTaskVoid SetHasDistanceToGroundRaycast()
{
distanceToGroundRaycastModel.OnSetHasDistanceToGroundRaycast();
await SetYieldOrSwitchToThreadPoolAsync();
}