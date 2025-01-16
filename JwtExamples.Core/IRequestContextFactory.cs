namespace JwtExamples.Core;

internal interface IRequestContextFactory
{
    Task<IRequestContext> Create(HttpContext httpContext);
}