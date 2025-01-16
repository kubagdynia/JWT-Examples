using System.Security.Claims;

namespace JwtExamples.Core.Middlewares;

internal class RequestContextMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Items.ContainsKey(nameof(RequestContext)))
        {
            using var scope = serviceScopeFactory.CreateScope();
            var requestContextFactory = scope.ServiceProvider.GetRequiredService<IRequestContextFactory>();
            
            var requestContext = await requestContextFactory.Create(context);
            context.Items[nameof(RequestContext)] = requestContext;

            AddClaims(context);
        }
        
        await next(context);
    }

    private static void AddClaims(HttpContext context)
    {
        if (context.User.Identity is ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "SuperUser334"));
        }
    }
}