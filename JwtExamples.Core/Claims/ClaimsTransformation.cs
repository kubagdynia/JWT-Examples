using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace JwtExamples.Core.Claims;

public class ClaimsTransformation(IHttpContextAccessor httpContextAccessor) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;

        var ok = identity.IsAuthenticated;

        List<Claim> claims =
        [
            new(ClaimTypes.Role, "SuperUser")
        ];
        
        identity.AddClaims(claims);
        
        return Task.FromResult(principal);
    }
}