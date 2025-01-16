namespace JwtExamples.Core.Providers;

internal interface ICoreDataProviderFactory
{
    ICoreDataProvider Create(HttpContext httpContext);
}