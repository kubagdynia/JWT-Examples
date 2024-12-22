namespace JwtExamples.Core;

internal class RequestContextFactory(IServiceProvider serviceProvider) : IRequestContextFactory
{
    public IRequestContext Create(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var requestContext = new RequestContext(httpContext);
            requestContext.Initialize();
            return requestContext;
        }

        // Create default request context for unauthenticated users
        return new RequestContext(httpContext);
    }
}