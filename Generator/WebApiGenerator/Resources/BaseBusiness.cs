using #projectname#.Common;
using #projectname#.Contracts.Business;
using #projectname#.Contracts.DataAccess;
using Focus.Common;
using System.Linq;

namespace #projectname#.Business
{
    public class BaseBusiness<T> : IBaseBusiness<T> where T : BaseModel
    {
        protected IBaseData<T> _data;

        public BaseBusiness(IBaseData<T> data) => this._data = data;

        public virtual ResponseResult Create(T model)
        {
            _data.Create(model);

            return new ResponseResult { Success = true, Messages = new[] { "Saved." } };
        }

        public virtual ResponseResult Update(T model)
        {
            _data.Update(model);

            return new ResponseResult { Success = true, Messages = new[] { "Updated." } };
        }

        public virtual ResponseResult Delete(int id)
        {
            _data.Delete(id);

            return new ResponseResult { Success = true, Messages = new[] { "Deleted." } };
        }

        public virtual T Get(int id) => _data.Get(id);

        public virtual IQueryable<T> GetAll() => _data.GetAll();
    }
}
