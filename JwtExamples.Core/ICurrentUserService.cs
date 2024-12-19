using System.Security.Claims;

namespace JwtExamples.Core;

public interface ICurrentUserService
{
    string UserId { get; }
    string UserName { get; }
    string Email { get; }
    string Role { get; }
    IEnumerable<Claim> Claims { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
}