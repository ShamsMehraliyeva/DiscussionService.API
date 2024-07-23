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
  
    
    public void Information(string message)
    {
        _logger.Information("Information log: {@message}", message);
    }

    public void Debug(string message)
    {
        _logger.Debug("Debug log: {@message}", message);
    }

    public void Warning(string message)
    {
        _logger.Warning("Warning log: {@message}", message);
    }

    public void Error(string message)
    {
        _logger.Error("Error log: {@message}", message);
    }

    public void Fatal(string message)
    {

        _logger.Fatal("Fatal log: {@message}", message);
    }
}