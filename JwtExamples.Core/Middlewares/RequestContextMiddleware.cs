using System.Security.Claims;

namespace JwtExamples.Core.Middlewares;

internal class RequestContextMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Items.ContainsKey(nameof(RequestContext)))
        {
            using var scope = serviceProvider.CreateScope();
            var requestContextFactory = scope.ServiceProvider.GetRequiredService<IRequestContextFactory>();
            
            var requestContext = requestContextFactory.Create(context);
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