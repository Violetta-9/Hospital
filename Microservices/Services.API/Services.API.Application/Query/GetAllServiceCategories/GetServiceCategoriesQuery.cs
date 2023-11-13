using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllServiceCategories
{
    public class GetServiceCategoriesQuery : IRequest<CategoriesDto[]>
    {
    }
}
