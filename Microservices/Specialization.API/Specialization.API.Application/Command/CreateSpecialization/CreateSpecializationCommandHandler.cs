using Authorization.Data.Repository;
using FluentValidation;
using MediatR;
using Services.API.Client.Abstraction;
using SpecializationEntity = Authorization.Data_Domain.Models.Specialization;

namespace Specialization.API.Application.Command.CreateSpecialization;

public class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, long>
{
    private readonly IServiceApiProxy _serviceApiProxy;
    private readonly ISpecializationRepository _specializationRepository;

    public CreateSpecializationCommandHandler(ISpecializationRepository specializationRepository,
        IServiceApiProxy serviceApiProxy)
    {
        _specializationRepository = specializationRepository;
        _serviceApiProxy = serviceApiProxy;
    }

    public async Task<long> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = new SpecializationEntity
        {
            Title = request.CreateSpecializationDto.Title,
            IsActive = request.CreateSpecializationDto.IsActive
        };


        await _specializationRepository.InsertAsync(specialization, cancellationToken);
        try
        {
            var serviceResponse = await _serviceApiProxy.SetSpecializationIdForServicesAsync(specialization.Id,
                request.CreateSpecializationDto.ServicesId, cancellationToken);
            if (!serviceResponse.IsSuccess) throw new Exception("CreateSpecializationCommandHandler work bad");
        }
        catch (ValidationException)
        {
            await _specializationRepository.DeleteAsync(specialization.Id, cancellationToken);
            throw;
        }
        catch (Exception)
        {
            await _specializationRepository.DeleteAsync(specialization.Id, cancellationToken);
            throw;
        }

        return specialization.Id;
    }
}