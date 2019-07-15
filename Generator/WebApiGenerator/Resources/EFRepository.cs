using Focus.Common;
using System;

namespace #projectname#.DataAccess.Repositories
{
    public class EFRepository<T> : EFRepositoryBase<T> where T : BaseModel
    {
        public EFRepository(#projectname#Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext can not be null.");
            _dbSet = dbContext.Set<T>();
        }
    }
}
