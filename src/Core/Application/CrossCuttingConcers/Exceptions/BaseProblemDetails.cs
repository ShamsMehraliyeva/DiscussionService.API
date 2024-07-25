using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.CrossCuttingConcers.Exceptions;

public class BaseProblemDetails: ProblemDetails
{
    public string TraceId { get; set; }

    public static string GenerateTraceId()
        => $"{DateTime.Now.ToString("ddMMyyyy-HH:mm:ss")}-{Guid.NewGuid().ToString()}";


    public override string ToString()
        => JsonConvert.SerializeObject(this);
}