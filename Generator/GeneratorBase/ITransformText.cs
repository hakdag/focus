using System.Threading.Tasks;

namespace GeneratorBase
{
    public interface ITransformText
    {
        Task<string> TransformText();
    }
}
