namespace JwtExamples.Core.Configuration;

public record SsoSettings
{
    public const string SectionName = "SsoSettings";
    
    public JwtSettings JwtSettings { get; init; } = null!;
}