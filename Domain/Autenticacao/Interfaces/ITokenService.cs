using System.Security.Claims;

namespace Domain.Autenticacao.Interfaces;

public interface ITokenService
{
    string GerarTokenParaUsuario(string email);
    string GerarRefreshToken();
    void SaveRefreshToken(string user, string refreshToken);
    List<string> ObterRefreshToken(string user);
    void DeleteRefreshToken(string user, string refreshToken);
    ClaimsPrincipal ObterClaimsPrincipalDoTokenExpirado(string token);  
}