using System.Runtime.CompilerServices;

namespace Infra.Log.Interfaces;

public interface ISingletonLoggerWrapper
{
    void LogInfoPadrao<TSource>(string message, [CallerMemberName] string sourceMethod = "");
    void LogErro<TSource>(string message, System.Exception exception, [CallerMemberName] string sourceMethod = "");
}