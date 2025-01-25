using JwtExamples.Core.Configuration;
using JwtExamples.Core.Providers;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core;

internal class RequestContextFactory(ICoreDataProviderFactory coreDataProviderFactory, IOptionsSnapshot<SsoSettings> settings) : IRequestContextFactory
{
    public async Task<IRequestContext> Create(HttpContext httpContext)
    {
        await Task.CompletedTask;

        // Create default request context for unauthenticated users
        return new RequestContext(null);
    }
}