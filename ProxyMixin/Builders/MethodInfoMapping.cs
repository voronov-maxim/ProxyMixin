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
        private readonly MethodInfo _methodInfoGetOrAdd;
        private readonly MethodInfo _methodInfoSetOrRemove;

        public MethodInfoMapping(MemberInfo memberInfo, MethodInfo getOrAddMethodInfo, MethodInfo setOrRemoveMethodInfo)
        {
            _memberInfo = memberInfo;
            _methodInfoGetOrAdd = getOrAddMethodInfo;
            _methodInfoSetOrRemove = setOrRemoveMethodInfo;
        }

        public FieldBuilder FieldBuilder
        {
            get;
            set;
        }
		public FieldBuilder FieldBuilderGetOrAdd
		{
			get;
			set;
		}
		public FieldBuilder FieldBuilderSetOrRemove
		{
			get;
			set;
		}
		public MethodInfo MethodInfoGetOrAdd
        {
            get
            {
                return _methodInfoGetOrAdd;
            }
        }
        public MemberInfo MemberInfo
        {
            get
            {
                return _memberInfo;
            }
        }
        public MethodInfo MethodInfoSetOrRemove
        {
            get
            {
                return _methodInfoSetOrRemove;
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
