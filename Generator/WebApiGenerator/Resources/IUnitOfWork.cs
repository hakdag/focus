using #projectname#.Common;
using Focus.Common;
using System;

namespace #projectname#.Contracts.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseModel;
        int SaveChanges();
        object DbContext { get; }
    }
}
