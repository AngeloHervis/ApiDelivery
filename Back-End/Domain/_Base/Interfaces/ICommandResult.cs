using Domain._Base.Models;

namespace Domain._Base.Interfaces;

public interface ICommandResult<out TValue> : ICommandResult
{
    TValue Data { get; }
}

public interface ICommandResult
{
    bool IsError { get; }
    int StatusCode { get; }
    List<CommandError> Errors { get; }
}