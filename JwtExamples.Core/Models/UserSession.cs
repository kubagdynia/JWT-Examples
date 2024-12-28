using System.Text.Json.Serialization;

namespace JwtExamples.Core.Models;

public record UserSession
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; init; } = string.Empty;
    
    [JsonPropertyName("lastName")]
    public string LastName { get; init; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;
    
    [JsonPropertyName("status")]
    public UserStatus Status { get; init; }
};

public enum UserStatus
{
    [JsonPropertyName("ACTIVE_USER")]
    Active,
    
    [JsonPropertyName("INACTIVE_USER")]
    Inactive
}