using System.Runtime.CompilerServices;
using Crosscutting.Interfaces.Log;
using Infra.Log.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Log.Wrappers;

public class SingletonLoggerWrapper(IServiceProvider serviceProvider) : ISingletonLoggerWrapper
{
    public void LogErro<TSource>(string message, Exception exception, [CallerMemberName] string sourceMethod = "")
    {
        using var scope = serviceProvider.CreateScope();
        var scopedLogger = scope.ServiceProvider.GetRequiredService<ILoggerPadrao>();
        scopedLogger.LogErro<TSource>(message, exception, sourceMethod);
    }

    public void LogInfoPadrao<TSource>(string message, [CallerMemberName] string sourceMethod = "")
    {
        using var scope = serviceProvider.CreateScope();
        var scopedLogger = scope.ServiceProvider.GetRequiredService<ILoggerPadrao>();
        scopedLogger.LogInfoPadrao<TSource>(message, sourceMethod);
    }
}