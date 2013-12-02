using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ProxyMixin.Mixins
{
    [Flags]
    public enum ChangedStates
    {
        None = 0,
        Self = 1,
        Child = 2
    }

    public interface IChangeTrackingMixin
    {
        void AddChangedStates(ChangedStates changedStates);
        void Update(bool acceptChanges);

        ChangedStates ChangedStates
        {
            get;
        }
        IChangeTrackingMixin Parent
        {
            get;
        }
    }

    public class ChangeTrackingMixin<T> : PropertyChangedMixin<T>, IRevertibleChangeTracking, IChangeTrackingMixin
    {
        private ChangedStates _changedStates;
        private readonly ChangeTrackingFactory _factory;
        private readonly Type[] _noImplementInterfaces;
        private readonly IChangeTrackingMixin _parent;

        public ChangeTrackingMixin(ChangeTrackingFactory factory, String isChangedPropertyName, IChangeTrackingMixin parent)
            : base(isChangedPropertyName)
        {
            _noImplementInterfaces = new[] { typeof(IChangeTrackingMixin) };

            _factory = factory;
            _parent = parent;
        }

        public virtual void AcceptChanges()
        {
            Factory.Update(this, true);
        }
        public void AddChangedStates(ChangedStates changedStates)
        {
            _changedStates |= changedStates;
            base.SetIsChanged(true);
            if (Parent != null)
                Parent.AddChangedStates(ChangedStates.Child);
        }
        protected override Object GetValue(PropertyDescriptor propertyDescriptor)
        {
            Object value = propertyDescriptor.GetValue(base.ProxyObject.WrappedObject);
            return Factory.Create(value, this);
        }
        public virtual void RejectChanges()
        {
            Factory.Update(this, false);
        }
        protected override void SetValue(PropertyDescriptor propertyDescriptor, Object value)
        {
            propertyDescriptor.SetValue(base.ProxyObject.WrappedObject, value);
            base.OnPropertyChanged(propertyDescriptor.Name);
            AddChangedStates(ChangedStates.Self);
        }
        public virtual void Update(bool acceptChanges)
        {
            if ((ChangedStates & ChangedStates.Self) != ChangedStates.None)
            {
                if (acceptChanges)
                    base.ProxyObject.MemberwiseMapFromWrappedObject();
                else
                    base.ProxyObject.MemberwiseMapToWrappedObject();
                base.OnPropertyChanged(null);
            }

            _changedStates = Mixins.ChangedStates.None;
            base.SetIsChanged(false);
        }

        public ChangedStates ChangedStates
        {
            get
            {
                return _changedStates;
            }
        }
        public IChangeTrackingMixin Parent
        {
            get
            {
                return _parent;
            }
        }
        protected ChangeTrackingFactory Factory
        {
            get
            {
                return _factory;
            }
        }
        public override Type[] NoImplementInterfaces
        {
            get
            {
                return _noImplementInterfaces;
            }
        }
    }
}
