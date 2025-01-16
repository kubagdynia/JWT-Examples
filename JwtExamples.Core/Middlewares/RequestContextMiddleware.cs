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
        //using var scope = serviceScopeFactory.CreateScope();
        //var coreDataProviderFactory = scope.ServiceProvider.GetRequiredService<ICoreDataProviderFactory>();
        var coreDataProvider = coreDataProviderFactory.Create(context);
        var value = await coreDataProvider.GetDataAsync();
        
        //var settings = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SsoSettings>>();
        
        //var requestContext = new RequestContext(context, settings);
        //var requestContext = scope.ServiceProvider.GetRequiredService<IRequestContext>();
        
        
        if (requestContext is IInternalRequestContext internalRequestContext)
        {
            // internal request context initialization
            await internalRequestContext.InitializeAsync(context, value);
        }
        
        //await requestContext.InitializeAsync(value);
        
        // if (!context.Items.ContainsKey(nameof(RequestContext)))
        // {
        //     using var scope = serviceScopeFactory.CreateScope();
        //     var requestContextFactory = scope.ServiceProvider.GetRequiredService<IRequestContextFactory>();
        //     
        //     var requestContext = await requestContextFactory.Create(context);
        //     context.Items[nameof(RequestContext)] = requestContext;
        //
        //     AddClaims(context);
        // }
        
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