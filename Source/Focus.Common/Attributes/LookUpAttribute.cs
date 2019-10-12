using System;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class LookUpAttribute : Attribute
    {
        public LookUpAttribute() { }
    }
}
