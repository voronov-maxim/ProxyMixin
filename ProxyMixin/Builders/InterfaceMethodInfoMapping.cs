using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ProxyMixin.Builders
{
    public struct InterfaceMethodInfoMapping
    {
        private readonly MethodInfo _interfaceMethod;
        private readonly Type _interfaceType;
        private MemberInfo _parentMemeber;
        private readonly MethodInfo _targetMethod;

        public InterfaceMethodInfoMapping(Type interfaceType, MethodInfo interfaceMethod, MethodInfo targetMethod)
        {
            _interfaceType = interfaceType;
            _interfaceMethod = interfaceMethod;
            _targetMethod = targetMethod;
            _parentMemeber = interfaceMethod;
        }

        private EventInfo GetParentEvent()
        {
            foreach (EventInfo eventInfo in InterfaceType.GetEvents())
                if (eventInfo.GetAddMethod() == InterfaceMethod || eventInfo.GetRemoveMethod() == InterfaceMethod)
                    return eventInfo;
            return null;
        }
        private PropertyInfo GetParentProperty()
        {
            foreach (PropertyInfo propertyInfo in InterfaceType.GetProperties())
                if (propertyInfo.GetGetMethod() == InterfaceMethod || propertyInfo.GetSetMethod() == InterfaceMethod)
                    return propertyInfo;
            return null;
        }
        public MethodInfo InterfaceMethod
        {
            get
            {
                return _interfaceMethod;
            }
        }
        public Type InterfaceType
        {
            get
            {
                return _interfaceType;
            }
        }
        public MemberInfo ParentMember
        {
            get
            {
                if (_parentMemeber == InterfaceMethod)
                {
                    _parentMemeber = GetParentProperty();
                    if (_parentMemeber == null)
                        _parentMemeber = GetParentEvent();
                }
                return _parentMemeber;
            }
        }
        public MethodInfo TargetMethod
        {
            get
            {
                return _targetMethod;
            }
        }
    }

    public sealed class InterfaceMethodInfoMappingCollection : ReadOnlyCollection<InterfaceMethodInfoMapping>
    {
        private InterfaceMethodInfoMappingCollection(IList<InterfaceMethodInfoMapping> mappings) : base(mappings)
        {
        }

        public static InterfaceMethodInfoMappingCollection Create(Type interfaceType, Type targetType)
        {
            var interfaceMappings = new List<InterfaceMapping>() { targetType.GetInterfaceMap(interfaceType) };
            interfaceMappings.AddRange(interfaceType.GetInterfaces().Select(i => targetType.GetInterfaceMap(i)));

            var methodMappings = new List<InterfaceMethodInfoMapping>();
            foreach (InterfaceMapping mapping in interfaceMappings)
                for (int i = 0; i < mapping.InterfaceMethods.Length; i++)
                    methodMappings.Add(new InterfaceMethodInfoMapping(mapping.InterfaceType, mapping.InterfaceMethods[i], mapping.TargetMethods[i]));

            return new InterfaceMethodInfoMappingCollection(methodMappings);
        }

        public InterfaceMethodInfoMapping FindByInterfaceMethod(MethodInfo interfaceMethod)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].InterfaceMethod == interfaceMethod)
                    return base[i];

            throw new InvalidOperationException("interfaceMethod not found");
        }
        public InterfaceMethodInfoMapping FindByTargetMethod(MethodInfo targetMethod)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].TargetMethod == targetMethod)
                    return base[i];

            throw new InvalidOperationException("targetMethod not found");
        }
    }
}
