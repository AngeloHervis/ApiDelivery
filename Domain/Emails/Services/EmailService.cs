using Crosscutting.Dto.Email;
using Crosscutting.Interfaces.Log;
using Domain.Emails.Interfaces;

namespace Domain.Emails.Services;

public class EmailService(ILoggerServicosDeDominio logger) : IEmailService
{
    public async Task EnviarEmailRedefinirSenhaAsync(FiltroEmailRedefinirSenhaDto filtro, string emailDestino)
    {
        try
        {
            var assunto = FiltroEmailRedefinirSenhaDto.Assunto;
            var mensagem = filtro.ObterMensagemFormatada(emailDestino);

            // Aqui você chamaria um provedor de email real.
            logger.LogInformacao<EmailService>($"Email enviado para {emailDestino} com o assunto: {assunto}");

            await Task.CompletedTask; // Simula a chamada async
        }
        catch (Exception ex)
        {
            logger.LogErro<EmailService>(ex, $"Falha ao enviar email para {emailDestino}");
            throw; // Lança a exceção para tratamento posterior, se necessário
        }
    }
}