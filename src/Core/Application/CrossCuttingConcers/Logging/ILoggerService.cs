namespace Application.CrossCuttingConcers.Logging;

public interface ILoggerService
{
    void Information(LogModel logModel);
    void Debug(LogModel logModel);
    void Warning(LogModel logModel);
    void Error(LogModel logModel);
    void Fatal(LogModel logModel);
}