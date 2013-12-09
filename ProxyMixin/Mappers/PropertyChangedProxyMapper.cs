using ProxyMixin.Mixins;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mappers
{
    public class PropertyChangedProxyMapper<T, K> : ArrayCopyProxyMapper<T, K> where K : PropertyChangedProxyMapper<T, K>
    {
        protected PropertyChangedProxyMapper()
        {
        }

        private static PropertyChangedEventHandler CopyPropertyChangedEventHandler(Object source, PropertyChangedEventHandler sourceHandler,
            Object target, PropertyChangedEventHandler targetHandler)
        {
            if (target.GetType() == typeof(T))
                return targetHandler;

            return (PropertyChangedEventHandler)Delegate.Combine(targetHandler, (PropertyChangedEventHandler)PropertyChangedMixin<T>.BasePropertyChangedHandler);
        }
        protected override void Emit(ILGenerator il, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType == typeof(PropertyChangedEventHandler))
            {
                MethodInfo copyMethodInfo = ((Func<Object, PropertyChangedEventHandler,
                    Object, PropertyChangedEventHandler, PropertyChangedEventHandler>)CopyPropertyChangedEventHandler).Method;

                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.Emit(OpCodes.Call, copyMethodInfo);

                il.Emit(OpCodes.Stfld, fieldInfo);

            }
            else
                base.Emit(il, fieldInfo);
        }
    }

    public sealed class PropertyChangedProxyMapper<T> : PropertyChangedProxyMapper<T, PropertyChangedProxyMapper<T>>
    {
        private PropertyChangedProxyMapper()
        {
        }
    }
}
