using JwtExamples.Core;
using JwtExamples.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtExamples.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController(IRequestContext requestContext, ITestClass testClass) : ControllerBase
{
    [HttpGet("hello-world")]
    [Authorize(Roles = "SuperUser335")]
    [HasGrants("A111", "A333")]
    public IActionResult HelloWorld()
    {
        var testValue = requestContext.MyName;
        
        var testValue2 = testClass.GetEmail;
        
        if (requestContext.IsInRole("SuperUser334"))
        {
            string res1 = $"Hello SuperUser334, {requestContext.UserName}, {requestContext.UserEmail}";
        }
        
        if (requestContext.IsInRole("Admin"))
        {
            string email = testClass.GetEmail;
            
            return Ok($"Hello Admin, {requestContext.UserName}, {requestContext.UserEmail}");
        }
        
        return Ok($"Hello User, {requestContext.UserName}, {requestContext.UserEmail}");
    }
    
    [HttpGet("hello-world2")]
    public IActionResult HelloWorld2()
    {
        if (requestContext.IsAuthenticated)
        {
            return Ok($"Hello Authenticated, {requestContext.UserName}, {requestContext.UserEmail}");
        }
        else
        {
            return Ok($"Hello Not Authenticated, {requestContext.UserName}, {requestContext.UserEmail}");
        }
        
        if (requestContext.IsInRole("SuperUser334"))
        {
            string res1 = $"Hello SuperUser334, {requestContext.UserName}, {requestContext.UserEmail}";
        }
        
        if (requestContext.IsInRole("Admin"))
        {
            string email = testClass.GetEmail;
            
            return Ok($"Hello Admin, {requestContext.UserName}, {requestContext.UserEmail}");
        }
        
        return Ok($"Hello User, {requestContext.UserName}, {requestContext.UserEmail}");
    }

    [HttpGet("hello-world3")]
    [Authorize]
    public IActionResult HelloWorld3()
    {
        if (requestContext.IsInRole("SuperUser334"))
        {
            string res1 = $"Hello SuperUser334, {requestContext.UserName}, {requestContext.UserEmail}";
        }
        
        if (requestContext.IsInRole("Admin"))
        {
            string email = testClass.GetEmail;
            
            return Ok($"Hello Admin, {requestContext.UserName}, {requestContext.UserEmail}");
        }
        
        return Ok($"Hello User, {requestContext.UserName}, {requestContext.UserEmail}");
    }
}