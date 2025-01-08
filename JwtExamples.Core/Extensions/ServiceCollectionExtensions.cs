using System.Text;
using JwtExamples.Core.Claims;
using JwtExamples.Core.Configuration;
using JwtExamples.Core.Middlewares;
using JwtExamples.Core.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtExamples.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder RegisterCore(this IHostApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services.AddHttpContextAccessor();
        
        var ssoSettingsSection = builder.Configuration.GetSection(SsoSettings.SectionName);
        services.Configure<SsoSettings>(ssoSettingsSection);

        SsoSettings settings = GetSsoSettings(builder);
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = settings.JwtSettings.Authority;
                options.Audience = settings.JwtSettings.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.Secret)),
                    ValidateIssuer = settings.JwtSettings.ValidateIssuer,
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateAudience = settings.JwtSettings.ValidateAudience,
                    ValidAudience = settings.JwtSettings.Audience
                };
            });
        
        // services.AddScoped(sp =>
        // {
        //     var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
        //     return new RequestContextFactory().Create(httpContextAccessor);
        // });
        services.AddScoped<IRequestContextFactory, RequestContextFactory>();
        services.AddScoped<IRequestContext>(sp =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            return httpContext?.Items[nameof(RequestContext)] as IRequestContext ?? throw new InvalidOperationException("RequestContext is not available.");
        });
        
        services.AddSingleton<CoreDataProviderFactory>();
        services.AddScoped<ICoreDataProvider>(sp => sp.GetRequiredService<CoreDataProviderFactory>().Create());
        
        services.AddScoped<ITestClass, TestClass>();
        
        services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
        
        return builder;
    }
    
    public static IApplicationBuilder UseSso(this IApplicationBuilder app)
    {
        //app.UseAuthentication();
        //app.UseMiddleware<SsoMiddleware>();
        app.UseMiddleware<RequestContextMiddleware>();
        //app.UseAuthorization();

        return app;
    }
    
    private static SsoSettings GetSsoSettings(IHostApplicationBuilder builder)
    {
        var ssoSettingsSection = builder.Configuration.GetSection(SsoSettings.SectionName);
        builder.Services.Configure<SsoSettings>(ssoSettingsSection);

        SsoSettings settings =
            ssoSettingsSection.Get<SsoSettings>() ?? throw new InvalidOperationException(
                $"Missing configuration section '{SsoSettings.SectionName}'.");
        
        return settings;
    }
    
}