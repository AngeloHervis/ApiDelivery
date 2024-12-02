using System.Runtime.CompilerServices;

namespace Crosscutting.Interfaces.Log;

public interface ILoggerPadrao
{
    void LogErro<TSource>(string message, System.Exception exception, [CallerMemberName] string sourceMethod = "");
    void LogErro<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogErroComDados<TSource>(string message, string dados, System.Exception exception, [CallerMemberName] string sourceMethod = "");
    void LogAviso<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogInfoPadrao<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogInfoPadraoComDados<TSource>(string message, string dados, [CallerMemberName] string sourceMethod = "");
    void LogWarningPadrao<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogErroSemException<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogErroSemExceptionComDados<TSource>(string message, object dados, [CallerMemberName] string sourceMethod = "");
}