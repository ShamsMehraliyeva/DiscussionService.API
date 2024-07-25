using Application.CrossCuttingConcers.Exceptions;

namespace Application.CrossCuttingConcers.Logging;

public class LogModel
{
    public string Method { get; set; }
    public string Url { get; set; }
    public string Body { get; set; }
    public string Step { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public DateTime CreateDate { get; set; }
    public BaseProblemDetails BaseProblemDetails { get; set; }
    public string RawException { get; set; }
}