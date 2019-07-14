using #projectname#.Common;
using Focus.Common;
using System.Linq;

namespace #projectname#.Contracts.Business
{
    public interface IBaseBusiness<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);
        ResponseResult Update(T model);
        ResponseResult Create(T model);
        ResponseResult Delete(int id);
    }
}
