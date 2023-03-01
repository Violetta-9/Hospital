using Authorization.Data.Repository;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Command.UpdateOffice;

internal class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Response>
{
    private readonly IOfficeRepository _officeRepository;

    public UpdateOfficeCommandHandler(IOfficeRepository oRepository)
    {
        _officeRepository = oRepository;
    }

    public async Task<Response> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await _officeRepository.GetAsync(request.UpdateOfficeDto.OfficeId, cancellationToken);
        if (office == null) return Response.Error;

        office.Address = request.UpdateOfficeDto.Address;
        office.RegistryPhoneNumber = request.UpdateOfficeDto.RegistryPhoneNumber;
        await _officeRepository.UpdateAsync(office, cancellationToken);
        return Response.Success;
    }
}