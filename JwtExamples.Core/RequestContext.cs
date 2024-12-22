using System.Security.Claims;

namespace JwtExamples.Core;

internal sealed class RequestContext(HttpContext httpContext) : IRequestContext
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
        
        // Do nothing
        _myName = "This is my name";
    }
}