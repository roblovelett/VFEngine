using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable UnusedType.Global
namespace VFEngine.Tools.BetterEvent.Editor
{
    public class BetterEventProcessor : OdinPropertyProcessor<BetterEventEntry>
    {
        public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
        {
            var val = (BetterEventEntry) Property.ValueEntry.WeakSmartValue;
            if (val.@delegate == null) return;
            if (val.@delegate.Method == null) return;
            var ps = val.@delegate.Method.GetParameters();
            for (var i = 0; i < ps.Length; i++)
            {
                var p = ps[i];
                var getterSetterType = typeof(ArrayIndexGetterSetter<>).MakeGenericType(p.ParameterType);
                var getterSetter = Activator.CreateInstance(getterSetterType, Property, i) as IValueGetterSetter;
                var info = InspectorPropertyInfo.CreateValue(p.Name, i, SerializationBackend.Odin, getterSetter);
                propertyInfos.Add(info);
            }
        }

        private class ArrayIndexGetterSetter<T> : IValueGetterSetter<object, T>
        {
            private readonly InspectorProperty property;
            private readonly int index;
            public bool IsReadonly => false;
            public Type OwnerType => typeof(object);
            public Type ValueType => typeof(T);

            public object[] ParameterValues
            {
                get
                {
                    var val = (BetterEventEntry) property.ValueEntry.WeakSmartValue;
                    return val.parameterValues;
                }
            }

            public ArrayIndexGetterSetter(InspectorProperty property, int index)
            {
                this.property = property;
                this.index = index;
            }

            public T GetValue(ref object owner)
            {
                var parametersInternal = ParameterValues;
                if (parametersInternal == null || index >= parametersInternal.Length) return default;
                if (parametersInternal[index] == null) return default;
                try
                {
                    return (T) parametersInternal[index];
                }
                catch
                {
                    return default;
                }
            }

            public object GetValue(object owner)
            {
                var parametersInternal = ParameterValues;
                if (parametersInternal == null || index >= parametersInternal.Length) return default(T);
                return ParameterValues[index];
            }

            public void SetValue(ref object owner, T value)
            {
                var parametersInternal = ParameterValues;
                if (parametersInternal == null || index >= parametersInternal.Length) return;
                parametersInternal[index] = value;
            }

            public void SetValue(object owner, object value)
            {
                var parametersInternal = ParameterValues;
                if (parametersInternal == null || index >= parametersInternal.Length) return;
                parametersInternal[index] = value;
            }
        }
    }
}