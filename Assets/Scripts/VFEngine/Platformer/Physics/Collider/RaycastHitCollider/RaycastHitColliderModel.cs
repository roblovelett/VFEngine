using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastHitColliderModel",
        menuName = "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Model", order = 0)]
    public class RaycastHitColliderModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Raycast Hit Collider Data")] [SerializeField]
        private RaycastHitColliderData rhc;
        private const string AssetPath = "Physics/Collider/RaycastHitCollider/DefaultRaycastHitColliderModel.asset";

        /* fields: methods */
        private async UniTaskVoid InitializeModelAsyncInternal()
        {
            rhc.SideHitsStorage = new RaycastHit2D[rhc.NumberOfHorizontalRays];
            rhc.BelowHitsStorage = new RaycastHit2D[rhc.NumberOfVerticalRays];
            rhc.AboveHitsStorage = new RaycastHit2D[rhc.NumberOfVerticalRays];
            rhc.State.Reset();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";
        
        /* properties: methods */
        public void InitializeData()
        {
            rhc.Initialize();
        }

        public UniTask<UniTaskVoid> InitializeModelAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(InitializeModelAsyncInternal());
            }
            finally
            {
                InitializeModelAsyncInternal().Forget();
            }
        }
    }
}

//private async UniTaskVoid OnInitializeModelAsyncInternal(int numberOfHorizontalRays, int numberOfVerticalRays)
//{
/* get data */
/*
data = new Data(1, 1);
originalColliderOffset = boxCollider.offset;
originalColliderSize = boxCollider.size;
if (displayWarnings)
*/
/*{*/ /*
                if (!boxCollider)
                    Debug.LogWarning(
                        $"{boxCollider} is not set to parent {character.name}'s object's box collider 2D.");
                if (boxCollider.offset.x != 0)
                    Debug.LogWarning(
                        $"{character.name}'s collider x offset not set to zero. Direction change near walls will error.");
            */ //}
/*
contactList = new List<RaycastHit2D>();
sideHitsStorage = new RaycastHit2D[data.NumberOfHorizontalRays];
belowHitsStorage = new RaycastHit2D[data.NumberOfVerticalRays];
aboveHitsStorage = new RaycastHit2D[data.NumberOfVerticalRays];
currentWallColliderGameObject = null;
State = new ModelState();
State.Reset();
await Yield();
*/
//}
/*
private async UniTaskVoid OnFrameInitializeModelAsyncInternal()
{
    contactList.Clear();
    State.WasGroundedLastFrame = State.IsCollidingBelow;
    State.WasTouchingCeilingLastFrame = State.IsCollidingAbove;
    standingOnLastFrame = standingOn;
    if (currentWallColliderGameObject) currentWallColliderGameObject = null;
    State.Reset();
    await Yield();
}

private async UniTaskVoid OnMovingPlatformAsyncInternal()
{
    if (movingPlatform && IsTime(timeScale, deltaTime) && !State.WasTouchingCeilingLastFrame &&
        !IsNan(movingPlatform.CurrentSpeed))
    {
        State.OnMovingPlatform = true;
        movingPlatformCurrentGravity = movingPlatformGravity;
    }

    await Yield();
}

private async UniTaskVoid OnRaycastHorizontalAsyncInternal(IReadOnlyList<RaycastHit2D> sideHitsStorage,
    int numberOfHorizontalRays, int movementDirection, float rayDirection, float maximumSlopeAngle,
    Vector2 horizontalRayCastFromBottom, Vector2 horizontalRayCastToTop)
{
    for (var i = 0; i < numberOfHorizontalRays; i++)
    {
        if (!(sideHitsStorage[i].distance > 0) || sideHitsStorage[i].collider == ignoredCollider) continue;
        var hitLateralSlopeAngle = Mathf.Abs(Vector2.Angle(sideHitsStorage[i].normal, transform.up));
        if (Math.Abs(movementDirection - rayDirection) < Tolerance)
            State.LateralSlopeAngle = hitLateralSlopeAngle;
        if (!(hitLateralSlopeAngle > maximumSlopeAngle)) continue;
        if (rayDirection < 0)
        {
            State.IsCollidingLeft = true;
            State.DistanceToLeftRaycastHit = sideHitsStorage[i].distance;
        }
        else
        {
            State.IsCollidingRight = true;
            State.DistanceToRightRaycastHit = sideHitsStorage[i].distance;
        }

        if (Math.Abs(movementDirection - rayDirection) < Tolerance)
        {
            currentWallColliderGameObject = sideHitsStorage[i].collider.gameObject;
            State.SlopeAnglePassed = false;
            distanceToWall = DistanceBetweenPointAndLine(sideHitsStorage[i].point, horizontalRayCastFromBottom,
                horizontalRayCastToTop);
            contactList.Add(sideHitsStorage[i]);
        }

        break;
    }

    await Yield();
}
*/