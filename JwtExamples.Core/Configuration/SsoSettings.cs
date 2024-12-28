namespace JwtExamples.Core.Configuration;

internal record SsoSettings
{
    public const string SectionName = "SsoSettings";
    
    public JwtSettings JwtSettings { get; init; } = null!;
    
    public MockSession MockSession { get; init; } = null!;
}

internal record MockSession
{
    public bool Enabled { get; init; }
    public string FilePath { get; init; } = string.Empty;
}