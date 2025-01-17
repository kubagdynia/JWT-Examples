using System.Security.Claims;
using JwtExamples.Core.Configuration;
using JwtExamples.Core.Providers;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core.Middlewares;

internal class RequestContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ICoreDataProviderFactory coreDataProviderFactory,
        IRequestContext requestContext,  IOptionsSnapshot<SsoSettings> settings)
    {
        const string middlewareKey = "RequestContextMiddlewareExecuted";
        
        if (!context.Items.ContainsKey(middlewareKey))
        {
            // mark middleware as executed to prevent multiple executions
            context.Items[middlewareKey] = true;

            var coreDataProvider = coreDataProviderFactory.Create(context);
            var value = await coreDataProvider.GetDataAsync();

            if (requestContext is IInternalRequestContext internalRequestContext)
            {
                // internal request context initialization
                await internalRequestContext.InitializeAsync(context, value);
            }
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