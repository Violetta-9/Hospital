using MassTransit;
using MediatR.Pipeline;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Response = Specialization.API.Application.Contracts.Outgoing.Response;

namespace Specialization.API.Application.PostProcessor;

public class SpecializationPostProcessor : IRequestPostProcessor<UpdateSpecializationStatusCommand, Response>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public SpecializationPostProcessor(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Process(UpdateSpecializationStatusCommand request, Response response,
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish<SpecializationStatusChanged.SpecializationStatusChanged>(new
        {
            SpecializationId = request.UpdateSpecializationStatusDto.Id, request.UpdateSpecializationStatusDto.IsActive
        }, cancellationToken);
    }
}