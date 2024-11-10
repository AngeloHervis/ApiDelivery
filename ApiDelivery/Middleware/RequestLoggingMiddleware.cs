using System.Text;
using Crosscutting.Constantes;
using Crosscutting.Extensions;
using Infra.Log.Builders;

namespace ApiDelivery.Middleware;

/// <summary>
/// Middleware para logar as requisições recebidas pela API.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly List<string> _pathstoIgnore = ["swagger", "autenticacao"];
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    /// <summary></summary>
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Método que será chamado para logar as requisições.
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        await GravarLog(context);

        await _next(context);
    }

    private async Task GravarLog(HttpContext context)
    {   
        try
        {
            if (context.Request.ContentLength > ValoresPadrao.TamanhoMaximoLog)
            {
                _logger.LogWarning("Requisição [{context.Request.Path}] excedeu o tamanho máximo permitido para log " +
                                   "[{context.Request.ContentLength}]", context.Request.Path, context.Request.ContentLength);
                return;
            }
            
            var path = context.Request.Path;
            if (_pathstoIgnore.Exists(x => path.Value != null && path.Value.Contains(x, StringComparison.OrdinalIgnoreCase)))
                return;
            
            var mensagem = ObterMensagem(context);
            var body = await ReadRequestBodyAsync(context.Request);
         
            var logData = GetLogData(LogLevel.Information, mensagem, "InvokeAsync", body);
            _logger.LogInformation("{logData}", logData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Falha ao logar requisição");
        }
    }

    private static string ObterMensagem(HttpContext context)
    {
        var path = context.Request.Path;
        var method = context.Request.Method;

        var message = $"O Usuário {ObterInformacoesLogin(CamposClaims.IdUsuario, context.Request).OcultarCpfCnpj()} " +
                      $"Do Email {ObterInformacoesLogin(CamposClaims.Email, context.Request)} " +
                      $"realizou a requisição [{method}] - {path}";
        
        return message;
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Position = 0;
        
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        
        return body;
    }
    
    private static string ObterInformacoesLogin(string informacaoDesejada, HttpRequest request)
    {
        var claim = request.HttpContext!
            .User
            .Claims
            .FirstOrDefault(x => x.Type == informacaoDesejada);

        return claim?.Value ?? ValoresPadrao.UsuarioAnonimo;
    }
    
    private static string GetLogData(
        LogLevel logLevel,
        string message,
        string sourceMethod,
        object sourceData)
        => LogDataBuilder
            .New()
            .WithLogLevel(logLevel)
            .WithMessage(message)
            .WithAppInfo<RequestLoggingMiddleware>(sourceMethod)
            .WithSourceData(sourceData)
            .Build()
            .Serializar();
}