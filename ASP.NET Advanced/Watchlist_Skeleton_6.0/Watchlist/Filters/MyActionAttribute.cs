using Microsoft.AspNetCore.Mvc.Filters;
using Watchlist.Contracts;

namespace Watchlist.Filters
{
    public class MyActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var movieService = context.HttpContext.RequestServices.GetService<IMovieService>();


            base.OnActionExecuting(context);
        }
        //The async version
        //public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    return base.OnActionExecutionAsync(context, next);
        //}
    }
}
