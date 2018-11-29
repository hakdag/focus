using Focus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace #projectname#.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                List<string> messages = new List<string>();
                foreach (var state in actionContext.ModelState)
                    messages.AddRange(state.Value.Errors.Select(x => !String.IsNullOrEmpty(x.ErrorMessage) ? x.ErrorMessage : x.Exception.Message));
                ResponseResult rr = new ResponseResult
                {
                    Success = false,
                    Messages = messages.ToArray()
                };
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, rr);
            }
        }
    }
}