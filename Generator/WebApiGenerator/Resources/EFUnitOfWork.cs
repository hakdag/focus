using #projectname#.Contracts.DataAccess;
using #projectname#.DataAccess.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace #projectname#.DataAccess.UnitOfWork
{
    public class EFUnitOfWork : EFUnitOfWorkBase
    {
        public EFUnitOfWork(#projectname#Context dbContext)
        {
            Database.SetInitializer <#projectname#Context>(null);
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext can not be null.");

            _dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //_dbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        public override IRepository<T> GetRepository<T>()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var repo = assembly.GetTypes().FirstOrDefault(t => t.BaseType.Equals(typeof(EFRepository<T>)));
            if (repo != null)
                return (IRepository<T>)Activator.CreateInstance(repo, _dbContext);

            return new EFRepository<T>((#projectname#Context)_dbContext);
        }
    }
}
