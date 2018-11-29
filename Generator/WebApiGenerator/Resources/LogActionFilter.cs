using #projectname#.Common.Attributes;
using #projectname#.Logs.Business;
using #projectname#.Logs.Contracts;
using #projectname#.Logs.Data;
using #projectname#.Logs.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http.Filters;

namespace #projectname#.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogBusiness logBusiness;

        public LogActionFilter()
        {
            this.logBusiness = new LogBusiness(new LogData(new EFUnitOfWorkLogs(new Logs.#projectname#Entities())));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            /*
             Action: Delete
             JSON: 
             Tarih:
             Kullanıcı: kullanıcı adı
             ---------
             Action: Post
             JSON: 
             Tarih:
             Kullanıcı: kullanıcı adı
             ---------
             Action: Put
             JSON: 
             Tarih:
             Kullanıcı: kullanıcı adı
             */
            var modelType = actionExecutedContext.ActionContext.ActionArguments.ElementAt(0).Value.GetType();
            var modelName = GetAttributeValue(modelType, (TitleAttribute ta) => ta.Title);
            var controller = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var model = actionExecutedContext.ActionContext.ActionArguments.ElementAt(0).Value;
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            var actionEnglishName = "";
            switch (actionName)
            {
                case "Post":
                    actionEnglishName = "added";
                    break;
                case "Put":
                    actionEnglishName = "edited";
                    break;
                case "Delete":
                    actionEnglishName = "deleted";
                    break;
            }
            logBusiness.Log(new Logs.Log
            {
                CreatedDate = DateTime.Now,
                Level = String.Empty,
                Message = $"{(String.IsNullOrWhiteSpace(modelName) ? controller : modelName)} {actionEnglishName}.",
                Action = actionName,
                ModelJson = json,
                User = logBusiness.UserName
            });
        }

        public TValue GetAttributeValue<TAttribute, TValue>(
            Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}