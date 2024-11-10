using Crosscutting.Exception;
using Crosscutting.Extensions;
using Infra.Log.Models;
using Microsoft.Extensions.Logging;

namespace Infra.Log.Builders;

public class LogDataBuilder
{
    private readonly LogData _logData = new();

    public static LogDataBuilder New() => new();

    public LogDataBuilder WithAppInfo<TSource>(string sourceMethod)
    {
        _logData.Class = typeof(TSource).Name;
        _logData.Method = sourceMethod;
        return this;
    }

    public LogDataBuilder WithMessage(string message)
    {
        _logData.Message = message;
        return this;
    }

    public LogDataBuilder WithLogLevel(LogLevel logLevel)
    {
        _logData.LogLevel = logLevel;
        return this;
    }

    public LogDataBuilder WithException(Exception exception)
    {
        if (exception == null) return this;
        _logData.ExceptionMessage = exception.InnerException?.Message ?? exception.Message;
        _logData.Stacktrace = exception.StackTrace;
        if (exception.GetType() != typeof(HttpException)) return this;

        var httpException = (HttpException)exception;

        if (httpException.Detalhes.IsEmpty()) return this;

        _logData.ErrorDetails = httpException.Detalhes;

        return this;
    }

    public LogDataBuilder WithSourceData(object sourceData)
    {
        if (sourceData == null) return this;
        _logData.SourceData = sourceData;
        return this;
    }

    public LogData Build() => _logData;
}