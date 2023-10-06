using Authorization.Data.Repository;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Command.UpdateSpecializationStatus;

public class UpdateSpecializationStatusCommandHandler : IRequestHandler<UpdateSpecializationStatusCommand, Response>
{
    private readonly ISpecializationRepository _specializationRepository;

    public UpdateSpecializationStatusCommandHandler(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
    }

    public async Task<Response> Handle(UpdateSpecializationStatusCommand request, CancellationToken cancellationToken)
    {
        var specialization =
            await _specializationRepository.GetAsync(request.UpdateSpecializationStatusDto.Id, cancellationToken);
        if (specialization == null) return Response.Error;
        specialization.IsActive = request.UpdateSpecializationStatusDto.IsActive;
        await _specializationRepository.UpdateAsync(specialization, cancellationToken);
        return Response.Success;
    }
}