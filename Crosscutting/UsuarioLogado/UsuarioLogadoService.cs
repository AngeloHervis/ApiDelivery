using Crosscutting.Constantes;
using Crosscutting.Dto.Usuario;
using Crosscutting.Enums;
using Crosscutting.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Crosscutting.UsuarioLogado;

public class UsuarioLogadoService : IUsuarioLogadoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioLogadoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string ObterUsuarioId()
    {
        if (_httpContextAccessor.HttpContext == null) return ValoresPadrao.UsuarioDoSistema;

        var claim = _httpContextAccessor.HttpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == CamposClaims.IdUsuario);

        return claim?.Value ?? ValoresPadrao.UsuarioAnonimo;
    }

    public string ObterEmailUsuario()
    {
        var claim = _httpContextAccessor.HttpContext!
            .User
            .Claims
            .FirstOrDefault(x => x.Type == CamposClaims.Email);

        if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
            return null;

        return claim.Value;
    }

    public TipoUsuario ObterTipoUsuario()
    {
        var tipoUsuario = _httpContextAccessor.HttpContext!
            .User
            .Claims
            .First(x => x.Type == CamposClaims.TipoUsuario).Value;

        return (TipoUsuario)Enum.Parse(typeof(TipoUsuario), tipoUsuario);
    }
    

    public string ObterNomeUsuario()
    {
        if (_httpContextAccessor.HttpContext == null) return ValoresPadrao.UsuarioDoSistema;

        var claim = _httpContextAccessor.HttpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == CamposClaims.Nome);

        return claim?.Value ?? ValoresPadrao.UsuarioDoSistema;
    }

    public InformacoesDoUsuarioDto ObterInformacoesDoUsuario()
    {
        var claims = _httpContextAccessor.HttpContext!.User.Claims.ToList();

        var tipoUsuario = claims.First(x => x.Type == CamposClaims.TipoUsuario).Value;

        return new InformacoesDoUsuarioDto
        {
            TipoUsuario = (TipoUsuario)Enum.Parse(typeof(TipoUsuario), tipoUsuario),
            Id = claims.First(x => x.Type == CamposClaims.IdUsuario).Value,
            Nome = claims.First(x => x.Type == CamposClaims.Nome).Value,
            Email = claims.Find(x => x.Type == CamposClaims.Email)?.Value
        };
    }
}