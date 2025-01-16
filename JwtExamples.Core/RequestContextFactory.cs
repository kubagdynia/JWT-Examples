using JwtExamples.Core.Configuration;
using JwtExamples.Core.Providers;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core;

internal class RequestContextFactory(ICoreDataProviderFactory coreDataProviderFactory, IOptionsSnapshot<SsoSettings> settings) : IRequestContextFactory
{
    public async Task<IRequestContext> Create(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var coreDataProvider = coreDataProviderFactory.Create(httpContext);
            var value = await coreDataProvider.GetDataAsync();
            
            var requestContext = new RequestContext(httpContext, settings);
            if (requestContext is IInternalRequestContext internalRequestContext)
            {
                // internal request context initialization
                internalRequestContext.Initialize2(requestContext);
            }
            await requestContext.InitializeAsync(value);
            return requestContext;
        }

        // Create default request context for unauthenticated users
        return new RequestContext(httpContext, null);
    }
}