using Crosscutting.DataHora;
using Microsoft.Extensions.Logging;

namespace Infra.Log.Models;

public class LogData
{
    public string EventId { get; } = Guid.NewGuid().ToString();
    public DateTime TimeStamp { get; } = PadroesDataHora.Agora;
    public string Class { get; set; }
    public string Method { get; set; }
    public LogLevel LogLevel { get; set; }
    public string Message { get; set; }
    public string ExceptionMessage { get; set; }
    public object ErrorDetails { get; set; }
    public string Stacktrace { get; set; }
    public object SourceData { get; set; }
}