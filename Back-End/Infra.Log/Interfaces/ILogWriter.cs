using Infra.Log.Models;

namespace Infra.Log.Interfaces;

public interface ILogWriter
{
    void Info(LogData logData);
    void Warning(LogData logData);
    void Error(LogData logData);
}