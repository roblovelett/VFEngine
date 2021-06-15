using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;

    [CreateAssetMenu(fileName = "PlatformerData", menuName = DataPath, order = 0)]
    public class Data : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public int Index { get; private set; }
        public float Tolerance { get; } = 0f;
        public float SmallValue { get; } = 0.0001f;
        public float SmallestDistance { get; private set; }
        public int SmallestDistanceIndex { get; private set; }
        public bool SmallestDistanceHitConnected { get; private set; }

        #endregion

        #region fields

        #endregion

        #region initialization

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void Initialize()
        {
            IndexInternal(0);
            InitializeSmallestDistanceProperties();
        }

        private void IndexInternal(int index)
        {
            Index = index;
        }

        private void InitializeSmallestDistanceProperties()
        {
            SmallestDistanceInternal(float.MaxValue);
            SmallestDistanceIndexInternal(0);
            SmallestDistanceHitConnectedInternal(false);
        }

        private void SmallestDistanceInternal(float distance)
        {
            SmallestDistance = distance;
        }

        private void SmallestDistanceIndexInternal(int index)
        {
            SmallestDistanceIndex = index;
        }

        private void SmallestDistanceHitConnectedInternal(bool connected)
        {
            SmallestDistanceHitConnected = connected;
        }

        private void SmallestHitConnected()
        {
            SmallestDistanceHitConnectedInternal(true);
        }

        private void SmallestDistanceProperties(int index, float belowRaycastHitDistance)
        {
            SmallestDistanceIndexInternal(index);
            SmallestDistanceInternal(belowRaycastHitDistance);
        }

        private void SmallestDistanceProperties(int index)
        {
            SmallestDistanceIndexInternal(index);
            SmallestDistanceHitConnectedInternal(true);
        }

        #endregion

        #region event handlers

        public void OnInitialize()
        {
            Initialize();
        }

        public void OnSetIndex(int index)
        {
            IndexInternal(index);
        }

        public void OnInitializeSmallestDistanceProperties()
        {
            InitializeSmallestDistanceProperties();
        }

        public void OnSmallestHitConnected()
        {
            SmallestHitConnected();
        }

        public void OnSetSmallestDistanceProperties(int index, float belowRaycastHitDistance)
        {
            SmallestDistanceProperties(index, belowRaycastHitDistance);
        }

        public void OnSetSmallestDistancePropertiesOnAboveRaycastHit(int index)
        {
            SmallestDistanceProperties(index);
        }

        public void OnSetSmallestDistance(float distance)
        {
            SmallestDistanceInternal(distance);
        }

        #endregion
    }
}