namespace JwtExamples.Core.Providers;

internal interface ICoreDataProvider
{
    Task<string> GetDataAsync();
}