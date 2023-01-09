using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateOffice;

internal class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Response>
{
    private readonly IReceptionistRepository _receptionistRepository;

    public UpdateOfficeCommandHandler(IReceptionistRepository receptionist)
    {
        _receptionistRepository = receptionist;
    }

    public async Task<Response> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var receptionist =
            await _receptionistRepository.GetReceptionistByAccountIdAsync(request.AccountId, cancellationToken);
        if (receptionist == null) return Response.Error;
        receptionist.OfficeId = request.NewOffice;
        await _receptionistRepository.UpdateAsync(receptionist, cancellationToken);
        return Response.Success;
    }
}