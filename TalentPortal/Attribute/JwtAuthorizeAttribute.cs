using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TalentPortal.Controllers;
using TalentPortal.Helpers;

public class JwtAuthorizeAttribute : ActionFilterAttribute
{
    public JwtAuthorizeAttribute()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Get JWT token from the request headers or query string
        string token = context.HttpContext.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(token))
        {
            token = context.HttpContext.Request.Query["token"];
        }

        // Validate JWT token and extract user ID
        int? userId = new JwtUtils().ValidateJwtToken(token.Replace("Bearer ",string.Empty));   

        // Set UserId property of the controller if a valid user ID is obtained
        if (userId.HasValue)
        {
            if (context.Controller is BaseController baseController)
            {
                baseController.UserId = userId.Value;
            }
        }
        else
        {
            // Unauthorized: Return 401 status code
            context.Result = new UnauthorizedResult();
        }
    }
}
