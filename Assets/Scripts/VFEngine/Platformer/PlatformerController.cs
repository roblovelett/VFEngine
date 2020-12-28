using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private PlatformerModel platformer;
        private RaycastController raycastController;
        private LayerMaskController layerMaskController;
        private PhysicsController physicsController;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            raycastController = GetComponent<RaycastController>();
            layerMaskController = GetComponent<LayerMaskController>();
            physicsController = GetComponent<PhysicsController>();
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            platformer = new PlatformerModel(settings, raycastController, layerMaskController, physicsController);
        }

        #endregion

        #endregion

        private void FixedUpdate()
        {
            platformer.Run();
        }

        #endregion

        #region properties

        public PlatformerData Data => platformer.Data;

        #region public methods

        #endregion

        #endregion
    }
}

//private void Platformer()
//{
//InitializeFrame();
//CastRaysDown();
/*SetExternalForce();
SetGravity();
SetHorizontalExternalForce();
CheckSlopes();
CastRaysToSides();
*/
//}
/*
private void CastRaysDown()
{
    raycastController.OnPlatformerInitializeIndex();
    for (var i = 0; i < VerticalRays; i++)
    {
        raycastController.OnPlatformerSetDownOrigin();
        raycastController.OnPlatformerSetDownHit();
        if (CastDownAtOneWayPlatform)
        {
            raycastController.OnPlatformerCastDownAtOneWayPlatform();
            if (HitMissed)
            {
                AddToIndex();
                continue;
            }
        }

        if (Hit)
        {
            raycastController.OnPlatformerOnDownHit();
            break;
        }

        AddToIndex();
    }

    void AddToIndex()
    {
        raycastController.OnPlatformerAddToIndex();
    }
}
*/
/*
private void SetExternalForce()
{
    physicsController.OnPlatformerSetExternalForce();
}

private void SetGravity()
{
    physicsController.OnPlatformerSetGravity();
}

private void SetHorizontalExternalForce()
{
    physicsController.OnPlatformerSetHorizontalExternalForce();
}

private void CheckSlopes()
{
    //if (!HorizontalMovement || !OnSlopes) return;
    //if (DescendingSlope) DescendSlope();
    //else ClimbSlope();
}

private void DescendSlope()
{
    physicsController.OnPlatformerDescendSlope();
    //downRaycastHitColliderController.OnPlatformerDescendSlope();
}

private void ClimbSlope()
{
    physicsController.OnPlatformerClimbSlope();
    //downRaycastHitColliderController.OnPlatformerClimbSlope();
}

private void CastRaysToSides()
{
    raycastController.OnPlatformerCastRaysToSides();
}*/