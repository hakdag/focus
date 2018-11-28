using Focus.Common.Attributes;
using GeneratorBase.Extensions;
using System;

namespace GeneratorBase
{
    public class GeneratorType
    {
        public Type Type { get; }
        public int TypeId { get; }
        public int ParentTypeId { get; }
        public string Title { get; }
        public string Name => Type.Name;
        public Type BaseType => Type.BaseType;

        public GeneratorType(Type type, int id, int parentTypeId)
        {
            Type = type;
            TypeId = id;
            ParentTypeId = parentTypeId;
            Title = Type.GetAttributeValue((TitleAttribute ta) => ta.Title);
        }
    }
}
