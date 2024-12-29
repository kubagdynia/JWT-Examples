namespace JwtExamples.Core.Providers;

internal class CoreRedisDataProvider : ICoreDataProvider
{
    public Task<string> GetDataAsync()
    {
        // TODO: Implement Redis data retrieval
        string data = "Data from Redis";
        
        return Task.FromResult(data);
    }
}