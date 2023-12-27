
using Authorization.Data.Repository;
using AutoMapper;
using Documents.API.Client.Abstraction;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetOfficeById
{
    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, OfficeDto>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IDocumentApiProxy _documentApiProxy;

        public GetOfficeByIdQueryHandler(IOfficeRepository officeRepository,IDocumentApiProxy documentApiProxy)
        {
            _officeRepository=officeRepository;
            _documentApiProxy = documentApiProxy;
        }

        public async Task<OfficeDto> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Authorization.Data_Domain.Models.Office, OfficeDto>());
            var mapper = new Mapper(config);
            var entity = await _officeRepository.GetAsync(request.Id, cancellationToken);
            var officeDto = mapper.Map<OfficeDto>(entity);
            if (entity.PhotoId != null)
            {
                var photo = await _documentApiProxy.GetBlobAsync((long)entity.PhotoId,true, cancellationToken);
                officeDto.FilePath = photo.AbsoluteUri;
            }
            
            return officeDto ;
        }
    }
}
