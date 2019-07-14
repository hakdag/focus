using #projectname#.Contracts.DataAccess;
using Focus.Common;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace #projectname#.DataAccess.Repositories
{
    public abstract class EFRepositoryBase<T> : IRepository<T> where T : BaseModel
    {
        protected DbContext _dbContext;
        protected DbSet<T> _dbSet;

        #region IRepository Members
        public virtual IQueryable<T> GetAll() => _dbSet.Where(entity => !entity.DeletedDate.HasValue);

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).Where(entity => !entity.DeletedDate.HasValue);

        public virtual T GetById(int id) => _dbSet.Where(entity => !entity.DeletedDate.HasValue).FirstOrDefault(entity => entity.Id == id);

        public virtual T Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).Where(entity => !entity.DeletedDate.HasValue).SingleOrDefault();

        public virtual void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;

            _dbSet.Attach(entity);
            _dbContext.Set<T>().AddOrUpdate(entity);
        }

        public virtual void Update(T entity)
        {
            var oldEntity = _dbContext.Set<T>().Find(entity.Id);
            entity.CreatedDate = oldEntity.CreatedDate;
            _dbContext.Entry<T>(oldEntity).State = EntityState.Modified;
            _dbContext.Set<T>().AddOrUpdate(entity);
        }

        public virtual void Delete(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            this.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;

            entity.DeletedDate = DateTime.Now;
            this.Update(entity);
        }
        #endregion
    }
}
