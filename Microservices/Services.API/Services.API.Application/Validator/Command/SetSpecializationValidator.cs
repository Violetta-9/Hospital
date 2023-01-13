using Authorization.Data.Repository;
using FluentValidation;
using Services.API.Application.Command.SetSpecializationForService;
using Services.API.Resources;
using System.Threading;

namespace Services.API.Application.Validator.Command;

public class SetSpecializationValidator : AbstractValidator<SetSpecializationCommand>
{
    private readonly IServiceRepository _serviceRepository;

    public SetSpecializationValidator(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.SetSpecializationDTO.ServicesId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.SetSpecializationDTO.ServicesId)))
            .MustAsync(ExistsServiceAsync)
            .WithMessage(Messages.OneOfServicesIdDonotExists)
            .MustAsync(ServiceShouldBeFreeAsync)
            .WithMessage(Messages.OneOfServicesAlredyTaken);
    }

    private async Task<bool> ServiceShouldBeFreeAsync(ICollection<long> servicesId,CancellationToken cancellationToken)
    {
        foreach (var id in servicesId)
        {
            if (await _serviceRepository.IsServiceContainsFreeSpecializationAsync(id, cancellationToken)) continue;

                return false;
        }

        return true;
    }

    private async Task<bool> ExistsServiceAsync(ICollection<long> servicesId, CancellationToken cancellationToken)
    {
        foreach (var id in servicesId)
        {
            if (await _serviceRepository.ExistsAsync(id, cancellationToken)) continue;

            return false;
        }

        return true;
    }
}