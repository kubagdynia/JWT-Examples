namespace JwtExamples.Core.Providers;

internal class CoreMockDataProvider(string filePath) : ICoreDataProvider
{
    public async Task<string> GetDataAsync()
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Mock data file not found.");
        }

        return await File.ReadAllTextAsync(filePath);
    }
}