using System.Net;


namespace Microservice.Content.SharedKernel.Model
{
    public class ApiResponse<T> where T : new()
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public T Data { get; set; }
        public ApiProblemDetails Error { get; set; }

        public ApiResponse<T> ResponseOK(T data)
        {
            Status =(int)HttpStatusCode.OK;
            Data = data;
            Title ="Success";
            Error = null;
            return this;
        }
        public ApiResponse<T> NotFound(string detail = "Resource not found")
        {
            return new ApiResponse<T>
            {
                Status = (int)HttpStatusCode.NotFound,
                Data = default(T),
                Title = "Not Found",
                Detail = detail
            };
        }
        public ApiResponse<T> Failure(string detail = "An error occurred", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Status = (int)statusCode;
            Data = default(T);
            Title = "Error";
            Detail = detail;
            return this;
        }
    }
}
