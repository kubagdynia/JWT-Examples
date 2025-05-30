using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;

namespace JwtExamples.Api.Tests;

public class Tests
{
    private CustomWebApplicationFactory<Program> _application;
    
    [SetUp]
    public void Setup()
    {
    }

    [OneTimeSetUp]
    public void Init()
    {
        var testConfig = new Dictionary<string, string?>
        {
            {"TestSso:TestKey", "TestValue"},
            {"SsoSettings:JwtSettings:Secret", "secret-key-secret-key-secret-key-secret-key-secret-key-15321-15321-15321-15321"},
            {"SsoSettings:JwtSettings:Issuer", "https://localhost:5001"},
            {"SsoSettings:JwtSettings:Audience", "https://localhost:5002"},
            {"SsoSettings:JwtSettings:Authority", "https://localhost:5001"},
            {"SsoSettings:JwtSettings:ValidateAudience", "false"},
            {"SsoSettings:MockSession:Enabled", "true"},
            {"SsoSettings:MockSession:FilePath", @"D:\Dev\MyProjects\JWT-Examples\data\mock-session.json"}
        };
        
        _application = new CustomWebApplicationFactory<Program>(testConfig);
    }
    
    [OneTimeTearDown]
    public void Cleanup()
    {
        _application.Dispose();
    }

    [Test]
    public async Task Test1()
    {
        // Arrange
        using var client = _application.CreateClient();
        
        List<Claim> customClaims =
        [
            new(ClaimTypes.Name, "Jan Kowalski"),
            new(ClaimTypes.Email, "jan.kowalski@example.com"),
            new(ClaimTypes.Role, "Admin")
        ];
        
        var token = CteateJwtToken(
            securityKey: "secret-key-secret-key-secret-key-secret-key-secret-key-15321-15321-15321-15321",
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            sub: "User:11",
            expires: DateTime.UtcNow.AddDays(7),
            iat: DateTimeOffset.UtcNow,
            customClaims: customClaims);
        
        // Act
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/Test/hello-world");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();

        true.Should().BeTrue();
    }
    
    [Test]
    public async Task Test11()
    {
        // Arrange
        using var client = _application.CreateClient();
        
        // Act
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFuIEtvd2Fsc2tpIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiamFuLmtvd2Fsc2tpQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MzUzODEzNTQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.Rm7KcWvgV46EJ9dfA1M8YSpQ_h63yFI52nDhp7bephw");
        var response = await client.GetAsync("api/Test/hello-world2");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Pass();
    }
    
    [Test]
    public async Task Test13()
    {
        // Arrange
        using var client = _application.CreateClient();
        
        List<Claim> claims =
        [
            new(ClaimTypes.Name, "Jan Kowalski"),
            new(ClaimTypes.Email, "jan.kowalski@example.com"),
            new(ClaimTypes.Role, "Admin")
        ];
        
        var identity = new ClaimsIdentity(claims, "Bearer");
        
        var principal = new ClaimsPrincipal(identity);
        
        var encodedToken = CteateJwtToken(
            securityKey: "secret-key-secret-key-secret-key-secret-key-secret-key-15321-15321-15321-15321",
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            sub: null,
            expires: DateTime.UtcNow.AddHours(24),
            iat: DateTimeOffset.UtcNow,
            claims);
        
        
        // Act
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encodedToken);
        var response = await client.GetAsync("api/Test/hello-world3");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Pass();
    }
    
    [Test]
    public void Test2()
    {
        List<Claim> claims =
        [
            new(ClaimTypes.Name, "Jan Kowalski"),
            new(ClaimTypes.Email, "jan.kowalski@example.com"),
            new(ClaimTypes.Role, "Admin")
        ];
        
        var identity = new ClaimsIdentity(claims, "Bearer");
        
        var principal = new ClaimsPrincipal(identity);
        
        var encodedToken = CteateJwtToken(
            securityKey: "secret-key-secret-key-secret-key-secret-key-secret-key-15321-15321-15321-15321",
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            sub: null,
            expires: DateTime.UtcNow.AddDays(7),
            iat: DateTimeOffset.UtcNow,
            claims);
        
        Assert.Pass();
    }
    
    private string CteateJwtToken(string securityKey, string? issuer, string? audience, string? sub, DateTime? expires, DateTimeOffset? iat, List<Claim>? customClaims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = [];
        
        if (sub != null)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));
        }
        
        if (iat != null)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, iat.Value.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        }
        
        if (customClaims != null)
        {
            claims.AddRange(customClaims);
        }
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private ClaimsPrincipal DecodeJwtToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestKey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = "TestSso",
            ValidateAudience = true,
            ValidAudience = "TestSso",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        
        var principal = handler.ValidateToken(token, validationParameters, out var securityToken);
        
        return principal;
    }
}