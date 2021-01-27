using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    public class PlatformerData : ScriptableObject
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
            SetIndex(0);
            InitializeSmallestDistanceProperties();
        }

        private void SetIndex(int index)
        {
            Index = index;
        }

        private void InitializeSmallestDistanceProperties()
        {
            SetSmallestDistance(float.MaxValue);
            SetSmallestDistanceIndex(0);
            SetSmallestDistanceHitConnected(false);
        }

        private void SetSmallestDistance(float distance)
        {
            SmallestDistance = distance;
        }

        private void SetSmallestDistanceIndex(int index)
        {
            SmallestDistanceIndex = index;
        }

        private void SetSmallestDistanceHitConnected(bool connected)
        {
            SmallestDistanceHitConnected = connected;
        }

        private void SmallestHitConnected()
        {
            SetSmallestDistanceHitConnected(true);
        }

        private void SetSmallestDistanceProperties(int index, float belowRaycastHitDistance)
        {
            SetSmallestDistanceIndex(index);
            SetSmallestDistance(belowRaycastHitDistance);
        }

        private void SetSmallestDistanceProperties(int index)
        {
            SetSmallestDistanceIndex(index);
            SetSmallestDistanceHitConnected(true);
        }

        #endregion

        #region event handlers

        public void OnInitialize()
        {
            Initialize();
        }

        public void OnSetIndex(int index)
        {
            SetIndex(index);
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
            SetSmallestDistanceProperties(index, belowRaycastHitDistance);
        }

        public void OnSetSmallestDistancePropertiesOnAboveRaycastHit(int index)
        {
            SetSmallestDistanceProperties(index);
        }

        public void OnSetSmallestDistance(float distance)
        {
            SetSmallestDistance(distance);
        }

        #endregion
    }
}