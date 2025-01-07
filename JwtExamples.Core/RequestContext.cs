using System.Security.Claims;
using System.Text.Json;
using JwtExamples.Core.Configuration;
using JwtExamples.Core.Exceptions;
using JwtExamples.Core.Models;
using JwtExamples.Core.Serialization;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core;

internal sealed class RequestContext(HttpContext httpContext, IOptionsSnapshot<SsoSettings>? settings) : IRequestContext, IInternalRequestContext
{
    private string _myName;
    public string MyName => _myName;

    public string UserName
        => httpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    
    public string UserEmail
        =>  httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    
    public bool IsAuthenticated
        => httpContext.User.Identity?.IsAuthenticated ?? false;
    
    public bool IsInRole(string role)
        => httpContext.User.IsInRole(role);

    public IEnumerable<Claim> Claims
        => httpContext.User.Claims;

    public IEnumerable<string> RoleList { get; }
    public IEnumerable<string> GrantList { get; }
    public RequestAuthorization? Authorization { get; }

    /// <summary>
    /// Initializes the request context.
    /// </summary>
    public void Initialize()
    {
        // Dodaj dodatkowe roszczenia
        if (httpContext.User.Identity is ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "SuperUser335"));
        }
        
        if (settings?.Value.MockSession.Enabled == true)
        {
            if (File.Exists(settings?.Value.MockSession.FilePath))
            {
                var json = File.ReadAllText(settings?.Value.MockSession.FilePath!);
                try
                {
                    var userSession = JsonSerializer.Deserialize<UserSession>(json, JsonSettings.JsonEnumOptions);

                }
                catch (JsonException ex)
                {
                    throw new SsoJsonException("Error deserializing JSON input.", ex);
                }
                
            }
        }
        
        // Do nothing
        _myName = "This is my name";
    }

    public void Initialize2(IRequestContext source)
    {
        _myName = source.MyName;
    }
}