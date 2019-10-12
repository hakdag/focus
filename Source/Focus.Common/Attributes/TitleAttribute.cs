using System;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
    public class TitleAttribute : Attribute
    {
        private string title;

        public TitleAttribute(string title)
        {
            this.title = title;
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }
    }
}
