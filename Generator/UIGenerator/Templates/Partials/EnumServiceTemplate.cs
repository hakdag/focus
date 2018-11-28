using System;
using System.Collections.Generic;
using GeneratorBase;

namespace UIGenerator.Templates
{
    public partial class EnumServiceTemplate
    {
        private GeneratorType type;
        private Dictionary<string, int> members = new Dictionary<string, int>();

        public EnumServiceTemplate(GeneratorType type)
        {
            this.type = type;

            Array enumValues = Enum.GetValues(type.Type);
            for (int i = 0; i < enumValues.Length; i++)
            {
                int intValue = (int)enumValues.GetValue(i);
                members.Add(enumValues.GetValue(i).ToString(), intValue);
            }
        }
    }
}
