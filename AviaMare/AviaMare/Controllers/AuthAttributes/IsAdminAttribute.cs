using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AviaMare.Services;

namespace AviaMare.Controllers.AuthAttributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var authService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<AuthService>();
            if (!authService.IsAdmin())
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuted(context);
        }
    }
}
