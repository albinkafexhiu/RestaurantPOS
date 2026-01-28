using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestaurantPOS.Web.Infrastructure
{
    public class PosAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var waiterId = context.HttpContext.Session.GetString(SessionKeys.WaiterId);

            if (string.IsNullOrWhiteSpace(waiterId))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}