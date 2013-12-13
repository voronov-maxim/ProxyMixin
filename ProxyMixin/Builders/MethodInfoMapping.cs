using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
    public sealed class MethodInfoMapping
    {
        private readonly MemberInfo _memberInfo;
        private readonly MethodInfo _getOrAddMethodInfo;
        private readonly MethodInfo _setOrRemoveMethodInfo;

        public MethodInfoMapping(MemberInfo memberInfo, MethodInfo getOrAddMethodInfo, MethodInfo setOrRemoveMethodInfo)
        {
            _memberInfo = memberInfo;
            _getOrAddMethodInfo = getOrAddMethodInfo;
            _setOrRemoveMethodInfo = setOrRemoveMethodInfo;
        }

        public FieldBuilder FieldBuilder
        {
            get;
            set;
        }
        public MethodInfo GetOrAddMethodInfo
        {
            get
            {
                return _getOrAddMethodInfo;
            }
        }
        public MemberInfo MemberInfo
        {
            get
            {
                return _memberInfo;
            }
        }
        public MethodInfo SetOrRemoveMethodInfo
        {
            get
            {
                return _setOrRemoveMethodInfo;
            }
        }

        public override string ToString()
        {
            return MemberInfo == null ? base.ToString() : MemberInfo.ToString();
        }
    }

    public sealed class MethodInfoMappingCollection : ReadOnlyCollection<MethodInfoMapping>
    {
        public MethodInfoMappingCollection(IList<MethodInfoMapping> mappings)
            : base(mappings)
        {
        }

        public MethodInfoMapping this[String name]
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                    if (base[i].MemberInfo.Name == name)
                        return base[i];

                throw new ArgumentOutOfRangeException("name", name);
            }
        }
    }
}
