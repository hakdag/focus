﻿using Focus.Common;
using Focus.Common.Pagination;
using #projectname#.Filters;
using #projectname#.Helpers;
using System;
using System.Linq;
using System.Web.Http;


namespace #projectname#.Controllers
{
    public abstract class BaseController<TModel> : ApiController where TModel : BaseModel
    {
        public virtual PageResult<TModel> Get(string searchProperty, string sortPropertyDefault, string filterQuery, int startIndex, int rowsOnPage, string sortBy, string sortOrder)
        {
            var items = Generate();
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
            var items = Generate();
            var model = items.FirstOrDefault(i => i.Id == id);
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [ValidateModel]
        [LogActionFilter]
        [UpdateFK]
        public virtual ResponseResult Post([FromBody]TModel model) => new ResponseResult { Success = true };

        [ValidateModel]
        [LogActionFilter]
        [UpdateFK]
        public virtual ResponseResult Put([FromBody]TModel model, int id) => new ResponseResult { Success = true };

        [LogActionFilter]
        public virtual ResponseResult Delete(int id) => new ResponseResult { Success = true };

        public abstract IQueryable<TModel> Generate();
    }
}