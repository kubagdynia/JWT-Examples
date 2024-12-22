namespace JwtExamples.Core;

internal interface IRequestContextFactory
{
    IRequestContext Create(HttpContext httpContext);
}