using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProxyMixin.Mappers
{
    public class ArrayCopyProxyMapper<T, K> : ProxyMapper<T, K> where K : ArrayCopyProxyMapper<T, K>
    {
        protected ArrayCopyProxyMapper()
        {
        }

        private static I[] CopyObjectArray<I>(I[] source, I[] target) where I : class
        {
            if (source == null)
                return null;

            if (target == null || source.Length != target.Length)
                target = new I[source.Length];

            Array.Copy(source, 0, target, 0, source.Length);
            return target;
        }
        private static I[] CopyPrimitiveArray<I>(I[] source, I[] target) where I : struct
        {
            if (source == null)
                return null;

            if (target == null || source.Length != target.Length)
                target = new I[source.Length];

            Buffer.BlockCopy(source, 0, target, 0, Buffer.ByteLength(source));
            return target;
        }
        protected override void Emit(ILGenerator il, FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                Type elementType = fieldInfo.FieldType.GetElementType();
                MethodInfo copyArray;
                if (elementType.IsPrimitive)
                    copyArray = ((Func<int[], int[], int[]>)CopyPrimitiveArray<int>).Method;
                else
                    copyArray = ((Func<Object[], Object[], Object[]>)CopyObjectArray<Object>).Method;
                MethodInfo copyMethodInfo = copyArray.GetGenericMethodDefinition().MakeGenericMethod(elementType);

                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.Emit(OpCodes.Call, copyMethodInfo);

                il.Emit(OpCodes.Stfld, fieldInfo);
            }
            else
                base.Emit(il, fieldInfo);
        }
    }

    public sealed class ArrayCopyProxyMapper<T> : ArrayCopyProxyMapper<T, ArrayCopyProxyMapper<T>>
    {
        private ArrayCopyProxyMapper()
        {
        }
    }
}
