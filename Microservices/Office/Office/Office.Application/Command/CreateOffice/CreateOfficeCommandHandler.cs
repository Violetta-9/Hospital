using Authorization.Data.Repository;
using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using MediatR;
using OfficeEntity = Authorization.Data_Domain.Models.Office;

namespace Office.Application.Command.CreateOffice;

internal class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, long>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IDocumentApiProxy _documentApiProxy;

    public CreateOfficeCommandHandler(IOfficeRepository officeRepository, IDocumentApiProxy documentApiProxy)
    {
        _officeRepository = officeRepository;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<long> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = new OfficeEntity
        {
            Address = request.OfficeDto.Address,
            RegistryPhoneNumber = request.OfficeDto.RegistryPhoneNumber,
            IsActive = request.OfficeDto.IsActive
        };
        await _officeRepository.InsertAsync(office, cancellationToken);
        if (request.OfficeDto?.File!= null)
        {
            var response = await _documentApiProxy.UploadBlobAsync(new FileParameter(request.OfficeDto.File.OpenReadStream(), request.OfficeDto.File.FileName,
                request.OfficeDto.File.ContentType),office.Id,0 ,SubjectUpdate.Office, cancellationToken);
            if (response > 0)
            {
                office.PhotoId=response;
                await _officeRepository.UpdateAsync(office, cancellationToken);
            }
        }
        return office.Id;
    }
}