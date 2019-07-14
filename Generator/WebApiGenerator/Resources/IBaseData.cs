using #projectname#.Common;
using Focus.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace #projectname#.Contracts.DataAccess
{
    public interface IBaseData<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);
        void Update(T model);
        void Create(T model);
        void Delete(int id);
    }
}
