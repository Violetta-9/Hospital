using Authorization.Data.Repository;
using FluentValidation;
using MediatR;
using Services.API.Client.Abstraction;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Command.UpdateSpecialization;

public class UpdateSpecializationCommandHandler : IRequestHandler<UpdateSpecializationCommand, Response>
{
    private readonly IServiceApiProxy _serviceApiProxy;
    private readonly ISpecializationRepository _specializationRepository;

    public UpdateSpecializationCommandHandler(IServiceApiProxy serviceApiProxy,
        ISpecializationRepository specializationRepository
    )
    {
        _serviceApiProxy = serviceApiProxy;
        _specializationRepository = specializationRepository;
    }

    public async Task<Response> Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await _specializationRepository.GetAsync(request.Id, cancellationToken);
        if (specialization == null) return Response.Error;
        specialization.Title = request.Title;

        try
        {
            var serviceResponse =
                await _serviceApiProxy.UpdateSpecializationIdForServicesAsync(specialization.Id, request.ServicesId,
                    cancellationToken);
            ;
            if (!serviceResponse.IsSuccess) throw new Exception("UpdateSpecializationCommandHandler work bad");
        }
        catch (ValidationException e)
        {
            throw e;
        }
        catch (Exception e)
        {
            throw e;
        }

        await _specializationRepository.UpdateAsync(specialization, cancellationToken);
        return Response.Success;
    }
}