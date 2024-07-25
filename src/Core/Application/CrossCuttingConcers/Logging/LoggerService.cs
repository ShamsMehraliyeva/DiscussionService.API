using Microsoft.Extensions.Configuration;
using Serilog;
namespace Application.CrossCuttingConcers.Logging;

public class LoggerService:ILoggerService
{
    private IConfiguration _configuration;
    private readonly ILogger _logger;
    
    public LoggerService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                             .Get<FileLogConfiguration>() ??
                                         throw new Exception(LogMessages.NullOptionsMessage);
         
        string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");
        
        _logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 5000000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
  
    
    public void Information(LogModel logModel)
    {
        logModel.Level = LogLevel.Debug;
        logModel.CreateDate = DateTime.Now;
        _logger.Information("Information log: {@message}", logModel);
    }

    public void Debug(LogModel logModel)
    {
        logModel.Level = LogLevel.Debug;
        logModel.CreateDate = DateTime.Now;
        _logger.Debug("Debug log: {@message}", logModel);
    }

    public void Warning(LogModel logModel)
    {
        logModel.Level = LogLevel.Debug;
        logModel.CreateDate = DateTime.Now;
        _logger.Warning("Warning log: {@message}", logModel);
    }

    public void Error(LogModel logModel)
    {
        logModel.Level = LogLevel.Debug;
        logModel.CreateDate = DateTime.Now;
        _logger.Error("Error log: {@message}", logModel);
    }

    public void Fatal(LogModel logModel)
    {
        logModel.Level = LogLevel.Debug;
        logModel.CreateDate = DateTime.Now;
        _logger.Fatal("Fatal log: {@message}", logModel);
    }
}