namespace JwtExamples.Core;

public interface ITestClass
{
    string GetEmail { get; }
}

internal class TestClass(IRequestContext requestContext) : ITestClass
{
    public string GetEmail
        => requestContext.MyName;
}