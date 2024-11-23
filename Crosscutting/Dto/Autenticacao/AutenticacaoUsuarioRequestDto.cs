namespace Crosscutting.Dto.Api.ApiPlaengeAutenticacao;

public class AutenticacaoUsuarioRequestDto
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public string GrantType { get; set; }
    public int Type { get; set; }
}