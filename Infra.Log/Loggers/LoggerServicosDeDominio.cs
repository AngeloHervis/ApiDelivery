using System.Runtime.CompilerServices;
using Crosscutting.Constantes;
using Crosscutting.Interfaces;
using Crosscutting.Interfaces.Log;
using Infra.Log._Base;
using Infra.Log.Interfaces;

namespace Infra.Log.Loggers;

public class LoggerServicosDeDominio : LoggerBase, ILoggerServicosDeDominio
{
    public LoggerServicosDeDominio(ILogWriter logger) : base(logger) 
    { }

    public void LogInformacao<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogInfo<TSource>(message, sourceMethod);

    public void LogInfoComDados<TSource>(string message, object sourceData, [CallerMemberName] string sourceMethod = "")
    {
        try
        {
            LogInfoWithSourceData<TSource>(message, sourceData, sourceMethod);
        }
        catch (Exception e)
        {
            LogErro<TSource>(e, string.Format(MensagensLogErro.ErroDeLogging, e.Message), sourceMethod);
        }
    }

    public void LogAviso<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogWarning<TSource>(message, sourceMethod);

    public void LogAvisoComDados<TSource>(string message, object sourceData, [CallerMemberName] string sourceMethod = "")
    {
        try
        {
            LogWarningWithSourceData<TSource>(message, sourceData, sourceMethod);
        }
        catch (Exception e)
        {
            LogErro<TSource>(e, string.Format(MensagensLogErro.ErroDeLogging, e.Message), sourceMethod);
        }
    }

    public void LogErro<TSource>(Exception exception, string message, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(exception, message, sourceMethod);

    public void LogErroComDados<TSource>(
        Exception exception, string message, object sourceData, [CallerMemberName] string sourceMethod = "")
    {
        try
        {
            LogErrorWithSourceData<TSource>(exception, message, sourceData, sourceMethod);
        }
        catch (Exception e)
        {
            LogErro<TSource>(e, string.Format(MensagensLogErro.ErroDeLogging, e.Message), sourceMethod);
        }
    }

    public void LogErroSemException<TSource>(string message, [CallerMemberName] string sourceMethod = "")
        => LogError<TSource>(message, sourceMethod);
}