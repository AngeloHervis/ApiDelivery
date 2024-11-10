using Crosscutting.Extensions;
using Infra.Log.Interfaces;
using Infra.Log.Models;
using Microsoft.Extensions.Logging;

namespace Infra.Log._Base;

public class LogWriter : ILogWriter
{
    private readonly ILogger<LogWriter> _logger;

    public LogWriter(ILogger<LogWriter> logger)
        => _logger = logger;

    public void Info(LogData logData)
        => _logger.LogInformation(logData.Serializar());

    public void Error(LogData logData)
        => _logger.LogError(logData.Serializar());

    public void Warning(LogData logData)
        => _logger.LogWarning(logData.Serializar());
}