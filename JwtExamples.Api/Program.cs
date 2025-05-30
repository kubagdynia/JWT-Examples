using JwtExamples.Core;
using JwtExamples.Core.Extensions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseSso();
app.UseAuthorization();

app.MapGet("/hello", [Authorize(Roles = "Admin")] async (IRequestContext requestContext) =>
{
    if (requestContext.IsInRole("Admin"))
    {
        return await Task.FromResult(
            Results.Ok($"Hello Admin, {requestContext.UserName}, {requestContext.UserEmail}"));
    }
    
    return await Task.FromResult(
        Results.Ok($"Hello User, {requestContext.UserName}, {requestContext.UserEmail}"));
});

app.MapControllers();

app.Run();

public partial class Program;
