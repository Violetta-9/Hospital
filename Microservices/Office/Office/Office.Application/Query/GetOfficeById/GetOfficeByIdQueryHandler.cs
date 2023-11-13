
using Authorization.Data.Repository;
using AutoMapper;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetOfficeById
{
    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, OfficeDto>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetOfficeByIdQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository=officeRepository;
        }

        public async Task<OfficeDto> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Authorization.Data_Domain.Models.Office, OfficeDto>());
            var mapper = new Mapper(config);
            var entity = await _officeRepository.GetAsync(request.Id, cancellationToken);
            return mapper.Map<OfficeDto>(entity);
        }
    }
}
