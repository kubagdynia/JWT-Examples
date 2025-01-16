using System.Security.Claims;
using System.Text.Json;
using JwtExamples.Core.Configuration;
using JwtExamples.Core.Exceptions;
using JwtExamples.Core.Models;
using JwtExamples.Core.Serialization;
using Microsoft.Extensions.Options;

namespace JwtExamples.Core;

internal sealed class RequestContext(IOptionsSnapshot<SsoSettings>? settings) : IRequestContext, IInternalRequestContext
{
    private string _myName;
    public string MyName => _myName;

    public string UserName => "UserName";
    //     => httpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    //
    public string UserEmail => "UserEmail";
    //     =>  httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    //
    public bool IsAuthenticated => true;
    //     => httpContext.User.Identity?.IsAuthenticated ?? false;
    //
    public bool IsInRole(string role) => true;
    //     => httpContext.User.IsInRole(role);
    //
    public IEnumerable<Claim> Claims => new List<Claim>();
    //     => httpContext.User.Claims;

    public IEnumerable<string> RoleList { get; }
    public IEnumerable<string> GrantList { get; }
    public RequestAuthorization? Authorization { get; }

    /// <summary>
    /// Initializes the request context.
    /// </summary>
    public async Task InitializeAsync(HttpContext httpContext, string value)
    {
        _myName = value;
        
        // Dodaj dodatkowe roszczenia
        if (httpContext.User.Identity is ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "SuperUser335"));
        }
        
        if (settings?.Value.MockSession.Enabled == true)
        {
            if (File.Exists(settings?.Value.MockSession.FilePath))
            {
                var json = await File.ReadAllTextAsync(settings?.Value.MockSession.FilePath!);
                try
                {
                    var userSession = JsonSerializer.Deserialize<UserSession>(json, JsonSettings.JsonEnumOptions);

                }
                catch (JsonException ex)
                {
                    throw new SsoJsonException("Error deserializing JSON input.", ex);
                }
            }

            await Task.CompletedTask;
            return;
        }
        
        // Do nothing
        _myName = "This is my name";
    }

    public void Initialize2(IRequestContext source)
    {
        _myName = source.MyName;
    }
}