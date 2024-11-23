namespace Crosscutting.Dto.Autenticacao;

public class RefreshTokenRequestDto
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
    public string GrantType { get; set; }
}