using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Crosscutting.Constantes;
using Crosscutting.Enums;
using Crosscutting.Interfaces.Log;
using Domain.Autenticacao.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Autenticacao.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private const string JwtSecretkey = "JWT:SecretKey";

    private static readonly List<(string, string)> RefreshTokens = new();

    public void SaveRefreshToken(string user, string refreshToken)
    {
        RefreshTokens.Add((user, refreshToken));
    }

    public List<string> ObterRefreshToken(string user)
    {
        return RefreshTokens.Where(x => x.Item1 == user).Select(y => y.Item2).ToList();
    }

    public void DeleteRefreshToken(string user, string refreshToken)
    {
        var item = RefreshTokens.Find(x => x.Item1 == user && x.Item2 == refreshToken);
        RefreshTokens.Remove(item);
    }

    public string GerarRefreshToken()
    {
        var numeroRandomico = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(numeroRandomico);

        return Convert.ToBase64String(numeroRandomico);
    }

    public ClaimsPrincipal ObterClaimsPrincipalDoTokenExpirado(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JwtSecretkey]!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException(MensagensErro.TokenInvalido);

        return principal;
    }

    public string GerarTokenParaUsuario(string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
