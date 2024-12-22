namespace JwtExamples.Core.Configuration;

internal record JwtSettings
{
    public const string SectionName = "JwtSettings";
    
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Authority { get; init; } = null!;
}