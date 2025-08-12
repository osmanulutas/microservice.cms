namespace Microservice.Account.SharedKernel.Models;
   
public class ApiProblemDetails
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public string TraceId { get; set; }
        public string Instance { get; set; }
        public string ErrorType { get; set; }
        public Dictionary<string, object?> Extensions { get; init; } = new(StringComparer.Ordinal);
    }

