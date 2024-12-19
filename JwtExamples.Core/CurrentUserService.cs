using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace JwtExamples.Core;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(HttpContext context)
    {
        
    }
    
    
    public string UserId { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Role { get; }
    public IEnumerable<Claim> Claims { get; }
    public bool IsAuthenticated { get; }
    public bool IsInRole(string role)
    {
        throw new NotImplementedException();
    }
}