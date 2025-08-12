using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Content.SharedKernel.Model
{
    public class ApiProblemDetails
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public string TraceId { get; set; }
        public string Instance { get; set; }
        public string ErrorType { get; set; }
        public Dictionary<string, object?> Extensions { get; init; } = new(StringComparer.Ordinal);
    }
}
