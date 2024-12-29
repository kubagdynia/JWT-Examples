using JwtExamples.Core.Configuration;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core.Providers;

internal class CoreDataProviderFactory(IServiceProvider serviceProvider, IOptionsSnapshot<SsoSettings> settings)
{
    public ICoreDataProvider Create()
    {
        return settings.Value.MockSession.Enabled
            ? new CoreMockDataProvider(settings.Value.MockSession.FilePath)
            : serviceProvider.GetRequiredService<ICoreDataProvider>();
    }
}