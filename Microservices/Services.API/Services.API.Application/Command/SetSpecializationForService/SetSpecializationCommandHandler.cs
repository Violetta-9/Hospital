using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.SetSpecializationForService;

internal class SetSpecializationCommandHandler : IRequestHandler<SetSpecializationCommand, Response>
{
    private readonly IServiceRepository _serviceRepository;

    public SetSpecializationCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<Response> Handle(SetSpecializationCommand request, CancellationToken cancellationToken)
    {
        await _serviceRepository.SetSpecializationAsync(request.SetSpecializationDTO.ServicesId,
            request.SetSpecializationDTO.SpecializationId,
            cancellationToken);
        return Response.Success;
    }
}