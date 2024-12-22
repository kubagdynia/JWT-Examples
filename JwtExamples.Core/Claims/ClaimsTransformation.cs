using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace JwtExamples.Core.Claims;

public class ClaimsTransformation(IHttpContextAccessor httpContextAccessor) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        // TODO: Implement the transformation logic
        
        // RequestContext is not available here, so we cannot add new Claims based on it.
        // We can only add new Claims based on the existing ClaimsPrincipal.

        // if (principal.Identity?.IsAuthenticated == true)
        // {
        //     var httpContext = httpContextAccessor.HttpContext;
        //     
        //     if (httpContext?.Items["RequestContext"] is IRequestContext requestContext)
        //     {
        //         // Dodawanie nowych Claims na podstawie RequestContext
        //         if (principal.Identity is ClaimsIdentity claimsIdentity)
        //         {
        //             string ccc = requestContext.MyName;
        //             //claimsIdentity.AddClaim(new Claim("UserId", requestContext.UserId));
        //             claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "SuperUser222"));
        //         }
        //     }
        // }
        
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