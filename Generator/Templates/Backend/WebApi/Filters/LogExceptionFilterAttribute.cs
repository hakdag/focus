using BetonCRM.Logs.Contracts;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BetonCRM.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IErrorBusiness errorBusiness;

        public LogExceptionFilterAttribute(IErrorBusiness errorBusiness)
        {
            this.errorBusiness = errorBusiness;
        }

        private Exception getInnerMostException(Exception exc)
        {
            if (exc.InnerException == null)
                return exc;

            return getInnerMostException(exc.InnerException);
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            var actionName = context.ActionContext.ActionDescriptor.ActionName;
            var controllerName = context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string json = "";
            if (context.ActionContext.ActionArguments.Count > 0)
            {
                var model = context.ActionContext.ActionArguments.ElementAt(0).Value;
                json = JsonConvert.SerializeObject(model);
            }

            var exception = getInnerMostException(context.Exception);
            errorBusiness.Log(new Logs.Error
            {
                CreatedDate = DateTime.Now,
                ExceptionData = JsonConvert.SerializeObject(context.Exception.Data),
                ExceptionMessage = context.Exception.Message,
                InnerExceptionMessage = exception.Message,
                Level = String.Empty,
                Message = $"{controllerName} {actionName} hata oluştu.",
                Action = actionName,
                ModelJson = json,
                StackTrace = context.Exception.StackTrace,
                User = errorBusiness.UserName
            });

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}