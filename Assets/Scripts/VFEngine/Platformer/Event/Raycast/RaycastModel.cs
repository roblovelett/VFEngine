using Cysharp.Threading.Tasks;
using UnityEngine;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static RaycastData;
    //using static RaycastData.RaycastDirection;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "RaycastModel", menuName = "VFEngine/Platformer/Event/Raycast/Raycast Model",
        order = 0)]
    public class RaycastModel : ScriptableObject, IModel
    {
        [SerializeField] private RaycastData r;


        private static async UniTaskVoid Method()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        
        private static async UniTaskVoid AnotherMethod()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        
        private static async UniTaskVoid InitializeInternal()
        {
            var foo = Async(AnotherMethod());
            var bar = Async(Method());
            var foobar = await (foo, bar);
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        public void Initialize()
        {
            Async(InitializeInternal());
        }
    }
}

/* fields: dependencies */
/*
[SerializeField] private RaycastData r;
/* fields */

/* fields: methods */
/*
private async UniTaskVoid SetDirectionUpAsyncInternal()
{
    SetDirection(Up);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetDirectionRightAsyncInternal()
{
    SetDirection(Right);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetDirectionDownAsyncInternal()
{
    SetDirection(Down);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetDirectionLeftAsyncInternal()
{
    SetDirection(Left);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private void SetDirection(RaycastDirection direction)
{
    r.RayDirection = direction;
    r.Direction = SetRaycastDirection(direction);
}

private static Vector2 SetRaycastDirection(RaycastDirection direction)
{
    Vector2 d;

    // ReSharper disable once ConvertSwitchStatementToSwitchExpression
    switch (direction)
    {
        case Up:
            d = new Vector2(0, 1);
            break;
        case Right:
            d = new Vector2(1, 0);
            break;
        case Down:
            d = new Vector2(0, -1);
            break;
        case Left:
            d = new Vector2(-1, 0);
            break;
        case None:
            d = new Vector2(0, 0);
            break;
        default:
            d = new Vector2(0, 0);
            break;
    }

    return d;
}

/* properties */

/* properties: methods */
/*
public UniTask<UniTaskVoid> SetDirectionUpAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetDirectionUpAsyncInternal());
    }
    finally
    {
        SetDirectionUpAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetDirectionRightAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetDirectionRightAsyncInternal());
    }
    finally
    {
        SetDirectionRightAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetDirectionDownAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetDirectionDownAsyncInternal());
    }
    finally
    {
        SetDirectionDownAsyncInternal().Forget();
    }
}

public UniTask<UniTaskVoid> SetDirectionLeftAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(SetDirectionLeftAsyncInternal());
    }
    finally
    {
        SetDirectionLeftAsyncInternal().Forget();
    }
}
*/