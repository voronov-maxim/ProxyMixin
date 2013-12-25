using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Builders
{
	public sealed class ProxyMethodBuilder
	{
		private readonly TypeBuilder _typeBuilder;
		private readonly List<InterfaceMapping> _mappings;

		public ProxyMethodBuilder(TypeBuilder typeBuilder)
		{
			_typeBuilder = typeBuilder;
			_mappings = new List<InterfaceMapping>();
		}

		public MethodBuilder DefineMethod(MethodInfo methodInfo, Action<ILGenerator> ilGenerator)
		{
			MethodAttributes methodAttributes = GetMethodAttributes(methodInfo);
			String methodName = (methodAttributes & MethodAttributes.Private) == 0 ?
				methodInfo.Name : methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
			Type[] parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
			MethodBuilder methodBuilder = _typeBuilder.DefineMethod(methodName, methodAttributes, methodInfo.ReturnType, parameterTypes);

			ilGenerator(methodBuilder.GetILGenerator());
			_typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
			return methodBuilder;
		}
		private MethodInfo GetTargetMethodInfo(MethodInfo methodInfo)
		{
			Type interfaceType = methodInfo.DeclaringType;
			var mapping = new InterfaceMapping();
			for (int i = 0; i < _mappings.Count; i++)
				if (_mappings[i].InterfaceType == interfaceType)
				{
					mapping = _mappings[i];
					break;
				}

			if (mapping.InterfaceType == null)
			{
				mapping = _typeBuilder.BaseType.GetInterfaceMap(interfaceType);
				_mappings.Add(mapping);
			}

			return ProxyBuilderHelper.GetTargetMethodInfo(ref mapping, methodInfo);
		}
		private MethodAttributes GetMethodAttributes(MethodInfo methodInfo)
		{
			if (methodInfo.DeclaringType.IsAssignableFrom(_typeBuilder.BaseType))
			{
				MethodInfo wrappedMethodInfo = GetTargetMethodInfo(methodInfo);
				if (!wrappedMethodInfo.IsFinal)
					return MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
			}

			return MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final;
		}
	}
}
