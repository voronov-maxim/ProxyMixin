using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProxyMixin.Mixins
{
    public class PropertyChangedMixin<T> : CustomTypeDescriptorMixin<T>, INotifyPropertyChanged
    {
        private bool _isChanged;
        private readonly PropertyDescriptor _isChangedPropertyDescriptor;
        private readonly String _isChangedPropertyName;
        private PropertyChangedEventHandler _propertyChanged;

        public PropertyChangedMixin(String isChangedPropertyName = null)
            : base(GetMixinProperties(isChangedPropertyName))
        {
            if (!String.IsNullOrEmpty(isChangedPropertyName))
            {
                _isChangedPropertyName = isChangedPropertyName;
                _isChangedPropertyDescriptor = TypeDescriptor.GetProperties(typeof(T))[isChangedPropertyName];
            }
        }

        private static MixinProperty[] GetMixinProperties(String isChangedPropertyName)
        {
            if (String.IsNullOrEmpty(isChangedPropertyName))
                return null;

            return new[] { new CustomTypeDescriptorMixin<T>.MixinProperty("IsChanged", isChangedPropertyName) };
        }
        protected void OnPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
                _propertyChanged(base.ProxyObject, new PropertyChangedEventArgs(propertyName));
        }
        private void OnPropertyIsChangedChanged()
        {
            if (_isChangedPropertyName != null)
                OnPropertyChanged(_isChangedPropertyName);
        }
        protected void SetIsChanged(bool value)
        {
            if (_isChanged == value)
                return;

            _isChanged = value;
            OnPropertyIsChangedChanged();
            if (_isChangedPropertyDescriptor != null)
                _isChangedPropertyDescriptor.SetValue(base.ProxyObject, value);

        }
        protected override void SetValue(PropertyDescriptor propertyDescriptor, Object value)
        {
            base.SetValue(propertyDescriptor, value);
            OnPropertyChanged(propertyDescriptor.Name);
            SetIsChanged(true);
        }

        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }
    }
}
