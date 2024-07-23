namespace Application.CrossCuttingConcers.Logging;

public interface ILoggerService
{
    void Information(string message);
    void Debug(string message);
    void Warning(string message);
    void Error(string message);
    void Fatal(string message);
}