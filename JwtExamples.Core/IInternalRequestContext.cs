namespace JwtExamples.Core;

internal interface IInternalRequestContext
{
    //void Initialize2(IRequestContext source);
    Task InitializeAsync(HttpContext httpContext, string value);
}