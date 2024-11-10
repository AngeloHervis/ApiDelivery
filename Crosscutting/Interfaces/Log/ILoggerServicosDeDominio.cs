using System.Runtime.CompilerServices;

namespace Crosscutting.Interfaces.Log;

public interface ILoggerServicosDeDominio
{
    void LogInformacao<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogInfoComDados<TSource>(string message, object sourceData, [CallerMemberName] string sourceMethod = "");
    void LogAviso<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogAvisoComDados<TSource>(string message, object sourceData, [CallerMemberName] string sourceMethod = "");
    void LogErro<TSource>(System.Exception exception, string message, [CallerMemberName] string sourceMethod = "");
    void LogErroComDados<TSource>(System.Exception exception, string message, object sourceData, [CallerMemberName] string sourceMethod = "");
    void LogErroSemException<TSource>(string message, [CallerMemberName] string sourceMethod = "");
}