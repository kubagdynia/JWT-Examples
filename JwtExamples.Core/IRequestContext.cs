using System.Security.Claims;

namespace JwtExamples.Core;

public interface IRequestContext
{
    string MyName { get; }
    
    string UserName { get; }
    string UserEmail { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
    IEnumerable<Claim> Claims { get; }
    IEnumerable<string> RoleList { get; }
    IEnumerable<string> GrantList { get; }
    RequestAuthorization? Authorization { get; }
}

public class RequestAuthorization
{
    public string Type { get; set; }
    public string Credentials { get; set; }
}