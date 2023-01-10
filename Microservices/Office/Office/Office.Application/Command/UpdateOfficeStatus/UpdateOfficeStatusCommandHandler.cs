using Authorization.Data.Repository;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Command.UpdateOfficeStatus;

internal class UpdateOfficeStatusCommandHandler : IRequestHandler<UpdateOfficeStatusCommand, Response>
{
    private readonly IOfficeRepository _officeRepository;

    public UpdateOfficeStatusCommandHandler(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Response> Handle(UpdateOfficeStatusCommand request, CancellationToken cancellationToken)
    {
        var office = await _officeRepository.GetAsync(request.OfficeId, cancellationToken);
        if (office == null) return Response.Error;
        office.IsActive = request.IsActive;
        await _officeRepository.UpdateAsync(office, cancellationToken);
        return Response.Success;
    }
}