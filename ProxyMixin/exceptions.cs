using System;

namespace ProxyMixin
{
    public sealed class TypeExistException : Exception
    {
        private readonly String _typeName;

        public TypeExistException(String typeName)
            : base(typeName + " already exist")
        {
            _typeName = typeName;
        }

        public String TypeName
        {
            get
            {
                return _typeName;
            }
        }
    }
}
