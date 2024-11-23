using Crosscutting.Dto.Autenticacao;

namespace Domain.Autenticacao.Interfaces;

public interface ILoginUsuarioService
{
    Task<LoginRespostaDto> LoginAsync(LoginDto loginDto);
    Task<LoginRespostaDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
}