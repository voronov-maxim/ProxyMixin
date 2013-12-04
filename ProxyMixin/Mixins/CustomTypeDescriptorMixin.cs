using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ProxyMixin.Mixins
{
    public class CustomTypeDescriptorMixin<T> : IDynamicMixin, ICustomTypeDescriptor
    {
        protected sealed class MixinProperty
        {
            private readonly String _mixinPropertyName;
            private readonly String _wrappedPropertyName;

            public MixinProperty(String mixinPropertyName, String wrappedPropertyName)
            {
                _mixinPropertyName = mixinPropertyName;
                _wrappedPropertyName = wrappedPropertyName;
            }

            public String MixinPropertyName
            {
                get
                {
                    return _mixinPropertyName;
                }
            }
            public String WrappedPropertyName
            {
                get
                {
                    return _wrappedPropertyName;
                }
            }
        }

        private sealed class PrivateTypeConverter : TypeConverter
        {
            private sealed class DynamicPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
            {
                private readonly CustomTypeDescriptorMixin<T> _mixin;
                private readonly PropertyDescriptor _originalPropertyDescriptor;

                public DynamicPropertyDescriptor(CustomTypeDescriptorMixin<T> mixin, PropertyDescriptor propertyDescriptor)
                    : base(typeof(T), propertyDescriptor.Name, propertyDescriptor.PropertyType)
                {
                    _mixin = mixin;
                    _originalPropertyDescriptor = propertyDescriptor;
                }

                public override Object GetValue(Object component)
                {
                    return _mixin.GetValue(_originalPropertyDescriptor);
                }
                public override void SetValue(Object component, Object value)
                {
                    _mixin.SetValue(_originalPropertyDescriptor, value);
                }
            }

            private sealed class MixinPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
            {
                private readonly CustomTypeDescriptorMixin<T> _mixin;
                private readonly PropertyInfo _propertyInfo;

                public MixinPropertyDescriptor(CustomTypeDescriptorMixin<T> mixin, PropertyInfo propertyInfo, String mapName)
                    : base(typeof(T), mapName, propertyInfo.PropertyType)
                {
                    _mixin = mixin;
                    _propertyInfo = propertyInfo;
                }

                public override Object GetValue(Object component)
                {
                    return _propertyInfo.GetValue(_mixin);
                }
                public override void SetValue(Object component, Object value)
                {
                    _propertyInfo.SetValue(_mixin, value);
                }
            }

            public static PropertyDescriptor CreateDynamicPropertyDescriptor(CustomTypeDescriptorMixin<T> mixin, PropertyDescriptor propertyDescriptor)
            {
                return new DynamicPropertyDescriptor(mixin, propertyDescriptor);
            }
            public static PropertyDescriptor CreateMixinPropertyDescriptor(CustomTypeDescriptorMixin<T> mixin, PropertyInfo propertyInfo, String mapName)
            {
                return new MixinPropertyDescriptor(mixin, propertyInfo, mapName);
            }
        }

        private readonly PropertyDescriptorCollection _fakePropertyDescriptors;
        private IDynamicProxy _proxyObject;

        public CustomTypeDescriptorMixin()
            : this(null)
        {
        }
        protected CustomTypeDescriptorMixin(IEnumerable<MixinProperty> mixinProperties)
        {
            _fakePropertyDescriptors = CreateFakePropertyDescriptors(this, mixinProperties);
        }

        private static PropertyDescriptorCollection CreateFakePropertyDescriptors(
            CustomTypeDescriptorMixin<T> mixin, IEnumerable<MixinProperty> mixinProperties)
        {
            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(typeof(T));
            List<PropertyDescriptor> fakePropertyDescriptors = propertyDescriptors.Cast<PropertyDescriptor>()
                .Select(p => PrivateTypeConverter.CreateDynamicPropertyDescriptor(mixin, p)).ToList();

            if (mixinProperties != null)
                foreach (MixinProperty mixinProperty in mixinProperties)
                {
                    int index = fakePropertyDescriptors.FindIndex(p => p.Name == mixinProperty.WrappedPropertyName);
                    if (index >= 0)
                        fakePropertyDescriptors.RemoveAt(index);

                    PropertyInfo propertyInfo = mixin.GetType().GetProperty(mixinProperty.MixinPropertyName);
                    fakePropertyDescriptors.Add(
                        PrivateTypeConverter.CreateMixinPropertyDescriptor(mixin, propertyInfo, mixinProperty.WrappedPropertyName));
                }

            return new PropertyDescriptorCollection(fakePropertyDescriptors.ToArray());
        }
        protected static PropertyDescriptor CreateMixinPropertyDescriptor(CustomTypeDescriptorMixin<T> mixin, PropertyInfo propertyInfo, String mapName)
        {
            return PrivateTypeConverter.CreateMixinPropertyDescriptor(mixin, propertyInfo, mapName);
        }
        protected virtual Object GetValue(PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.GetValue(ProxyObject);
        }
        protected virtual void SetValue(PropertyDescriptor propertyDescriptor, Object value)
        {
            propertyDescriptor.SetValue(ProxyObject, value);
            propertyDescriptor.SetValue(ProxyObject.WrappedObject, value);
        }

        public virtual Type[] NoImplementInterfaces
        {
            get
            {
                return Type.EmptyTypes;
            }
        }
        public IDynamicProxy ProxyObject
        {
            get
            {
                return _proxyObject;
            }
            set
            {
                _proxyObject = value;
            }
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(ProxyObject);
        }
        String ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(ProxyObject);
        }
        String ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(ProxyObject);
        }
        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(ProxyObject);
        }
        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(ProxyObject);
        }
        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(ProxyObject);
        }
        Object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(ProxyObject, editorBaseType);
        }
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(ProxyObject, attributes);
        }
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(ProxyObject);
        }
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(ProxyObject, attributes);
        }
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return _fakePropertyDescriptors;
        }
        Object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return ProxyObject;
        }
    }
}
