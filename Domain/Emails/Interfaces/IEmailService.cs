using Crosscutting.Dto.Email;

namespace Domain.Emails.Interfaces;

public interface IEmailService
{
    Task EnviarEmailRedefinirSenhaAsync(FiltroEmailRedefinirSenhaDto filtro, string email);
}