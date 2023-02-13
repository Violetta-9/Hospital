using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServiceCategories
{
    internal class GetServiceCategoriesQueryHandler : IRequestHandler<GetServiceCategoriesQuery, ServiceCategoriesDTO[]>
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;

        public GetServiceCategoriesQueryHandler(IServiceCategoryRepository serviceCategoryRepository)
        {
            _serviceCategoryRepository= serviceCategoryRepository;
        }

        public async Task<ServiceCategoriesDTO[]> Handle(GetServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
           return await _serviceCategoryRepository.GetServiceCategories(cancellationToken);
        }
    }
}
