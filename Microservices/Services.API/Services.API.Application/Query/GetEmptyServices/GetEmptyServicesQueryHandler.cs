using Authorization.Data.Repository;
using AutoMapper;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetEmptyServices
{
    public class GetEmptyServicesQueryHandler : IRequestHandler<GetEmptyServicesQuery, EmptyServices[]>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetEmptyServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<EmptyServices[]> Handle(GetEmptyServicesQuery request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Authorization.Data_Domain.Models.Service, EmptyServices>());
            var mapper = new Mapper(config);
            return mapper.Map<EmptyServices[]>(await _serviceRepository.GetAllFreeServiceAsync(cancellationToken));
        }
    }
}
