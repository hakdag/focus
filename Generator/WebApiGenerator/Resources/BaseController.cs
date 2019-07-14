using Focus.Common;
using Focus.Common.Pagination;
using #projectname#.Filters;
using #projectname#.Helpers;
using #projectname#.Contracts.Business;
using System;
using System.Linq;
using System.Web.Http;


namespace #projectname#.Controllers
{
    public abstract class BaseController<TBusiness, TModel> : ApiController where TBusiness : IBaseBusiness<TModel>
    {
        protected TBusiness business;


        public BaseController(TBusiness business)
        {
            this.business = business;
        }

        public virtual PageResult<TModel> Get(string searchProperty, string sortPropertyDefault, string filterQuery, int startIndex, int rowsOnPage, string sortBy, string sortOrder)
        {
            var items = business.GetAll();
            // apply filter
            if (!String.IsNullOrEmpty(filterQuery))
                items = items.Filter(searchProperty, filterQuery);
            // sort
            if (String.IsNullOrEmpty(sortBy))
                sortBy = sortPropertyDefault;
            bool desc = String.IsNullOrEmpty(sortOrder) ? false : sortOrder == "desc" ? true : sortOrder == "asc" ? false : true;
            items = items.OrderBy(sortBy, desc);

            PageResult<TModel> pr = new PageResult<TModel>();
            pr.AllItems = items.Count();
            pr.CurrentPage = (int)Math.Floor((double)startIndex / rowsOnPage) + 1;
            pr.PageCount = (int)Math.Floor((double)pr.AllItems / rowsOnPage) + 1;
            pr.Items = items.Skip(startIndex).Take(rowsOnPage).ToArray();

            return pr;
        }

        public virtual IHttpActionResult Get(int id)
        {
            var model = business.Get(id);
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [ValidateModel]
        [LogActionFilter]
        [UpdateFK]
        public virtual ResponseResult Post([FromBody]TModel model) => business.Create(model);

        [ValidateModel]
        [LogActionFilter]
        [UpdateFK]
        public virtual ResponseResult Put([FromBody]TModel model, int id) => business.Update(model);

        [LogActionFilter]
        public virtual ResponseResult Delete(int id) => business.Delete(id);
    }
}
