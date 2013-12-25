using ProxyMixin.Ctors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace ProxyMixin.Mixins
{
    public sealed class ListChangeTrackingMixin<T, K> : ChangeTrackingMixin<T>, IList<K>, IList, ICollectionViewFactory where T : IList<K>
    {
        private sealed class ListChangeTrackingCollectionView : ListCollectionView, IEditableCollectionView
        {
            public ListChangeTrackingCollectionView(ListChangeTrackingMixin<T, K> list)
                : base(list)
            {
            }

            Object IEditableCollectionView.AddNew()
            {
                var list = (ListChangeTrackingMixin<T, K>)base.InternalList;
                K proxy = list.Factory.Create(Activator.CreateInstance<K>(), null, list);
                return base.AddNewItem(proxy);
            }
            public void Reset()
            {
                base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private ListChangeTrackingCollectionView _collectionView;
        private readonly Object _syncRoot;
        private IList<K> _interfaceInvoker;

        public ListChangeTrackingMixin(ChangeTrackingCtor factory, String isChangedPropertyName = null)
            : this(factory, isChangedPropertyName, null)
        {
        }
        public ListChangeTrackingMixin(ChangeTrackingCtor factory, String isChangedPropertyName, IChangeTrackingMixin parent) :
            base(factory, isChangedPropertyName, parent)
        {
            _syncRoot = new Object();
        }

        public void Add(K item)
        {
            WrappedList.Add(GetWrappedItem(item));
            base.AddChangedStates(ChangedStates.Self);
        }
        public void Clear()
        {
            WrappedList.Clear();
            base.AddChangedStates(ChangedStates.Self);
        }
        public bool Contains(K item)
        {
            return WrappedList.Contains(GetWrappedItem(item));
        }
        public void CopyTo(K[] array, int arrayIndex)
        {
            WrappedList.CopyTo(array, arrayIndex);
        }
        private static K GetWrappedItem(K item)
        {
            var proxy = item as IDynamicProxy;
            return proxy == null ? item : (K)proxy.WrappedObject;
        }
        public IEnumerator<K> GetEnumerator()
        {
            return WrappedList.GetEnumerator();
        }
        public int IndexOf(K item)
        {
            return WrappedList.IndexOf(GetWrappedItem(item));
        }
        public void Insert(int index, K item)
        {
            WrappedList.Insert(index, GetWrappedItem(item));
            base.AddChangedStates(ChangedStates.Self);
        }
        protected override void OnProxyObjectChanged()
        {
            _interfaceInvoker = ProxyCtor.GetMethodInvoker<T, IList<K>>((T)base.ProxyObject);
        }
        public bool Remove(K item)
        {
            bool flag = WrappedList.Remove(GetWrappedItem(item));
            base.AddChangedStates(ChangedStates.Self);
            return flag;
        }
        public void RemoveAt(int index)
        {
            WrappedList.RemoveAt(index);
            base.AddChangedStates(ChangedStates.Self);
        }
        public override void Update(bool acceptChanges)
        {
            if ((ChangedStates & ChangedStates.Self) != ChangedStates.None)
            {
                base.Update(acceptChanges);
                if (_collectionView != null)
                    _collectionView.Reset();
            }
        }

        public int Count
        {
            get
            {
                return WrappedList.Count;
            }
        }
        public K this[int index]
        {
            get
            {
                return base.Factory.Create(WrappedList[index], null, this);
            }
            set
            {
                WrappedList[index] = GetWrappedItem(value);
                base.AddChangedStates(ChangedStates.Self);
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return WrappedList.IsReadOnly;
            }
        }
        private IList<K> WrappedList
        {
            get
            {
                return _interfaceInvoker;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((K[])array, index);
        }

        int ICollection.Count
        {
            get
            {
                return Count;
            }
        }
        bool ICollection.IsSynchronized
        {
            get
            {
                var list = WrappedList as IList;
                return list == null ? false : list.IsSynchronized;
            }
        }
        Object ICollection.SyncRoot
        {
            get
            {
                return _syncRoot;
            }
        }

        ICollectionView ICollectionViewFactory.CreateView()
        {
            if (_collectionView == null)
                _collectionView = new ListChangeTrackingCollectionView(this);
            return _collectionView;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return WrappedList.GetEnumerator();
        }

        int IList.Add(Object value)
        {
            Add((K)value);
            return Count - 1;
        }
        void IList.Clear()
        {
            Clear();
        }
        bool IList.Contains(Object value)
        {
            return Contains((K)value);
        }
        int IList.IndexOf(Object value)
        {
            return IndexOf((K)value);
        }
        void IList.Insert(int index, Object value)
        {
            Insert(index, (K)value);
        }
        void IList.Remove(Object value)
        {
            Remove((K)value);
        }
        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        bool IList.IsFixedSize
        {
            get
            {
                var list = WrappedList as IList;
                return list == null ? false : list.IsFixedSize;
            }
        }
        bool IList.IsReadOnly
        {
            get
            {
                return IsReadOnly;
            }
        }
        Object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (K)value;
            }
        }
    }
}
