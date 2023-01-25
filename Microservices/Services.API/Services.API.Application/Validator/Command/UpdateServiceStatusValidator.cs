using Authorization.Data.Repository;
using FluentValidation;
using Services.API.Application.Command.UpdateServiceStatus;
using Services.API.Resources;

namespace Services.API.Application.Validator.Command;

public class UpdateServiceStatusValidator : AbstractValidator<UpdateServiceStatusCommand>
{
    private readonly IServiceRepository _serviceRepository;

    public UpdateServiceStatusValidator(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.UpdateServiceStatusDTO.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateServiceStatusDTO.Id)))
            .MustAsync(ExistsServiceAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundService, opt.UpdateServiceStatusDTO.Id));


    }

    private async Task<bool> ExistsServiceAsync(long serviceId, CancellationToken cancellationToken)
    {
        return await _serviceRepository.ExistsAsync(serviceId, cancellationToken);
    }
}