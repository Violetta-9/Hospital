using Authorization.Data.Repository;
using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using MediatR;
using Office.Application.Contracts.Outgoing;
using Response = Office.Application.Contracts.Outgoing.Response;

namespace Office.Application.Command.UpdateOffice;

internal class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Response>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IDocumentApiProxy _documentApiProxy;

    public UpdateOfficeCommandHandler(IOfficeRepository oRepository,IDocumentApiProxy documentApiProxy)
    {
        _officeRepository = oRepository;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await _officeRepository.GetAsync(request.UpdateOfficeDto.OfficeId, cancellationToken);
        if (office is null) return Response.Error;
        office.Address = request.UpdateOfficeDto.Address ?? office.Address;
        office.RegistryPhoneNumber = request.UpdateOfficeDto.RegistryPhoneNumber ?? office.RegistryPhoneNumber;
        if (request.UpdateOfficeDto?.File != null)
        {
            var response = await _documentApiProxy.UpdateBlobAsync(
                new FileParameter(request.UpdateOfficeDto.File.OpenReadStream(), request.UpdateOfficeDto.File.FileName,
                    request.UpdateOfficeDto.File.ContentType), (long)office.PhotoId, SubjectUpdate._3, cancellationToken);
            if (response > 0)
            {
                office.PhotoId=response;
            }
        }
        await _officeRepository.UpdateAsync(office, cancellationToken);
        return Response.Success;
    }
}