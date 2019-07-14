using #projectname#.Contracts.DataAccess;
using Focus.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace #projectname#.DataAccess.Data
{
    public class BaseData<T> : IBaseData<T> where T : BaseModel
    {
        protected IUnitOfWork _uow;

        public BaseData(IUnitOfWork uow) => _uow = uow;

        public void Create(T model)
        {
            model.CreatedDate = DateTime.Now;

            _uow.GetRepository<T>().Add(model);
            _uow.SaveChanges();
        }

        public void Delete(int id)
        {
            _uow.GetRepository<T>().Delete(id);
            _uow.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> predicate) => _uow.GetRepository<T>().Get(predicate);

        public T Get(int id) => _uow.GetRepository<T>().GetById(id);

        public virtual IQueryable<T> GetAll() => _uow.GetRepository<T>().GetAll();

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) => _uow.GetRepository<T>().GetAll(predicate);

        public void Update(T model)
        {
            _uow.GetRepository<T>().Update(model);
            _uow.SaveChanges();
        }
    }
}
