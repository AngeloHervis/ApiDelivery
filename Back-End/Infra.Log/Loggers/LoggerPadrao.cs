using System.Runtime.CompilerServices;
using Crosscutting.Constantes;
using Crosscutting.Interfaces.Log;
using Infra.Log._Base;
using Infra.Log.Interfaces;

namespace Infra.Log.Loggers;

public class LoggerPadrao : LoggerBase, ILoggerPadrao
{
    public LoggerPadrao(ILogWriter logger) : base(logger) { }

    public void LogAviso<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogInfo<TSource>(message, sourceMethod);

    public void LogErro<TSource>(string message, Exception exception, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(exception, message, sourceMethod);

    public void LogErro<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(message, sourceMethod);
        
    public void LogErroComDados<TSource>(string message, string dados, Exception exception, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(exception, message, sourceMethod, dados);

    public void LogInfoPadrao<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogInfo<TSource>(message, sourceMethod);

    public void LogInfoPadraoComDados<TSource>(string message, string dados, string sourceMethod = "")
        => LogInfoWithSourceData<TSource>(message, dados, sourceMethod);

    public void LogWarningPadrao<TSource>(string message, string sourceMethod = "")
        => LogWarning<TSource>(message, sourceMethod);

    public void LogErroSemException<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(message, sourceMethod);

    public void LogErroSemExceptionComDados<TSource>(
        string message, object dados, [CallerMemberName] string sourceMethod = "")
    {
        try
        {
            LogError<TSource>(message, sourceMethod, dados);
        }
        catch (Exception e)
        {
            LogErro<TSource>(string.Format(MensagensLogErro.ErroDeLogging, e.Message), e, sourceMethod);
        }
    }
}