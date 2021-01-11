using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
namespace VFEngine.Tools.BetterEvent
{
    [Serializable]
    public class BetterEventEntry : ISerializationCallbackReceiver
    {
        [NonSerialized] public Delegate @delegate;
        [NonSerialized] public object[] parameterValues;

        public BetterEventEntry(Delegate del)
        {
            if (del == null || del.Method == null) return;
            @delegate = del;
            parameterValues = new object[del.Method.GetParameters().Length];
        }

        public void Invoke()
        {
            if (@delegate != null && parameterValues != null)
                @delegate.Method.Invoke(@delegate.Target, parameterValues);
        }

        #region OdinSerialization

        [SerializeField] [HideInInspector] private List<Object> unityReferences;
        [SerializeField] [HideInInspector] private byte[] bytes;

        public void OnAfterDeserialize()
        {
            var val = SerializationUtility.DeserializeValue<OdinSerializedData>(bytes, DataFormat.Binary,
                unityReferences);
            @delegate = val.@delegate;
            parameterValues = val.parameterValues;
        }

        public void OnBeforeSerialize()
        {
            var val = new OdinSerializedData {@delegate = @delegate, parameterValues = parameterValues};
            bytes = SerializationUtility.SerializeValue(val, DataFormat.Binary, out unityReferences);
        }

        private struct OdinSerializedData
        {
            public Delegate @delegate;
            public object[] parameterValues;
        }

        #endregion
    }
}