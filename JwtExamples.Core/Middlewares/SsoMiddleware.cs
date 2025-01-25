using System.Security.Claims;

namespace JwtExamples.Core.Middlewares;

internal class SsoMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            List<Claim> claims =
            [
                new(ClaimTypes.Role, "SuperUser2")
            ];
            
            identity?.AddClaims(claims);
        }
        
        await next(context);
    }
    
}