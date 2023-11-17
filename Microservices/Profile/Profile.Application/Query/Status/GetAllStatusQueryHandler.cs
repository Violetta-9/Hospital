using Authorization.Data.Repository;
using AutoMapper;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Status
{
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, StatusAllDto[]>
    {   
        private readonly IStatusRepository _statusRepository;

        public GetAllStatusQueryHandler(IStatusRepository sailRepository)
        {
            _statusRepository= sailRepository;
        }

        public async Task<StatusAllDto[]> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Authorization.Data_Domain.Models.Status, StatusAllDto>());
            var mapper = new Mapper(config);
            return mapper.Map<StatusAllDto[]>(await _statusRepository.GetAllAsync(cancellationToken));
        }
    }
}
