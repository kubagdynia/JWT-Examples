namespace JwtExamples.Core.Configuration;

internal record SsoSettings
{
    public const string SectionName = "SsoSettings";
    
    public JwtSettings JwtSettings { get; init; } = null!;
}