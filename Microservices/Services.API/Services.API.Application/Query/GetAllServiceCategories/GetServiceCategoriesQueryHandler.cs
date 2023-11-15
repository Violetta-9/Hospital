using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllServiceCategories
{
    public class GetServiceCategoriesQueryHandler : IRequestHandler<GetServiceCategoriesQuery, CategoriesDto[]>
    {
        private readonly IServiceCategoryRepository _categoryRepository;

        public GetServiceCategoriesQueryHandler(IServiceCategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        public async Task<CategoriesDto[]> Handle(GetServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories= await _categoryRepository.GetAllAsync(cancellationToken);
            return categories.Select(x => new CategoriesDto()
            {
                Id = x.Id,
                Title = x.Title,
                Time = x.TimeSlotSize,
            }).ToArray();
        }
    }
}
