using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtExamples.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class HasGrantsAttribute(params string[] grants) : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// The grants that the user must have to access the resource.
    /// </summary>
    public string[] Grants { get; set; } = grants;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var requestContext2 = context.HttpContext.RequestServices.GetRequiredService<IRequestContext>();
        
        if (Grants.Length == 0)
        {
            // No grants specified, allow access
            return;
        }
        
        var user = context.HttpContext.User;
        
        // If the user is not authenticated, return Unauthorized
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Check if there is a context element named RequestContext
        // if (!context.HttpContext.Items.TryGetValue(nameof(RequestContext), out var httpContextItem))
        // {
        //     context.Result = new UnauthorizedResult();
        //     return;
        // }

        // if (httpContextItem is not IRequestContext requestContext)
        // {
        //     context.Result = new UnauthorizedResult();
        //     return;
        // }

        // Sample user grants
        List<string> userGrants =
        [
            "A111",
            "A222",
            "A333"
        ];
        
        // Check if all required grants are present in userGrants
        if (!Grants.All(grant => userGrants.Contains(grant)))
        {
            context.Result = new ForbidResult();
        }
    }
}