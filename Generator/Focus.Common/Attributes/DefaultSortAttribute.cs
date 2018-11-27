using System;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class DefaultSortAttribute : Attribute
    {
        public DefaultSortAttribute() { }
    }
}
