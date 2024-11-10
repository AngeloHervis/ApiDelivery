using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ApiDelivery.Configuration;

/// <summary>
/// 
/// </summary>
public static class AutenticacaoAutorizacaoConfiguracao
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddConfiguracaoAutenticacaoAutorizacao(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguracaoAutenticacao(configuration);
        services.AddConfiguracaoAutorizacao();
    }

    private static void AddConfiguracaoAutenticacao(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration["JWT:Audience"],
                ValidIssuer = configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                                   GetBytes(configuration["JWT:SecretKey"]!))
            };
        });
    }

    private static void AddConfiguracaoAutorizacao(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
    }
}