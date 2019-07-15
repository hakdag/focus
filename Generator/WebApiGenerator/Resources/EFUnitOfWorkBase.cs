using #projectname#.Contracts.DataAccess;
using Focus.Common;
using System;
using System.Data.Entity;

namespace #projectname#.DataAccess.UnitOfWork
{
    public abstract class EFUnitOfWorkBase : IUnitOfWork
    {
        public DbContext _dbContext;

        #region IUnitOfWork Members
        public abstract IRepository<T> GetRepository<T>() where T : BaseModel;

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch(Exception exc)
            {
                throw;
            }
        }
        #endregion

        public object DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        #region IDisposable Members
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
