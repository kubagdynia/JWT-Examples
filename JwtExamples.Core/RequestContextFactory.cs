using JwtExamples.Core.Configuration;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core;

internal class RequestContextFactory(IServiceProvider serviceProvider, IOptionsSnapshot<SsoSettings> settings) : IRequestContextFactory
{
    public IRequestContext Create(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var requestContext = new RequestContext(httpContext, settings);
            requestContext.Initialize();
            return requestContext;
        }

        // Create default request context for unauthenticated users
        return new RequestContext(httpContext, null);
    }
}