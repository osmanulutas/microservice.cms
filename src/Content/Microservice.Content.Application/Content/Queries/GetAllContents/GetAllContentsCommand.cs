using MediatR;
using Microservice.Content.SharedKernel.Model;

namespace Microservice.Content.Application.Content.Queries.GetAllContents
{
    public class GetAllContentsCommand : IRequest<ApiResponse<List<GetAllContentsCommandDto>>>
    {
    }
}
