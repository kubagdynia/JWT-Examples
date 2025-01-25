namespace JwtExamples.Core;

internal interface IInternalRequestContext
{
    Task InitializeAsync(HttpContext httpContext, string value);
}