using Authorization.Data.Repository;
using FluentValidation;
using Services.API.Application.Command.UpdateService;
using Services.API.Resources;

namespace Services.API.Application.Validator.Command;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceCommand>
{
    private readonly IServiceCategoryRepository _categoryRepository;
    private readonly IServiceRepository _serviceRepository;

    public UpdateServiceValidator(IServiceRepository serviceRepository, IServiceCategoryRepository serviceCategory)
    {
        _serviceRepository = serviceRepository;
        _categoryRepository = serviceCategory;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.UpdateServiceDto.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateServiceDto.Id)))
            .MustAsync(ExistsServiceAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundService, opt.UpdateServiceDto.Id));
        RuleFor(x => x.UpdateServiceDto.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateServiceDto.Price)))
            .Must(CorrectPrice)
            .WithMessage(Messages.InCorrectPrice);
        RuleFor(x => x.UpdateServiceDto.ServiceCategoryId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt =>
                string.Format(Messages.NotEmptyField, nameof(opt.UpdateServiceDto.ServiceCategoryId)))
            .MustAsync(ExistsServiceCategory)
            .WithMessage(opt =>
                string.Format(Messages.NotFoundServiceCategory, opt.UpdateServiceDto.ServiceCategoryId));
    }

    private async Task<bool> ExistsServiceCategory(long categoryId, CancellationToken cancellationToken)
    {
        return await _categoryRepository.ExistsAsync(categoryId, cancellationToken);
    }

    private bool CorrectPrice(double price)
    {
        return price > 0;
    }

    private async Task<bool> ExistsServiceAsync(long serviceId, CancellationToken cancellationToken)
    {
        return await _serviceRepository.ExistsAsync(serviceId, cancellationToken);
    }
}