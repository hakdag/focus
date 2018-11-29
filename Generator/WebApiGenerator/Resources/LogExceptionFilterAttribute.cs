using System.Web.Http.Filters;

namespace #projectname#.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
        }
    }
}