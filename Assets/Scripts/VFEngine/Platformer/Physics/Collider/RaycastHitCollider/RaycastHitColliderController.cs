using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static Debug;
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;
    using static ColliderDirection;

    public class RaycastHitColliderController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private RaycastHitColliderModel upColliderModel;
        [SerializeField] private RaycastHitColliderModel rightColliderModel;
        [SerializeField] private RaycastHitColliderModel downColliderModel;
        [SerializeField] private RaycastHitColliderModel leftColliderModel;

        /* fields */
        private RaycastHitColliderModel[] models;

        /* fields: methods */
        private async void Awake()
        {
            GetModels();
            var rTask1 = upColliderModel.Initialize(Up);
            var rTask2 = rightColliderModel.Initialize(Right);
            var rTask3 = downColliderModel.Initialize(Down);
            var rTask4 = leftColliderModel.Initialize(Left);
            var rTask = await (rTask1, rTask2, rTask3, rTask4);
        }

        private void GetModels()
        {
            models = new[] {upColliderModel, rightColliderModel, downColliderModel, leftColliderModel};
            var names = new[] {"upColliderModel", "rightColliderModel", "downColliderModel", "leftColliderModel"};
            for (var i = 0; i < models.Length; i++)
            {
                if (models[i]) continue;
                models[i] = LoadData(ModelPath) as RaycastHitColliderModel;
                switch (i)
                {
                    case 0:
                        upColliderModel = models[i];
                        break;
                    case 1:
                        rightColliderModel = models[i];
                        break;
                    case 2:
                        downColliderModel = models[i];
                        break;
                    case 3:
                        leftColliderModel = models[i];
                        break;
                }

                Assert(models[i] != null, names[i] + " != null");
            }
        }
    }
}