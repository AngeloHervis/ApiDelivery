using Infra.Log.Builders;
using Infra.Log.Interfaces;
using Infra.Log.Models;
using Microsoft.Extensions.Logging;

namespace Infra.Log._Base;

public class LoggerBase
{
    private readonly ILogWriter _logger;

    public LoggerBase(ILogWriter logger)
        => _logger = logger;

    protected void LogInfo<TSource>(string message, string sourceMethod)
        => _logger.Info(GetLogData<TSource>(LogLevel.Information, message, sourceMethod));

    protected void LogInfoWithSourceData<TSource>(string message, object sourceData, string sourceMethod)
        => _logger.Info(GetLogData<TSource>(LogLevel.Information, message, sourceMethod, null, sourceData));

    protected void LogWarning<TSource>(string message, string sourceMethod, Exception exception = null)
        => _logger.Warning(GetLogData<TSource>(LogLevel.Warning, message, sourceMethod, exception));

    protected void LogWarningWithSourceData<TSource>(string message, object sourceData, string sourceMethod)
        => _logger.Warning(GetLogData<TSource>(LogLevel.Warning, message, sourceMethod, null, sourceData));

    protected void LogError<TSource>(Exception exception, string message, string sourceMethod, string sourceData = "")
        => _logger.Error(GetLogData<TSource>(LogLevel.Error, message, sourceMethod, exception, sourceData));

    protected void LogError<TSource>(string message, string sourceMethod, object sourceData = null)
        => _logger.Error(GetLogData<TSource>(LogLevel.Error, message, sourceMethod, sourceData:sourceData));

    protected void LogErrorWithSourceData<TSource>(Exception exception, string message, object sourceData, string sourceMethod)
        => _logger.Error(GetLogData<TSource>(LogLevel.Error, message, sourceMethod, exception, sourceData));

    private static LogData GetLogData<TSource>(
        LogLevel logLevel,
        string message,
        string sourceMethod,
        Exception exception = null,
        object sourceData = null)
        => LogDataBuilder
            .New()
            .WithLogLevel(logLevel)
            .WithMessage(message)
            .WithAppInfo<TSource>(sourceMethod)
            .WithException(exception)
            .WithSourceData(sourceData)
            .Build();
}