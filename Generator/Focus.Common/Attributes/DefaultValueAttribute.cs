using System;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class DefaultValueAttribute : Attribute
    {
        private int val;
        public DefaultValueAttribute(int val)
        {
            this.val = val;
        }

        public int Val
        {
            get
            {
                return val;
            }

            set
            {
                val = value;
            }
        }
    }
}
