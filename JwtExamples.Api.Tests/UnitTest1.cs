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
            {"SsoSettings:JwtSettings:Audience", "https://localhost:5001"},
            {"SsoSettings:JwtSettings:Authority", "https://localhost:5001"},
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
        
        // Act
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFuIEtvd2Fsc2tpIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiamFuLmtvd2Fsc2tpQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MzY0Mzk4MjgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.zIMavCRyoY_TJhWPIy0MrvHsoTPRTMSggHpmrhfdqtI");
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
        
        // Act
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFuIEtvd2Fsc2tpIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiamFuLmtvd2Fsc2tpQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MzUzODEzNTQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.Rm7KcWvgV46EJ9dfA1M8YSpQ_h63yFI52nDhp7bephw");
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
            new Claim(ClaimTypes.Name, "Jan Kowalski"),
            new Claim(ClaimTypes.Email, "jan.kowalski@example.com"),
            new Claim(ClaimTypes.Role, "Admin")
        ];
        
        var identity = new ClaimsIdentity(claims, "Bearer1");
        
        var principal = new ClaimsPrincipal(identity);
        
        var encodedToken = CteateJwtToken(claims);
        
        Assert.Pass();
    }
    
    private string CteateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key-secret-key-secret-key-secret-key-secret-key-15321-15321-15321-15321"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddDays(7),
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