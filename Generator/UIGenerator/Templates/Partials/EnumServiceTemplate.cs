using System;
using System.Collections.Generic;

namespace UIGenerator.Templates
{
    public partial class EnumServiceTemplate
    {
        private Type type;
        private Dictionary<string, int> members = new Dictionary<string, int>();

        public EnumServiceTemplate(Type type)
        {
            this.type = type;

            Array enumValues = Enum.GetValues(type);
            for (int i = 0; i < enumValues.Length; i++)
            {
                int intValue = (int)enumValues.GetValue(i);
                members.Add(enumValues.GetValue(i).ToString(), intValue);
            }
        }
    }
}
